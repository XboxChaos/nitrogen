using Nitrogen.Core.IO;
using System;

namespace Nitrogen.Halo4.ContentData.MapVariants
{
    public class InitialOrdnanceDropPoint
        : Halo4MapVariantObject
    {
        public InitialOrdnanceDropPoint()
        {

        }

        #region Halo4MapVariantObject Members

        protected override void SerializeOrdnanceData(BitStream s)
        {
            
            /*int4 (+1 encoded)
				byte
				int16*/
        }

        #endregion
    }
}
