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
            vehicleRechargeDuration,
            vehicleEMPDisableDuration,
            fallDamageMultiplier;

            private InheritableToggle
            headshotImmunity,
            assassinationImmunity,
            invincibility,
            fastTrack,
            cancelCurrentPowerup;

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
                AddModifier(s, ref this.vehicleRechargeDuration);
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
