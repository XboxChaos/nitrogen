using Nitrogen.Data;
using Nitrogen.IO;
using Nitrogen.Shared;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Base
{
	public sealed class MapOverrides
		: ISerializable<BitStream>
	{
		private const int PowerupCount = 4;

		private bool
            _indestructibleVehicles,
            _turretsOnMap,
            _powerupsOnMap,
            _aaOnMap,
            _shortcutsOnMap,
            _grenadesOnMap;

		private sbyte
			_weaponSet,
			_vehicleSet,
			_aaSet;

		private PlayerTraits _baseTraits;
		private PowerupSettings[] _powerups;

		/// <summary>
		/// Initializes a new instance of the <see cref="MapOverrides"/> class with default values.
		/// </summary>
		public MapOverrides ()
		{
			_baseTraits = new PlayerTraits();
			_powerups = new PowerupSettings[PowerupCount];
			_powerups[0] = PowerupSettings.CreateDamageBoost();
			_powerups[1] = PowerupSettings.CreateSpeedBoost();
			_powerups[2] = PowerupSettings.CreateOvershield();
			_powerups[3] = new PowerupSettings();
			_turretsOnMap = true;
			_powerupsOnMap = true;
			_aaOnMap = true;
			_shortcutsOnMap = true;
			_grenadesOnMap = true;
			ArmorAbilitySet = ArmorAbilitySet.MapDefault;
			WeaponSet = WeaponSet.MapDefault;
			VehicleSet = VehicleSet.MapDefault;
		}

		/// <summary>
		/// Gets or sets whether vehicles are indestructible.
		/// </summary>
		public bool IndestructibleVehicles
		{
			get { return _indestructibleVehicles; }
			set { _indestructibleVehicles = value; }
		}

		/// <summary>
		/// Gets or sets whether grenades can be spawned on the map.
		/// </summary>
		public bool GrenadesOnMap
		{
			get { return _grenadesOnMap; }
			set { _grenadesOnMap = value; }
		}

		/// <summary>
		/// Gets or sets whether armor abilities can be spawned on the map.
		/// </summary>
		public bool ArmorAbilitiesOnMap
		{
			get { return _aaOnMap; }
			set { _aaOnMap = value; }
		}

		/// <summary>
		/// Gets or sets the weapon set for all weapons placed on the map. This does not affect
		/// ordnance drops.
		/// </summary>
		public WeaponSet WeaponSet
		{
			get { return (WeaponSet) _weaponSet; }
			set { _weaponSet = (sbyte) value; }
		}

		/// <summary>
		/// Gets or sets the vehicle set for all vehicles placed on the map.
		/// </summary>
		public VehicleSet VehicleSet
		{
			get { return (VehicleSet) _vehicleSet; }
			set { _vehicleSet = (sbyte) value; }
		}

		/// <summary>
		/// Gets or sets the armor ability set for all armor abilities placed on the map.
		/// </summary>
		public ArmorAbilitySet ArmorAbilitySet
		{
			get { return (ArmorAbilitySet) _aaSet; }
			set { _aaSet = (sbyte) value; }
		}

		/// <summary>
		/// Gets or sets the traits that applies to all players in the match.
		/// </summary>
		public PlayerTraits BasePlayerTraits
		{
			get { return _baseTraits; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_baseTraits = value;
			}
		}

		/// <summary>
		/// Gets or sets the properties of the Damage Boost powerup.
		/// </summary>
		public PowerupSettings DamageBoost
		{
			get { return _powerups[0]; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_powerups[0] = value;
			}
		}

		/// <summary>
		/// Gets or sets the properties of the Speed Boost powerup.
		/// </summary>
		public PowerupSettings SpeedBoost
		{
			get { return _powerups[1]; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_powerups[1] = value;
			}
		}

		/// <summary>
		/// Gets or sets the properties of the Overshield powerup.
		/// </summary>
		public PowerupSettings Overshield
		{
			get { return _powerups[2]; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_powerups[2] = value;
			}
		}

		/// <summary>
		/// Gets or sets the properties of the custom powerup.
		/// </summary>
		public PowerupSettings CustomPowerup
		{
			get { return _powerups[3]; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_powerups[3] = value;
			}
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _indestructibleVehicles);
			s.Stream(ref _turretsOnMap);
			s.Stream(ref _powerupsOnMap);
			s.Stream(ref _aaOnMap);
			s.Stream(ref _shortcutsOnMap);
			s.Stream(ref _grenadesOnMap);
			s.SerializeObject(_baseTraits);
			s.Stream(ref _weaponSet);
			s.Stream(ref _vehicleSet);
			s.Stream(ref _aaSet);
			s.SerializeObjects(_powerups, 0, PowerupCount);
		}

		#endregion
	}
}
