using NUnit.Framework;

namespace DerAtrox.BeerNET.Tests {
    [TestFixture]
    public class BeerTest {
        static IBeerEncoder[] encoders = {
            new Beer(),
            new BeerEx()
        };

        [Test, TestCaseSource("encoders")]
        public void TestSerialize(IBeerEncoder encoder) {
            string serialized = encoder.SerializeBeer("qpalym._#ä");
            Assert.AreEqual(serialized, "BEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEER-BEER∫_#ä");
        }

        [Test, TestCaseSource("encoders")]
        public void TestDeserialize(IBeerEncoder encoder) {
            string deserialized = encoder.DeserializeBeer("BEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEERBEER∫BEER-BEER∫_#ä");
            Assert.AreEqual(deserialized, "qpalym._#ä");
        }

        [Test, TestCaseSource("encoders")]
        public void TestLoop(IBeerEncoder encoder) {
            Assert.AreEqual(encoder.DeserializeBeer(encoder.SerializeBeer("QPqpALalYMymberBER,.-12#")), "QPqpALalYMymberBER,.-12#");
        }

        [Test, TestCaseSource("encoders")]
        public void TestInvalidDelimiter(IBeerEncoder encoder)
        {
            var input = "BEE∫BEER∫";
            var expected = "BEE∫q";
            var actual = encoder.DeserializeBeer(input);
            Assert.AreEqual(expected, actual);
        }

        [Test, TestCaseSource("encoders")]
        public void TestMixedCases(IBeerEncoder encoder)
        {
            var input = "BEERµµ∫BEER∫µµBEER∫";
            var expected = "BEERWqµµq";
            var actual = encoder.DeserializeBeer(input);
            Assert.AreEqual(expected, actual);
        }
    }
}