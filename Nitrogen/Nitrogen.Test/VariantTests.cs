using Nitrogen;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.GameVariants.Megalo.ParameterTypes;

namespace Nitrogen.Test
{
	[TestClass]
	public class VariantTests
	{
		[TestMethod]
		public void ReadGametype ()
		{
			MegaloVariant gt;

			using ( var fs = File.OpenRead("C:/Users/Matt/Desktop/h4_rumble_tu.game") )
				gt = (MegaloVariant) ContentFactory.ReadGameVariant(fs);


		}
	}
}