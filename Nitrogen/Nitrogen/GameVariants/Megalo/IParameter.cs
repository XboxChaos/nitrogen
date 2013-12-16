using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo
{
	internal interface IParameter
	{
		void SerializeObject (BitStream s, ParameterDefinition definition);
	}
}
