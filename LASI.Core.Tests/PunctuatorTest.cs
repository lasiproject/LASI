using NFluent;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for PunctuatorTest and is intended
    ///to contain all PunctuatorTest Unit Tests
    /// </summary>
    public class PunctuatorTest
    { 
        /// <summary>
        ///A test for Punctuation Constructor
        /// </summary>
        [Fact]
        public void PunctuatorConstructorTest()
        {
            var puncChar = '\u0021';
            var target = new Punctuator(puncChar);
            Check.That(target.LiteralCharacter).IsEqualTo(puncChar);
        }


        /// <summary>
        ///A test for Text
        /// </summary>
        [Fact]
        public void TextTest()
        {
            var puncChar = '\u0021';
            var target = new Punctuator(puncChar);
            var expected = "!";
            string actual;
            actual = target.Text;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for Punctuator Constructor
        /// </summary>
        [Fact]
        public void PunctuatorConstructorTest2()
        {
            var punctuation = "!";
            var target = new Punctuator(punctuation);
            var expected = "!";
            string actual;
            actual = target.Text;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for Punctuator Constructor
        /// </summary>
        [Fact]
        public void PunctuatorConstructorTest3()
        {
            var punctuation = '.';
            var target = new Punctuator(punctuation);
            var expected = '.';
            var actual = target.LiteralCharacter;
            Check.That(expected).IsEqualTo(actual);
        }
    }
}
