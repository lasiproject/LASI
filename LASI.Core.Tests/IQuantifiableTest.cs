using NFluent;
using Xunit;

namespace LASI.Core.Tests
{

    /// <summary>
    ///This is a test class for IQuantifiableTest and is intended
    ///to contain all IQuantifiableTest Unit Tests
    /// </summary>
    public class IQuantifiableTest
    {
        private static IQuantifiable CreateIQuantifiable() => new CommonPluralNoun("mittens");

        /// <summary>
        ///A test for QuantifiedBy
        /// </summary>
        [Fact]
        public void QuantifiedByTest()
        {
            var target = CreateIQuantifiable();
            IQuantifier expected = new Quantifier("all");
            IQuantifier actual;
            target.QuantifiedBy = expected;
            actual = target.QuantifiedBy;
            Check.That(expected).IsEqualTo(actual);
        }
    }
}
