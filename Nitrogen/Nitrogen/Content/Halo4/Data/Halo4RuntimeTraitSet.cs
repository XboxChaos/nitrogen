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

using Nitrogen.Content.Halo4.Enums;
using Nitrogen.Content.Shared.Enums;
using System;
using System.Drawing;

namespace Nitrogen.Content.Halo4.Data
{
    /// <summary>
    /// Represents a set of traits which will be applied to a player at runtime. Initial weapons,
    /// armor ability, grenades, tactical package, and support upgrade options are not available.
    /// </summary>
    [Synchronizable]
    public class Halo4RuntimeTraitSet
    {
        public Halo4RuntimeTraitSet()
        {
            DeathEffect = Halo4ArmorEffect.MapDefault;
            LoopingEffect = Halo4ArmorEffect.MapDefault;
            PrimaryColor = null;
            SecondaryColor = null;
        }

        [PropertyBinding("DamageResistance")]
        public float? DamageResistance { get; set; }

        [PropertyBinding("ShieldMultiplier")]
        public float? ShieldMultiplier { get; set; }

        [PropertyBinding("ShieldMultiplier")]
        public float? HealthMultiplier { get; set; }

        [PropertyBinding("ShieldStunDuration")]
        public float? ShieldStunDuration { get; set; }

        [PropertyBinding("ShieldRechargeRate")]
        public float? ShieldRechargeRate { get; set; }

        [PropertyBinding("HealthRechargeRate")]
        public float? HealthRechargeRate { get; set; }

        [PropertyBinding("OvershieldDecayRate")]
        public float? OvershieldDecayRate { get; set; }

        [PropertyBinding("ShieldVampirismPercentage")]
        public float? ShieldVampirismPercentage { get; set; }

        [PropertyBinding("ExplosiveDamageResistance")]
        public float? ExplosiveDamageResistance { get; set; }

        [PropertyBinding("VehicleStunDuration")]
        public float? VehicleStunDuration { get; set; }

        [PropertyBinding("VehicleRechargeDuration")]
        public float? VehicleRechargeDuration { get; set; }

        [PropertyBinding("VehicleEMPDisableDuration")]
        public float? VehicleEMPDisableDuration { get; set; }

        [PropertyBinding("FallDamageMultiplier")]
        public float? FallDamageMultiplier { get; set; }

        [PropertyBinding("HeadshotImmunity")]
        public bool? HeadshotImmunity { get; set; }

        [PropertyBinding("AssassinationImmunity")]
        public bool? AssassinationImmunity { get; set; }

        [PropertyBinding("Invincibility")]
        public bool? Invincibility { get; set; }

        [PropertyBinding("CancelCurrentPowerup")]
        public bool? CancelCurrentPowerup { get; set; }

        [PropertyBinding("DamageMultiplier")]
        public float? DamageMultiplier { get; set; }

        [PropertyBinding("MeleeDamageMultiplier")]
        public float? MeleeDamageMultiplier { get; set; }

        [PropertyBinding("FragGrenadeRegenDuration")]
        public float? FragGrenadeRegenDuration { get; set; }

        [PropertyBinding("PlasmaGrenadeRegenDuration")]
        public float? PlasmaGrenadeRegenDuration { get; set; }

        [PropertyBinding("PulseGrenadeRegenDuration")]
        public float? PulseGrenadeRegenDuration { get; set; }

        [PropertyBinding("ArmorAbilityEnergyUseRate")]
        public float? ArmorAbilityEnergyUseRate { get; set; }

        [PropertyBinding("ArmorAbilityEnergyRechargeDelay")]
        public float? ArmorAbilityEnergyRechargeDelay { get; set; }

        [PropertyBinding("ArmorAbilityEnergyRechargeRate")]
        public float? ArmorAbilityEnergyRechargeRate { get; set; }

        [PropertyBinding("ArmorAbilityInitialEnergy")]
        public float? ArmorAbilityInitialEnergy { get; set; }

        [PropertyBinding("OrdnancePointsMultiplier")]
        public float? OrdnancePointsMultiplier { get; set; }

        [PropertyBinding("ExplosionRadiusMultiplier")]
        public float? ExplosionRadius { get; set; }

        [PropertyBinding("TurretCooldownRate")]
        public float? TurretCooldownRate { get; set; }

        [PropertyBinding("FlinchRate")]
        public float? FlinchRate { get; set; }

        [PropertyBinding("WeaponPickup")]
        public bool? WeaponPickup { get; set; }

        [PropertyBinding("InfiniteAmmo")]
        public InfiniteAmmoMode InfiniteAmmo { get; set; }

        [PropertyBinding("ArmorAbilityUsage")]
        public bool? ArmorAbilityUsage { get; set; }

        [PropertyBinding("DropCurrentArmorAbility")]
        public bool? DropCurrentArmorAbility { get; set; }

        [PropertyBinding("InfiniteArmorAbilityUsage")]
        public bool? InfiniteArmorAbilityUsage { get; set; }

        [PropertyBinding("ExtraLoadoutAndOrdnanceAmmo")]
        public bool? ExtraAmmo { get; set; }

        [PropertyBinding("ExtraGrenadeCarryLimit")]
        public bool? ExtraGrenade { get; set; }

        [PropertyBinding("LastGasp")]
        public bool? LastGasp { get; set; }

        [PropertyBinding("SprintEnergyUseRate")]
        public float? SprintEnergyUseRate { get; set; }

        [PropertyBinding("SprintEnergyRechargeDelay")]
        public float? SprintEnergyRechargeDelay { get; set; }

        [PropertyBinding("SprintEnergyRechargeRate")]
        public float? SprintEnergyRechargeRate { get; set; }

        [PropertyBinding("SprintInitialEnergy")]
        public float? SprintInitialEnergy { get; set; }

        [PropertyBinding("AssassinationSpeedMultiplier")]
        public float? AssassinationSpeed { get; set; }

        [PropertyBinding("SwitchSpeedMultiplier")]
        public float? SwitchSpeed { get; set; }

        [PropertyBinding("ReloadSpeedMultiplier")]
        public float? ReloadSpeed { get; set; }

        [PropertyBinding("DropReconWarningDuration")]
        public float? DropReconDuration { get; set; }

        [PropertyBinding("DropReconDistanceModifier")]
        public float? DropReconDistance { get; set; }

        [PropertyBinding("OrdnanceNavpointsVisible")]
        public bool? OrdnanceWaypointsVisible { get; set; }

        [PropertyBinding("Requisition")]
        public bool? Requisition { get; set; }

        [PropertyBinding("GrenadePickup")]
        public bool? GrenadePickup { get; set; }

        [PropertyBinding("PersonalOrdnance")]
        public bool? PersonalOrdnanceEnabled { get; set; }

        [PropertyBinding("PlayerSpeedMultiplier")]
        public float? MovementSpeed { get; set; }

        [PropertyBinding("GravityMultiplier")]
        public float? Gravity { get; set; }

        [PropertyBinding("JumpHeightMultiplier")]
        public float? JumpHeight { get; set; }

        [PropertyBinding("ControllerSensitivityMultiplier")]
        public float? ControllerSensitivity { get; set; }

        [PropertyBinding("VehicleUsage")]
        public VehicleUsageMode VehicleUsage { get; set; }

        [PropertyBinding("Sprint")]
        public bool? Sprint { get; set; }

        [PropertyBinding("Stealth")]
        public float? Stealth { get; set; }

        [PropertyBinding("PlayerScale")]
        public float? Scale { get; set; }

        [PropertyBinding("ActiveCamo")]
        public ActiveCamoMode ActiveCamo { get; set; }

        [PropertyBinding("WaypointVisibility")]
        public HudVisibility WaypointVisibility { get; set; }

        [PropertyBinding("GamertagVisibility")]
        public HudVisibility GamertagVisibility { get; set; }

        [PropertyBinding("DeathEffect")]
        public Halo4ArmorEffect DeathEffect { get; set; }

        [PropertyBinding("LoopingEffect")]
        public Halo4ArmorEffect LoopingEffect { get; set; }

        [PropertyBinding("ShieldHUDVisible")]
        public bool? ShieldBarVisible { get; set; }

        [PropertyBinding("NemesisDuration")]
        public float? NemesisDuration { get; set; }

        [PropertyBinding("MotionTrackerRange")]
        public float? MotionSensorRange { get; set; }

        [PropertyBinding("MotionTrackerMode")]
        public MotionSensorMode MotionSensorMode { get; set; }

        [PropertyBinding("MotionTrackerWhileScoped")]
        public bool? MotionSensorWhileScoped { get; set; }

        [PropertyBinding("DirectionalDamageIndicator")]
        public bool? DirectionalDamageIndicator { get; set; }

        [PropertyBinding("BattleAwareness")]
        public bool? BattleAwareness { get; set; }

        [PropertyBinding("AuralEnhancement")]
        public bool? AuralEnhancement { get; set; }

        [PropertyBinding("Nemesis")]
        public bool? Nemesis { get; set; }

        /// <summary>
        /// Gets or sets the primary armor color. A value of null indicates that the color should be
        /// inherited.
        /// </summary>
        public Color? PrimaryColor
        {
            get
            {
                return Color.FromArgb(PrimaryColorRed, PrimaryColorGreen, PrimaryColorBlue);
            }

            set
            {
                if (value == null)
                {
                    PrimaryColorRed = 255;
                    PrimaryColorGreen = 255;
                    PrimaryColorBlue = 255;
                    UseDefaultPrimaryColor = true;
                }
                else
                {
                    Color color = value.Value;
                    PrimaryColorRed = color.R;
                    PrimaryColorGreen = color.G;
                    PrimaryColorBlue = color.B;
                    UseDefaultPrimaryColor = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the secondary armor color. A value of null indicates that the color should
        /// be inherited.
        /// </summary>
        public Color? SecondaryColor
        {
            get
            {
                return Color.FromArgb(SecondaryColorRed, SecondaryColorGreen, SecondaryColorBlue);
            }

            set
            {
                if (value == null)
                {
                    SecondaryColorRed = 255;
                    SecondaryColorGreen = 255;
                    SecondaryColorBlue = 255;
                    UseDefaultSecondaryColor = true;
                }
                else
                {
                    Color color = value.Value;
                    SecondaryColorRed = color.R;
                    SecondaryColorGreen = color.G;
                    SecondaryColorBlue = color.B;
                    UseDefaultSecondaryColor = false;
                }
            }
        }

        [PropertyBinding("PrimaryColor/UseDefault")]
        private bool UseDefaultPrimaryColor { get; set; }

        [PropertyBinding("PrimaryColor/Red")]
        private byte PrimaryColorRed { get; set; }

        [PropertyBinding("PrimaryColor/Green")]
        private byte PrimaryColorGreen { get; set; }

        [PropertyBinding("PrimaryColor/Blue")]
        private byte PrimaryColorBlue { get; set; }

        [PropertyBinding("SecondaryColor/UseDefault")]
        private bool UseDefaultSecondaryColor { get; set; }

        [PropertyBinding("SecondaryColor/Red")]
        private byte SecondaryColorRed { get; set; }

        [PropertyBinding("SecondaryColor/Green")]
        private byte SecondaryColorGreen { get; set; }

        [PropertyBinding("SecondaryColor/Blue")]
        private byte SecondaryColorBlue { get; set; }

        [PropertyBinding("FastTrack")]
        private bool? FastTrack { get; set; }

        [PropertyBinding("ArmorAbilityUsageExceptAutoturret")]
        private bool? ArmorAbilityUsageExcludingAutoTurret { get; set; }

        [PropertyBinding("FirepowerUpgradeEnabled")]
        private bool? FirepowerUpgradeEnabled { get; set; }

        [PropertyBinding("DoubleJump")]
        private bool? DoubleJump { get; set; }

        [PropertyBinding("AutomaticMomentumUsage")]
        private bool? AutomaticMomentumUsage { get; set; }

        [PropertyBinding("Vaulting")]
        private bool? Vaulting { get; set; }

        [PropertyBinding("Aura")]
        private byte Aura { get; set; }

        [PropertyBinding("VisionMode")]
        private byte VisionMode { get; set; }

        [PropertyBinding("ThreatView")]
        private byte ThreatView { get; set; }
    }
}
