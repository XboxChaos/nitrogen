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
    public class Halo4PlayerTraitSet
        : Halo4RuntimeTraitSet
    {
        public Halo4PlayerTraitSet()
        {
            InitialPrimaryWeapon = Halo4Weapon.Unchanged;
            InitialSecondaryWeapon = Halo4Weapon.Unchanged;
            InitialGrenades = Halo4GrenadeCount.MapDefault;
            InitialArmorAbility = Halo4ArmorAbility.Unchanged;
            TacticalPackage = Halo4ArmorMod.Unchanged;
            SupportUpgrade = Halo4ArmorMod.Unchanged;
        }

        [PropertyBinding("InitialPrimaryWeapon")]
        public Halo4Weapon InitialPrimaryWeapon { get; set; }

        [PropertyBinding("InitialSecondaryWeapon")]
        public Halo4Weapon InitialSecondaryWeapon { get; set; }

        [PropertyBinding("InitialGrenadeCount")]
        public Halo4GrenadeCount InitialGrenades { get; set; }

        [PropertyBinding("InitialArmorAbility")]
        public Halo4ArmorAbility InitialArmorAbility { get; set; }

        [PropertyBinding("InitialTacticalPackage")]
        public Halo4ArmorMod TacticalPackage { get; set; }

        [PropertyBinding("InitialSupportUpgrade")]
        public Halo4ArmorMod SupportUpgrade { get; set; }
    }
}
