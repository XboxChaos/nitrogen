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

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo
{
    public class GlobalGameOptions
        : ISerializable<BitStream>
    {
        private short scoreLimit;
        private bool unk0, unk1, unk2;

        /// <summary>
        /// Gets or sets the score limit for each round.
        /// </summary>
        public short ScoreLimit
        {
            get { return this.scoreLimit; }
            set { this.scoreLimit = value; }
        }

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.scoreLimit);
            s.Stream(ref this.unk0);
            s.Stream(ref this.unk1);
            s.Stream(ref this.unk2);
        }
    }
}
