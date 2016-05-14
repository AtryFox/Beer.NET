using System;

namespace DerAtrox.BeerNET.Test {
	class Program {
		static void Main(string[] args) {
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			while (true) {
				Console.Clear();

				Console.Write("Input: ");
				string input = Console.ReadLine();

				string serialized = Beer.SerializeBeer(input);
				string deserialized = Beer.DeserializeBeer(serialized);

				Console.WriteLine("\nSerialized string to:");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine(serialized);
				Console.ResetColor();

				Console.WriteLine("\nDeserialized to:");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine(deserialized);
				Console.ResetColor();


				if (deserialized == input) {
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("\nDeserialized string matches input!");
				}
				else {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\nDeserialized string doesn't match input! Please report this on GitHub!");
				}
				Console.ResetColor();

				Console.Write("\nPress [enter] to continue...");
				Console.ReadLine();
			}

			//Console.WriteLine(Beer.DeserializeBeer("BEER∫BEERBEER∫BEERBEERBEER∫"));
			//Console.ReadLine();
		}
	}
}
