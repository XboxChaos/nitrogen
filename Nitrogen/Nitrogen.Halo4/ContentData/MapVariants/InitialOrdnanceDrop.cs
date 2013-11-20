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
    /// <summary>
    /// An initial ordnance drop point object.
    /// </summary>
    public class InitialOrdnanceDrop
        : Halo4MapVariantObject
    {
        private sbyte unk0;
        private byte unk1;
        private short unk2;

        public InitialOrdnanceDrop()
            : this(new Halo4MapVariantObjectHeader(ObjectType.InitialOrdnanceDropPoint))
        {
        }

        internal InitialOrdnanceDrop(Halo4MapVariantObjectHeader header)
            : base(header)
        {
            Contract.Requires(header.Type == ObjectType.InitialOrdnanceDropPoint);
        }

        public override void Serialize(BitStream s)
        {
            s.StreamPlusOne(ref this.unk0, 4);
            s.Stream(ref this.unk1);
            s.Stream(ref this.unk2);
        }
    }
}
