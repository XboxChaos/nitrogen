using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class FloatValue
		: Parameter
	{
		public FloatValue () { }

		public FloatValue (float value)
		{
			
		}

		public override void SerializeObject (BitStream s)
		{
			throw new NotImplementedException();
		}
	}
}
