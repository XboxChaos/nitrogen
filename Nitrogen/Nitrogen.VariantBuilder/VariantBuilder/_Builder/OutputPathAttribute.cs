using System;

namespace Nitrogen.VariantBuilder
{
	public sealed class OutputPathAttribute
		: Attribute
	{
		public OutputPathAttribute(string outputPath)
		{
			OutputPath = outputPath;
		}

		public string OutputPath { get; set; }
	}
}
