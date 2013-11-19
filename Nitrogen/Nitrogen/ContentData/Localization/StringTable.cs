using Ionic.Zlib;
using Nitrogen.Core.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.Core.ContentData.Localization
{
    public sealed class StringTable
    {
        private LanguageTable languages;
        private int offsetSize, lengthSize, countSize;
        private List<LocalizedString> table;

        public StringTable(LanguageTable map)
        {
            this.languages = map;
            this.table = new List<LocalizedString>();
        }

        public LocalizedString this[int index]
        {
            get { return this.table[index]; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.table[index] = value;
            }
        }

        public int Count
        {
            get { return this.table.Count; }
        }

        public LocalizedString Get(int index)
        {
            return this.table[index];
        }

        public void Set(int index, LocalizedString value)
        {
            this.table[index] = value;
        }

        public void Serialize(BitStream s, int offsetBitLength, int lengthBitLength, int countBitLength)
        {
            this.offsetSize = offsetBitLength;
            this.lengthSize = lengthBitLength;
            this.countSize = countBitLength;

            if (s.State == StreamState.Read)
            {
                ReadTable(s);
            }
            else
            {
                WriteTable(s);
            }
        }

        private void ReadTable(BitStream s)
        {
            var reader = s.Reader as BitReader;

            ulong stringCount;
            reader.Read(out stringCount, this.countSize);

            // Read string offsets.
            var offsetLists = new List<List<long>>();
            for (ulong i = 0; i < stringCount; i++)
            {
                var currentOffsets = new List<long>();
                for (int j = 0; j < this.languages.Count; j++)
                {
                    bool hasString;
                    reader.Read(out hasString);

                    if (hasString)
                    {
                        long offset;
                        reader.Read(out offset, this.offsetSize);
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
            if (stringCount == 0)
                return;

            byte[] uncompressedData;
            long uncompressedSize;
            reader.Read(out uncompressedSize, this.lengthSize);
            bool isTableCompressed;
            reader.Read(out isTableCompressed);
            if (isTableCompressed)
            {
                long compressedSize;
                reader.Read(out compressedSize, this.lengthSize);
                var buffer = new List<byte>();
                for (int i = 0; i < compressedSize; i++)
                {
                    byte temp;
                    reader.Read(out temp);
                    buffer.Add(temp);
                }

                using (var dataStream = new MemoryStream(buffer.ToArray()))
                {
                    dataStream.Position = 4; // Skip over 32-bit uncompressed size; we already have it.

                    uncompressedData = new byte[uncompressedSize];
                    using (var zlibStream = new ZlibStream(dataStream, CompressionMode.Decompress, true))
                        zlibStream.Read(uncompressedData, 0, (int)uncompressedSize);
                }
            }
            else
            {
                var buffer = new List<byte>();
                for (int i = 0; i < uncompressedSize; i++)
                {
                    byte temp;
                    reader.Read(out temp);
                    buffer.Add(temp);
                }
                uncompressedData = buffer.ToArray();
            }

            // Extract strings from the table.
            var tableStream = new MemoryStream(uncompressedData);
            using (var tableReader = new System.IO.BinaryReader(tableStream))
            {
                for (int i = 0; i < offsetLists.Count; i++)
                {
                    var offsetList = offsetLists[i];

                    var localizedString = new LocalizedString(this.languages);
                    this.table.Add(localizedString);

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
                            localizedString.Set(locale, value);
                        }
                    }
                }
            }
        }

        private void WriteTable(BitStream s)
        {

        }
    }
}
