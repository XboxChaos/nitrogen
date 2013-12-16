using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class StringReferenceSingleToken
		: StringReference
	{
		public override int MaxTokens { get { return 1; } }
	}
}