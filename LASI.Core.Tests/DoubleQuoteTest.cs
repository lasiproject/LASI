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
            Assert.Equal(target.PairedWith, null);
            Assert.Equal(target.Text, "\"");
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
            Assert.Same(target.PairedWith, complement);
            Assert.Same(target, target.PairedWith.PairedWith);
            target.PairWith(target);
            Assert.NotSame(target.PairedWith, target);
        }
    }
}
