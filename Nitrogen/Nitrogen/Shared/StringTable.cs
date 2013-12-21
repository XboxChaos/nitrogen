using Ionic.Zlib;
using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.Shared
{
	public sealed class StringTable

	{
		private const bool IsCompressed = true;

		private static readonly int LanguageCount = Enum.GetValues(typeof(Language)).Length;

		private int _offsetSize, _lengthSize, _countSize;
		private List<LocalizedString> _table;

		public StringTable ()
		{
			_table = new List<LocalizedString>();
		}

		public LocalizedString this[int index]
		{
			get
			{
				Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
				return _table[index];
			}

			set
			{
				Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
				Contract.Requires<ArgumentNullException>(value != null);
				_table[index] = value;
			}
		}

		public int Count
		{
			get { return _table.Count; }
		}

		public LocalizedString Get (int index)
		{
			Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
			return _table[index];
		}

		public void Set (int index, LocalizedString value)
		{
			Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
			Contract.Requires<ArgumentNullException>(value != null);
			_table[index] = value;
		}

		public int Add (LocalizedString value)
		{
			_table.Add(value);
			return _table.Count - 1;
		}

		internal void Serialize(BitStream s, int offsetBitLength, int lengthBitLength, int countBitLength)
		{
			_offsetSize = offsetBitLength;
			_lengthSize = lengthBitLength;
			_countSize = countBitLength;

			if ( s.State == StreamState.Read )
			{
				ReadTable(s);
			}
			else
			{
				WriteTable(s);
			}
		}

		private void ReadTable (BitStream s)
		{
			ulong stringCount = s.Reader.ReadUIntN(_countSize);

			// Read string offsets.
			var offsetLists = new List<List<long>>();
			for ( ulong i = 0; i < stringCount; i++ )
			{
				var currentOffsets = new List<long>();
				for ( int j = 0; j < LanguageCount; j++ )
				{
					bool hasString = s.Reader.ReadBit();
					if ( hasString )
					{
						long offset = s.Reader.ReadIntN(_offsetSize);
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
			if ( stringCount == 0 )
				return;

			byte[] uncompressedData;
			long uncompressedSize = s.Reader.ReadIntN(_lengthSize);
			bool isTableCompressed = s.Reader.ReadBit();
			if ( isTableCompressed )
			{
				long compressedSize = s.Reader.ReadIntN(_lengthSize);
				var buffer = new List<byte>();
				for ( int i = 0; i < compressedSize; i++ )
				{
					byte temp = s.Reader.ReadByte();
					buffer.Add(temp);
				}

				using ( var dataStream = new MemoryStream(buffer.ToArray()) )
				{
					dataStream.Position = 4; // Skip over 32-bit uncompressed size; we already have it.

					uncompressedData = new byte[uncompressedSize];
					using ( var zlibStream = new ZlibStream(dataStream, CompressionMode.Decompress, true) )
						zlibStream.Read(uncompressedData, 0, (int) uncompressedSize);
				}
			}
			else
			{
				var buffer = new List<byte>();
				for ( int i = 0; i < uncompressedSize; i++ )
				{
					byte temp = s.Reader.ReadByte();
					buffer.Add(temp);
				}
				uncompressedData = buffer.ToArray();
			}

			// Extract strings from the table.
			_table.Clear();
			var tableStream = new MemoryStream(uncompressedData);
			using ( var tableReader = new System.IO.BinaryReader(tableStream) )
			{
				for ( int i = 0; i < offsetLists.Count; i++ )
				{
					var offsetList = offsetLists[i];

					var localizedString = new LocalizedString();
					_table.Add(localizedString);

					for ( int j = 0; j < offsetList.Count; j++ )
					{
						var locale = (Language) j;
						var offset = offsetList[j];
						if ( offset >= 0 )
						{
							tableStream.Position = offset;

							var stringRaw = new List<byte>();
							while ( tableStream.Position < tableStream.Length )
							{
								byte b = tableReader.ReadByte();
								if ( b == 0 )
									break;

								stringRaw.Add(b);
							}

							byte[] rawArray = stringRaw.ToArray();
							string value = Encoding.UTF8.GetString(rawArray, 0, rawArray.Length);
							localizedString.Set(locale, value);
						}
					}
				}
			}
		}

		private void WriteTable (BitStream s)
		{
			s.Writer.Write(_table.Count, bits: _countSize);

			// Write the offset table and build the string buffer
			byte[] buffer;
			using (var dataStream = new MemoryStream())
			{
				var offsets = new Dictionary<string, uint>();

				foreach ( var localizedString in _table )
				{
					var values = localizedString.GetTable();
					foreach ( var locale in values )
					{
						if ( string.IsNullOrEmpty(locale.Value) )
						{
							s.Writer.WriteBit(false); // offset is not present
						}
						else
						{
							uint offset;
							if ( !offsets.TryGetValue(locale.Value, out offset) )
							{
								// No offset reqistered, add the string to the buffer.
								offset = (uint) dataStream.Position;
								offsets[locale.Value] = offset;

								var encodedValue = Encoding.UTF8.GetBytes(locale.Value);
								dataStream.Write(encodedValue, 0, encodedValue.Length);
								dataStream.WriteByte(0); // null terminator
							}

							// Write string offset
							s.Writer.WriteBit(true); // offset is present
							s.Writer.Write(offset, bits: _offsetSize);
						}
					}
				}

				buffer = dataStream.ToArray();
			}

			// Write the table data
			if (_table.Count > 0)
			{
				s.Writer.Write(buffer.Length, bits: _lengthSize); // uncompressed size
				s.Writer.WriteBit(true); // always compress the string table

#pragma warning disable 162
				if (IsCompressed)
				{
					// Compress the buffer
					using (var compressedStream = new MemoryStream())
					{
						// Write 32-bit big endian uncompressed size
						compressedStream.WriteByte((byte) ( buffer.Length >> 24 ));
						compressedStream.WriteByte((byte) ( buffer.Length >> 16 ));
						compressedStream.WriteByte((byte) ( buffer.Length >> 8 ));
						compressedStream.WriteByte((byte) buffer.Length);

						// zlib-compress the buffer
						using ( var zlibStream = new ZlibStream(compressedStream, CompressionMode.Compress, true) )
							zlibStream.Write(buffer, 0, buffer.Length);

						// Get the compressed buffer data and write it
						var compressedBuffer = compressedStream.ToArray();
						s.Writer.Write(compressedBuffer.Length, bits: _lengthSize);
						s.Writer.BaseStream.Write(compressedBuffer, 0, compressedBuffer.Length);
					}
				}
				else
				{
					s.Writer.BaseStream.Write(buffer, 0, buffer.Length);
				}
#pragma warning restore 162
			}
		}
	}
}
