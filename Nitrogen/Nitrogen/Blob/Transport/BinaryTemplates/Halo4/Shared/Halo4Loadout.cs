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

namespace Nitrogen.Blob.Transport.BinaryTemplates.Halo4.Shared
{
    /// <summary>
    /// Defines the structure of a loadout in Halo 4.
    /// </summary>
    internal class Halo4Loadout
        : DataTemplate
    {
        protected override void Define()
        {
            Register<bool>("Enabled");
            bool doesNotHaveName = Register<bool>("UseDefaultName");
            if (!doesNotHaveName)
            {
                // Indices are defined in the lgtd tag.
                Register<uint>("LoadoutNameIndex", n: 7);
            }
            Register<sbyte>("PrimaryWeapon");
            Register<sbyte>("SecondaryWeapon");
            Register<sbyte>("ArmorAbility");
            Register<sbyte>("TacticalPackage");
            Register<sbyte>("SupportUpgrade");
            Register<byte>("GrenadeType", n: 5);

            // These will make 343 mad, so please don't use them.
            Register<byte>("PrimaryWeaponSkin", n: 3);
            Register<byte>("SecondaryWeaponSkin", n: 3);
        }
    }
}
