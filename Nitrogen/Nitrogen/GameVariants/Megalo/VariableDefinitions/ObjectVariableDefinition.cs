using Nitrogen.Utilities;
using Nitrogen.IO;
using System;
using Nitrogen.GameVariants.Megalo.ParameterTypes;
using System.Diagnostics.Contracts;
using Nitrogen.GameVariants.Megalo.Definitions;

namespace Nitrogen.GameVariants.Megalo.VariableDefinitions
{
	public sealed class ObjectVariableDefinition
		: ISerializable<BitStream>
	{
		private byte _priority;
		private bool _unknownFlag;

		public ObjectVariableDefinition () { }

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _priority, 2);
			s.Stream(ref _unknownFlag);
		}

		#endregion
	}
}