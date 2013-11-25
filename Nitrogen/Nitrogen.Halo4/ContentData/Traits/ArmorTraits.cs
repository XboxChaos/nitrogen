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
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Nitrogen.Halo4.ContentData.Traits
{
    public partial class Halo4PlayerTraits
    {
        /// <summary>
        /// Represents the group of armor-related traits in a Halo 4 player trait set.
        /// </summary>
        public class ArmorTraits
        : ISerializable<BitStream>
        {
            private float?
            damageResistance,
            shieldMultiplier,
            healthMultiplier,
            shieldStunDuration,
            shieldRechargeRate,
            healthRechargeRate,
            overshieldDecayRate,
            shieldVampirismPercentage,
            explosiveDamageResistance,
            vehicleStunDuration,
            vehicleRechargeRate,
            vehicleEMPDisableDuration,
            fallDamageMultiplier;

            private InheritableToggle
            headshotImmunity,
            assassinationImmunity,
            invincibility,
            fastTrack,
            cancelCurrentPowerup;

            /// <summary>
            /// Gets or sets a player's resistance to various forms of damage.
            /// </summary>
            public float? DamageResistance
            {
                get { return this.damageResistance; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.damageResistance = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's shield amount (by percentage).
            /// </summary>
            public float? ShieldAmount
            {
                get { return this.shieldMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.shieldMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's health amount (by percentage).
            /// </summary>
            public float? Health
            {
                get { return this.healthMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.healthMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets the duration in seconds before a player's shield begins recharging.
            /// </summary>
            public float? ShieldRechargeDelay
            {
                get { return this.shieldStunDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.shieldStunDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which a player's shield recharges.
            /// </summary>
            public float? ShieldRechargeRate
            {
                get { return this.shieldRechargeRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.shieldRechargeRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which a player's health is restored.
            /// </summary>
            public float? HealthRestoreRate
            {
                get { return this.healthRechargeRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.healthRechargeRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which the Overshield recharges. Use a negative value to set it to decay.
            /// </summary>
            public float? OvershieldRechargeRate
            {
                get { return this.overshieldDecayRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.overshieldDecayRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the amount of shields to steal from another player when inflicting damage.
            /// </summary>
            public float? ShieldVampirismPercentage
            {
                get { return this.shieldVampirismPercentage; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.shieldVampirismPercentage = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's resistance to explosive damage (e.g. grenades).
            /// </summary>
            public float? ExplosiveDamageResistance
            {
                get { return this.explosiveDamageResistance; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.explosiveDamageResistance = value;
                }
            }

            /// <summary>
            /// Gets or sets the duration in seconds before the shield of a player's vehicle begins recharging.
            /// </summary>
            public float? VehicleShieldRechargeDelay
            {
                get { return this.vehicleStunDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.vehicleStunDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which the shield of a player's vehicle recharges.
            /// </summary>
            public float? VehicleShieldRechargeRate
            {
                get { return this.vehicleRechargeRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.vehicleRechargeRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the duration in seconds a player's vehicle is disabled after taking an EMP hit.
            /// </summary>
            public float? VehicleEMPDisableDuration
            {
                get { return this.vehicleEMPDisableDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.vehicleEMPDisableDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's fall damage percentage.
            /// </summary>
            public float? FallDamage
            {
                get { return this.fallDamageMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.fallDamageMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is immune to headshots.
            /// </summary>
            public InheritableToggle HeadshotImmunity
            {
                get { return this.headshotImmunity; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.headshotImmunity = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is immune to assassinations.
            /// </summary>
            public InheritableToggle AssassinationImmunity
            {
                get { return this.assassinationImmunity; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.assassinationImmunity = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is not able to die from losing all health.
            /// </summary>
            public InheritableToggle Invincibility
            {
                get { return this.invincibility; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.invincibility = value;
                }
            }

            /// <summary>
            /// Gets or sets whether more than one powerup can be active at the same time.
            /// </summary>
            public InheritableToggle PowerupStacking
            {
                get { return this.cancelCurrentPowerup; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.cancelCurrentPowerup = value;
                }
            }

            #region ISerializable<BitStream> Members

            public void Serialize(BitStream s)
            {
                AddModifier(s, ref this.damageResistance);
                AddModifier(s, ref this.shieldMultiplier);
                AddModifier(s, ref this.healthMultiplier);
                AddModifier(s, ref this.shieldStunDuration);
                AddModifier(s, ref this.shieldRechargeRate);
                AddModifier(s, ref this.healthRechargeRate);
                AddModifier(s, ref this.overshieldDecayRate);
                AddModifier(s, ref this.shieldVampirismPercentage);
                AddModifier(s, ref this.explosiveDamageResistance);
                AddModifier(s, ref this.vehicleStunDuration);
                AddModifier(s, ref this.vehicleRechargeRate);
                AddModifier(s, ref this.vehicleEMPDisableDuration);
                AddModifier(s, ref this.fallDamageMultiplier);
                AddBoolTrait(s, ref this.headshotImmunity);
                AddBoolTrait(s, ref this.assassinationImmunity);
                AddBoolTrait(s, ref this.invincibility);
                AddBoolTrait(s, ref this.fastTrack);
                AddBoolTrait(s, ref this.cancelCurrentPowerup);
            }

            #endregion
        }
    }
}
