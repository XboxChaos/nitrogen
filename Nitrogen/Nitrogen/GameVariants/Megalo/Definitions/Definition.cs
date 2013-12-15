using System;
using System.Collections.Generic;

namespace Nitrogen.GameVariants.Megalo.Definitions
{
	internal sealed class Definition
	{
		public Definition (int opcode) : this(opcode, null) { }

		public Definition (int opcode, string name)
		{
			Opcode = opcode;
			Name = name;
			Parameters = new List<ParameterDefinition>();
		}

		public int Opcode { get; private set; }

		public string Name { get; private set; }

		public List<ParameterDefinition> Parameters { get; private set; }
	}
}
