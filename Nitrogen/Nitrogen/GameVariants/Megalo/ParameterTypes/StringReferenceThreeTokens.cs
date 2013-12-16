using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class StringReferenceThreeTokens
		: StringReference
	{
		public override int MaxTokens { get { return 3; } }

		public override ParameterType ParameterType { get { return ParameterType.StringReferenceTwoTokens; } }
	}
}