using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using System;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class RespawnSettingsViewModel
		: Inpc
	{
		private RespawnSettings _respawn;
		private GameVariant _variant;

		public RespawnSettingsViewModel (GameVariant variant)
		{
			_variant = variant;
			_respawn = _variant.RespawnSettings;
		}
	}
}
