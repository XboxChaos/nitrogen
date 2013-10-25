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

using Ionic.Zlib;
using Nitrogen.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.Blob.Transport.BinaryTemplates.Shared
{
    /// <summary>
    /// Defines the structure of a string table in Halo: Reach and Halo 4.
    /// </summary>
    internal class StringTable
        : DataTemplate
    {
        /// <summary>
        /// Indicates the number of locales.
        /// </summary>
        private const int LanguageCount = 17;

        /// <summary>
        /// Specifies whether string tables should be compressed during serialization.
        /// </summary>
        private const bool IsCompressed = true;

        private int offsetSize, lengthSize, countSize;
        private int languageCount = LanguageCount;

        protected override void Define(Context namedArgs)
        {
            this.offsetSize = (int)namedArgs["OffsetSize"];
            this.lengthSize = (int)namedArgs["LengthSize"];
            this.countSize = (int)namedArgs["CountSize"];

            if (namedArgs.ContainsKey("LanguageCount"))
                this.languageCount = (int)namedArgs["LanguageCount"];

            if (Mode == SerializationMode.Deserialize)
            {
                ReadTable();
            }
            else
            {
                WriteTable();
            }
        }

        protected override void Define()
        {
            throw new InvalidOperationException();
        }

        private void ReadTable()
        {
            uint stringCount = Register<uint>("Count", n: this.countSize);

            // Read string offsets.
            var offsetLists = new List<List<int>>();
            for (int i = 0; i < stringCount; i++)
            {
                var currentOffsets = new List<int>();
                for (int j = 0; j < LanguageCount; j++)
                {
                    bool hasString = (bool)Read(typeof(bool));
                    if (hasString)
                    {
                        int offset = Convert.ToInt32(Read(typeof(uint), n: this.offsetSize));
                        currentOffsets.Add(offset);
                    }
                    else
                    {
                        currentOffsets.Add(-1);
                    }
                }
                offsetLists.Add(currentOffsets);
            }

            // Extract string table.
            if (stringCount > 0)
            {
                byte[] uncompressedData;
                int uncompressedSize = Convert.ToInt32(Read(typeof(uint), n: this.lengthSize));
                bool isTableCompressed = (bool)Read(typeof(bool));
                if (isTableCompressed)
                {
                    int compressedSize = Convert.ToInt32(Read(typeof(uint), n: this.lengthSize));
                    var buffer = new List<byte>();
                    for (int i = 0; i < compressedSize; i++)
                    {
                        buffer.Add((byte)Read(typeof(byte)));
                    }
                    using (var dataStream = new MemoryStream(buffer.ToArray()))
                    {
                        dataStream.Position = 4; // Skip over 32-bit uncompressed size; we already have it

                        uncompressedData = new byte[uncompressedSize];
                        using (var zlibStream = new ZlibStream(dataStream, CompressionMode.Decompress, true))
                            zlibStream.Read(uncompressedData, 0, uncompressedSize);
                    }
                }
                else
                {
                    var buffer = new List<byte>();
                    for (int i = 0; i < uncompressedSize; i++)
                    {
                        buffer.Add((byte)Read(typeof(byte)));
                    }
                    uncompressedData = buffer.ToArray();
                }

                // Extract strings from the table.
                var tableStream = new MemoryStream(uncompressedData);
                using (var tableReader = new BinaryReader(tableStream))
                {
                    for (int i = 0; i < offsetLists.Count; i++)
                    {
                        Group(i.ToString(), () =>
                        {
                            var offsetList = offsetLists[i];
                            for (int j = 0; j < offsetList.Count; j++)
                            {
                                var locale = (Language)j;
                                var offset = offsetList[j];
                                if (offset >= 0)
                                {
                                    tableStream.Position = offset;

                                    var stringRaw = new List<byte>();
                                    while (tableStream.Position < tableStream.Length)
                                    {
                                        byte b = tableReader.ReadByte();
                                        if (b == 0)
                                            break;

                                        stringRaw.Add(b);
                                    }

                                    string value = Encoding.UTF8.GetString(stringRaw.ToArray());
                                    SetValue(locale.ToString(), value);
                                }
                            }
                        });
                    }
                }
            }
        }

        private void WriteTable()
        {
            // Write string count.
            var count = GetValue<uint>("Count");
            Write(count, n: this.countSize);

            // Write the offset table and build the string buffer.
            byte[] buffer;
            var offsets = new Dictionary<string, uint>();
            using (var dataStream = new MemoryStream())
            {
                for (var i = 0; i < count; i++)
                {
                    Group(i.ToString(), () =>
                    {
                        for (var j = 0; j < languageCount; j++)
                        {
                            var locale = (Language)j;
                            var name = locale.ToString();
                            var str = IsRegistered(name) ? GetValue<string>(name) : null;
                            if (str != null)
                            {
                                uint offset;
                                if (!offsets.TryGetValue(str, out offset))
                                {
                                    // No offset registered - add the string to the buffer
                                    offset = (uint)dataStream.Position;
                                    offsets[str] = offset;

                                    var bytes = Encoding.UTF8.GetBytes(str);
                                    dataStream.Write(bytes, 0, bytes.Length);
                                    dataStream.WriteByte(0);
                                }

                                // Write the string offset
                                Write(true); // Offset is present
                                Write(offset, n: this.offsetSize);
                            }
                            else
                            {
                                Write(false); // Offset is not present.
                            }
                        }
                    });
                }

                buffer = new byte[dataStream.Length];
                Buffer.BlockCopy(dataStream.GetBuffer(), 0, buffer, 0, buffer.Length);
            }

            // Write the table data
            if (count > 0)
            {
                Write(buffer.Length, n: this.lengthSize); // Uncompressed size
                Write(IsCompressed);

#pragma warning disable 162 // Disable "unreachable code detected" warnings with this if statement
                if (IsCompressed)
                {
                    // Compress the buffer
                    using (var compressedStream = new MemoryStream())
                    {
                        // Write 32-bit big-endian uncompressed size
                        compressedStream.WriteByte((byte)(buffer.Length >> 24));
                        compressedStream.WriteByte((byte)(buffer.Length >> 16));
                        compressedStream.WriteByte((byte)(buffer.Length >> 8));
                        compressedStream.WriteByte((byte)buffer.Length);

                        // zlib-compress the buffer
                        using (var zlibStream = new ZlibStream(compressedStream, CompressionMode.Compress, true))
                            zlibStream.Write(buffer, 0, buffer.Length);

                        // Get the compressed buffer data and write it
                        var compressedBuffer = new byte[compressedStream.Length];
                        Buffer.BlockCopy(compressedStream.GetBuffer(), 0, compressedBuffer, 0, compressedBuffer.Length);
                        Write(compressedBuffer.Length, n: this.lengthSize); // Compressed size
                        foreach (byte b in compressedBuffer)
                            Write(b);
                    }
                }
                else
                {
                    foreach (byte b in buffer)
                        Write(b);
                }
#pragma warning restore 162
            }
        }
    }
}