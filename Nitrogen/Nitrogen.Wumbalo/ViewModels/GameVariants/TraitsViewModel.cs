using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using Nitrogen.Shared;
using System;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class TraitsViewModel
		: Inpc
	{
		private PlayerTraits _traits;

		public TraitsViewModel (PlayerTraits traits)
		{
			_traits = traits;
		}
	}
}