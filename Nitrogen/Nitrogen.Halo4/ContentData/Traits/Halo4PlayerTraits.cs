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

using Nitrogen.Core.ContentData.Traits;
using Nitrogen.Core.IO;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.Halo4.ContentData.Traits
{
    /// <summary>
    /// Represents a set of traits in Halo 4.
    /// </summary>
    public partial class Halo4PlayerTraits
        : ISerializable<BitStream>
    {
        private ArmorTraits armor;
        private EquipmentTraits equipment;
        private MovementTraits movement;
        private AppearanceTraits appearance;
        private ScreenAndAudioTraits hudAudioTraits;

        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4PlayerTraits"/> class with default values.
        /// </summary>
        public Halo4PlayerTraits()
        {
            this.armor = new ArmorTraits();
            this.equipment = new EquipmentTraits();
            this.movement = new MovementTraits();
            this.appearance = new AppearanceTraits();
            this.hudAudioTraits = new ScreenAndAudioTraits();
        }

        /// <summary>
        /// Gets or sets the set of traits related to a player's armor.
        /// </summary>
        public ArmorTraits Armor
        {
            get { return this.armor; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.armor = value;
            }
        }

        /// <summary>
        /// Gets or sets the set of traits related to a player's equipment.
        /// </summary>
        public EquipmentTraits Equipment
        {
            get { return this.equipment; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.equipment = value;
            }
        }

        /// <summary>
        /// Gets or sets the set of traits related to a player's movement.
        /// </summary>
        public MovementTraits Movement
        {
            get { return this.movement; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.movement = value;
            }
        }

        /// <summary>
        /// Gets or sets the set of traits related to a player's outward appearance.
        /// </summary>
        public AppearanceTraits Appearance
        {
            get { return this.appearance; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.appearance = value;
            }
        }

        /// <summary>
        /// Gets or sets the set of traits related to a player's HUD and audio.
        /// </summary>
        public ScreenAndAudioTraits ScreenAndAudio
        {
            get { return this.hudAudioTraits; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.hudAudioTraits = value;
            }
        }

        /// <summary>
        /// Applies the properties of the Shielding tactical package to this trait set.
        /// </summary>
        public void ApplyShieldingUpgrade()
        {
            Armor.ShieldRechargeDelay = 6.0f;
            Armor.ShieldRechargeRate = 2.0f;
        }

        /// <summary>
        /// Applies the properties of the Mobility tactical package to this trait set.
        /// </summary>
        public void ApplyMobilityUpgrade()
        {
            Equipment.SprintStaminaDepletionRate = 0f;
            Equipment.SprintStaminaRestoreDelay = 0.66f;
            Equipment.SprintStaminaRestoreRate = 1.35f;
        }

        /// <summary>
        /// Applies the properties of the Grenadier tactical package to this trait set.
        /// </summary>
        public void ApplyGrenadierUpgrade()
        {
            Equipment.ExtraGrenade = InheritableToggle.Enabled;
        }
        /// <summary>
        /// Applies the properties of the Resupply tactical package to this trait set.
        /// </summary>

        public void ApplyResupplyUpgrade()
        {
            Equipment.GrenadePickup = InheritableToggle.Enabled;
        }

        /// <summary>
        /// Applies the properties of the Requisition tactical package to this trait set.
        /// </summary>
        public void ApplyRequisitionUpgrade()
        {
            Equipment.Requisition = InheritableToggle.Enabled;
        }

        /// <summary>
        /// Applies the properties of the Wheelman tactical package to this trait set.
        /// </summary>
        public void ApplyWheelmanUpgrade()
        {
            Armor.VehicleShieldRechargeDelay = 1.50f;
            Armor.VehicleShieldRechargeRate = 1.50f;
            Armor.VehicleEMPDisableDuration = 0.50f;
        }

        /// <summary>
        /// Applies the properties of the A.A. Efficiency tactical package to this trait set.
        /// </summary>
        public void ApplyAAEfficiencyUpgrade()
        {
            Equipment.ArmorAbilityEnergyRechargeDelay = 1.0f;
            Equipment.ArmorAbilityEnergyRechargeRate = 2.0f;
            Equipment.ArmorAbilityInitialEnergy = 1.0f;
        }
        
        /// <summary>
        /// Applies the properties of the Ammo support upgrade to this trait set.
        /// </summary>
        public void ApplyAmmoUpgrade()
        {
            Equipment.ExtraAmmo = InheritableToggle.Enabled;
        }

        /// <summary>
        /// Applies the properties of the Dexterity support upgrade to this trait set.
        /// </summary>
        public void ApplyDexterityUpgrade()
        {
            Equipment.WeaponSwitchSpeed = 1.15f;
            Equipment.WeaponReloadSpeed = 1.66f;
        }

        /// <summary>
        /// Applies the properties of the Sensor support upgrade to this trait set.
        /// </summary>
        public void ApplySensorUpgrade()
        {
            ScreenAndAudio.MotionSensorRange = 60f;
        }

        /// <summary>
        /// Applies the properties of the Ordnance Priority support upgrade to this trait set.
        /// </summary>
        public void ApplyOrdnancePriorityUpgrade()
        {
            Equipment.OrdnancePointsModifier = 1.40f;
        }

        /// <summary>
        /// Applies the properties of the Awareness support upgrade to this trait set.
        /// </summary>
        public void ApplyAwarenessUpgrade()
        {
            ScreenAndAudio.MotionSensorWhileScoped = InheritableToggle.Enabled;
        }

        /// <summary>
        /// Applies the properties of the Stability support upgrade to this trait set.
        /// </summary>
        public void ApplyStabilityUpgrade()
        {
            Equipment.FlinchRate = 0.50f;
        }

        /// <summary>
        /// Applies the properties of the Explosives support upgrade to this trait set.
        /// </summary>
        public void ApplyExplosivesUpgrade()
        {
            Armor.ExplosiveDamageResistance = 0.25f;
            Equipment.ExplosionRadius = 1.25f;
        }

        /// <summary>
        /// Applies the properties of the Stealth support upgrade to this trait set.
        /// </summary>
        public void ApplyStealthUpgrade()
        {
            Movement.Stealth = InheritableToggle.Enabled;
            Equipment.AssassinationSpeed = 1.20f;
        }

        /// <summary>
        /// Applies the properties of the Gunner support upgrade to this trait set.
        /// </summary>
        public void ApplyGunnerUpgrade()
        {
            Equipment.TurretCooldownRate = 0.66f;
        }

        /// <summary>
        /// Applies the properties of the Drop Recon support upgrade to this trait set.
        /// </summary>
        public void ApplyDropReconUpgrade()
        {
            Equipment.OrdnanceWarningDistance = 1.2f;
            Equipment.OrdnanceWarningDuration = 5f;
        }

        /// <summary>
        /// Applies the properties of the Nemesis support upgrade to this trait set.
        /// </summary>
        public void ApplyNemesisUpgrade()
        {
            ScreenAndAudio.Nemesis = InheritableToggle.Enabled;
            ScreenAndAudio.NemesisDuration = 4.5f;
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Serialize(this.armor);
            s.Serialize(this.equipment);
            s.Serialize(this.movement);
            s.Serialize(this.appearance);
            s.Serialize(this.hudAudioTraits);
        }

        #endregion

        protected static void AddModifier(BitStream s, ref float? value)
        {
            if (s.State == StreamState.Read)
            {
                var r = (s.Reader as BitReader);

                bool isPresent;
                s.Reader.Read(out isPresent);

                if (isPresent)
                    value = r.ReadEncodedFloat(n: 16, min: -200f, max: 200f, signed: true, flag: true, isRounded: true);
                else
                    value = null;
            }
            else if (s.State == StreamState.Write)
            {
                var w = (s.Writer as BitWriter);
                w.Write(value.HasValue);
                if (value.HasValue)
                {
                    w.WriteEncodedFloat(value.Value, n: 16, min: -200f, max: 200f, signed: true, flag: true, isRounded: true);
                }
            }
        }

        protected static void AddBoolTrait(BitStream s, ref InheritableToggle value)
        {
            var temp = (byte)value;
            s.Stream(ref temp, 2);
            value = (InheritableToggle)temp;
        }
    }
}
