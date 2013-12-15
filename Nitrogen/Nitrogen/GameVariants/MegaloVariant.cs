using Nitrogen.GameVariants.Megalo;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants
{
	public class MegaloVariant
		: GameVariant
	{
		public MegaloData MegaloData
		{
			get { return EngineData as MegaloData; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				EngineData = value;
			}
		}
	}
}
