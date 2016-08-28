using NFluent;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for ProperPluralNounTest and is intended
    ///to contain all ProperPluralNounTest Unit Tests
    /// </summary>
    public class ProperPluralNounTest
    {
        /// <summary>
        ///A test for ProperPluralNoun Constructor
        /// </summary>
        [Fact]
        public void ProperPluralNounConstructorTest()
        {
            var text = "Canadians";
            var target = new ProperPluralNoun(text);
            Check.That(target.Text).IsEqualTo(text);
        }

        /// <summary>
        ///A test for QuantifiedBy
        /// </summary>
        [Fact]
        public void QuantifierTest()
        {
            var text = "Canadians";
            var target = new ProperPluralNoun(text);
            IQuantifier expected = new Quantifier("5");
            IQuantifier actual;
            target.QuantifiedBy = expected;
            actual = target.QuantifiedBy;
            Check.That(expected).IsEqualTo(actual);

        }



    }
}
