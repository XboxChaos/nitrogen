using Nitrogen.Data;
using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Base
{
	/// <summary>
	/// Represents a loadout.
	/// </summary>
	public sealed class Loadout
		: ISerializable<BitStream>
	{
		private bool _enabled;
		private byte? _loadoutNameIndex;

		private byte
			_primaryWeaponSkin,
			_secondaryWeaponSkin;

		private sbyte
            _primaryWeapon,
            _secondaryWeapon,
            _armorAbility,
            _tacticalPackage,
            _supportUpgrade,
            _grenadeCount;

		/// <summary>
		/// Initializes a new instance of the <see cref="Loadout"/> class with default values.
		/// </summary>
		public Loadout ()
		{
			PrimaryWeapon = Weapon.MapDefault;
			SecondaryWeapon = Weapon.MapDefault;
			ArmorAbility = ArmorAbility.MapDefault;
			TacticalPackage = ArmorMod.MapDefault;
			SupportUpgrade = ArmorMod.MapDefault;
		}

		/// <summary>
		/// Gets or sets whether this loadout is enabled.
		/// </summary>
		public bool Enabled
		{
			get { return _enabled; }
			set { _enabled = value; }
		}

		/// <summary>
		/// Gets or sets the name of this loadout. A value of <c>null</c> indicates that the default
		/// name is to be used.
		/// </summary>
		public byte? LoadoutNameIndex
		{
			get { return _loadoutNameIndex; }
			set { _loadoutNameIndex = value; }
		}

		/// <summary>
		/// Gets or sets the primary weapon.
		/// </summary>
		public Weapon PrimaryWeapon
		{
			get { return (Weapon) _primaryWeapon; }
			set { _primaryWeapon = (sbyte) value; }
		}

		/// <summary>
		/// Gets or sets the secondary weapon.
		/// </summary>
		public Weapon SecondaryWeapon
		{
			get { return (Weapon) _secondaryWeapon; }
			set { _secondaryWeapon = (sbyte) value; }
		}

		/// <summary>
		/// Gets or sets the armor ability.
		/// </summary>
		public ArmorAbility ArmorAbility
		{
			get { return (ArmorAbility) _armorAbility; }
			set { _armorAbility = (sbyte) value; }
		}

		/// <summary>
		/// Gets or sets the tactical package.
		/// </summary>
		public ArmorMod TacticalPackage
		{
			get { return (ArmorMod) _tacticalPackage; }
			set { _tacticalPackage = (sbyte) value; }
		}

		/// <summary>
		/// Gets or sets the support upgrade.
		/// </summary>
		public ArmorMod SupportUpgrade
		{
			get { return (ArmorMod) _supportUpgrade; }
			set { _supportUpgrade = (sbyte) value; }
		}

		/// <summary>
		/// Gets or sets the initial grenade count.
		/// </summary>
		public GrenadeCount Grenades
		{
			get { return (GrenadeCount) _grenadeCount; }
			set { _grenadeCount = (sbyte) value; }
		}

		/// <summary>
		/// Gets or sets the skin of the primary weapon.
		/// </summary>
		public byte PrimaryWeaponSkin
		{
			get { return _primaryWeaponSkin; }
			set { _primaryWeaponSkin = value; }
		}

		/// <summary>
		/// Gets or sets the skin of the secondary weapon.
		/// </summary>
		public byte SecondaryWeaponSkin
		{
			get { return _secondaryWeaponSkin; }
			set { _secondaryWeaponSkin = value; }
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _enabled);
			s.StreamOptional(ref _loadoutNameIndex, 7);
			s.Stream(ref _primaryWeapon);
			s.Stream(ref _secondaryWeapon);
			s.Stream(ref _armorAbility);
			s.Stream(ref _tacticalPackage);
			s.Stream(ref _supportUpgrade);
			s.Stream(ref _grenadeCount, bits: 5);
			s.Stream(ref _primaryWeaponSkin, bits: 3);
			s.Stream(ref _secondaryWeaponSkin, bits: 3);
		}

		#endregion
	}
}
