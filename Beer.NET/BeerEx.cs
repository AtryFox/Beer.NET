using System.Collections.Generic;
using System.Text;

namespace DerAtrox.BeerNET {
    public class BeerEx : IBeerEncoder {
        /// <summary>
        /// The delimiter that is used to mark the end of a character in Beer
        /// </summary>
        private const char delimiter = '∫';
        /// <summary>
        /// The latin alphabet as used by Beer
        /// </summary>
        private const string lowerAlphabet = "qwertzuiopasdfghjklyxcvbnm";
        /// <summary>
        /// The latin alphabet as used by Beer, but in upper case :)
        /// </summary>
        private const string upperAlphabet = "QWERTZUIOPASDFGHJKLYXCVBNM";

        /// <summary>
        /// Static dictionary of subsitutions
        /// </summary>
        private static Dictionary<char, string> substitutions;

        /// <summary>
        /// Static constructor to initialize substitutions
        /// </summary>
        static BeerEx() {
            substitutions = new Dictionary<char, string>();
            for (int i = 0; i < lowerAlphabet.Length; i++) {
                substitutions.Add(lowerAlphabet[i], RepeatString("BEER", i + 1) + delimiter);
                substitutions.Add(upperAlphabet[i], RepeatString("µ", i + 1) + delimiter);
            }

            substitutions['B'] = "B";
            substitutions['E'] = "E";
            substitutions['R'] = "R";
            substitutions['.'] = "BEER-BEER" + delimiter;
            substitutions[','] = "BEER_BEER" + delimiter;
        }

        /// <summary>
        /// Repeats a string <paramref name="input"/> <paramref name="count"/>-times
        /// </summary>
        /// <param name="input">The string to repeat</param>
        /// <param name="count">How often to repeat the string</param>
        /// <returns>The input string, repeated n-times</returns>
        private static string RepeatString(string input, int count) {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < count; i++) {
                builder.Append(input);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Serializes a string into Beer.
        /// </summary>
        /// <param name="input">The string to serialize.</param>
        /// <returns>Serialized string.</returns>
        public string SerializeBeer(string input) {
            StringBuilder builder = new StringBuilder();
            foreach (var str in input) {
                builder.Append(Substitute(str));
            }
            return builder.ToString();
        }

        /// <summary>
        /// Determines the substitution for a single character
        /// </summary>
        /// <param name="c">The character to substitute</param>
        /// <returns>The matching substitution for the character</returns>
        private static string Substitute(char c) {
            string substitution;
            if (substitutions.TryGetValue(c, out substitution)) {
                return substitution;
            } else {
                return c.ToString();
            }
        }

        /// <summary>
        /// Deserializes a string from Beer.
        /// </summary>
        /// <param name="input">The string to deserialize.</param>
        /// <returns>Deserialized string.</returns>
        public string DeserializeBeer(string input) {
            StringBuilder builder = new StringBuilder();
            foreach (var c in DeserializeBeerImpl(input)) {
                builder.Append(c);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Represents the current state of the finite state machine implemented by <see cref="DeserializeBeerImpl(string)"/>
        /// </summary>
        private enum State {
            B1 = 1,
            E11 = 2,
            E12 = 4,
            R1 = 8,
            Hyphen = 16,
            Underscore = 32,
            B2 = 64,
            E21 = 128,
            E22 = 256,
            R2 = 512,
            My = 1024,
            None = 0
        }

        /// <summary>
        /// Enumerator method that yields single characters as soon as they're discovered
        /// </summary>
        /// <param name="input">The input string to parse</param>
        /// <returns>IEnumerable </returns>
        private static IEnumerable<char> DeserializeBeerImpl(string input) {
            // The current state of the FSM
            State state = State.None;
            // buffer for the characters discovered since last valid character
            StringBuilder buffer = new StringBuilder();
            // counts appearances of "BEER" or "µ"
            int counter = 0;
            // flag to be set if the character during the current pass was invalid
            bool isValid = false;
            // flag to be set if a hyphen ("-", true) or an underscore ("_") was seen
            bool hyphen = false;

            foreach (var c in input) {
                buffer.Append(c);
                isValid = true;

                switch (c) {
                    case 'B':
                        if (state == State.None) {
                            state = State.B1;
                        } else if (state == State.Hyphen || state == State.Underscore) {
                            state = State.B2;
                        } else if (state == State.R1) {
                            counter++;
                            state = State.B1;
                        } else {
                            isValid = false;
                        }
                        break;
                    case 'E':
                        if (state == State.B1) {
                            state = State.E11;
                        } else if (state == State.E11) {
                            state = State.E12;
                        } else if (state == State.B2) {
                            state = State.E21;
                        } else if (state == State.E21) {
                            state = State.E22;
                        } else {
                            isValid = false;
                        }
                        break;
                    case 'R':
                        if (state == State.E12) {
                            state = State.R1;
                        } else if (state == State.E22) {
                            state = State.R2;
                        } else {
                            isValid = false;
                        }
                        break;
                    case '-':
                        if (state == State.R1) {
                            state = State.Hyphen;
                            hyphen = true;
                        } else {
                            isValid = false;
                        }
                        break;
                    case '_':
                        if (state == State.R1) {
                            state = State.Underscore;
                            hyphen = false;
                        } else {
                            isValid = false;
                        }
                        break;
                    case 'µ':
                        if (state == State.None) {
                            state = State.My;
                        } else if (state == State.My) {
                            counter++;
                        } else {
                            isValid = false;
                        }
                        break;
                    case delimiter:
                        if (state == State.R1) {
                            yield return lowerAlphabet[counter];
                        } else if (state == State.My) {
                            yield return upperAlphabet[counter];
                        } else if (state == State.R2) {
                            yield return hyphen ? '.' : ',';
                        } else {
                            isValid = false;
                        }
                        buffer = new StringBuilder();
                        state = State.None;
                        counter = 0;
                        break;
                    default:
                        isValid = false;
                        break;
                }

                // if the current character did not meet the expectations based on
                // the current state, yield everything from the buffer, clean it
                // and reset the state
                if (!isValid) {
                    foreach (var backlog in buffer.ToString()) {
                        yield return backlog;
                    }
                    buffer = new StringBuilder();
                    state = State.None;
                }
            }
        }
    }
}
