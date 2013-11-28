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
using Nitrogen.Games.Halo4.ContentData.Traits;
using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant
{
    /// <summary>
    /// Represents a set of map overrides settings in a Halo 4 multiplayer variant.
    /// </summary>
    public class MapOverrides
        : ISerializable<BitStream>
    {
        private const int PowerupCount = 4;

        private bool
            indestructibleVehicles,
            turretsOnMap,
            powerupsOnMap,
            aaOnMap,
            shortcutsOnMap,
            grenadesOnMap;

        private sbyte weaponSet, vehicleSet, aaSet;
        private Halo4PlayerTraits baseTraits;
        private PowerupSettings[] powerups;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapOverrides"/> class with default values.
        /// </summary>
        public MapOverrides()
        {
            this.baseTraits = new Halo4PlayerTraits();
            this.powerups = new PowerupSettings[PowerupCount];
            this.powerups[0] = PowerupSettings.CreateDamageBoost();
            this.powerups[1] = PowerupSettings.CreateSpeedBoost();
            this.powerups[2] = PowerupSettings.CreateOvershield();
            this.powerups[3] = new PowerupSettings();
            this.turretsOnMap = true;
            this.powerupsOnMap = true;
            this.aaOnMap = true;
            this.shortcutsOnMap = true;
            this.grenadesOnMap = true;
            this.aaSet = -2;
            this.weaponSet = -2;
            this.vehicleSet = -2;
        }

        /// <summary>
        /// Gets or sets whether vehicles are indestructible.
        /// </summary>
        public bool IndestructibleVehicles
        {
            get { return this.indestructibleVehicles; }
            set { this.indestructibleVehicles = value; }
        }

        /// <summary>
        /// Gets or sets whether grenades can be spawned on the map.
        /// </summary>
        public bool GrenadesOnMap
        {
            get { return this.grenadesOnMap; }
            set { this.grenadesOnMap = value; }
        }

        /// <summary>
        /// Gets or sets whether armor abilities can be spawned on the map.
        /// </summary>
        public bool ArmorAbilitiesOnMap
        {
            get { return this.aaOnMap; }
            set { this.aaOnMap = value; }
        }

        /// <summary>
        /// Gets or sets the weapon set for all weapons placed on the map. This does not affect
        /// ordnance drops.
        /// </summary>
        public sbyte WeaponSet
        {
            get { return this.weaponSet; }
            set { this.weaponSet = value; }
        }

        /// <summary>
        /// Gets or sets the vehicle set for all vehicles placed on the map.
        /// </summary>
        public sbyte VehicleSet
        {
            get { return this.vehicleSet; }
            set { this.vehicleSet = value; }
        }

        /// <summary>
        /// Gets or sets the armor ability set for all armor abilities placed on the map.
        /// </summary>
        public sbyte ArmorAbilitySet
        {
            get { return this.aaSet; }
            set { this.aaSet = value; }
        }

        /// <summary>
        /// Gets or sets the traits that applies to all players in the match.
        /// </summary>
        public Halo4PlayerTraits BasePlayerTraits
        {
            get { return this.baseTraits; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.baseTraits = value;
            }
        }

        /// <summary>
        /// Gets or sets the properties of the Damage Boost powerup.
        /// </summary>
        public PowerupSettings DamageBoost
        {
            get { return this.powerups[0]; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.powerups[0] = value;
            }
        }

        /// <summary>
        /// Gets or sets the properties of the Speed Boost powerup.
        /// </summary>
        public PowerupSettings SpeedBoost
        {
            get { return this.powerups[1]; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.powerups[1] = value;
            }
        }

        /// <summary>
        /// Gets or sets the properties of the Overshield powerup.
        /// </summary>
        public PowerupSettings Overshield
        {
            get { return this.powerups[2]; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.powerups[2] = value;
            }
        }

        /// <summary>
        /// Gets or sets the properties of the custom powerup.
        /// </summary>
        public PowerupSettings CustomPowerup
        {
            get { return this.powerups[3]; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.powerups[3] = value;
            }
        }

        #region ISerializable<BitStream>

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.indestructibleVehicles);
            s.Stream(ref this.turretsOnMap);
            s.Stream(ref this.powerupsOnMap);
            s.Stream(ref this.aaOnMap);
            s.Stream(ref this.shortcutsOnMap);
            s.Stream(ref this.grenadesOnMap);
            s.Serialize(this.baseTraits);
            s.Stream(ref this.weaponSet);
            s.Stream(ref this.vehicleSet);
            s.Stream(ref this.aaSet);
            s.Serialize(this.powerups, 0, PowerupCount);
        }

        #endregion
    }
}