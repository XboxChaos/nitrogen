using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class OptionalUInt16Value
		: IParameter
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

		#region IParameter Members

		void IParameter.SerializeObject (BitStream s, Definitions.ParameterDefinition definition)
		{
			s.StreamOptional(ref _value);
		}

		#endregion
	}
}
