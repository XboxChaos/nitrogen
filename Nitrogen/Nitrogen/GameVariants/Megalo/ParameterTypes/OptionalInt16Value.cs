using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class OptionalInt16Value
		: IParameter
	{
		private short? _value;

		public OptionalInt16Value () { }

		public OptionalInt16Value(short? value)
		{
			_value = value;
		}

		public short? Value
		{
			get { return _value; }
			set { _value = value; }
		}

		#region IParameter Members

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			if ( definition.UsePlusOneEncoding )
				s.StreamPlusOneOptional(ref _value);
			else
				s.StreamOptional(ref _value);
		}

		#endregion
	}
}
