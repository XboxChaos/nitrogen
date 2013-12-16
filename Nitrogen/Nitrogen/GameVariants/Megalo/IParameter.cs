using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo
{
	internal interface IParameter
	{
		ParameterType ParameterType { get; }

		void SerializeObject (BitStream s, ParameterDefinition definition);
	}
}
