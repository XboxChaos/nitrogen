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

using Nitrogen.Content.Halo4.Data;
using Nitrogen.Content.Halo4.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.Content.Halo4.BaseVariant
{
    [Synchronizable]
    public class Halo4LoadoutSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4LoadoutSettings"/> class with default
        /// values.
        /// </summary>
        public Halo4LoadoutSettings()
        {
            LoadoutUsage = Halo4LoadoutUsage.Personal;
            MapLoadoutsEnabled = true;
            Palettes = new List<Halo4LoadoutPalette>();
        }

        public Halo4LoadoutUsage LoadoutUsage
        {
            get { return PersonalLoadoutsEnabled ? Halo4LoadoutUsage.Personal : Halo4LoadoutUsage.Game; }
            set { PersonalLoadoutsEnabled = value == Halo4LoadoutUsage.Personal; }
        }

        [PropertyBinding("MapLoadoutsEnabled")]
        public bool MapLoadoutsEnabled { get; set; }

        [PropertyBinding("LoadoutPalettes", Count = 6, Prefix = "Palette")]
        public List<Halo4LoadoutPalette> Palettes { get; set; }

        public Halo4LoadoutPalette GameLoadouts
        {
            get { return Palettes[0]; }
            set
            {
                Contract.Requires<ArgumentNullException>(value == null);
                Palettes[0] = value;
            }
        }

        [PropertyBinding("PersonalLoadoutsEnabled")]
        private bool PersonalLoadoutsEnabled { get; set; }

        [PropertyBinding("__unnamed_0")]
        private bool Unknown0 { get; set; }

        [PropertyBinding("__unnamed_1")]
        private bool Unknown1 { get; set; }
    }
}
