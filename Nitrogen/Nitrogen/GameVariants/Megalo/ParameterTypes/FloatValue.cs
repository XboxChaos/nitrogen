using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class FloatValue
		: Parameter<float>
	{
		public FloatValue () { }

		public FloatValue (float value)
		{
			Value = value;
		}

		public override void SerializeObject (BitStream s)
		{
			throw new NotImplementedException();
		}
	}
}
