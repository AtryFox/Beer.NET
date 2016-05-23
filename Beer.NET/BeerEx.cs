using System.Collections.Generic;
using System.Text;

namespace DerAtrox.BeerNET {
    public class BeerEx {
        private const char delimiter = '∫';
        private const string lowerAlphabet = "qwertzuiopasdfghjklyxcvbnm";
        private const string upperAlphabet = "QWERTZUIOPASDFGHJKLYXCVBNM";

        private static Dictionary<char, string> substitutions;

        static BeerEx() {
            substitutions = new Dictionary<char, string>();
            for(int i = 0; i < lowerAlphabet.Length; i++) {
                substitutions.Add(lowerAlphabet[i], RepeatString("BEER", i + 1) + delimiter);
                substitutions.Add(upperAlphabet[i], RepeatString("µ", i + 1) + delimiter);
            }

            substitutions['B'] = "B";
            substitutions['E'] = "E";
            substitutions['R'] = "R";
            substitutions['.'] = "BEER-BEER" + delimiter;
            substitutions[','] = "BEER_BEER" + delimiter;
        }

        private static string RepeatString(string input, int count) {
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < count; i++) {
                builder.Append(input);
            }
            return builder.ToString();
        }

        public static string SerializeBeer(string input) {
            StringBuilder builder = new StringBuilder();
            foreach (var str in input ) {
                builder.Append(Substitute(str));
            }
            return builder.ToString();
        }

        private static string Substitute(char c) {
            string substitution;
            if(substitutions.TryGetValue(c, out substitution)) {
                return substitution;
            } else {
                return c.ToString();
            }
        }

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
            None = 0
        }

        public static string DeserializeBeer(string input) {
            StringBuilder builder = new StringBuilder();
            foreach(var c in DeserializeBeerImpl(input)) {
                builder.Append(c);
            }
            return builder.ToString();
        }

        private static IEnumerable<char> DeserializeBeerImpl(string input) {
            State state = State.None;
            StringBuilder buffer = new StringBuilder();
            int counter = 0;
            bool my = false;
            bool isValid = false;
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
                        } else if(state == State.R1) {
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
                        if(state == State.E12) {
                            state = State.R1;
                        } else if(state == State.E22) {
                            state = State.R2;
                        } else {
                            isValid = false;
                        }
                        break;
                    case '-':
                        if(state == State.R1) {
                            state = State.Hyphen;
                            hyphen = true;
                        } else {
                            isValid = false;
                        }
                        break;
                    case '_':
                        if(state == State.R1) {
                            state = State.Underscore;
                            hyphen = false;
                        } else {
                            isValid = false;
                        }
                        break;
                    case 'µ':
                        if(state == State.None) {
                            counter++;
                            my = true;
                        } else {
                            isValid = false;
                        }
                        break;
                    case delimiter:
                        if(state == State.R1) {
                            counter++;
                            yield return (my ? upperAlphabet : lowerAlphabet)[counter - 1];
                            counter = 0;
                            buffer = new StringBuilder();
                        } else if(state == State.R2) {
                            yield return hyphen ? '.' : ',';
                            buffer = new StringBuilder();
                        } else {
                            isValid = false;
                        }
                        state = State.None;
                        counter = 0;
                        break;
                    default:
                        isValid = false;
                        break;
                }

                if (!isValid) {
                    foreach(var backlog in buffer.ToString()) {
                        yield return backlog;
                        buffer = new StringBuilder();
                    }
                }
            }
        }
    }
}
