using Nitrogen.Core.IO;
using System;
using System.Drawing;

namespace Nitrogen.Halo4.ContentData.Traits
{
    public class ArmorColor
        : ISerializable<BitStream>
    {
        private bool useDefault;
        private byte r, g, b;
 
        public ArmorColor()
        {
            this.useDefault = true;
            this.r = this.g = this.b = 255;
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.useDefault);
            s.Stream(ref this.r);
            s.Stream(ref this.g);
            s.Stream(ref this.b);
        }

        #endregion
    }
}
