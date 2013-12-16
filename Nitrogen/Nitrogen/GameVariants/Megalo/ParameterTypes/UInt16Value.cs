using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class UInt16Value
		: IParameter
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

		#region IParameter Members

		void IParameter.SerializeObject (BitStream s, Definitions.ParameterDefinition definition)
		{
			s.Stream(ref _value);
		}

		#endregion
	}
}
