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
    /// Indicates the weapon set to use.
    /// </summary>
    public enum WeaponSet
        : sbyte
    {
        MapDefault = -2,
        Disabled,
        Human,
        Covenant,
        Forerunner,
        Snipers,
        NoSnipers,
        Rockets,
        NoPowerWeapons,
        Juggernaut,
        SlayerPro,
        Rifles,
        MidRange,
        LongRange,
        UNSCSnipers,
        MeleeWeapons,
        Swords,
        Hammers,
        WeaponsOfMassDestruction,
        NoWeapons,
        PowerWeapons,
        SpartanOps,
    }

    /// <summary>
    /// Indicates the vehicle set to use.
    /// </summary>
    public enum VehicleSet
        : sbyte
    {
        MapDefault = -2,
        Disabled,
        AllVehicles,
        Mongooses,
        Warthogs,
        LightGround,
        Tanks,
        Aircraft,
        Mantis,
        AllUNSC,
        AllCovenant,
        NoUNSC,
        NoCovenant,
        NoLightGround,
        NoTanks,
        NoAircraft,
        NoMantis,
        NoHeavy,
        NoVehicles,
        DeleteAllExceptMongoose
    }

    /// <summary>
    /// Indicates the armor ability set to use.
    /// </summary>
    public enum ArmorAbilitySet
        : sbyte
    {
        MapDefault = -2,
        Disabled,
        SpartanOps
    }

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
            VehiclesOnMap = VehicleSet.MapDefault;
            WeaponsOnMap = WeaponSet.MapDefault;
            ArmorAbilitiesOnMap = ArmorAbilitySet.MapDefault;
        }

        public bool IndestructibleVehicles
        {
            get { return this.indestructibleVehicles; }
            set { this.indestructibleVehicles = value; }
        }

        public bool GrenadesOnMap
        {
            get { return this.grenadesOnMap; }
            set { this.grenadesOnMap = value; }
        }

        public WeaponSet WeaponsOnMap
        {
            get { return (WeaponSet)this.weaponSet; }
            set
            {
                Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(WeaponSet), value));
                this.weaponSet = (sbyte)value;
            }
        }

        public VehicleSet VehiclesOnMap
        {
            get { return (VehicleSet)this.vehicleSet; }
            set
            {
                Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(VehicleSet), value));
                this.vehicleSet = (sbyte)value;
            }
        }

        public ArmorAbilitySet ArmorAbilitiesOnMap
        {
            get { return (ArmorAbilitySet)this.aaSet; }
            set
            {
                Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(ArmorAbilitySet), value));

                this.aaSet = (sbyte)value;
                this.aaOnMap = (value == ArmorAbilitySet.Disabled);
            }
        }

        public Halo4PlayerTraits BasePlayerTraits
        {
            get { return this.baseTraits; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.baseTraits = value;
            }
        }

        public PowerupSettings DamageBoost
        {
            get { return this.powerups[0]; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.powerups[0] = value;
            }
        }

        public PowerupSettings SpeedBoost
        {
            get { return this.powerups[1]; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.powerups[1] = value;
            }
        }

        public PowerupSettings Overshield
        {
            get { return this.powerups[2]; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.powerups[2] = value;
            }
        }

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