using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class OptionalInt16Value
		: Parameter
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

		internal bool UsePlusOneEncoding { get; set; }

		public override void SerializeObject (BitStream s)
		{
			if ( UsePlusOneEncoding )
				s.StreamPlusOneOptional(ref _value);
			else
				s.StreamOptional(ref _value);
		}
	}
}
