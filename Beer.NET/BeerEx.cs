using System.Collections.Generic;
using System.Text;

namespace DerAtrox.BeerNET
{
    public class BeerEx : IBeerEncoder
    {
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
        static BeerEx()
        {
            substitutions = new Dictionary<char, string>();
            for (int i = 0; i < lowerAlphabet.Length; i++)
            {
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
        private static string RepeatString(string input, int count)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                builder.Append(input);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Serializes a string into Beer.
        /// </summary>
        /// <param name="input">The string to serialize.</param>
        /// <returns>Serialized string.</returns>
        public string Encode(string input) {
            StringBuilder builder = new StringBuilder();
            foreach (var str in input)
            {
                builder.Append(Substitute(str));
            }
            return builder.ToString();
        }

        /// <summary>
        /// Determines the substitution for a single character
        /// </summary>
        /// <param name="c">The character to substitute</param>
        /// <returns>The matching substitution for the character</returns>
        private static string Substitute(char c)
        {
            string substitution;
            if (substitutions.TryGetValue(c, out substitution))
            {
                return substitution;
            }
            else
            {
                return c.ToString();
            }
        }

        /// <summary>
        /// Deserializes a string from Beer.
        /// </summary>
        /// <param name="input">The string to deserialize.</param>
        /// <returns>Deserialized string.</returns>
        public string Decode(string input) {
            StringBuilder builder = new StringBuilder();
            foreach (var c in DecodeImpl(input))
            {
                builder.Append(c);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Represents the current state of the finite state machine implemented by <see cref="DecodeImpl(string)"/>
        /// </summary>
        private enum State
        {
            B1,
            E11,
            E12,
            R1,
            Hyphen,
            Underscore,
            B2,
            E21,
            E22,
            R2,
            My,
            None
        }

        /// <summary>
        /// Represents the result of a single character inspection.
        /// </summary>
        private enum PassResult
        {
            /// <summary>
            /// The current character is valid, append it to buffer
            /// </summary>
            Valid,
            /// <summary>
            /// The current character is invalid, yield + clean the buffer + current char
            /// </summary>
            InvalidYield,
            /// <summary>
            /// The current character is invalid, yield + clean the buffer, append current char
            /// </summary>
            InvalidYieldOld
        }

        /// <summary>
        /// Enumerator method that yields single characters as soon as they're discovered
        /// </summary>
        /// <param name="input">The input string to parse</param>
        /// <returns>IEnumerable </returns>
        private static IEnumerable<char> DecodeImpl(string input)
        {
            // The current state of the FSM
            State state = State.None;
            // buffer for the characters discovered since last valid character
            StringBuilder buffer = new StringBuilder();
            // counts appearances of "BEER" or "µ"
            int counter = 0;
            // Result of the current character: Is it valid? What should happen if it's not?
            PassResult result;
            // flag to be set if a hyphen ("-", true) or an underscore ("_", false) was seen
            bool hyphen = false;

            foreach (var c in input)
            {
                result = PassResult.Valid;
                switch (c)
                {
                    case 'B':
                        if (state == State.R1)
                        {
                            counter++;
                            state = State.B1;
                        }
                        else if (state == State.Hyphen || state == State.Underscore)
                        {
                            state = State.B2;
                        }
                        else
                        {
                            state = State.B1;
                            result = PassResult.InvalidYieldOld;
                        }
                        break;
                    case 'E':
                        if (state == State.B1)
                        {
                            state = State.E11;
                        }
                        else if (state == State.E11)
                        {
                            state = State.E12;
                        }
                        else if (state == State.B2)
                        {
                            state = State.E21;
                        }
                        else if (state == State.E21)
                        {
                            state = State.E22;
                        }
                        else
                        {
                            result = PassResult.InvalidYield;
                        }
                        break;
                    case 'R':
                        if (state == State.E12)
                        {
                            state = State.R1;
                        }
                        else if (state == State.E22)
                        {
                            state = State.R2;
                        }
                        else
                        {
                            result = PassResult.InvalidYield;
                        }
                        break;
                    case '-':
                        if (state == State.R1)
                        {
                            state = State.Hyphen;
                            hyphen = true;
                        }
                        else
                        {
                            result = PassResult.InvalidYield;
                        }
                        break;
                    case '_':
                        if (state == State.R1)
                        {
                            state = State.Underscore;
                            hyphen = false;
                        }
                        else
                        {
                            result = PassResult.InvalidYield;
                        }
                        break;
                    case 'µ':
                        if (state == State.My)
                        {
                            counter++;
                        }
                        else
                        {
                            state = State.My;
                            result = PassResult.InvalidYieldOld;
                        }
                        break;
                    case delimiter:
                        var yielded = false;
                        if (state == State.R1)
                        {
                            yield return lowerAlphabet[counter];
                            yielded = true;
                        }
                        else if (state == State.My)
                        {
                            yield return upperAlphabet[counter];
                            yielded = true;
                        }
                        else if (state == State.R2)
                        {
                            yield return hyphen ? '.' : ',';
                            yielded = true;
                        }
                        else
                        {
                            result = PassResult.InvalidYield;
                        }

                        if (yielded)
                        {
                            buffer = new StringBuilder();
                            state = State.None;
                            counter = 0;
                            continue;
                        }

                        break;
                    default:
                        result = PassResult.InvalidYield;
                        break;
                }

                switch (result)
                {
                    case PassResult.Valid:
                        buffer.Append(c);
                        break;
                    case PassResult.InvalidYield:
                        // Yield everything from the buffer, up-to and including the current character
                        foreach (var backlog in buffer.Append(c).ToString())
                        {
                            yield return backlog;
                        }
                        buffer = new StringBuilder();
                        counter = 0;
                        break;
                    case PassResult.InvalidYieldOld:
                        // Yield everything from the buffer
                        foreach (var backlog in buffer.ToString())
                        {
                            yield return backlog;
                        }
                        // Clear the buffer and append the current char
                        buffer = new StringBuilder()
                            .Append(c);
                        counter = 0;
                        break;
                }
            }

            // Yield everything that remained in the buffer
            foreach (var backlog in buffer.ToString())
            {
                yield return backlog;
            }
        }
    }
}
