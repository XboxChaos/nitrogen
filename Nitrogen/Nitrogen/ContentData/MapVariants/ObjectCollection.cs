using System;
using System.Collections.Generic;

namespace Nitrogen.ContentData.MapVariants
{
    public class ObjectCollection
        : List<IMapVariantObject>
    {
        public void Translate(float shiftX, float shiftY, float shiftZ)
        {
            foreach (var o in this)
            {
                o.X += shiftX;
                o.Y += shiftY;
                o.Z += shiftZ;
            }
        }

        public void Rotate(float pitch, float yaw, float roll)
        {
            throw new NotImplementedException();
        }

        public void Rotate(float degree)
        {
            throw new NotImplementedException();
        }
    }
}
