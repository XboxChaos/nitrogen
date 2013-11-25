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
using Nitrogen.ContentData;
using Nitrogen.Games.HaloReach.ContentData.GameVariants;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.HaloReach.ContentData
{
    /// <summary>
    /// Represents a Halo: Reach multiplayer variant.
    /// </summary>
    /// <remarks>Represents the 'mpvr' chunk in a game variant BLF file.</remarks>
    public sealed class HaloReachMultiplayerVariant
        : MultiplayerVariant
    {
        private HaloReachGameVariantData data;

        public HaloReachMultiplayerVariant()
            : base(version: 54)
        {
            this.data = new HaloReachGameVariantData();
        }

        public HaloReachGameVariantData Data
        {
            get { return this.data; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.data = value;
            }
        }

        #region MultiplayerVariant Members

        protected override void SerializeData(BitStream s)
        {
            this.data.Serialize(s);
        }

        #endregion
    }
}