using Nitrogen.IO;
using System;

namespace Nitrogen.Metadata
{
	[Flags]
	internal enum BuildFlags
	{
		None,
		UntrackedBuild = 1 << 0,
	}

	internal sealed class ContentHeader
		: ISerializable<BinaryStream>
	{
		public const short DefaultBuildNumber = 0x53FD; // TU 8

		public short _buildNumber, _buildFlags;
		public ContentMetadata _metadata;

		public ContentHeader (ContentMetadata metadata)
		{
			_metadata = metadata;
			_buildNumber = DefaultBuildNumber;
		}

		public short BuildNumber
		{
			get { return _buildNumber; }
			set { _buildNumber = value; }
		}

		public BuildFlags BuildFlags
		{
			get { return (BuildFlags) _buildFlags; }
			set { _buildFlags = (short) value; }
		}

		public ContentMetadata Metadata
		{
			get { return _metadata; }
			set { _metadata = value; }
		}

		#region ISerializable<BinaryStream> Members

		void ISerializable<BinaryStream>.SerializeObject (BinaryStream s)
		{
			s.Stream(ref _buildNumber);
			s.Stream(ref _buildFlags);
			s.Serialize(_metadata);
		}

		#endregion
	}
}
