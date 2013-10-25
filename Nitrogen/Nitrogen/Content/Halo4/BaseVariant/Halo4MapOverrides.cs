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

namespace Nitrogen.Content.Halo4.BaseVariant
{
    [Synchronizable]
    public class Halo4MapOverrides
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4MapOverrides"/> class with default values.
        /// </summary>
        public Halo4MapOverrides()
        {
            ArmorAbilitiesOnMap = true;
            GrenadesOnMap = true;
            BasePlayerTraits = new Halo4PlayerTraitSet();
            VehiclesOnMap = Halo4VehicleSet.MapDefault;
            WeaponsOnMap = Halo4WeaponSet.MapDefault;
        }

        [PropertyBinding("IndestructibleVehicles")]
        public bool IndestructibleVehicles { get; set; }

        [PropertyBinding("ArmorAbilitiesOnMap")]
        public bool ArmorAbilitiesOnMap { get; set; }

        [PropertyBinding("GrenadesOnMap")]
        public bool GrenadesOnMap { get; set; }

        [PropertyBinding("BasePlayerTraits/Traits")]
        public Halo4PlayerTraitSet BasePlayerTraits { get; set; }

        [PropertyBinding("VehicleSet")]
        public Halo4VehicleSet VehiclesOnMap { get; set; }

        [PropertyBinding("WeaponSet")]
        public Halo4WeaponSet WeaponsOnMap { get; set; }
    }
}
