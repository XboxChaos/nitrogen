using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class IntegerValue
		: Parameter<short>
	{
		public IntegerValue () { }

		public IntegerValue (short value)
		{
			Value = value;
		}

		public override void SerializeObject (BitStream s)
		{
			throw new NotImplementedException();
		}
	}
}
