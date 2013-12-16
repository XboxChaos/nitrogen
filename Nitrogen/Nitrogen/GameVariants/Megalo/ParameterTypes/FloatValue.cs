using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class FloatValue
		: IParameter
	{
		public FloatValue () { }

		public FloatValue (float value)
		{
			
		}

		#region IParameter Members

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
