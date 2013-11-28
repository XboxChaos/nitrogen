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
    /// Represents a loadout.
    /// </summary>
    public class Loadout
        : ISerializable<BitStream>
    {
        private bool enabled;
        private byte? loadoutNameIndex; // Names are defined in the lgtd tag.
        private byte primaryWeaponSkin, secondaryWeaponSkin;
        private sbyte
            primaryWeapon,
            secondaryWeapon,
            armorAbility,
            tacticalPackage,
            supportUpgrade,
            grenadeCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Loadout"/> class with default values.
        /// </summary>
        public Loadout()
        {
            this.primaryWeapon = -3;
            this.secondaryWeapon = -3;
            this.armorAbility = -3;
            this.tacticalPackage = -3;
            this.supportUpgrade = -3;
        }

        /// <summary>
        /// Gets or sets whether this loadout is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }

        /// <summary>
        /// Gets or sets the name of this loadout. A value of <c>null</c> indicates that the default
        /// name is to be used.
        /// </summary>
        public byte? LoadoutNameIndex
        {
            get { return this.loadoutNameIndex; }
            set { this.loadoutNameIndex = value; }
        }

        /// <summary>
        /// Gets or sets the primary weapon.
        /// </summary>
        public sbyte PrimaryWeapon
        {
            get { return this.primaryWeapon; }
            set { this.primaryWeapon = value; }
        }

        /// <summary>
        /// Gets or sets the secondary weapon.
        /// </summary>
        public sbyte SecondaryWeapon
        {
            get { return this.secondaryWeapon; }
            set { this.secondaryWeapon = value; }
        }

        /// <summary>
        /// Gets or sets the armor ability.
        /// </summary>
        public sbyte ArmorAbility
        {
            get { return this.armorAbility; }
            set { this.armorAbility = value; }
        }

        /// <summary>
        /// Gets or sets the tactical package.
        /// </summary>
        public sbyte TacticalPackage
        {
            get { return this.tacticalPackage; }
            set { this.tacticalPackage = value; }
        }

        /// <summary>
        /// Gets or sets the support upgrade.
        /// </summary>
        public sbyte SupportUpgrade
        {
            get { return this.supportUpgrade; }
            set { this.supportUpgrade = value; }
        }

        /// <summary>
        /// Gets or sets the initial grenade count.
        /// </summary>
        public sbyte Grenades
        {
            get { return this.grenadeCount; }
            set { this.grenadeCount = value; }
        }

        /// <summary>
        /// Gets or sets the skin of the primary weapon.
        /// </summary>
        public byte PrimaryWeaponSkin
        {
            get { return this.primaryWeaponSkin; }
            set { this.primaryWeaponSkin = value; }
        }

        /// <summary>
        /// Gets or sets the skin of the secondary weapon.
        /// </summary>
        public byte SecondaryWeaponSkin
        {
            get { return this.secondaryWeaponSkin; }
            set { this.secondaryWeaponSkin = value; }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.enabled);
            s.StreamOptional(ref this.loadoutNameIndex, 7);
            s.Stream(ref this.primaryWeapon);
            s.Stream(ref this.secondaryWeapon);
            s.Stream(ref this.armorAbility);
            s.Stream(ref this.tacticalPackage);
            s.Stream(ref this.supportUpgrade);
            s.Stream(ref this.grenadeCount, 5);
            s.Stream(ref this.primaryWeaponSkin, 3);
            s.Stream(ref this.secondaryWeaponSkin, 3);
        }

        #endregion
    }
}
