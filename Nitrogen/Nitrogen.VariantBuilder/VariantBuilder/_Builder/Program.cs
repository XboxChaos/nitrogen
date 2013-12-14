 using Nitrogen;
using Nitrogen.GameVariants;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Nitrogen.VariantBuilder
{
	class Program
	{
		static void Main (string[] args)
		{
			Console.WriteLine("Nitrogen Variant Builder");

			var variants = ( from type in typeof(Program).Assembly.GetTypes()
							 where Attribute.IsDefined(type, typeof(OutputPathAttribute))
							 select type );

			foreach ( var variant in variants )
			{
				var gt = Activator.CreateInstance(variant) as IGameVariant;
				Console.WriteLine("Creating {0}...", gt.GetType().Name);

				var output = gt.GetType().GetCustomAttribute<OutputPathAttribute>();
				if ( output == null ) { continue; }

				var gameVariant = new GameVariant();
				gt.Create(gameVariant);
				gameVariant.Metadata.Category = 25;

				using ( var fs = File.Create(output.OutputPath) )
					ContentFactory.CreateGameVariant(fs, gameVariant);
			}
		}
	}
}
