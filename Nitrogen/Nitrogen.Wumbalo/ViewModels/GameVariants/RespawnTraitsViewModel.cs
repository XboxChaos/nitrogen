using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using Nitrogen.Shared;
using System;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class RespawnTraitsViewModel
		: TraitsViewModel
	{
		private RespawnSettings _respawn;

		public RespawnTraitsViewModel (RespawnSettings respawn)
			: base(respawn.RespawnTraits)
		{
			_respawn = respawn;
		}

		public byte Duration
		{
			get { return _respawn.RespawnTraitsDuration; }
			set
			{
				_respawn.RespawnTraitsDuration = value;
				OnPropertyChanged();
			}
		}
	}
}