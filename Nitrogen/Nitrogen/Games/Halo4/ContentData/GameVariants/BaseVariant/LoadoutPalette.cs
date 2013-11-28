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
    /// Represents a loadout palette.
    /// </summary>
    public class LoadoutPalette
        : ISerializable<BitStream>
    {
        /// <summary>
        /// Specifies the number of loadouts in a palette.
        /// </summary>
        public const int LoadoutCount = 5;

        private Loadout[] loadouts;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadoutPalette"/> class with default values.
        /// </summary>
        public LoadoutPalette()
        {
            this.loadouts = new Loadout[LoadoutCount];
            for (int i = 0; i < LoadoutCount; i++)
                this.loadouts[i] = new Loadout();
        }

        /// <summary>
        /// Gets or sets the loadout at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index in this palette.</param>
        /// <returns>A <see cref="Loadout"/> object.</returns>
        public Loadout this[int index]
        {
            get { return this.loadouts[index]; }
            set
            {
                Contract.Requires<IndexOutOfRangeException>(index >= 0 && index < LoadoutCount);
                Contract.Requires<ArgumentNullException>(value != null);
                this.loadouts[index] = value;
            }
        }

        /// <summary>
        /// Gets or sets the loadouts in this palette.
        /// </summary>
        public Loadout[] Loadouts
        {
            get { return this.loadouts; }
            set
            {
                Contract.Requires(value.Length == LoadoutCount);

                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] == null)
                        throw new ArgumentException("Loadout at index " + i + " is null");
                }

                this.loadouts = value;
            }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Serialize(this.loadouts, 0, LoadoutCount);
        }

        #endregion
    }
}