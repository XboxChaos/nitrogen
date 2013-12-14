using Nitrogen.IO;
using System;

namespace Nitrogen.Blf
{
	internal sealed class BlobFooter
		: ISerializable<BinaryStream>
	{
		private int SignatureSize = 256;

		private int _chunkOffset;
		private byte _flags;
		private byte[] _rsaSignature;

		public BlobFooter ()
		{
			_rsaSignature = new byte[SignatureSize];
		}

		internal int ChunkOffset
		{
			get { return _chunkOffset; }
			set { _chunkOffset = value; }
		}

		/// <summary>
		/// Appends an empty RSA signature to this chunk. This is needed for signed BLF files. The
		/// modified xex ignores the signature but the chunk size needs to accomodate the signature.
		/// </summary>
		internal void Resign ()
		{
			_flags = 3;
		}

		#region ISerializable<BinaryStream> Members

		void ISerializable<BinaryStream>.SerializeObject (BinaryStream s)
		{
			s.Stream(ref _chunkOffset);
			s.Stream(ref _flags);

			if ( _flags == 3 && s.State == StreamState.Write )
				s.Writer.Write(_rsaSignature, 0, SignatureSize);
		}

		#endregion
	}
}
