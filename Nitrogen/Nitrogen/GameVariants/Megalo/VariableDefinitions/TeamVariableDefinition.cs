using Nitrogen.Utilities;
using Nitrogen.IO;
using System;
using Nitrogen.GameVariants.Megalo.ParameterTypes;
using System.Diagnostics.Contracts;
using Nitrogen.GameVariants.Megalo.Definitions;

namespace Nitrogen.GameVariants.Megalo.VariableDefinitions
{
	public sealed class TeamVariableDefinition
		: ISerializable<BitStream>
	{
		private byte _value; // TeamVariableType enum
		private byte _priority;
		private bool _unknownFlag;

		public TeamVariableDefinition () { }

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _value, 4);
			s.Stream(ref _priority, 2);
			s.Stream(ref _unknownFlag);
		}

		#endregion
	}
}