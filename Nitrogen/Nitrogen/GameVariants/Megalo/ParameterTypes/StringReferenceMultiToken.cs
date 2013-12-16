using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class StringReferenceMultiToken
		: StringReference
	{
		public override int MaxTokens { get { return 3; } }
	}
}
