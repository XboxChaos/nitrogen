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
    public class Weapon
        : NormalObject
    {
        private byte clips;

        public Weapon()
            : this(new Halo4MapVariantObjectHeader(ObjectType.Weapon))
        {
        }

        internal Weapon(Halo4MapVariantObjectHeader header)
            : base(header)
        {
            Contract.Requires(header.Type == ObjectType.Weapon);
        }

        public override void Serialize(BitStream s)
        {
            base.Serialize(s);
            s.Stream(ref this.clips);
        }
    }
}
