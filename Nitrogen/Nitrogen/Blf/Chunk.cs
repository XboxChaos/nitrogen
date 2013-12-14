using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.Blf
{
	[Flags]
	internal enum ChunkFlags
	{
		None = 0,
		IsInitialized = 1 << 0,
		IsHeader = 1 << 1,
	}

	internal sealed class Chunk
		: ISerializable<BinaryStream>
	{
		private string _signature;
		private short _version;
		private short _flags;
		private ISerializable<BinaryStream> _payload;

		public Chunk (string signature, short version, ChunkFlags flags, ISerializable<BinaryStream> payload)
		{
			Contract.Requires<ArgumentNullException>(signature != null && payload != null);
			Contract.Requires(Encoding.UTF8.GetByteCount(signature) == 4);

			_signature = signature;
			_version = version;
			_flags = (short) flags;
			_payload = payload;
		}

		private Chunk () { }

		public string Signature { get { return _signature; } }

		public short Version { get { return _version; } }

		public ChunkFlags Flags { get { return (ChunkFlags) _flags; } }

		public ISerializable<BinaryStream> Payload { get { return _payload; } }

		#region ISerializable<BinaryStream> Members

		void ISerializable<BinaryStream>.SerializeObject (BinaryStream s)
		{
			if ( s.State == StreamState.Read )
			{
				long offset = s.BaseStream.Position;

				// chunk signature is actually an int32
				var signatureBytes = BitConverter.GetBytes(s.Reader.ReadInt32());
				if ( BitConverter.IsLittleEndian && s.Reader.Endianness != ByteOrder.LittleEndian )
					Array.Reverse(signatureBytes);
				_signature = Encoding.UTF8.GetString(signatureBytes, 0, 4);

				int length = s.Reader.ReadInt32();
				_version = s.Reader.ReadInt16();
				_flags = s.Reader.ReadInt16();

				byte[] buffer = new byte[length - 12];
				s.BaseStream.Read(buffer, 0, buffer.Length);

				using ( var ms = new MemoryStream(buffer) )
				using ( var bufferStream = new BinaryStream(ms, StreamState.Read, s.Reader.Endianness, leaveOpen: true) )
					_payload.SerializeObject(bufferStream);

				s.BaseStream.Position = offset + length;
			}
			else if ( s.State == StreamState.Write )
			{
				int length;
				byte[] buffer;
				using ( var ms = new MemoryStream() )
				{
					using ( var bufferStream = new BinaryStream(ms, StreamState.Write, s.Writer.Endianness, leaveOpen: true) )
						_payload.SerializeObject(bufferStream);

					length = (int) ( ms.Length );
					buffer = ms.ToArray();
				}

				// chunk signature is actually an int32
				byte[] signature = Encoding.UTF8.GetBytes(_signature);
				if ( BitConverter.IsLittleEndian && s.Reader.Endianness != ByteOrder.LittleEndian )
					Array.Reverse(signature);
				s.Writer.Write(BitConverter.ToInt32(signature, 0));

				s.Writer.Write(length + 12);
				s.Writer.Write(_version);
				s.Writer.Write(_flags);

				s.BaseStream.Write(buffer, 0, length);
			}
		}

		#endregion
	}
}
