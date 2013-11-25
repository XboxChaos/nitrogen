/*
 *   Nitrogen - Halo Content API
 *   Copyright © 2013 The Nitrogen Authors. All rights reserved.
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

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nitrogen.IO;

namespace Nitrogen.Games.Halo4.ContentData.MapVariants
{
    // TODO: Come up with better names for this and SimpleObject once more research is done
    public abstract class NormalObject
        : Halo4MapVariantObject
    {
        private const int LabelCount = 4;

        private sbyte unk0;
        private byte unk1;
        private byte? unk2;
        private sbyte unk3;
        private sbyte unk4;
        private byte?[] labels = new byte?[LabelCount];

        internal NormalObject(Halo4MapVariantObjectHeader header)
            : base(header)
        {
            Contract.Requires(header.Type != ObjectType.InitialOrdnanceDropPoint);
            Contract.Requires(header.Type != ObjectType.RandomOrdnanceDropPoint);
            Contract.Requires(header.Type != ObjectType.ObjectiveOrdnanceDropPoint);
            Contract.Requires(header.Type != ObjectType.PersonalOrdnanceDropPoint);
        }

        /// <summary>
        /// Gets the labels that the object has.
        /// </summary>
        public byte?[] Labels
        {
            get { return this.labels; }
        }

        public override void Serialize(BitStream s)
        {
            base.Serialize(s);
            s.StreamPlusOne(ref this.unk0, 4);
            s.Stream(ref this.unk1);
            s.StreamOptional(ref this.unk2, 3);
            s.Stream(ref this.unk3);
            s.Stream(ref this.unk4);
            for (int i = 0; i < LabelCount; i++)
                s.StreamOptional(ref this.labels[i]);
        }
    }
}
