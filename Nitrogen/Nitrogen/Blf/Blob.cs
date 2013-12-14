using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.Blf
{
	internal sealed class Blob
		: ISerializable<BinaryStream>
	{
		private List<Chunk> _chunks;
		private Chunk _header, _footer;

		public Blob ()
		{
			_chunks = new List<Chunk>();
			_header = new Chunk("_blf", 1, ChunkFlags.IsHeader, new BlobHeader());
			_footer = new Chunk("_eof", 1, ChunkFlags.IsInitialized, new BlobFooter());
		}

		public Blob (params Chunk[] chunks)
			: this()
		{
			_chunks.AddRange(chunks);
		}

		public void AddChunk (Chunk chunk)
		{
			_chunks.Add(chunk);
		}

		public void AddChunks (params Chunk[] chunks)
		{
			_chunks.AddRange(chunks);
		}

		public bool HasChunk(string signature)
		{
			foreach ( Chunk c in _chunks )
			{
				if ( c.Signature == signature )
					return true;
			}
			return false;
		}

		public Chunk GetChunk (string signature)
		{
			foreach ( Chunk c in _chunks )
			{
				if ( c.Signature == signature )
					return c;
			}

			throw new KeyNotFoundException();
		}

		#region ISerializable<BinaryStream> Members

		void ISerializable<BinaryStream>.SerializeObject (BinaryStream s)
		{
			( _header as ISerializable<BinaryStream> ).SerializeObject(s);

			foreach ( ISerializable<BinaryStream> c in _chunks )
				c.SerializeObject(s);

			( _footer.Payload as BlobFooter ).ChunkOffset = (int) s.BaseStream.Position;
			( _footer as ISerializable<BinaryStream> ).SerializeObject(s);
		}

		#endregion
	}
}
