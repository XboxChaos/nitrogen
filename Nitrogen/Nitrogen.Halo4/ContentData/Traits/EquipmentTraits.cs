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
        /// Represents the group of equipment-related traits in a Halo 4 player trait set.
        /// </summary>
        public class EquipmentTraits
        : ISerializable<BitStream>
        {
            private float?
                damageMultiplier,
                meleeDamageMultiplier,
                fragGrenadeRegenDuration,
                plasmaGrenadeRegenDuration,
                pulseGrenadeRegenDuration,
                sprintEnergyUseRate,
                sprintEnergyRechargeDelay,
                sprintEnergyRechargeRate,
                sprintInitialEnergy,
                aaEnergyUseRate,
                aaEnergyRechargeDelay,
                aaEnergyRechargeRate,
                aaInitialEnergy,
                switchSpeedMultiplier,
                reloadSpeedMultiplier,
                ordnancePointsMultiplier,
                explosionRadiusMultiplier,
                turretCooldownRate,
                flinchRate,
                dropReconDuration,
                dropReconDistance,
                assassinationSpeed;

            private byte infiniteAmmo;

            private sbyte
                initialGrenades,
                primaryWeapon,
                secondaryWeapon,
                armorAbility,
                tacticalPackage,
                supportUpgrade;

            private InheritableToggle
                weaponPickup,
                aaUsage,
                aaUsageExceptingAutoturret,
                dropCurrentAA,
                infiniteAAUsage,
                extraAmmo,
                extraGrenade,
                lastGasp,
                ordnanceNavpointsVisible,
                requisition,
                grenadePickup,
                firepowerEnabled,
                personalOrdnance;

            /// <summary>
            /// Gets or sets the initial primary weapon.
            /// </summary>
            public sbyte InitialPrimaryWeapon
            {
                get { return this.primaryWeapon; }
                set { this.primaryWeapon = value; }
            }

            /// <summary>
            /// Gets or sets the initial secondary weapon.
            /// </summary>
            public sbyte InitialSecondaryWeapon
            {
                get { return this.secondaryWeapon; }
                set { this.secondaryWeapon = value; }
            }

            /// <summary>
            /// Gets or sets the initial armor ability.
            /// </summary>
            public sbyte InitialArmorAbility
            {
                get { return this.armorAbility; }
                set { this.armorAbility = value; }
            }

            /// <summary>
            /// Gets or sets the initial tactical package.
            /// </summary>
            public sbyte TacticalPackage
            {
                get { return this.tacticalPackage; }
                set { this.tacticalPackage = value; }
            }

            /// <summary>
            /// Gets or sets the initial support upgrade.
            /// </summary>
            public sbyte SupportUpgrade
            {
                get { return this.supportUpgrade; }
                set { this.supportUpgrade = value; }
            }

            /// <summary>
            /// Gets or sets the initial grenade amount.
            /// </summary>
            public sbyte InitialGrenades
            {
                get { return this.initialGrenades; }
                set { this.initialGrenades = value; }
            }

            /// <summary>
            /// Gets or sets a player's damage rate.
            /// </summary>
            public float? DamageMultiplier
            {
                get { return this.damageMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.damageMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's melee damage rate.
            /// </summary>
            /// <remarks>
            /// This value stacks with <see cref="DamageMultiplier"/>.
            /// </remarks>
            public float? MeleeDamageMultiplier
            {
                get { return this.meleeDamageMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.meleeDamageMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets the recurring duration in seconds before a player obtains a Frag Grenade.
            /// </summary>
            public float? FragGrenadeRegen
            {
                get { return this.fragGrenadeRegenDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.fragGrenadeRegenDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets the recurring duration in seconds before a player obtains a Plasma Grenade.
            /// </summary>
            public float? PlasmaGrenadeRegen
            {
                get { return this.plasmaGrenadeRegenDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.plasmaGrenadeRegenDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets the recurring duration in seconds before a player obtains a Pulse Grenade.
            /// </summary>
            public float? PulseGrenadeRegen
            {
                get { return this.pulseGrenadeRegenDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.pulseGrenadeRegenDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets the sprint stamina depletion rate for a player.
            /// </summary>
            public float? SprintStaminaDepletionRate
            {
                get { return this.sprintEnergyUseRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.sprintEnergyUseRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the sprint stamina restoration delay in seconds. 
            /// </summary>
            public float? SprintStaminaRestoreDelay
            {
                get { return this.sprintEnergyRechargeDelay; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.sprintEnergyRechargeDelay = value;
                }
            }

            /// <summary>
            /// Gets or sets the sprint stamina restoration rate.
            /// </summary>
            public float? SprintStaminaRestoreRate
            {
                get { return this.sprintEnergyRechargeRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.sprintEnergyRechargeRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the initial sprint stamina for a player.
            /// </summary>
            public float? SprintInitialStamina
            {
                get { return this.sprintInitialEnergy; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.sprintInitialEnergy = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's armor ability energy depletion rate.
            /// </summary>
            public float? ArmorAbilityEnergyDepletionRate
            {
                get { return this.aaEnergyUseRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.aaEnergyUseRate = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's armor ability energy recharge delay in seconds.
            /// </summary>
            public float? ArmorAbilityEnergyRechargeDelay
            {
                get { return this.aaEnergyRechargeDelay; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.aaEnergyRechargeDelay = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's armor ability energy recharge rate.
            /// </summary>
            public float? ArmorAbilityEnergyRechargeRate
            {
                get { return this.aaEnergyRechargeRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.aaEnergyRechargeRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the initial amount of energy a player's armor ability contains.
            /// </summary>
            public float? ArmorAbilityInitialEnergy
            {
                get { return this.aaInitialEnergy; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.aaInitialEnergy = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which a player switches weapons.
            /// </summary>
            public float? WeaponSwitchSpeed
            {
                get { return this.switchSpeedMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.switchSpeedMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which a player reloads their weapon.
            /// </summary>
            public float? WeaponReloadSpeed
            {
                get { return this.reloadSpeedMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.reloadSpeedMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets the modifier for a player's personal ordnance points.
            /// </summary>
            public float? OrdnancePointsModifier
            {
                get { return this.ordnancePointsMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.ordnancePointsMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets the effective range of an explosion triggered by a player.
            /// </summary>
            public float? ExplosionRadius
            {
                get { return this.explosionRadiusMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.explosionRadiusMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which a chaingun turret cools down.
            /// </summary>
            public float? TurretCooldownRate
            {
                get { return this.turretCooldownRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.turretCooldownRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate of a player's flinch when they get hit by a projectile.
            /// </summary>
            public float? FlinchRate
            {
                get { return this.flinchRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.flinchRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the effective range when <see cref="OrdnanceWarning"/> is enabled.
            /// </summary>
            public float? OrdnanceWarningDistance
            {
                get { return this.dropReconDistance; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.dropReconDistance = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which a player performs an assassination on another player.
            /// </summary>
            public float? AssassinationSpeed
            {
                get { return this.assassinationSpeed; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    this.assassinationSpeed = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player has infinite ammo.
            /// </summary>
            public InfiniteAmmoMode InfiniteAmmo
            {
                get { return (InfiniteAmmoMode)this.infiniteAmmo; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InfiniteAmmoMode), value));
                    this.infiniteAmmo = (byte)value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is able to pick up weapons on the ground.
            /// </summary>
            public InheritableToggle WeaponPickup
            {
                get { return this.weaponPickup; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.weaponPickup = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is able to use their armor ability.
            /// </summary>
            public InheritableToggle ArmorAbilityUsage
            {
                get { return this.aaUsage; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.aaUsage = value;
                }
            }

            /// <summary>
            /// Gets or sets whether the <see cref="ArmorAbilityUsage"/> value does not apply to an Autoturret.
            /// </summary>
            public InheritableToggle ArmorAbilityUsageExceptingAutoturret
            {
                get { return this.aaUsageExceptingAutoturret; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.aaUsageExceptingAutoturret = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player will lose their currently equipped armor ability when this
            /// trait is applied.
            /// </summary>
            public InheritableToggle DropCurrentArmorAbility
            {
                get { return this.dropCurrentAA; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.dropCurrentAA = value;
                }
            }
            
            /// <summary>
            /// Gets or sets whether a player's armor ability never depletes.
            /// </summary>
            public InheritableToggle InfiniteArmorAbilityUsage
            {
                get { return this.infiniteAAUsage; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.infiniteAAUsage = value;
                }
            }

            /// <summary>
            /// Gets or sets whether the weapons a player spawns with and ordnance has increased ammo.
            /// </summary>
            public InheritableToggle ExtraAmmo
            {
                get { return this.extraAmmo; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.extraAmmo = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player spawns with an extra grenade.
            /// </summary>
            public InheritableToggle ExtraGrenade
            {
                get { return this.extraGrenade; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.extraGrenade = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player drops a live grenade upon death.
            /// </summary>
            public InheritableToggle LastGasp
            {
                get { return this.lastGasp; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.lastGasp = value;
                }
            }

            /// <summary>
            /// Gets or sets whether ordnance waypoints are visible to a player.
            /// </summary>
            public InheritableToggle OrdnanceWaypoints
            {
                get { return this.ordnanceNavpointsVisible; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.ordnanceNavpointsVisible = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is able to reroll their personal ordnance choices.
            /// </summary>
            public InheritableToggle Requisition
            {
                get { return this.requisition; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.requisition = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is able to pick up grenades from dead players.
            /// </summary>
            public InheritableToggle GrenadePickup
            {
                get { return this.grenadePickup; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.grenadePickup = value;
                }
            }

            /// <summary>
            /// Gets or sets whether personal ordnance is enabled for a player.
            /// </summary>
            public InheritableToggle PersonalOrdnance
            {
                get { return this.personalOrdnance; }
                set
                {
                    Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(InheritableToggle), value));
                    this.personalOrdnance = value;
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="EquipmentTraits"/> with default values.
            /// </summary>
            public EquipmentTraits()
            {
                this.primaryWeapon = this.secondaryWeapon = this.armorAbility = this.supportUpgrade = this.tacticalPackage = -3;
            }

            #region ISerializable<BitStream> Members

            public void Serialize(BitStream s)
            {
                AddModifier(s, ref this.damageMultiplier);
                AddModifier(s, ref this.meleeDamageMultiplier);
                AddModifier(s, ref this.fragGrenadeRegenDuration);
                AddModifier(s, ref this.plasmaGrenadeRegenDuration);
                AddModifier(s, ref this.pulseGrenadeRegenDuration);
                AddModifier(s, ref this.sprintEnergyUseRate);
                AddModifier(s, ref this.sprintEnergyRechargeDelay);
                AddModifier(s, ref this.sprintEnergyRechargeRate);
                AddModifier(s, ref this.sprintInitialEnergy);
                AddModifier(s, ref this.aaEnergyUseRate);
                AddModifier(s, ref this.aaEnergyRechargeDelay);
                AddModifier(s, ref this.aaEnergyRechargeRate);
                AddModifier(s, ref this.aaInitialEnergy);
                AddModifier(s, ref this.switchSpeedMultiplier);
                AddModifier(s, ref this.reloadSpeedMultiplier);
                AddModifier(s, ref this.ordnancePointsMultiplier);
                AddModifier(s, ref this.explosionRadiusMultiplier);
                AddModifier(s, ref this.turretCooldownRate);
                AddModifier(s, ref this.flinchRate);
                AddModifier(s, ref this.dropReconDuration);
                AddModifier(s, ref this.dropReconDistance);
                AddModifier(s, ref this.assassinationSpeed);
                AddBoolTrait(s, ref this.weaponPickup);
                s.Stream(ref this.initialGrenades, 5);
                s.Stream(ref this.infiniteAmmo, 2);
                AddBoolTrait(s, ref this.aaUsage);
                AddBoolTrait(s, ref this.aaUsageExceptingAutoturret);
                AddBoolTrait(s, ref this.dropCurrentAA);
                AddBoolTrait(s, ref this.infiniteAAUsage);
                AddBoolTrait(s, ref this.extraAmmo);
                AddBoolTrait(s, ref this.extraGrenade);
                AddBoolTrait(s, ref this.lastGasp);
                AddBoolTrait(s, ref this.ordnanceNavpointsVisible);
                AddBoolTrait(s, ref this.requisition);
                AddBoolTrait(s, ref this.grenadePickup);
                AddBoolTrait(s, ref this.firepowerEnabled);
                AddBoolTrait(s, ref this.personalOrdnance);
                s.Stream(ref this.primaryWeapon);
                s.Stream(ref this.secondaryWeapon);
                s.Stream(ref this.armorAbility);
                s.Stream(ref this.tacticalPackage);
                s.Stream(ref this.supportUpgrade);
            }

            #endregion
        }
    }
}
