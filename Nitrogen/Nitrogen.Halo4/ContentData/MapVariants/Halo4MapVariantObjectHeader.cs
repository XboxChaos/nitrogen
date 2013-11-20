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
using System.Diagnostics.Contracts;

namespace Nitrogen.Halo4.ContentData.MapVariants
{
    class Halo4MapVariantObjectHeader
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
        private ObjectType objectType;
        private short unk10;

        public Halo4MapVariantObjectHeader()
        {
        }

        public Halo4MapVariantObjectHeader(ObjectType type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Gets or sets the shape of the object's boundary.
        /// </summary>
        public IBoundary Shape
        {
            get { return this.shape; }
            set { this.shape = value; }
        }

        /// <summary>
        /// Gets or sets the object's type.
        /// </summary>
        public ObjectType Type
        {
            get { return this.objectType; }
            set
            {
                Contract.Requires<ArgumentException>(Enum.IsDefined(typeof(ObjectType), value));
                this.objectType = value;
            }
        }

        #region ISerializable<BitStream> Members

        public virtual void Serialize(BitStream s)
        {
            s.Stream(ref this.unk0, 2);
            s.StreamOptional(ref this.unk1);
            s.StreamOptional(ref this.unk2, 5);

            bool irrelevant = false;
            s.Stream(ref irrelevant);
            /* An optional 2-bit integer goes here if the above value is true, but it'll never get
             * read anyway since the other part of the condition will never be true. */

            s.Stream(ref this.unk3, 21);
            s.Stream(ref this.unk4, 21);
            s.Stream(ref this.unk5, 21);
            s.StreamOptional(ref unk6, 20);
            s.Stream(ref this.unk7, 14, -(float)Math.PI, (float)Math.PI, false, false, false);
            s.StreamPlusOne(ref this.unk8, 10);
            s.Stream(ref this.unk9, 6, 0.0f, 10.0f, false, false, true);
            s.Stream(ref this.isLocked);

            byte shapeType = (this.shape == null) ? (byte)0 : this.shape.BoundaryIndex;
            s.Stream(ref shapeType, 2);
            if (this.shape != null)
                s.Serialize(this.shape);

            byte type = 0;
            s.Stream(ref type, 6);
            this.objectType = (ObjectType)type;

            s.Stream(ref this.unk10, 10);
        }

        #endregion
    }
}