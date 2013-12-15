using System;
using System.Collections.Generic;

namespace Nitrogen.GameVariants.Megalo.Definitions
{
	internal struct ParameterDefinition
	{
		public ParameterType Type { get; set; }

		public string Name { get; set; }

		public bool Nullable { get; set; }

		public bool Unsigned { get; set; }

		public bool UsePlusOneEncoding { get; set; }

		public float MinFloatValue { get; set; }

		public float MaxFloatValue { get; set; }

		public bool FloatFlag { get; set; }

		public int BitLength { get; set; }
	}
}
