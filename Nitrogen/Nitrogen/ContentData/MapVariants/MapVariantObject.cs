/*
 *   Nitrogen - Halo Content API
 *   Copyright (c) 2013 Matt Saville and Aaron Dierking
 * 
 *   This file is part of Nitrogen.
 *
 *   Nitrogen is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   Nitrogen is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with Nitrogen.  If not, see <http://www.gnu.org/licenses/>.
 */

using Nitrogen.Core.ContentData.MapVariants;
using Nitrogen.Core.IO;
using System;

namespace Nitrogen.Core.ContentData.MapVariants
{
    public class MapVariantObject
        : ISerializable<BitStream>
    {
        private byte unk0;
        private byte? unk1;
        private sbyte? unk2;
        private int unk3, unk4, unk5;
        private int? unk6;
        private float unk7;
        private short unk8;
        private float unk9;
        private bool isLocked;
        private IBoundary shape;

        public MapVariantObject()
        {
        }

        /// <summary>
        /// Gets 
        /// </summary>
        public IBoundary Shape
        {
            get { return this.shape; }
            set { this.shape = value; }
        }

        #region ISerializable<BitStream> Members

        public virtual void Serialize(BitStream s)
        {
            s.Stream(ref this.unk0, 2);
            s.StreamOptional(ref this.unk1);
            s.StreamOptional(ref this.unk2, 5);

            bool alwaysTrue = true;
            s.Stream(ref alwaysTrue);
            if (!alwaysTrue) { throw new InvalidOperationException(); }

            s.Stream(ref this.unk3, 20);
            s.Stream(ref this.unk4, 20);
            s.Stream(ref this.unk5, 20);
            s.StreamOptional(ref unk6, 20);
            // TODO: encoded float (14 bits, flag1: false, flag2: false, min: -pi, max: pi)
            s.StreamPlusOne(ref this.unk8, 10);
            // TODO: encoded float (6 bits, flag1: false, flag2: true, min: 0.0, max: 10.0)
            s.Stream(ref this.isLocked);

            byte shapeType = (this.shape == null) ? (byte)0 : this.shape.BoundaryIndex;
            s.Stream(ref shapeType);
            if (this.shape != null)
                s.Serialize(this.shape);
        }

        #endregion
    }
}