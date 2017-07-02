using NFluent;
using Xunit;

namespace LASI.Core.Tests
{

    /// <summary>
    ///This is A test class for GenericPluralNounTest and is intended
    ///to contain all GenericPluralNounTest Unit Tests
    /// </summary>
    public class CommonPluralNounTest
    {
        /// <summary>
        ///A test for CommonPluralNoun Constructor
        /// </summary>
        [Fact]
        public void CommonPluralNounConstructorTest()
        {
            var text = "clowns";
            var target = new CommonPluralNoun(text);
            Check.That(target.Text).IsEqualTo(text);
        }

        /// <summary>
        ///A test for QuantifiedBy
        /// </summary>
        [Fact]
        public void QuantifiedByTest()
        {
            var text = "clowns";
            var target = new CommonPluralNoun(text);
            IQuantifier expected = new Quantifier("all");
            IQuantifier actual;
            target.QuantifiedBy = expected;
            actual = target.QuantifiedBy;
            Check.That(expected).IsEqualTo(actual);
        }
    }
}
