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

using System;

namespace Nitrogen.Blob.Transport.BinaryTemplates.HaloReach.Shared
{
    /// <summary>
    /// Defines the structure of a Halo: Reach trait set.
    /// </summary>
    internal class HaloReachTraitSet
        : DataTemplate
    {
        protected override void Define()
        {
            // Armor
            Register<byte>("DamageResistance", n: 4);
            Register<byte>("HealthMultiplier", n: 3);
            Register<byte>("HealthRechargeRate", n: 4);
            Register<byte>("ShieldMultiplier", n: 3);
            Register<byte>("ShieldRechargeRate", n: 4);
            Register<byte>("ShieldStunDuration", n: 4);
            Register<byte>("HeadshotImmunity", n: 2);
            Register<byte>("ShieldVampirismPercentage", n: 3);
            Register<byte>("AssassinationImmunity", n: 2);
            Register<byte>(n: 2);

            // Weapon & Equipment
            Register<byte>("DamageMultiplier", n: 4);
            Register<byte>("MeleeDamageMultiplier", n: 4);
            Register<sbyte>("InitialPrimaryWeapon");
            Register<sbyte>("InitialSecondaryWeapon");
            Register<byte>("InitialGrenadeCount", n: 4);
            Register<byte>("InfiniteAmmo", n: 2);
            Register<byte>("GrenadeRegeneration", n: 2);
            Register<byte>("WeaponPickup", n: 2);
            Register<byte>("DropCurrentArmorAbility", n: 2);
            Register<byte>("InfiniteArmorAbilityUsage", n: 2);
            Register<byte>("ArmorAbilityUsage", n: 2);
            Register<sbyte>("InitialArmorAbility");

            // Movement
            Register<byte>("PlayerSpeedMultiplier", n: 5);
            Register<byte>("GravityMultiplier", n: 4);
            Register<byte>("VehicleUsage", n: 4);
            Register<byte>(n: 2);
            Register<byte>("JumpHeightMultiplier", n: 9);

            // Appearance
            Register<byte>("ActiveCamo", n: 3);
            Register<byte>("WaypointVisibility", n: 2);
            Register<byte>("GamertagVisibility", n: 2);
            Register<byte>("Aura", n: 3);
            Register<byte>("ForcedColor", n: 4);

            // HUD
            Register<byte>("MotionTrackerMode", n: 3);
            Register<byte>("MotionTrackerRange", n: 3);
            Register<byte>("DirectionalDamageIndicator", n: 2);
        }
    }
}
