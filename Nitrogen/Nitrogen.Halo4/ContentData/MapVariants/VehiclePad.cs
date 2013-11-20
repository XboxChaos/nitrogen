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
    public class VehiclePad
        : NormalObject
    {
        private sbyte cooldown;

        public VehiclePad()
            : this(new Halo4MapVariantObjectHeader(ObjectType.Dispenser))
        {
        }

        internal VehiclePad(Halo4MapVariantObjectHeader header)
            : base(header)
        {
            Contract.Requires(header.Type == ObjectType.Dispenser);
        }

        public override void Serialize(BitStream s)
        {
            base.Serialize(s);
            s.Stream(ref this.cooldown);
        }
    }
}
