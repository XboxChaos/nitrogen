using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using System;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class LoadoutSettingsViewModel
		: Inpc
	{
		private LoadoutSettings _loadouts;
		private GameVariant _variant;

		public LoadoutSettingsViewModel (GameVariant variant)
		{
			_variant = variant;
			_loadouts = _variant.LoadoutSettings;
		}

		public int LoadoutSet
		{
			get { return _loadouts.PersonalLoadoutsEnabled ? 0 : 1; }
			set
			{
				_loadouts.PersonalLoadoutsEnabled = (value == 1);
				OnPropertyChanged();
			}
		}

		public bool MapLoadoutsEnabled
		{
			get { return _loadouts.MapLoadoutsEnabled; }
			set
			{
				_loadouts.MapLoadoutsEnabled = value;
				OnPropertyChanged();
			}
		}
	}
}