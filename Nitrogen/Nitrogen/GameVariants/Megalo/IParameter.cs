using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo
{
	internal interface IParameter
		: ISerializable<BitStream>
	{
		ParameterType ParameterType { get; }
	}
}
