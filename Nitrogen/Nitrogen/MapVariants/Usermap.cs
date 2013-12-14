using Nitrogen.Metadata;
using Nitrogen.Utilities;
using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Collections.Generic;

namespace Nitrogen.MapVariants
{
	internal sealed class Usermap
		: ISerializable<BinaryStream>
	{
		private byte[] _hashSalt;
		private MapVariant _variantData;

		public Usermap (MapVariant variantData)
			: this(KeyVault.VariantKey, variantData) { }

		public Usermap (byte[] hashSalt, MapVariant variantData)
		{
			_hashSalt = hashSalt;
			_variantData = variantData;
		}

		#region ISerializable<BinaryStream>

		void ISerializable<BinaryStream>.SerializeObject (BinaryStream s)
		{
			long hashOffset = s.BaseStream.Position;
			s.BaseStream.Position += SaltedSHA1.Sha1HashLength;

			MemoryStream buffer;
			using ( buffer = new MemoryStream() )
			{
				if ( s.State == StreamState.Read )
				{
					int length = s.Reader.ReadInt32();
					var data = new byte[length];
					s.Reader.BaseStream.Read(data, 0, length);
					buffer.Write(data, 0, length);
					buffer.Position = 0;
				}

				using ( var bitStream = new BitStream(buffer, s.State, true) )
					bitStream.Serialize(_variantData);
			}

			if ( s.State == StreamState.Write )
			{
				// Create a hash for the data and write it to the stream.
				byte[] dataBuffer = buffer.ToArray();
				byte[] lengthBytes = BitConverter.GetBytes(dataBuffer.Length);
				if ( BitConverter.IsLittleEndian && s.Writer.Endianness != ByteOrder.LittleEndian )
					Array.Reverse(lengthBytes);
				var hashedData = new List<byte>();
				hashedData.AddRange(lengthBytes);
				hashedData.AddRange(dataBuffer);
				s.BaseStream.Position = hashOffset;
				s.Writer.Write(SaltedSHA1.GenerateHash(_hashSalt, hashedData.ToArray()), 0, SaltedSHA1.Sha1HashLength);
				s.Writer.Write(hashedData, 0, hashedData.Count);
			}

			s.PadBytes(0x7028 - 0xC - (int) s.BaseStream.Position);
		}

		#endregion
	}
}