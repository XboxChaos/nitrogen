using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using Nitrogen.Utilities;
using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Collections.Generic;

namespace Nitrogen.GameVariants
{
    internal sealed class MultiplayerVariant
        : ISerializable<BinaryStream>
    {
        private GameVariant _variantData;
        private byte[] _hashSalt;

		public MultiplayerVariant (GameVariant variantData)
			: this(KeyVault.VariantKey, variantData) { }

		public MultiplayerVariant (byte[] hashSalt, GameVariant variantData)
		{
			_hashSalt = hashSalt;
			_variantData = variantData;
		}

		public GameVariant VariantData
		{
			get { return _variantData; }
			set { _variantData = value; }
		}

        void ISerializable<BinaryStream>.SerializeObject(BinaryStream s)
        {
            long hashOffset = s.BaseStream.Position;
            s.BaseStream.Position += SaltedSHA1.Sha1HashLength + 4;

            MemoryStream buffer;
            using (buffer = new MemoryStream())
            {
                if (s.State == StreamState.Read)
                {
                    int length = s.Reader.ReadInt32();
                    var data = new byte[length];
					s.Reader.BaseStream.Read(data, 0, length);
					buffer.Write(data, 0, length);
                    buffer.Position = 0;
                }

				using ( var bitStream = new BitStream(buffer, s.State, true) )
				{
					var engine = (sbyte) _variantData.Metadata.Engine;
					bitStream.Stream(ref engine, 4);
					if ( _variantData.Metadata.Engine == GameEngine.Forge || _variantData.Metadata.Engine == GameEngine.PVP )
						bitStream.Serialize(new MegaloHeader(0, 0));

					bitStream.Serialize(_variantData);
				}
            }

            if (s.State == StreamState.Write)
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
                s.BaseStream.Position += 4; // skipped by the engine
				s.Writer.Write(hashedData, 0, hashedData.Count);
            }

			s.PadBytes(0x7C28 - 0xC - (int) s.BaseStream.Position);
        }
    }
}