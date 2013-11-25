using Nitrogen.IO;
using System;

namespace Nitrogen.ContentData.GameVariants
{
    public class MegaloMetadataHeader
        : ISerializable<BitStream>
    {
        private int encodingVersion;
        private uint buildNumber;

        public int EncodingVersion
        {
            get { return this.encodingVersion; }
            set { this.encodingVersion = value; }
        }

        public ushort BuildNumber
        {
            get { return (ushort)this.buildNumber; }
            set { this.buildNumber = (ushort)value; }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.encodingVersion);
            s.Stream(ref this.buildNumber);
        }

        #endregion
    }
}
