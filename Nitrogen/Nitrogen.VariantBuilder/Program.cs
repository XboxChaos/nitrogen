using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Nitrogen.VariantBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Nitrogen Variant Builder";
            var originalColor = Console.ForegroundColor;

            // Print header.
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Nitrogen Variant Builder" + '\u008D' + "test");
            Console.WriteLine();
            Console.ForegroundColor = originalColor;

            // Get all variants in this assembly.
            var vault = FindVariants();

            // List all variants.
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < vault.Length; i++)
                Console.WriteLine("  {0}    {1} by {2}", (i + 1).ToString().PadLeft(3), vault[i].Name, vault[i].Author);
            Console.WriteLine();
            Console.ForegroundColor = originalColor;

            // Get the selected variant.
            int index = 0;
            while (index <= 0 || index > vault.Length)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Which variant would you like to build? ");
                    Console.ForegroundColor = originalColor;

                    if (args.Length > 0 && Int32.TryParse(args[0], out index))
                        Console.WriteLine(args[0]);
                    else
                        index = Convert.ToInt32(Console.ReadLine());
                }
                catch { }

                if (args.Length > 0)
                    args[0] = "";
            }
            index--;

            // Get output path.
            string output = null;
            while (String.IsNullOrEmpty(output))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Output Path? ");
                Console.ForegroundColor = originalColor;

                if (args.Length > 1)
                {
                    output = args[1].Trim();
                    Console.WriteLine(output);
                }
                else
                {
                    output = Console.ReadLine().Trim();
                }

                // Make sure this isn't a directory.
                if (Directory.Exists(output) || output.EndsWith("/") || output.EndsWith("\\"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: This is a directory");
                    Console.ForegroundColor = originalColor;

                    output = null;
                    if (args.Length > 1)
                        args = new string[0];
                }
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Building {0}...", vault[index].Name);
            Console.ForegroundColor = originalColor;

            // Build the selected variant.
            vault[index].Build(output);

            Console.WriteLine("Done!");
            Thread.Sleep(500);
        }

        private static Variant[] FindVariants()
        {
            // Temp code; TODO: Use Reflection to find all variants.
            return new List<Variant>()
            {
                new Vault.Race(),
            }.ToArray();
        }
    }
}
