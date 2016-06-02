using NUnit.Framework;
// ReSharper disable UseNameofExpression

namespace DerAtrox.BeerNET.Tests {
    [TestFixture]
    public class BeerTest {
        public static readonly IBeerEncoder[] Encoders = {
            new Beer(),
            new BeerEx()
        };

        [Test, TestCaseSource("Encoders")]
        public void TestSerialize(IBeerEncoder encoder) {
            string serialized = encoder.Encode("qpalym._#ä");
            Assert.AreEqual(serialized, "BEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEER-BEER∫_#ä");
        }

        [Test, TestCaseSource("Encoders")]
        public void TestDeserialize(IBeerEncoder encoder) {
            string deserialized = encoder.Decode("BEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEER-BEER∫_#ä");
            Assert.AreEqual(deserialized, "qpalym._#ä");
        }

        [Test, TestCaseSource("Encoders")]
        public void TestLoop(IBeerEncoder encoder) {
            Assert.AreEqual(encoder.Decode(encoder.Encode("QPqpALalYMymberBER,.-12#")), "QPqpALalYMymberBER,.-12#");
        }

        [Test, TestCaseSource("Encoders")]
        public void TestInvalidDelimiter(IBeerEncoder encoder)
        {
            var input = "BEE∫BEER∫";
            var expected = "BEE∫q";
            var actual = encoder.Decode(input);
            Assert.AreEqual(expected, actual);
        }

        [Test, TestCaseSource("Encoders")]
        public void TestMixedCases(IBeerEncoder encoder)
        {
            var input = "BEERµµ∫BEER∫µµBEER∫";
            var expected = "BEERWqµµq";
            var actual = encoder.Decode(input);
            Assert.AreEqual(expected, actual);
        }
    }
}