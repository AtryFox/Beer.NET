using System.Collections.Generic;

namespace DerAtrox.BeerNET {
    public class Beer : IBeerEncoder {
        /// <summary>
        /// All letters of the latin alphabet which are used in Beer.
        /// </summary>
        private static readonly List<char> LowerCase = new List<char>() { 'q', 'w', 'e', 'r', 't', 'z', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'y', 'x', 'c', 'v', 'b', 'n', 'm' };

        /// <summary>
        /// Serializes a string into Beer.
        /// </summary>
        /// <param name="input">The string to serialize.</param>
        /// <returns>Serialized string.</returns>
        public string SerializeBeer(string input) {
            input = input.Replace(".", "BEER-BEER∫");
            input = input.Replace(",", "BEER_BEER∫");

            for (int i = 0; i < LowerCase.Count; i++) {
                if (!new List<char>() { 'b', 'e', 'r' }.Contains(LowerCase[i])) {
                    input = input.Replace(LowerCase[i].ToString().ToUpper(), RepeateString('∫', "µ", i));
                }

                input = input.Replace(LowerCase[i].ToString(), RepeateString('∫', "BEER", i));

            }

            return input;
        }

        /// <summary>
        /// Deserializes a string from Beer.
        /// </summary>
        /// <param name="input">The string to deserialize.</param>
        /// <returns>Deserialized string.</returns>
        public string DeserializeBeer(string input) {
            input = input.Replace("BEER-BEER∫", ".");
            input = input.Replace("BEER_BEER∫", ",");

            for (int i = LowerCase.Count - 1; i >= 0; i--) {
                if (!new List<char>() { 'b', 'e', 'r' }.Contains(LowerCase[i])) {
                    input = input.Replace(RepeateString('∫', "µ", i), LowerCase[i].ToString().ToUpper());
                }

                input = input.Replace(RepeateString('∫', "BEER", i), LowerCase[i].ToString());
            }

            return input;
        }

        /// <summary>
        /// Creates a repated string and adds a final character.
        /// </summary>
        /// <param name="finalChar">The last character to add to the string.</param>
        /// <param name="stringToRepeat">The string to repeat.</param>
        /// <param name="count">The number of times the stringToRepeat is repeated.</param>
        /// <returns>Repeated string with final character.</returns>
        private static string RepeateString(char finalChar, string stringToRepeat, int count) {
            string result = stringToRepeat;

            for (int i = 0; i < count; i++) {
                result += stringToRepeat;
            }

            return result + finalChar;
        }
    }
}
