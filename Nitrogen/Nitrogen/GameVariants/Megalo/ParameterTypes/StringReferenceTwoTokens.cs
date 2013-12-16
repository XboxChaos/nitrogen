using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class StringReferenceTwoTokens
		: StringReference
	{
		public override int MaxTokens { get { return 2; } }

		public override ParameterType ParameterType { get { return ParameterType.StringReferenceTwoTokens; } }
	}
}
