using Nitrogen.Enums;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.Shared
{
    /// <summary>
    /// Extends the <see cref="PlayerTraits"/> class with various convenience methods.
    /// </summary>
    [ContractVerification(true)]
    public static class PlayerTraitsExtensions
    {
        /// <summary>
        /// Applies the properties of the Shielding tactical package to this trait set.
        /// </summary>
        public static void ApplyShieldingUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
            traits.Armor.ShieldRechargeDelay = 6.0f;
            traits.Armor.ShieldRechargeRate = 2.0f;
        }

        /// <summary>
        /// Applies the properties of the Mobility tactical package to this trait set.
        /// </summary>
        public static void ApplyMobilityUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
            traits.Equipment.SprintStaminaDepletionRate = 0f;
            traits.Equipment.SprintStaminaRestoreDelay = 0.66f;
            traits.Equipment.SprintStaminaRestoreRate = 1.35f;
        }

        /// <summary>
        /// Applies the properties of the Grenadier tactical package to this trait set.
        /// </summary>
        public static void ApplyGrenadierUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
			traits.Equipment.ExtraGrenade = TraitFlag.Enabled;
        }
        /// <summary>
        /// Applies the properties of the Resupply tactical package to this trait set.
        /// </summary>

        public static void ApplyResupplyUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
			traits.Equipment.GrenadePickup = TraitFlag.Enabled;
        }

        /// <summary>
        /// Applies the properties of the Requisition tactical package to this trait set.
        /// </summary>
        public static void ApplyRequisitionUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
			traits.Equipment.Requisition = TraitFlag.Enabled;
        }

        /// <summary>
        /// Applies the properties of the Wheelman tactical package to this trait set.
        /// </summary>
        public static void ApplyWheelmanUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
            traits.Armor.VehicleShieldRechargeDelay = 1.50f;
            traits.Armor.VehicleShieldRechargeRate = 1.50f;
            traits.Armor.VehicleEMPDisableDuration = 0.50f;
        }

        /// <summary>
        /// Applies the properties of the A.A. Efficiency tactical package to this trait set.
        /// </summary>
        public static void ApplyAAEfficiencyUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
            traits.Equipment.ArmorAbilityEnergyRechargeDelay = 1.0f;
            traits.Equipment.ArmorAbilityEnergyRechargeRate = 2.0f;
            traits.Equipment.ArmorAbilityInitialEnergy = 1.0f;
        }

        /// <summary>
        /// Applies the properties of the Ammo support upgrade to this trait set.
        /// </summary>
        public static void ApplyAmmoUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
			traits.Equipment.ExtraAmmo = TraitFlag.Enabled;
        }

        /// <summary>
        /// Applies the properties of the Dexterity support upgrade to this trait set.
        /// </summary>
        public static void ApplyDexterityUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
            traits.Equipment.WeaponSwitchSpeed = 1.15f;
            traits.Equipment.WeaponReloadSpeed = 1.66f;
        }

        /// <summary>
        /// Applies the properties of the Sensor support upgrade to this trait set.
        /// </summary>
        public static void ApplySensorUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
            traits.ScreenAndAudio.MotionSensorRange = 60f;
        }

        /// <summary>
        /// Applies the properties of the Ordnance Priority support upgrade to this trait set.
        /// </summary>
        public static void ApplyOrdnancePriorityUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
            traits.Equipment.OrdnancePointsModifier = 1.40f;
        }

        /// <summary>
        /// Applies the properties of the Awareness support upgrade to this trait set.
        /// </summary>
        public static void ApplyAwarenessUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
			traits.ScreenAndAudio.MotionSensorWhileScoped = TraitFlag.Enabled;
        }

        /// <summary>
        /// Applies the properties of the Stability support upgrade to this trait set.
        /// </summary>
        public static void ApplyStabilityUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
            traits.Equipment.FlinchRate = 0.50f;
        }

        /// <summary>
        /// Applies the properties of the Explosives support upgrade to this trait set.
        /// </summary>
        public static void ApplyExplosivesUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
            traits.Armor.ExplosiveDamageResistance = 0.25f;
            traits.Equipment.ExplosionRadius = 1.25f;
        }

        /// <summary>
        /// Applies the properties of the Stealth support upgrade to this trait set.
        /// </summary>
        public static void ApplyStealthUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
			traits.Movement.Stealth = TraitFlag.Enabled;
            traits.Equipment.AssassinationSpeed = 1.20f;
        }

        /// <summary>
        /// Applies the properties of the Gunner support upgrade to this trait set.
        /// </summary>
        public static void ApplyGunnerUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
            traits.Equipment.TurretCooldownRate = 0.66f;
        }

        /// <summary>
        /// Applies the properties of the Drop Recon support upgrade to this trait set.
        /// </summary>
        public static void ApplyDropReconUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
            traits.Equipment.OrdnanceWarningDistance = 1.2f;
            traits.Equipment.OrdnanceWarningDuration = 5f;
        }

        /// <summary>
        /// Applies the properties of the Nemesis support upgrade to this trait set.
        /// </summary>
        public static void ApplyNemesisUpgrade(this PlayerTraits traits)
        {
            Contract.Requires<ArgumentNullException>(traits != null);
			traits.ScreenAndAudio.Nemesis = TraitFlag.Enabled;
            traits.ScreenAndAudio.NemesisDuration = 4.5f;
        }
    }
}
