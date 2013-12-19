using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using System;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class GeneralSettingsViewModel
		: Inpc
	{
		private GeneralSettings _general;
		private GameVariant _variant;

		public GeneralSettingsViewModel (GameVariant variant)
		{
			_variant = variant;
			_metadata = _variant.GeneralSettings;
		}
	}
}
