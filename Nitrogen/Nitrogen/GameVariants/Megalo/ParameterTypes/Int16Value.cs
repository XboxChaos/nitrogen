using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class Int16Value
		: Parameter
	{
		private short _value;

		public Int16Value () { }

		public Int16Value (short value)
		{
			_value = value;
		}

		public short Value
		{
			get { return _value; }
			set { _value = value; }
		}

		internal bool UsePlusOneEncoding { get; set; }

		public override void SerializeObject (BitStream s)
		{
			if ( UsePlusOneEncoding )
				s.StreamPlusOne(ref _value);
			else
				s.Stream(ref _value);
		}
	}
}
