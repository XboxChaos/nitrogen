using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class StringReferenceThreeTokens
		: StringReference
	{
		public override int MaxTokens { get { return 3; } }
	}
}