using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo
{
    internal sealed class MegaloHeader
        : ISerializable<BitStream>
    {
        private int _encodingVersion, _buildNumber;

		public MegaloHeader () { }

		public MegaloHeader (int encodingVersion, short buildNumber)
		{
			_encodingVersion = encodingVersion;
			_buildNumber = buildNumber;
		}

        public int EncodingVersion
        {
            get { return _encodingVersion; }
            set { _encodingVersion = value; }
        }

        public short BuildNumber
        {
            get { return (short)_buildNumber; }
            set { _buildNumber = value; }
        }

        void ISerializable<BitStream>.SerializeObject(BitStream s)
        {
            s.Stream(ref _encodingVersion);
            s.Stream(ref _buildNumber);
        }
    }
}
