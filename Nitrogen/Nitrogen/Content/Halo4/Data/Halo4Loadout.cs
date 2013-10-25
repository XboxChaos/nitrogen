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

using Nitrogen.Content.Halo4.Enums;
using System;

namespace Nitrogen.Content.Halo4.Data
{
    [Synchronizable]
    public class Halo4Loadout
    {
        public Halo4Loadout()
        {
            IsEnabled = false;
            PrimaryWeapon = Halo4Weapon.MapDefault;
            SecondaryWeapon = Halo4Weapon.MapDefault;
            ArmorAbility = Halo4ArmorAbility.MapDefault;
            TacticalPackage = Halo4ArmorMod.MapDefault;
            SupportUpgrade = Halo4ArmorMod.MapDefault;
            Grenades = Halo4GrenadeCount.MapDefault;
        }

        [PropertyBinding("Enabled")]
        public bool IsEnabled { get; set; }

        [PropertyBinding("PrimaryWeapon")]
        public Halo4Weapon PrimaryWeapon { get; set; }

        [PropertyBinding("SecondaryWeapon")]
        public Halo4Weapon SecondaryWeapon { get; set; }

        [PropertyBinding("ArmorAbility")]
        public Halo4ArmorAbility ArmorAbility { get; set; }

        [PropertyBinding("TacticalPackage")]
        public Halo4ArmorMod TacticalPackage { get; set; }

        [PropertyBinding("SupportUpgrade")]
        public Halo4ArmorMod SupportUpgrade { get; set; }

        [PropertyBinding("GrenadeType")]
        public Halo4GrenadeCount Grenades { get; set; }
    }
}
