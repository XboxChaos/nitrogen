using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo
{
	public abstract class Parameter
		: ISerializable<BitStream>
	{
		internal Parameter (ParameterDefinition definition)
		{
			Definition = definition;
		}

		protected Parameter () { }

		internal ParameterDefinition Definition { get; private set; }

		internal ParameterType ParameterType { get { return Definition.ParameterType; } }

		#region ISerializable<BitStream> Members

		public abstract void SerializeObject (BitStream s);

		#endregion
	}
}
