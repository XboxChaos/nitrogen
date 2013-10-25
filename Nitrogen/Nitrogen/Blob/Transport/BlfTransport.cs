/*
 *   Nitrogen - Halo Content API
 *   Copyright (c) 2013 Matt Saville and Aaron Dierking
 * 
 *   This file is part of Nitrogen.
 *
 *   Nitrogen is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   Nitrogen is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with Nitrogen.  If not, see <http://www.gnu.org/licenses/>.
 */

using Nitrogen.Blob.Transport.BinaryTemplates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace Nitrogen.Blob.Transport
{
    /// <summary>
    /// Specifies special actions to perform during BLF data transportation.
    /// </summary>
    [Flags]
    public enum BlfTransportFlags
    {
        /// <summary>
        /// No special action is to be taken.
        /// </summary>
        None,

        /// <summary>
        /// The BLF data may not be modified.
        /// </summary>
        IsReadOnly = 1 << 0,

        /// <summary>
        /// The underlying stream containing the BLF data should not be disposed of after serialization.
        /// </summary>
        LeaveOpen = 1 << 1,

        /// <summary>
        /// The length of each chunk should be recalculated.
        /// </summary>
        ReallocateChunks = 1 << 2, // To be used

        /// <summary>
        /// The _blf and _eof chunks should be exposed.
        /// </summary>
        ExposeBlobHeaderAndFooter = 1 << 3,

        /// <summary>
        /// Only well-defined chunks should be exposed.
        /// </summary>
        ExposeWellDefinedChunksOnly = 1 << 4,

        /// <summary>
        /// During serialization, do not merge the input chunk collection with the original collection.
        /// </summary>
        DoNotMergeChunks = 1 << 5,
    }

    /// <summary>
    /// Transports data to and from a BLF file.
    /// </summary>
    public class BlfTransport
        : IBlobTransport, IDisposable
    {
        private Stream baseStream;
        private ChunkCollection chunkCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlfTransport"/> class with the specified
        /// <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">The input or output stream.</param>
        public BlfTransport(Stream stream) : this(stream, BlfTransportFlags.None) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlfTransport"/> class with the specified
        /// <paramref name="stream"/>, and optionally leaves the stream open.
        /// </summary>
        /// <param name="stream">The input or output stream.</param>
        /// <param name="flags">A set of flags allowing for special actions to be taken.</param>
        public BlfTransport(Stream stream, BlfTransportFlags flags)
        {
            this.baseStream = stream;
            TransportFlags = flags;
            ChunkTemplates = new Dictionary<string, ChunkTemplate>();

            // Set the IsReadOnly flag if the stream is read-only.
            if (!stream.CanWrite)
                TransportFlags |= BlfTransportFlags.IsReadOnly;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BlfTransport"/> class.
        /// </summary>
        ~BlfTransport()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the flags applied to this instance of the <see cref="BlfTransport"/> class as
        /// initially defined in the constructor.
        /// </summary>
        public virtual BlfTransportFlags TransportFlags { get; protected set; }

        /// <summary>
        /// Gets a dictionary associating a chunk signature with its corresponding template.
        /// </summary>
        public virtual Dictionary<string, ChunkTemplate> ChunkTemplates { get; protected set; }

        /// <summary>
        /// Opens a file stream and passes it to a new instance of the <see cref="BlfTransport"/>
        /// class with the specified <paramref name="flags"/>.
        /// </summary>
        /// <param name="path">A path to the file to open.</param>
        /// <param name="flags">A set of flags allowing for special actions to be taken.</param>
        /// <returns>A <see cref="BlfTransport"/> object based on the loaded file.</returns>
        public static BlfTransport CreateFromFile(string path, BlfTransportFlags flags)
        {
            Contract.Requires<InvalidOperationException>(
                !flags.HasFlag(BlfTransportFlags.IsReadOnly) ||
                (flags.HasFlag(BlfTransportFlags.IsReadOnly) && File.Exists(path))
            );
            Contract.Requires<FileNotFoundException>(File.Exists(path));

            return new BlfTransport(
                flags.HasFlag(BlfTransportFlags.IsReadOnly)
                    ? File.OpenRead(path)
                    : File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite),
                flags
            );
        }

        /// <summary>
        /// Associates a template with a specific chunk signature.
        /// </summary>
        /// <param name="signature">The signature of the chunk.</param>
        /// <param name="template">The template to invoke when processing the specified chunk.</param>
        public virtual void RegisterTemplate(string signature, ChunkTemplate template)
        {
            ChunkTemplates.Add(signature, template);
        }

        /// <summary>
        /// Registers a set of associations between a chunk signature and a template.
        /// </summary>
        /// <param name="templateSet"></param>
        public void RegisterTemplateSet(TemplateSet templateSet)
        {
            templateSet.ForEach((ChunkTemplate template) => RegisterTemplate(template.ChunkSignature, template));
        }

        /// <summary>
        /// Writes the given chunk collection in its serialized form back to the underlying stream.
        /// </summary>
        /// <param name="chunks">
        /// A <see cref="ChunkCollection"/> instance containing the chunks to serialize.
        /// </param>
        public void Serialize(ChunkCollection chunks)
        {
            if (this.chunkCollection != null && !TransportFlags.HasFlag(BlfTransportFlags.DoNotMergeChunks))
            {
                // Merge chunks with the original chunk collection.
                var newCollection = new List<Chunk>();
                foreach (var chunk in this.chunkCollection)
                {
                    if (chunks.HasChunk(chunk.Signature))
                    {
                        newCollection.Add(chunks[chunk.Signature]);
                    }
                    else
                    {
                        newCollection.Add(chunk);
                    }
                }
                this.chunkCollection = new ChunkCollection(newCollection);
            }
            else
            {
                this.chunkCollection = chunks;
            }

            // Serialize data.
            SerializeToStream();
        }

        /// <summary>
        /// Deserializes the data in the underlying stream.
        /// </summary>
        /// <returns>
        /// A <see cref="ChunkCollection"/> instance containing the deserialized chunks.
        /// </returns>
        public ChunkCollection Deserialize()
        {
            // Deserialize the stream.
            DeserializeStream();

            // Hide the _blf and _eof chunks unless specified.
            var filteredChunks = new List<string>();
            if (!TransportFlags.HasFlag(BlfTransportFlags.ExposeBlobHeaderAndFooter))
            {
                filteredChunks.AddRange(new[] { "_blf", "_eof" });
            }

            // Expose only well-defined chunks if needed.
            if (TransportFlags.HasFlag(BlfTransportFlags.ExposeWellDefinedChunksOnly))
            {
                foreach (var template in ChunkTemplates)
                {
                    if (!template.Value.IsWellDefined)
                        filteredChunks.Add(template.Key);
                }
            }

            // Create a filtered collection.
            var newCollection = this.chunkCollection;
            if (filteredChunks.Count > 0)
                newCollection = newCollection.CreateFilteredCollection(FilterType.Exclusive, filteredChunks.ToArray());

            return newCollection;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="BlfTransport"/> class, and
        /// optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only unmanaged
        /// resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!TransportFlags.HasFlag(BlfTransportFlags.LeaveOpen))
                    this.baseStream.Dispose();

                this.baseStream = null;
            }
        }

        /// <summary>
        /// Starting at the current offset in the stream, partitions the whole BLF data and deserializes
        /// each chunk if an applicable serializer is registered.
        /// </summary>
        private void DeserializeStream()
        {
            var chunks = new List<Chunk>();

            // Partition the blob into chunks and deserialize each chunk.
            using (var reader = new BinaryReader(this.baseStream, Encoding.BigEndianUnicode, true))
            {
                bool eofReached = false;
                while (!eofReached)
                {
                    // Reads up to 4 bytes from the stream in big endian format as a signed integer.
                    Func<int, int> readInteger = (int bytes) =>
                    {
                        Contract.Requires<ArgumentOutOfRangeException>(bytes <= 4 && bytes > 0);

                        byte[] value = reader.ReadBytes(bytes);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(value);
                        Array.Resize(ref value, 4);

                        return BitConverter.ToInt32(value, 0);
                    };

                    // Read chunk header.
                    long offset = reader.BaseStream.Position;
                    string signature = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    int length = readInteger(4);
                    int version = readInteger(2);
                    var flags = (ChunkFlags)readInteger(2);
                    DataTable data = null;

                    // Deserialize chunk if a template is registered.
                    if (ChunkTemplates.ContainsKey(signature))
                    {
                        var template = ChunkTemplates[signature];
                        bool skip = false;

                        // Ignore if not well-defined and the ExposeWellDefinedChunksOnly flag is set.
                        if (TransportFlags.HasFlag(BlfTransportFlags.ExposeWellDefinedChunksOnly) && !template.IsWellDefined)
                            skip = true;

                        // Ignore if this is a header or footer chunk and the ExposeBlobHeaderAndFooter flag is set.
                        if (!TransportFlags.HasFlag(BlfTransportFlags.ExposeBlobHeaderAndFooter) && (signature == "_blf" || signature == "_eof"))
                            skip = true;

                        // Deserialize the chunk data if not skipped.
                        if (!skip)
                        {
                            var context = new Context { { ChunkTemplate.ChunkVersion, version } };
                            data = template.Deserialize(reader.BaseStream, context);
                        }
                    }

                    // Add chunk to collection.
                    chunks.Add(new Chunk(signature, version, flags, data) { Offset = offset });

                    // Skip to the next offset.
                    reader.BaseStream.Position = offset + length;

                    // Break loop when _eof is reached.
                    if (signature == "_eof")
                        eofReached = true;
                }
            }

            this.chunkCollection = new ChunkCollection(chunks);
        }

        /// <summary>
        /// Serializes each chunk in the underlying chunk collection to the underlying stream.
        /// </summary>
        private void SerializeToStream()
        {
            Contract.Requires<ReadOnlyException>(!TransportFlags.HasFlag(BlfTransportFlags.IsReadOnly));
            Contract.Requires<ReadOnlyException>(this.baseStream.CanWrite);

            foreach (var chunk in this.chunkCollection)
            {
                if (ChunkTemplates.ContainsKey(chunk.Signature))
                {
                    // Go to the beginning of the data in this chunk.
                    this.baseStream.Position = chunk.Offset + 12;

                    // Get template and serialize data to the stream.
                    var template = ChunkTemplates[chunk.Signature];
                    var context = new Context { { ChunkTemplate.ChunkVersion, chunk.Version } };
                    template.Serialize(this.baseStream, chunk.Data, context);
                }
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
