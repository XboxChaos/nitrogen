using Nitrogen.Utilities;
using Nitrogen.IO;
using System;
using Nitrogen.GameVariants.Megalo.ParameterTypes;
using System.Diagnostics.Contracts;
using Nitrogen.GameVariants.Megalo.Definitions;

namespace Nitrogen.GameVariants.Megalo.VariableDefinitions
{
	public sealed class TimerVariableDefinition
		: ISerializable<BitStream>
	{
		private IParameter _value;

		public TimerVariableDefinition ()
			: this(new IntegerReference() { ReferenceType = IntegerReferenceType.Constant, ConstantValue = 0 }) { }

		public TimerVariableDefinition (IntegerReference initialValue)
		{
			Contract.Requires<ArgumentNullException>(initialValue != null);
			_value = initialValue;
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			_value.SerializeObject(s, new ParameterDefinition());
		}

		#endregion
	}
}