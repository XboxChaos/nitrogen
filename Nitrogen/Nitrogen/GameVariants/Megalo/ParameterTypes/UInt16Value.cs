using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class UInt16Value
		: Parameter
	{
		private ushort _value;

		public UInt16Value () { }

		public UInt16Value (ushort value)
		{
			_value = value;
		}

		public ushort Value
		{
			get { return _value; }
			set { _value = value; }
		}

		internal bool UsePlusOneEncoding { get; set; }

		public override void SerializeObject (BitStream s)
		{
			s.Stream(ref _value);
		}
	}
}
