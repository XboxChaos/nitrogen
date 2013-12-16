using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class FloatValue
		: IParameter
	{
		public FloatValue (float value)
		{
			
		}

		internal FloatValue () { }

		#region IParameter Members

		ParameterType IParameter.ParameterType { get { return ParameterType.Float; } }

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
