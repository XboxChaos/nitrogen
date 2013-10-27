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

namespace Nitrogen.Blob.Transport.BinaryTemplates.Halo4.Shared
{
    /// <summary>
    /// Defines the structure of a Halo 4 trait set.
    /// </summary>
    internal class Halo4TraitSet
        : DataTemplate
    {
        protected override void Define()
        {
            Group("Traits", () =>
            {
                // Armor
                AddModifier("DamageResistance");
                AddModifier("ShieldMultiplier");
                AddModifier("HealthMultiplier");
                AddModifier("ShieldStunDuration");
                AddModifier("ShieldRechargeRate");
                AddModifier("HealthRechargeRate");
                AddModifier("OvershieldDecayRate");
                AddModifier("ShieldVampirismPercentage");
                AddModifier("ExplosiveDamageResistance");
                AddModifier("VehicleStunDuration");
                AddModifier("VehicleRechargeDuration");
                AddModifier("VehicleEMPDisableDuration");
                AddModifier("FallDamageMultiplier");
                AddBoolTrait("HeadshotImmunity");
                AddBoolTrait("AssassinationImmunity");
                AddBoolTrait("Invincibility");
                AddBoolTrait("FastTrack");
                AddBoolTrait("CancelCurrentPowerup");

                // Weapon and Equipment
                AddModifier("DamageMultiplier");
                AddModifier("MeleeDamageMultiplier");
                AddModifier("FragGrenadeRegenDuration");
                AddModifier("PlasmaGrenadeRegenDuration");
                AddModifier("PulseGrenadeRegenDuration");
                AddModifier("SprintEnergyUseRate");
                AddModifier("SprintEnergyRechargeDelay");
                AddModifier("SprintEnergyRechargeRate");
                AddModifier("SprintInitialEnergy");
                AddModifier("ArmorAbilityEnergyUseRate");
                AddModifier("ArmorAbilityEnergyRechargeDelay");
                AddModifier("ArmorAbilityEnergyRechargeRate");
                AddModifier("ArmorAbilityInitialEnergy");
                AddModifier("SwitchSpeedMultiplier");
                AddModifier("ReloadSpeedMultiplier");
                AddModifier("OrdnancePointsMultiplier");
                AddModifier("ExplosionRadiusMultiplier");
                AddModifier("TurretCooldownRate");
                AddModifier("FlinchRate");
                AddModifier("DropReconWarningDuration");
                AddModifier("DropReconDistanceModifier");
                AddModifier("AssassinationSpeedMultiplier");
                AddBoolTrait("WeaponPickup");
                Register<byte>("InitialGrenadeCount", n: 5);
                Register<byte>("InfiniteAmmo", n: 2);
                AddBoolTrait("ArmorAbilityUsage");
                AddBoolTrait("ArmorAbilityUsageExceptAutoturret");
                AddBoolTrait("DropCurrentArmorAbility");
                AddBoolTrait("InfiniteArmorAbilityUsage");
                AddBoolTrait("ExtraLoadoutAndOrdnanceAmmo");
                AddBoolTrait("ExtraGrenadeCarryLimit");
                AddBoolTrait("LastGasp");
                AddBoolTrait("OrdnanceNavpointsVisible");
                AddBoolTrait("Requisition");
                AddBoolTrait("GrenadePickup");
                AddBoolTrait("FirepowerUpgradeEnabled");
                AddBoolTrait("PersonalOrdnance");
                Register<sbyte>("InitialPrimaryWeapon");
                Register<sbyte>("InitialSecondaryWeapon");
                Register<sbyte>("InitialArmorAbility");
                Register<sbyte>("InitialTacticalPackage");
                Register<sbyte>("InitialSupportUpgrade");

                // Movement
                AddModifier("PlayerSpeedMultiplier");
                AddModifier("GravityMultiplier");
                AddModifier("JumpHeightMultiplier");
                AddModifier("ControllerSensitivityMultiplier");
                Register<byte>("VehicleUsage", n: 4);
                AddBoolTrait("DoubleJump");
                AddBoolTrait("Sprint");
                AddBoolTrait("AutomaticMomentumUsage");
                AddBoolTrait("Vaulting");
                AddBoolTrait("Stealth");

                // Appearance
                AddModifier("PlayerScale");
                Register<byte>("ActiveCamo", n: 3);
                Register<byte>("WaypointVisibility", n: 2);
                Register<byte>("GamertagVisibility", n: 2);
                Register<byte>("Aura", n: 3);
                Group("PrimaryColor", () =>
                {
                    Register<bool>("UseDefault");
                    Register<byte>("Red");
                    Register<byte>("Green");
                    Register<byte>("Blue");
                });
                Group("SecondaryColor", () =>
                {
                    Register<bool>("UseDefault");
                    Register<byte>("Red");
                    Register<byte>("Green");
                    Register<byte>("Blue");
                });
                Group("ModelVariant", () =>
                {
                    Register<bool>("UseDefault");
                    Register<sbyte>("VariantNameStringIndex");
                });
                Register<int>("DeathEffect");
                Register<int>("LoopingEffect");

                // HUD
                AddBoolTrait("ShieldHUDVisible");
                AddModifier("NemesisDuration");
                AddModifier("MotionTrackerRange");
                Register<byte>("MotionTrackerMode", n: 3);
                AddBoolTrait("MotionTrackerWhileScoped");
                AddBoolTrait("DirectionalDamageIndicator");
                Register<byte>("VisionMode", n: 2);
                AddBoolTrait("BattleAwareness");
                AddBoolTrait("ThreatView");
                AddBoolTrait("AuralEnhancement");
                AddBoolTrait("Nemesis");
            });
        }

        private void AddModifier(string name)
        {
            if (Mode == SerializationMode.Deserialize)
            {
                float? value = null;

                bool isPresent = Convert.ToBoolean(Read(typeof(bool)));
                if (isPresent)
                    value = ReadEncodedFloat(n: 16, minValue: -200.0f, maxValue: 200.0f, isSigned: true, flag: true, roundFloat: true);

                SetValue(name, value);
            }
            else
            {
                if (IsRegistered(name))
                {
                    var value = GetValue<float?>(name);
                    Write(value.HasValue);
                    if (value.HasValue)
                    {
                        WriteEncodedFloat(value.Value, n: 16, minValue: -200.0f, maxValue: 200.0f, isSigned: true, flag: true, roundFloat: true);
                    }
                }
                else
                {
                    Write(false);
                }
            }
        }

        private void AddBoolTrait(string name)
        {
            byte encodedValue;
            bool? value;

            if (Mode == SerializationMode.Deserialize)
            {
                encodedValue = Convert.ToByte(Read(typeof(byte), n: 2));
                value = null;

                if (encodedValue == 1)
                    value = false;
                else if (encodedValue == 2)
                    value = true;

                SetValue(name, value);
            }
            else
            {
                encodedValue = 0;

                if (IsRegistered(name))
                {
                    value = GetValue<bool?>(name);
                    if (value.HasValue)
                        encodedValue = (byte)(value.Value ? 2 : 1);
                }

                Write(encodedValue, n: 2);
            }
        }
    }
}