using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class StringReferenceOneToken
		: StringReference
	{
		public override int MaxTokens { get { return 1; } }

		public override ParameterType ParameterType { get { return ParameterType.StringReferenceOneToken; } }
	}
}