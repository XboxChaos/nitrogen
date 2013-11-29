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

using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo
{
    public class MapLoadout
        : ISerializable<BitStream>
    {
        private byte group;
        private Loadout loadout;

        public MapLoadout()
        {
            this.loadout = new Loadout();
        }

        public Loadout Loadout
        {
            get { return this.loadout; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.loadout = value;
            }
        }

        public byte Group
        {
            get { return this.group; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 3);
                this.group = value;
            }
        }

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.group, 2);
            s.Serialize(this.loadout);
        }
    }
}
