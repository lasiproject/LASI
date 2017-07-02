using Xunit;

namespace LASI.Core.Tests
{

    /// <summary>
    ///This is a test class for SingleQuoteTest and is intended
    ///to contain all SingleQuoteTest Unit Tests
    /// </summary>
    public class SingleQuoteTest
    { 
        /// <summary>
        ///A test for PairWith
        /// </summary>
        [Fact]
        public void PairWithTest() {
            var target = new SingleQuote();
            var complement = new SingleQuote();
            target.PairWith(complement);
            Assert.Equal(target, complement.PairedWith);
            Assert.Equal(complement.PairedWith, target);
        }

        /// <summary>
        ///A test for SingleQuote Constructor
        /// </summary>
        [Fact]
        public void SingleQuoteConstructorTest() {
            var target = new SingleQuote();
            Assert.True(target.Text == "'");
            Assert.Equal(target.LiteralCharacter, '\'');
        }
    }
}
