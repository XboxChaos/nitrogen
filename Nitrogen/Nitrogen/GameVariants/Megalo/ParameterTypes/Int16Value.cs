using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class Int16Value
		: IParameter
	{
		private short _value;

		public Int16Value (short value)
		{
			_value = value;
		}

		internal Int16Value () { }

		public short Value
		{
			get { return _value; }
			set { _value = value; }
		}

		#region IParameter Members

		ParameterType IParameter.ParameterType { get { return ParameterType.Integer; } }

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			if ( definition.UsePlusOneEncoding )
				s.StreamPlusOne(ref _value);
			else
				s.Stream(ref _value);
		}

		#endregion
	}
}
