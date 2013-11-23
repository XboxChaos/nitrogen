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

            private byte initialGrenades, infiniteAmmo;

            private sbyte
                primaryWeapon,
                secondaryWeapon,
                armorAbility,
                tacticalPackage,
                supportUpgrade;

            private InheritableBool
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
            /// Initializes a new instance of the <see cref="EquipmentTraits"/> with default values.
            /// </summary>
            public EquipmentTraits()
            {
                //this.primaryWeapon = this.secondaryWeapon = this.armorAbility = this.supportUpgrade = this.tacticalPackage = -3;
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
