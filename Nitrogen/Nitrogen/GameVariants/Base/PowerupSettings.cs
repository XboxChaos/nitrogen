using Nitrogen.Data;
using Nitrogen.IO;
using Nitrogen.Shared;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Base
{
	public sealed class PowerupSettings
		: ISerializable<BitStream>
	{
		private byte _primaryDuration, _secondaryDuration;
		private PlayerTraits _primary, _secondary;

		/// <summary>
		/// Creates a new instance of the <see cref="PowerupSettings"/> class based on the
		/// Overshield powerup.
		/// </summary>
		/// <returns>
		/// A new instance of the <see cref="PowerupSettings"/> class representing the Overshield
		/// powerup.
		/// </returns>
		public static PowerupSettings CreateOvershield ()
		{
			return new PowerupSettings
			{
				PrimaryDuration = 1,
				PrimaryTraits = new PlayerTraits
				{
					Armor = new PlayerTraits.ArmorTraits
					{
						ShieldAmount = 2.0f,
						OvershieldRechargeRate = 5.0f,
						ShieldRechargeRate = 5.0f,
						NoPowerupStacking = PlayerTraits.TraitFlag.Disabled
					}
				},

				SecondaryDuration = 120,
				SecondaryTraits = new PlayerTraits
				{
					Armor = new PlayerTraits.ArmorTraits
					{
						OvershieldRechargeRate = -0.30f,
						DamageResistance = 3.4f,
						NoPowerupStacking = PlayerTraits.TraitFlag.Enabled
					},

					Appearance = new PlayerTraits.AppearanceTraits
					{
						LoopingEffect = ArmorEffect.Overshield
					}
				}
			};
		}

		/// <summary>
		/// Creates a new instance of the <see cref="PowerupSettings"/> class based on the
		/// Speed Boost powerup.
		/// </summary>
		/// <returns>
		/// A new instance of the <see cref="PowerupSettings"/> class representing the Speed Boost
		/// powerup.
		/// </returns>
		public static PowerupSettings CreateSpeedBoost ()
		{
			return new PowerupSettings
			{
				PrimaryDuration = 45,
				PrimaryTraits = new PlayerTraits
				{
					Equipment = new PlayerTraits.EquipmentTraits
					{
						WeaponReloadSpeed = 1.75f,
						WeaponSwitchSpeed = 1.75f
					},

					Movement = new PlayerTraits.MovementTraits
					{
						MovementSpeed = 1.5f
					},

					Appearance = new PlayerTraits.AppearanceTraits
					{
						LoopingEffect = ArmorEffect.SpeedBoost
					}
				}
			};
		}

		/// <summary>
		/// Creates a new instance of the <see cref="PowerupSettings"/> class based on the
		/// Damage Boost powerup.
		/// </summary>
		/// <returns>
		/// A new instance of the <see cref="PowerupSettings"/> class representing the Damage Boost
		/// powerup.
		/// </returns>
		public static PowerupSettings CreateDamageBoost ()
		{
			return new PowerupSettings
			{
				PrimaryDuration = 30,
				PrimaryTraits = new PlayerTraits
				{
					Equipment = new PlayerTraits.EquipmentTraits
					{
						DamageMultiplier = 1.45f,
						MeleeDamageMultiplier = 1.60f
					},

					Appearance = new PlayerTraits.AppearanceTraits
					{
						ActiveCamo = PlayerTraits.ActiveCamoMode.Disabled,
						LoopingEffect = ArmorEffect.DamageBoost
					}
				}
			};
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PowerupSettings"/> class with default values.
		/// </summary>
		public PowerupSettings ()
		{
			_primary = new PlayerTraits();
			_secondary = new PlayerTraits();
		}

		/// <summary>
		/// Gets or sets the duration in seconds of the primary powerup traits is active.
		/// 
		/// The value must fall in the range between 0 and 127 or an exception will be thrown.
		/// </summary>
		public byte PrimaryDuration
		{
			get { return _primaryDuration; }
			set
			{
				Contract.Requires<ArgumentOutOfRangeException>(value <= 127);
				_primaryDuration = value;
			}
		}

		/// <summary>
		/// Gets or sets the duration in seconds of the secondary powerup traits is active.
		/// 
		/// The value must fall in the range between 0 and 127 or an exception will be thrown.
		/// </summary>
		public byte SecondaryDuration
		{
			get { return _secondaryDuration; }
			set
			{
				Contract.Requires<ArgumentOutOfRangeException>(value <= 127);
				_secondaryDuration = value;
			}
		}

		/// <summary>
		/// Gets or sets the primary powerup traits.
		/// </summary>
		public PlayerTraits PrimaryTraits
		{
			get { return _primary; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_primary = value;
			}
		}

		/// <summary>
		/// Gets or sets the secondary powerup traits.
		/// </summary>
		public PlayerTraits SecondaryTraits
		{
			get { return _secondary; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_secondary = value;
			}
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.SerializeObject(_primary);
			s.Stream(ref _primaryDuration, 7);

			s.SerializeObject(_secondary);
			s.Stream(ref _secondaryDuration, 7);
		}

		#endregion
	}
}
