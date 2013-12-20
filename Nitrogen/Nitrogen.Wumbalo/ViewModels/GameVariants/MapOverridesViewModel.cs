using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using System;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class MapOverridesViewModel
		: Inpc
	{
		private MapOverrides _map;
		private GameVariant _variant;

		public MapOverridesViewModel (GameVariant variant)
		{
			_variant = variant;
			_map = _variant.MapOverrides;
		}

		public bool IndestructibleVehicles
		{
			get { return _map.IndestructibleVehicles; }
			set
			{
				_map.IndestructibleVehicles = value;
				OnPropertyChanged();
			}
		}

		public bool GrenadesOnMap
		{
			get { return _map.GrenadesOnMap; }
			set
			{
				_map.GrenadesOnMap = value;
				OnPropertyChanged();
			}
		}

		public bool ArmorAbilitiesOnMap
		{
			get { return _map.ArmorAbilitiesOnMap; }
			set
			{
				_map.ArmorAbilitiesOnMap = value;
				OnPropertyChanged();
			}
		}

		public int WeaponSet
		{
			get { return (int) _map.WeaponSet + 2; }
			set
			{
				_map.WeaponSet = (WeaponSet) ( value - 2 );
				OnPropertyChanged();
			}
		}

		public int VehicleSet
		{
			get { return (int) _map.VehicleSet + 2; }
			set
			{
				_map.VehicleSet = (VehicleSet) ( value - 2 );
				OnPropertyChanged();
			}
		}
	}
}