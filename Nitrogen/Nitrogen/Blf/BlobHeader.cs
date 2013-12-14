using Nitrogen.IO;
using System;
using System.Reflection;
using System.Text;

namespace Nitrogen.Blf
{
	internal sealed class BlobHeader
		: ISerializable<BinaryStream>
	{
		private string _fileSummary;
		private short _byteOrderMark;

		public BlobHeader ()
		{
			var version = (new AssemblyName(typeof(BlobHeader).Assembly.FullName)).Version;
			_fileSummary = "\0\0Nitrogen\0" + version;
			_byteOrderMark = -2;
		}

		#region ISerializable<BinaryStream> Members

		void ISerializable<BinaryStream>.SerializeObject (BinaryStream s)
		{
			s.Stream(ref _byteOrderMark);
			s.StreamString(ref _fileSummary, Encoding.UTF8, 32);
			s.PadBytes(2);

			// This won't really work due to the fact a new BinaryStream is created for each chunk.
			// This doesn't really matter though because Halo 4 files are always in big endian.
			/*if ( _byteOrderMark != -2 )
			{
				if ( s.State == StreamState.Read )
				{
					s.Reader.Endianness = ByteOrder.LittleEndian;
				}
				else if ( s.State == StreamState.Write )
				{
					s.Writer.Endianness = ByteOrder.LittleEndian;
				}
			}*/
		}

		#endregion
	}
}
