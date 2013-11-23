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

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nitrogen.Core.IO;

namespace Nitrogen.Halo4.ContentData.MapVariants
{
    public class ObjectiveOrdnanceDrop
        : Halo4MapVariantObject
    {
        private byte unk0, unk1, unk2, unk3, unk4, unk5, unk6, unk7, unk8;

        public ObjectiveOrdnanceDrop()
            : this(new Halo4MapVariantObjectHeader(ObjectType.ObjectiveOrdnanceDropPoint))
        {
        }

        internal ObjectiveOrdnanceDrop(Halo4MapVariantObjectHeader header)
            : base(header)
        {
            Contract.Requires(header.Type == ObjectType.ObjectiveOrdnanceDropPoint);
        }

        public override void Serialize(BitStream s)
        {
            base.Serialize(s);
            s.Stream(ref this.unk0);
            s.Stream(ref this.unk1);
            s.Stream(ref this.unk2);
            s.Stream(ref this.unk3);
            s.Stream(ref this.unk4);
            s.Stream(ref this.unk5);
            s.Stream(ref this.unk6);
            s.Stream(ref this.unk7);
            s.Stream(ref this.unk8);
        }
    }
}
