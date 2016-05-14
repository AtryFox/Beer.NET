using System.Collections.Generic;
using System.Text;

namespace DerAtrox.BeerNET {
	public class Beer {
		private static readonly List<char> LowerCase = new List<char>() { 'q', 'w', 'e', 'r', 't', 'z', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'y', 'x', 'c', 'v', 'b', 'n', 'm' };

		public static string SerializeBeer(string input) {
			input = input.Replace(".", "BEER-BEER∫");
			input = input.Replace(",", "BEER_BEER∫");

			for (int i = 0; i < LowerCase.Count; i++) {
				if (!new List<string>() { "B", "E", "R" }.Contains(LowerCase[i].ToString().ToUpper())) {
					input = input.Replace(LowerCase[i].ToString().ToUpper(), RepeateString('∫', "µ", i));
				}

				input = input.Replace(LowerCase[i].ToString(), RepeateString('∫', "BEER", i));

			}

			return input;
		}

		public static string DeserializeBeer(string input) {
			input = input.Replace("BEER-BEER∫", ".");
			input = input.Replace("BEER_BEER∫", ",");

			for (int i = LowerCase.Count - 1; i >= 0; i--) {
				if (!new List<string>() { "B", "E", "R" }.Contains(LowerCase[i].ToString().ToUpper())) {
					input = input.Replace(RepeateString('∫', "µ", i), LowerCase[i].ToString().ToUpper());
				}

				input = input.Replace(RepeateString('∫', "BEER", i), LowerCase[i].ToString());

			}

			return input;
		}

		private static string RepeateString(char finalChar, string stringToRepeat, int count) {
			string result = stringToRepeat;

			for (int i = 0; i < count; i++) {
				result += stringToRepeat;
			}

			return result + finalChar;
		}
	}
}
