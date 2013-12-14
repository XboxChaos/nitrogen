using Nitrogen.Utilities;
using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;
using Nitrogen.Data;

namespace Nitrogen.Shared
{
    /// <summary>
    /// Represents a set of traits in Halo 4 applied to a player's biped.
    /// </summary>
    public sealed partial class PlayerTraits
        : ISerializable<BitStream>        
    {
        private ArmorTraits _armorTraits;
        private EquipmentTraits _equipmentTraits;
        private MovementTraits _movementTraits;
        private AppearanceTraits _appearanceTraits;
        private ScreenAndAudioTraits _hudAudioTraits;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerTraits"/> class with default values.
        /// </summary>
        public PlayerTraits()
        {
            _armorTraits = new ArmorTraits();
            _equipmentTraits = new EquipmentTraits();
            _movementTraits = new MovementTraits();
            _appearanceTraits = new AppearanceTraits();
            _hudAudioTraits = new ScreenAndAudioTraits();
        }

        /// <summary>
        /// Gets or sets the set of traits related to a player's armor.
        /// </summary>
        public ArmorTraits Armor
        {
            get { return _armorTraits; }
            set { _armorTraits = value; }
        }

        /// <summary>
        /// Gets or sets the set of traits related to a player's equipment.
        /// </summary>
        public EquipmentTraits Equipment
        {
            get { return _equipmentTraits; }
            set { _equipmentTraits = value; }
        }

        /// <summary>
        /// Gets or sets the set of traits related to a player's movement.
        /// </summary>
        public MovementTraits Movement
        {
            get { return _movementTraits; }
            set { _movementTraits = value; }
        }

        /// <summary>
        /// Gets or sets the set of traits related to a player's outward appearance.
        /// </summary>
        public AppearanceTraits Appearance
        {
            get { return _appearanceTraits; }
            set { _appearanceTraits = value; }
        }

        /// <summary>
        /// Gets or sets the set of traits related to a player's HUD and audio.
        /// </summary>
        public ScreenAndAudioTraits ScreenAndAudio
        {
            get { return _hudAudioTraits; }
            set { _hudAudioTraits = value; }
        }

        private static void StreamModifier(BitStream s, ref float? value)
        {
            Contract.Requires<ArgumentNullException>(s != null);

            bool isPresent = value.HasValue;
            s.Stream(ref isPresent);

            if (isPresent)
            {
				float temp = value ?? 0;
                s.StreamEncodedFloat(ref temp, bits: 16, min: -200f, max: 200f, signed: true, flag: true, isRounded: true);
                value = temp;
            }
            else
            {
                value = null;
            }
        }

        private static void StreamFlag(BitStream s, ref TraitFlag flag)
        {
            Contract.Requires<ArgumentNullException>(s != null);

            byte value = (byte)flag;
            s.Stream(ref value, 2);
            flag = (TraitFlag)value;
        }

        void ISerializable<BitStream>.SerializeObject(BitStream s)
        {
            s.Serialize(_armorTraits);
            s.Serialize(_equipmentTraits);
            s.Serialize(_movementTraits);
            s.Serialize(_appearanceTraits);
            s.Serialize(_hudAudioTraits);
        }

        #region Armor

        /// <summary>
        /// Represents the group of armor-related traits in a Halo 4 player trait set.
        /// </summary>
        public class ArmorTraits
            : ISerializable<BitStream> 
        {
            private float?
                _damageResistance,                                                                                                       
                _shieldMultiplier,
                _healthMultiplier,
                _shieldStunDuration,
                _shieldRechargeRate,
                _healthRechargeRate,
                _overshieldDecayRate,
                _shieldVampirismPercentage,
                _explosiveDamageResistance,
                _vehicleStunDuration,
                _vehicleRechargeRate,
                _vehicleEMPDisableDuration,
                _fallDamageMultiplier;
                                          
            private TraitFlag
                _headshotImmunity,
                _assassinationImmunity,
                _invincibility,
                _fastTrack,
                _cancelCurrentPowerup;

            /// <summary>
            /// Gets or sets a player's resistance to various forms of damage.
            /// </summary>
            public float? DamageResistance
            {
                get { return _damageResistance; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _damageResistance = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's shield amount (by percentage).
            /// </summary>
            public float? ShieldAmount
            {
                get { return _shieldMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _shieldMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's health amount (by percentage).
            /// </summary>
            public float? Health
            {
                get { return _healthMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _healthMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets the duration in seconds before a player's shield begins recharging.
            /// </summary>
            public float? ShieldRechargeDelay
            {
                get { return _shieldStunDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _shieldStunDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which a player's shield recharges.
            /// </summary>
            public float? ShieldRechargeRate
            {
                get { return _shieldRechargeRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _shieldRechargeRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which a player's health is restored.
            /// </summary>
            public float? HealthRestoreRate
            {
                get { return _healthRechargeRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _healthRechargeRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which the Overshield recharges. Use a negative value to set it to decay.
            /// </summary>
            public float? OvershieldRechargeRate
            {
                get { return _overshieldDecayRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _overshieldDecayRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the amount of shields to steal from another player when inflicting damage.
            /// </summary>
            public float? ShieldVampirismPercentage
            {
                get { return _shieldVampirismPercentage; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _shieldVampirismPercentage = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's resistance to explosive damage (e.g. grenades).
            /// </summary>
            public float? ExplosiveDamageResistance
            {
                get { return _explosiveDamageResistance; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _explosiveDamageResistance = value;
                }
            }

            /// <summary>
            /// Gets or sets the duration in seconds before the shield of a player's vehicle begins recharging.
            /// </summary>
            public float? VehicleShieldRechargeDelay
            {
                get { return _vehicleStunDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _vehicleStunDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which the shield of a player's vehicle recharges.
            /// </summary>
            public float? VehicleShieldRechargeRate
            {
                get { return _vehicleRechargeRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _vehicleRechargeRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the duration in seconds a player's vehicle is disabled after taking an EMP hit.
            /// </summary>
            public float? VehicleEMPDisableDuration
            {
                get { return _vehicleEMPDisableDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _vehicleEMPDisableDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's fall damage percentage.
            /// </summary>
            public float? FallDamage
            {
                get { return _fallDamageMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _fallDamageMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is immune to headshots.
            /// </summary>
            public TraitFlag HeadshotImmunity
            {
                get { return _headshotImmunity; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _headshotImmunity = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is immune to assassinations.
            /// </summary>
            public TraitFlag AssassinationImmunity
            {
                get { return _assassinationImmunity; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _assassinationImmunity = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is not able to die from losing all health.
            /// </summary>
            public TraitFlag Invincibility
            {
                get { return _invincibility; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _invincibility = value;
                }
            }

            /// <summary>
            /// Gets or sets whether more than one powerup can be active at the same time.
            /// </summary>
            public TraitFlag NoPowerupStacking
            {
                get { return _cancelCurrentPowerup; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _cancelCurrentPowerup = value;
                }
            }

            void ISerializable<BitStream>.SerializeObject(BitStream s)
            {
                StreamModifier(s, ref _damageResistance);
                StreamModifier(s, ref _shieldMultiplier);
                StreamModifier(s, ref _healthMultiplier);
                StreamModifier(s, ref _shieldStunDuration);
                StreamModifier(s, ref _shieldRechargeRate);
                StreamModifier(s, ref _healthRechargeRate);
                StreamModifier(s, ref _overshieldDecayRate);
                StreamModifier(s, ref _shieldVampirismPercentage);
                StreamModifier(s, ref _explosiveDamageResistance);
                StreamModifier(s, ref _vehicleStunDuration);
                StreamModifier(s, ref _vehicleRechargeRate);
                StreamModifier(s, ref _vehicleEMPDisableDuration);
                StreamModifier(s, ref _fallDamageMultiplier);
                StreamFlag(s, ref _headshotImmunity);
                StreamFlag(s, ref _assassinationImmunity);
                StreamFlag(s, ref _invincibility);
                StreamFlag(s, ref _fastTrack);
                StreamFlag(s, ref _cancelCurrentPowerup);
            }
        }

        #endregion

        #region Equipment

        /// <summary>
        /// Represents the group of equipment-related traits in a Halo 4 player trait set.
        /// </summary>
        public class EquipmentTraits
            : ISerializable<BitStream>
        {
            private float?
                _damageMultiplier,
                _meleeDamageMultiplier,
                _fragGrenadeRegenDuration,
                _plasmaGrenadeRegenDuration,
                _pulseGrenadeRegenDuration,
                _sprintEnergyUseRate,
                _sprintEnergyRechargeDelay,
                _sprintEnergyRechargeRate,
                _sprintInitialEnergy,
                _aaEnergyUseRate,
                _aaEnergyRechargeDelay,
                _aaEnergyRechargeRate,
                _aaInitialEnergy,
                _switchSpeedMultiplier,
                _reloadSpeedMultiplier,
                _ordnancePointsMultiplier,
                _explosionRadiusMultiplier,
                _turretCooldownRate,
                _flinchRate,
                _dropReconDuration,
                _dropReconDistance,
                _assassinationSpeed;

            private byte _infiniteAmmo;

            private sbyte
                _initialGrenades,
                _primaryWeapon,
                _secondaryWeapon,
                _armorAbility,
                _tacticalPackage,
                _supportUpgrade;

            private TraitFlag
               _weaponPickup,
               _aaUsage,
               _aaUsageExceptingAutoturret,
               _dropAAOnDeath,
               _infiniteAAUsage,
               _extraAmmo,
               _extraGrenade,
               _lastGasp,
               _ordnanceNavpointsVisible,
               _requisition,
               _grenadePickup,
               _firepowerEnabled,
               _personalOrdnance;

			public EquipmentTraits()
			{
				InitialArmorAbility = ArmorAbility.Inherit;
				InitialGrenadeCount = GrenadeCount.Inherit;
				InitialPrimaryWeapon = Weapon.Inherit;
				InitialSecondaryWeapon = Weapon.Inherit;
				SupportUpgrade = ArmorMod.Inherit;
				TacticalPackage = ArmorMod.Inherit;
			}

            /// <summary>
            /// Gets or sets the initial primary weapon.
            /// </summary>
			public Weapon InitialPrimaryWeapon
			{
				get { return (Weapon) _primaryWeapon; }
				set { _primaryWeapon = (sbyte) value; ; }
			}

            /// <summary>
            /// Gets or sets the initial secondary weapon.
            /// </summary>
            public Weapon InitialSecondaryWeapon
            {
				get { return (Weapon) _secondaryWeapon; }
				set { _secondaryWeapon = (sbyte) value; }
            }

            /// <summary>
            /// Gets or sets the initial armor ability.
            /// </summary>
			public ArmorAbility InitialArmorAbility
			{
				get { return (ArmorAbility) _armorAbility; }
				set { _armorAbility = (sbyte) value; }
			}

            /// <summary>
            /// Gets or sets the initial tactical package.
            /// </summary>
			public ArmorMod TacticalPackage
			{
				get { return (ArmorMod) _tacticalPackage; }
				set { _tacticalPackage = (sbyte) value; }
			}

            /// <summary>
            /// Gets or sets the initial support upgrade.
            /// </summary>
			public ArmorMod SupportUpgrade
			{
				get { return (ArmorMod) _supportUpgrade; }
				set { _supportUpgrade = (sbyte) value; }
			}

            /// <summary>
            /// Gets or sets the initial grenade amount.
            /// </summary>
			public GrenadeCount InitialGrenadeCount
			{
				get { return (GrenadeCount) _initialGrenades; }
				set { _initialGrenades = (sbyte) value; }
			}

            /// <summary>
            /// Gets or sets a player's damage rate.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? DamageMultiplier
            {
                get { return _damageMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _damageMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's melee damage rate.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            /// <remarks>
            /// This value stacks with <see cref="DamageMultiplier"/>.
            /// </remarks>
            public float? MeleeDamageMultiplier
            {
                get { return _meleeDamageMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _meleeDamageMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets the recurring duration in seconds before a player obtains a Frag Grenade.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? FragGrenadeRegen
            {
                get { return _fragGrenadeRegenDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _fragGrenadeRegenDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets the recurring duration in seconds before a player obtains a Plasma Grenade.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? PlasmaGrenadeRegen
            {
                get { return _plasmaGrenadeRegenDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _plasmaGrenadeRegenDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets the recurring duration in seconds before a player obtains a Pulse Grenade.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? PulseGrenadeRegen
            {
                get { return _pulseGrenadeRegenDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _pulseGrenadeRegenDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets the sprint stamina depletion rate for a player.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? SprintStaminaDepletionRate
            {
                get { return _sprintEnergyUseRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _sprintEnergyUseRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the sprint stamina restoration delay in seconds.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? SprintStaminaRestoreDelay
            {
                get { return _sprintEnergyRechargeDelay; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _sprintEnergyRechargeDelay = value;
                }
            }

            /// <summary>
            /// Gets or sets the sprint stamina restoration rate.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? SprintStaminaRestoreRate
            {
                get { return _sprintEnergyRechargeRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _sprintEnergyRechargeRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the initial sprint stamina for a player.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? SprintInitialStamina
            {
                get { return _sprintInitialEnergy; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _sprintInitialEnergy = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's armor ability energy depletion rate.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? ArmorAbilityEnergyDepletionRate
            {
                get { return _aaEnergyUseRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _aaEnergyUseRate = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's armor ability energy recharge delay in seconds.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? ArmorAbilityEnergyRechargeDelay
            {
                get { return _aaEnergyRechargeDelay; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _aaEnergyRechargeDelay = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's armor ability energy recharge rate.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? ArmorAbilityEnergyRechargeRate
            {
                get { return _aaEnergyRechargeRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _aaEnergyRechargeRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the initial amount of energy a player's armor ability contains.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? ArmorAbilityInitialEnergy
            {
                get { return _aaInitialEnergy; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _aaInitialEnergy = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which a player switches weapons.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? WeaponSwitchSpeed
            {
                get { return _switchSpeedMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _switchSpeedMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which a player reloads their weapon.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? WeaponReloadSpeed
            {
                get { return _reloadSpeedMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _reloadSpeedMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets the modifier for a player's personal ordnance points.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? OrdnancePointsModifier
            {
                get { return _ordnancePointsMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _ordnancePointsMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets the effective range of an explosion triggered by a player.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? ExplosionRadius
            {
                get { return _explosionRadiusMultiplier; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _explosionRadiusMultiplier = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which a chaingun turret cools down.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? TurretCooldownRate
            {
                get { return _turretCooldownRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _turretCooldownRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate of a player's flinch when they get hit by a projectile.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? FlinchRate
            {
                get { return _flinchRate; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _flinchRate = value;
                }
            }

            /// <summary>
            /// Gets or sets the effective range when <see cref="OrdnanceWarning"/> is enabled.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? OrdnanceWarningDistance
            {
                get { return _dropReconDistance; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _dropReconDistance = value;
                }
            }

            /// <summary>
            /// Gets or sets the rate at which a player performs an assassination on another player.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? AssassinationSpeed
            {
                get { return _assassinationSpeed; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _assassinationSpeed = value;
                }
            }

            /// <summary>
            /// Gets or sets the duration in seconds of the incoming ordnance waypoint.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? OrdnanceWarningDuration
            {
                get { return _dropReconDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _dropReconDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player has infinite ammo.
            /// </summary>
            public InfiniteAmmoMode InfiniteAmmo
            {
                get { return (InfiniteAmmoMode)_infiniteAmmo; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _infiniteAmmo = (byte)value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is able to pick up weapons on the ground.
            /// </summary>
            public TraitFlag WeaponPickup
            {
                get { return _weaponPickup; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _weaponPickup = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is able to use their armor ability.
            /// </summary>
            public TraitFlag ArmorAbilityUsage
            {
                get { return _aaUsage; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _aaUsage = value;
                }
            }

            /// <summary>
            /// Gets or sets whether the <see cref="ArmorAbilityUsage"/> value does not apply to an Autoturret.
            /// </summary>
            public TraitFlag ArmorAbilityUsageExceptingAutoturret
            {
                get { return _aaUsageExceptingAutoturret; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _aaUsageExceptingAutoturret = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player's armor ability never depletes.
            /// </summary>
            public TraitFlag InfiniteArmorAbilityUsage
            {
                get { return _infiniteAAUsage; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _infiniteAAUsage = value;
                }
            }

            /// <summary>
            /// Gets or sets whether the weapons a player spawns with and ordnance has increased ammo.
            /// </summary>
            public TraitFlag ExtraAmmo
            {
                get { return _extraAmmo; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _extraAmmo = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player spawns with an extra grenade.
            /// </summary>
            public TraitFlag ExtraGrenade
            {
                get { return _extraGrenade; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _extraGrenade = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player drops a live grenade upon death.
            /// </summary>
            public TraitFlag LastGasp
            {
                get { return _lastGasp; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _lastGasp = value;
                }
            }

            /// <summary>
            /// Gets or sets whether ordnance waypoints are visible to a player.
            /// </summary>
            public TraitFlag OrdnanceWaypoints
            {
                get { return _ordnanceNavpointsVisible; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _ordnanceNavpointsVisible = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is able to reroll their personal ordnance choices.
            /// </summary>
            public TraitFlag Requisition
            {
                get { return _requisition; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _requisition = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player is able to pick up grenades from dead players.
            /// </summary>
            public TraitFlag GrenadePickup
            {
                get { return _grenadePickup; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _grenadePickup = value;
                }
            }

            /// <summary>
            /// Gets or sets whether personal ordnance is enabled for a player.
            /// </summary>
            public TraitFlag PersonalOrdnance
            {
                get { return _personalOrdnance; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _personalOrdnance = value;
                }
            }

            void ISerializable<BitStream>.SerializeObject(BitStream s)
            {
                StreamModifier(s, ref _damageMultiplier);
                StreamModifier(s, ref _meleeDamageMultiplier);
                StreamModifier(s, ref _fragGrenadeRegenDuration);
                StreamModifier(s, ref _plasmaGrenadeRegenDuration);
                StreamModifier(s, ref _pulseGrenadeRegenDuration);
                StreamModifier(s, ref _sprintEnergyUseRate);
                StreamModifier(s, ref _sprintEnergyRechargeDelay);
                StreamModifier(s, ref _sprintEnergyRechargeRate);
                StreamModifier(s, ref _sprintInitialEnergy);
                StreamModifier(s, ref _aaEnergyUseRate);
                StreamModifier(s, ref _aaEnergyRechargeDelay);
                StreamModifier(s, ref _aaEnergyRechargeRate);
                StreamModifier(s, ref _aaInitialEnergy);
                StreamModifier(s, ref _switchSpeedMultiplier);
                StreamModifier(s, ref _reloadSpeedMultiplier);
                StreamModifier(s, ref _ordnancePointsMultiplier);
                StreamModifier(s, ref _explosionRadiusMultiplier);
                StreamModifier(s, ref _turretCooldownRate);
                StreamModifier(s, ref _flinchRate);
                StreamModifier(s, ref _dropReconDuration);
                StreamModifier(s, ref _dropReconDistance);
                StreamModifier(s, ref _assassinationSpeed);
                StreamFlag(s, ref _weaponPickup);
                s.Stream(ref _initialGrenades, bits: 5);
                s.Stream(ref _infiniteAmmo, bits: 2);
                StreamFlag(s, ref _aaUsage);
                StreamFlag(s, ref _aaUsageExceptingAutoturret);
                StreamFlag(s, ref _dropAAOnDeath);
                StreamFlag(s, ref _infiniteAAUsage);
                StreamFlag(s, ref _extraAmmo);
                StreamFlag(s, ref _extraGrenade);
                StreamFlag(s, ref _lastGasp);
                StreamFlag(s, ref _ordnanceNavpointsVisible);
                StreamFlag(s, ref _requisition);
                StreamFlag(s, ref _grenadePickup);
                StreamFlag(s, ref _firepowerEnabled);
                StreamFlag(s, ref _personalOrdnance);
                s.Stream(ref _primaryWeapon);
                s.Stream(ref _secondaryWeapon);
                s.Stream(ref _armorAbility);
                s.Stream(ref _tacticalPackage);
                s.Stream(ref _supportUpgrade);
            }
        }

        #endregion

        #region Movement

        /// <summary>
        /// Represents a group of movement-related traits in a Halo 4 player trait set.
        /// </summary>
        public class MovementTraits
            : ISerializable<BitStream>
        {
            private float?
                _playerSpeed,
                _gravity,
                _jumpHeight,
                _lookSpeed;

            private byte _vehicleUsage;

            private TraitFlag
                _doubleJump, // idk what this is but it's modified in Lightning Flag
                _sprint,
                _automaticMomentum, // idk what this is but it's modified in Lightning Flag
                _vaulting,
                _stealth;

            /// <summary>
            /// Initializes a new instance of the <see cref="MovementTraits"/> class with default values.
            /// </summary>
            public MovementTraits()
            {
                //_playerSpeed = 1.10f; // this is the new default speed
				_vaulting = TraitFlag.Enabled;
            }

            /// <summary>
            /// Gets or sets a player's movement speed.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? MovementSpeed
            {
                get { return _playerSpeed; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _playerSpeed = value;
                }
            }

            /// <summary>
            /// Gets or sets the gravity intensity for a player.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? Gravity
            {
                get { return _gravity; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _gravity = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's jump height.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? JumpHeight
            {
                get { return _jumpHeight; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _jumpHeight = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's controller sensitivity.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? ControllerSensitivity
            {
                get { return _lookSpeed; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _lookSpeed = value;
                }
            }

            /// <summary>
            /// Gets or sets whether sprint is enabled for a player.
            /// </summary>
            public TraitFlag Sprint
            {
                get { return _sprint; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _sprint = value;
                }
            }

            /// <summary>
            /// Gets or sets whether a player's footsteps are dampened.
            /// </summary>
            public TraitFlag Stealth
            {
                get { return _stealth; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _stealth = value;
                }
            }

            /// <summary>
            /// Gets or sets the vehicle usage permissions of a player.
            /// </summary>
            public VehicleUsageMode VehicleUsage
            {
                get { return (VehicleUsageMode)_vehicleUsage; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _vehicleUsage = (byte)value;
                }
            }

            void ISerializable<BitStream>.SerializeObject(BitStream s)
            {
                StreamModifier(s, ref _playerSpeed);
                StreamModifier(s, ref _gravity);
                StreamModifier(s, ref _jumpHeight);
                StreamModifier(s, ref _lookSpeed);
                s.Stream(ref _vehicleUsage, bits: 4);
                StreamFlag(s, ref _doubleJump);
                StreamFlag(s, ref _sprint);
                StreamFlag(s, ref _automaticMomentum);
                StreamFlag(s, ref _vaulting);
                StreamFlag(s, ref _stealth);
            }
        }

        #endregion

        #region Appearance

        /// <summary>
        /// Represents a group of appearance-related traits in a Halo 4 player trait set.
        /// </summary>
        public class AppearanceTraits
            : ISerializable<BitStream>
        {
            private float? _playerScale;
			private byte _activeCamo, _waypointVisibility, _gamertagVisibility, _aura;
            private int _deathEffect, _loopingEffect;
            private ArmorColor _primary, _secondary;
            private bool _useDefaultModel;
            private sbyte _modelStringIndex;

            /// <summary>
            /// Initializes a new instance of the <see cref="AppearanceTraits"/> class with default values.
            /// </summary>
            public AppearanceTraits()
            {
                _primary = new ArmorColor();
                _secondary = new ArmorColor();
                _useDefaultModel = true;
                _modelStringIndex = -1;
				_gamertagVisibility = 1;
				_aura = 1;

				GamertagVisibility = HudVisibility.None;
                DeathEffect = LoopingEffect = ArmorEffect.MapDefault;
            }

            /// <summary>
            /// Gets or sets the scale of a player's model.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? PlayerScale
            {
                get { return _playerScale; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _playerScale = value;
                }
            }

            /// <summary>
            /// Gets or sets a player's primary armor color.
            /// </summary>
            public ArmorColor PrimaryColor
            {
                get { return _primary; }
                set { _primary = value; }
            }

            /// <summary>
            /// Gets or sets a player's secondary armor color.
            /// </summary>
            public ArmorColor SecondaryColor
            {
                get { return _secondary; }
                set { _secondary = value; }
            }

            /// <summary>
            /// Gets or sets a player's Active Camouflage state.
            /// </summary>
            public ActiveCamoMode ActiveCamo
            {
                get { return (ActiveCamoMode)_activeCamo; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _activeCamo = (byte)value;
                }
            }

            /// <summary>
            /// Gets or sets a player's waypoint visibility.
            /// </summary>
            public HudVisibility WaypointVisibility
            {
                get { return (HudVisibility)_waypointVisibility; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _waypointVisibility = (byte)value;
                }
            }

            /// <summary>
            /// Gets or sets a player's gamertag visibility.
            /// </summary>
            public HudVisibility GamertagVisibility
            {
                get { return (HudVisibility)_gamertagVisibility; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _gamertagVisibility = (byte)value;
                }
            }

            /// <summary>
            /// Gets or sets a player's continuous armor effect.
            /// </summary>
			public ArmorEffect LoopingEffect
			{
				get { return (ArmorEffect) _loopingEffect; }
				set { _loopingEffect = (sbyte) value; }
			}

            /// <summary>
            /// Gets or sets a player's armor effect to display upon death.
            /// </summary>
			public ArmorEffect DeathEffect
            {
                get { return (ArmorEffect)_deathEffect; }
				set { _deathEffect = (sbyte) value; }
            }

            public ModelVariant ModelVariant
            {
				get { return (ModelVariant) _modelStringIndex; }
                set
                {
					_modelStringIndex = (sbyte) value;
                    if (value < 0) _useDefaultModel = true;
                }
            }

            void ISerializable<BitStream>.SerializeObject(BitStream s)
            {
                StreamModifier(s, ref _playerScale);
                s.Stream(ref _activeCamo, bits: 3);
                s.Stream(ref _waypointVisibility, bits: 2);
                s.Stream(ref _gamertagVisibility, bits: 2);
                s.Stream(ref _aura, bits: 3);
                s.Serialize(_primary);
                s.Serialize(_secondary);
                s.Stream(ref _useDefaultModel);
                s.Stream(ref _modelStringIndex);
                s.Stream(ref _deathEffect);
                s.Stream(ref _loopingEffect);
            }

			public class ArmorColor
				: ISerializable<BitStream>
			{
				private byte _red, _green, _blue;
				private bool _useDefault;

				public ArmorColor (byte red, byte green, byte blue)
				{
					_red = red;
					_green = green;
					_blue = blue;
				}

				public ArmorColor ()
				{
					_useDefault = true;
					_red = _green = _blue = 255;
				}

				public byte Red { get { return _red; } }
				public byte Green { get { return _green; } }
				public byte Blue { get { return _blue; } }

				#region ISerializable<BitStream> Members

				void ISerializable<BitStream>.SerializeObject (BitStream s)
				{
					s.Stream(ref _useDefault);
					s.Stream(ref _red);
					s.Stream(ref _green);
					s.Stream(ref _blue);
				}

				#endregion
			}
        }

        #endregion

        #region Screen and Audio

        /// <summary>
        /// Represents a group of traits in a Halo 4 player trait set which affects a player's HUD
        /// and audio.
        /// </summary>
        public class ScreenAndAudioTraits
            : ISerializable<BitStream>
        {
            private TraitFlag
                _shieldHudVisible,
                _motionTrackerWhileScoped,
                _directionalDamageIndicator,
                _battleAwareness,
                _threatView,
                _auralEnhancement,
                _nemesis;

            private byte
                _motionTrackerMode,
                _visionMode; // Zedd sez this is from AI traits

            private float?
                _nemesisDuration,
                _motionTrackerRange;

			public ScreenAndAudioTraits()
			{
				_directionalDamageIndicator = TraitFlag.Enabled;
				_visionMode = 1;
			}

            /// <summary>
            /// Gets or sets whether the shield bar is visible.
            /// </summary>
            public TraitFlag ShieldHud
            {
                get { return _shieldHudVisible; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _shieldHudVisible = value;
                }
            }

            /// <summary>
            /// Gets or sets whether the motion sensor is visible while zoomed in with a weapon.
            /// </summary>
            public TraitFlag MotionSensorWhileScoped
            {
                get { return _motionTrackerWhileScoped; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _motionTrackerWhileScoped = value;
                }
            }

            /// <summary>
            /// Gets or sets whether the directional damage indicator is visible.
            /// </summary>
            public TraitFlag DirectionalDamageIndicator
            {
                get { return _directionalDamageIndicator; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _directionalDamageIndicator = value;
                }
            }

            /// <summary>
            /// Gets or sets whether other players' shields and service tag is visible when in sight.
            /// </summary>
            public TraitFlag BattleAwareness
            {
                get { return _battleAwareness; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _battleAwareness = value;
                }
            }

            /// <summary>
            /// Gets or sets whether players beep when moving near you.
            /// </summary>
            public TraitFlag AuralEnhancement
            {
                get { return _auralEnhancement; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _auralEnhancement = value;
                }
            }

            /// <summary>
            /// Gets or sets whether the players see a waypoint above their killer for a period of time.
            /// </summary>
            public TraitFlag Nemesis
            {
                get { return _nemesis; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _nemesis = value;
                }
            }

            /// <summary>
            /// Gets or sets the state of the motion sensor.
            /// </summary>
            public MotionSensorMode MotionSensor
            {
                get { return (MotionSensorMode)_motionTrackerMode; }
                set
                {
                    Contract.Requires(value.IsDefined());
                    _motionTrackerMode = (byte)value;
                }
            }

            /// <summary>
            /// Gets or sets the duration in seconds the a waypoint above a player's killer is visible
            /// when the <see cref="Nemesis"/> trait is active.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? NemesisDuration
            {
                get { return _nemesisDuration; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _nemesisDuration = value;
                }
            }

            /// <summary>
            /// Gets or sets the effective range of a player's motion sensor.
            /// 
            /// The value must fall in the range between -200 and 200 or an exception will be thrown.
            /// </summary>
            public float? MotionSensorRange
            {
                get { return _motionTrackerRange; }
                set
                {
                    Contract.Requires<ArgumentOutOfRangeException>(value == null || (value >= -200f && value <= 200f));
                    _motionTrackerRange = value;
                }
            }

            void ISerializable<BitStream>.SerializeObject(BitStream s)
            {
                StreamFlag(s, ref _shieldHudVisible);
                StreamModifier(s, ref _nemesisDuration);
                StreamModifier(s, ref _motionTrackerRange);
                s.Stream(ref _motionTrackerMode, bits: 3);
                StreamFlag(s, ref _motionTrackerWhileScoped);
                StreamFlag(s, ref _directionalDamageIndicator);
                s.Stream(ref _visionMode, bits: 2);
                StreamFlag(s, ref _battleAwareness);
                StreamFlag(s, ref _threatView);
                StreamFlag(s, ref _auralEnhancement);
                StreamFlag(s, ref _nemesis);
            }
        }

        #endregion

	}
}
