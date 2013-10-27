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

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Nitrogen.Blob.Transport
{
    /// <summary>
    /// Specifies special actions to perform during XML data transportation.
    /// </summary>
    [Flags]
    public enum XmlTransportFlags
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
        /// The XML output should not be formatted to be human-readable.
        /// </summary>
        DisableFormatting = 1 << 2,

        /// <summary>
        /// During serialization, do not merge the input chunk collection with the original collection.
        /// </summary>
        DoNotMergeChunks = 1 << 5,
    }

    /// <summary>
    /// Transports data to and from a XML file.
    /// </summary>
    public class XmlTransport
        : IBlobTransport, IDisposable
    {
        private Stream baseStream;
        private ChunkCollection chunkCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlTransport"/> class with the specified
        /// <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">The input or output stream.</param>
        public XmlTransport(Stream stream) : this(stream, XmlTransportFlags.None) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlTransport"/> class with the specified
        /// <paramref name="stream"/>, and optionally leaves the stream open.
        /// </summary>
        /// <param name="stream">The input or output stream.</param>
        /// <param name="flags">A set of flags allowing for special actions to be taken.</param>
        public XmlTransport(Stream stream, XmlTransportFlags flags)
        {
            this.baseStream = stream;
            TransportFlags = flags;

            // Set the IsReadOnly flag if the stream is read-only.
            if (!stream.CanWrite)
                TransportFlags |= XmlTransportFlags.IsReadOnly;
        }

        /// <summary>
        /// Gets the flags applied to this instance of the <see cref="XmlTransport"/> class as
        /// initially defined in the constructor.
        /// </summary>
        public virtual XmlTransportFlags TransportFlags { get; protected set; }

        /// <summary>
        /// Writes the given chunk collection in its serialized form back to the underlying stream.
        /// </summary>
        /// <param name="chunks">
        /// A <see cref="ChunkCollection"/> instance containing the chunks to serialize.
        /// </param>
        public void Serialize(ChunkCollection chunks)
        {
            if (this.chunkCollection != null && !TransportFlags.HasFlag(XmlTransportFlags.DoNotMergeChunks))
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="XmlTransport"/> class, and
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
                if (!TransportFlags.HasFlag(XmlTransportFlags.LeaveOpen))
                    this.baseStream.Dispose();

                this.baseStream = null;
            }
        }

        /// <summary>
        /// Serializes each chunk in the underlying chunk collection to the underlying stream.
        /// </summary>
        private void SerializeToStream()
        {
            Contract.Requires<ReadOnlyException>(!TransportFlags.HasFlag(XmlTransportFlags.IsReadOnly));
            Contract.Requires<ReadOnlyException>(this.baseStream.CanWrite);
            Contract.Requires<InvalidOperationException>(this.chunkCollection != null);

            var document = new XDocument();
            var rootElement = new XElement("Blob");
            document.Add(rootElement);

            foreach (var chunk in this.chunkCollection)
            {
                var chunkElement = new XElement(chunk.Signature.ToUpper());
                rootElement.Add(chunkElement);
                OutputData(chunkElement, chunk.Data);
            }

            SaveOptions option = TransportFlags.HasFlag(XmlTransportFlags.DisableFormatting) ? SaveOptions.DisableFormatting : SaveOptions.None;
            document.Save(this.baseStream, option);
        }

        private void OutputData(XElement parent, DataTable data)
        {
            foreach (var entry in data)
            {
                string name = entry.Key.Replace('[', '_').Replace("]", "");
                if (Char.IsDigit(name[0])) name = "_" + name;

                var element = new XElement(name);

                if (entry.Value is DataTable)
                {
                    OutputData(element, entry.Value as DataTable);
                }
                else if (entry.Value is IEnumerable<byte>)
                {
                    var builder = new StringBuilder();
                    foreach (byte b in entry.Value as IEnumerable<byte>)
                    {
                        builder.Append(string.Format("{0:x} ", b).PadLeft(2, '0'));
                    }
                    element.Value = builder.ToString();
                }
                else
                {
                    if (element.Value == null || entry.Value == null)
                        element.Value = "{null}";
                    else
                        element.Value = entry.Value.ToString().Trim().Replace("\0", "");
                }
                parent.Add(element);
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
