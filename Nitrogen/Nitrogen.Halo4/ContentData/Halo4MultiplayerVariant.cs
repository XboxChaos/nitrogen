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

using Nitrogen.Core.IO;
using Nitrogen.Core.ContentData;
using Nitrogen.Halo4.ContentData.GameVariants;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.Halo4.ContentData
{
    /// <summary>
    /// Represents a Halo 4multiplayer variant.
    /// </summary>
    /// <remarks>Represents the 'mpvr' chunk in a game variant BLF file.</remarks>
    public sealed class Halo4MultiplayerVariant
        : MultiplayerVariant
    {
        private Halo4MultiplayerVariantData data;

        public Halo4MultiplayerVariant()
            : base(version: 132)
        {
            this.data = new Halo4MultiplayerVariantData();
        }

        public Halo4MultiplayerVariantData Data
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
