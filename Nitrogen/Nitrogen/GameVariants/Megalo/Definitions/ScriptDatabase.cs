using System;

namespace Nitrogen.GameVariants.Megalo.Definitions
{
	internal sealed class ScriptDatabase
	{
		private IDefinitionsTransport _transport;

		internal ScriptDatabase (IDefinitionsTransport transport)
		{
			_transport = transport;
		}

		public DefinitionDatabase Definitions { get; private set; }

		internal Definition GetDefinition (int opcode)
		{
			if ( Definitions == null )
				Definitions = _transport.ReadDefinitions();

			var definition = Definitions.Get(opcode);
			if ( definition != null )
				return definition;

			throw new Exception(string.Format("No definition found for opcode {0}", opcode));
		}
	}
}