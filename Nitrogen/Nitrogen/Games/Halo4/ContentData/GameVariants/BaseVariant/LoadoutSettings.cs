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
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant
{
    /// <summary>
    /// Represents a set of loadout settings in a Halo 4 multiplayer variant.
    /// </summary>
    public class LoadoutSettings
        : ISerializable<BitStream>
    {
        /// <summary>
        /// Specifies the number of palettes available.
        /// </summary>
        public const int PaletteCount = 6;

        private bool personalLoadoutsEnabled, mapLoadoutsEnabled;
        private bool unk0, unk1;
        private LoadoutPalette[] palettes;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadoutSettings"/> class with default values.
        /// </summary>
        public LoadoutSettings()
        {
            this.palettes = new LoadoutPalette[PaletteCount];
            for (int i = 0; i < PaletteCount; i++)
                this.palettes[i] = new LoadoutPalette();
        }

        /// <summary>
        /// Gets or sets whether personal loadouts are enabled.
        /// </summary>
        public bool PersonalLoadoutsEnabled
        {
            get { return this.personalLoadoutsEnabled; }
            set { this.personalLoadoutsEnabled = value; }
        }

        /// <summary>
        /// Gets or sets whether map loadouts are enabled.
        /// </summary>
        public bool MapLoadoutsEnabled
        {
            get { return this.mapLoadoutsEnabled; }
            set { this.mapLoadoutsEnabled = value; }
        }
        
        /// <summary>
        /// Gets or sets the loadout palettes.
        /// </summary>
        public LoadoutPalette[] Palettes
        {
            get { return this.palettes; }
            set
            {
                Contract.Requires(value.Length == PaletteCount);

                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] == null)
                        throw new ArgumentException("Palette at index " + i + " is null");
                }

                this.palettes = value;
            }
        }

        #region ISerializable<BitStream>

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.personalLoadoutsEnabled);
            s.Stream(ref this.unk0);
            s.Stream(ref this.unk1);
            s.Stream(ref this.mapLoadoutsEnabled);
            s.Serialize(this.palettes, 0, PaletteCount);
        }

        #endregion
    }
}