using NFluent;
using Xunit;

namespace LASI.Core.Tests
{

    /// <summary>
    ///This is a test class for DoubleQuoteTest and is intended
    ///to contain all DoubleQuoteTest Unit Tests
    /// </summary>
    public class DoubleQuoteTest
    {
        /// <summary>
        ///A test for DoubleQuote Constructor
        /// </summary>
        [Fact]
        public void DoubleQuoteConstructorTest()
        {
            var target = new DoubleQuote();
            Check.That(target.PairedWith).IsNull();
            Check.That(target.Text).IsEqualTo("\"");
        }

        /// <summary>
        ///A test for PairWith
        /// </summary>
        [Fact]
        public void PairWithTest()
        {
            var target = new DoubleQuote();
            var complement = new DoubleQuote();

            target.PairWith(complement);
            Check.That(target.PairedWith).IsSameReferenceAs(complement);

            target.PairWith(target);

            Check.That(target.PairedWith).Not.IsSameReferenceAs(target);
        }
    }
}
