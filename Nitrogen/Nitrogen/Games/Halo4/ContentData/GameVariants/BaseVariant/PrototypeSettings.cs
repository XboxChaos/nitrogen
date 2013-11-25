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

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant
{
    /// <summary>
    /// Represents a set of settings in a multiplayer variant for a prototype feature which was cut
    /// from the release version of Halo 4.
    /// </summary>
    public class PrototypeSettings
        : ISerializable<BitStream>
    {
        private byte
            mode,
            prometheanEnergyKill,
            prometheanEnergyTime,
            prometheanEnergyMedal,
            prometheanDuration;

        private bool unk0;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeSettings"/> class with default values.
        /// </summary>
        public PrototypeSettings()
        {
            this.mode = 1;
            this.prometheanEnergyKill = 3;
            this.prometheanEnergyMedal = 3;
            this.prometheanEnergyTime = 3;
            this.prometheanDuration = 0;
        }

        /// <summary>
        /// Gets or sets whether the multiplayer variant contains weapon tuning data.
        /// </summary>
        public bool HasWeaponTuning
        {
            get { return this.mode == 1; }
            set { this.mode = (byte)(value ? 1 : 0); }
        }

        #region ISerializable<BitStream>

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.mode, 2);
            s.Stream(ref this.prometheanEnergyKill, 3);
            s.Stream(ref this.prometheanEnergyTime, 3);
            s.Stream(ref this.prometheanEnergyMedal, 3);
            s.Stream(ref this.prometheanDuration, 4);
            s.Stream(ref this.unk0);
        }

        #endregion
    }
}