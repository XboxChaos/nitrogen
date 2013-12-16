using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class OptionalUInt16Value
		: Parameter
	{
		private ushort? _value;

		public OptionalUInt16Value () { }

		public OptionalUInt16Value (ushort? value)
		{
			_value = value;
		}

		public ushort? Value
		{
			get { return _value; }
			set { _value = value; }
		}

		public override void SerializeObject (BitStream s)
		{
			s.StreamOptional(ref _value);
		}
	}
}
