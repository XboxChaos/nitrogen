using Nitrogen.Utilities;
using Nitrogen.IO;
using System;
using Nitrogen.GameVariants.Megalo.ParameterTypes;
using System.Diagnostics.Contracts;
using Nitrogen.GameVariants.Megalo.Definitions;

namespace Nitrogen.GameVariants.Megalo.VariableDefinitions
{
	public sealed class IntegerVariableDefinition
		: ISerializable<BitStream>
	{
		private IParameter _value;
		private byte _priority;
		private bool _unknownFlag;

		public IntegerVariableDefinition ()
			: this(new IntegerReference() { ReferenceType = IntegerReferenceType.Constant, ConstantValue = 0 }) { }

		public IntegerVariableDefinition (IntegerReference initialValue)
			: this(NetworkPriority.High, initialValue) { }

		public IntegerVariableDefinition(NetworkPriority priority, IntegerReference initialValue)
		{
			Contract.Requires<ArgumentNullException>(initialValue != null);
			Contract.Requires(priority.IsDefined());

			_priority = (byte) priority;
			_value = initialValue;
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			_value.SerializeObject(s, new ParameterDefinition());
			s.Stream(ref _priority, 2);
			s.Stream(ref _unknownFlag);
		}

		#endregion
	}
}
