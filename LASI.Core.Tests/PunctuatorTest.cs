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
            char puncChar = '\u0021';
            Punctuator target = new Punctuator(puncChar);
            Check.That(target.LiteralCharacter).IsEqualTo(puncChar);
        }


        /// <summary>
        ///A test for Text
        /// </summary>
        [Fact]
        public void TextTest()
        {
            char puncChar = '\u0021';
            Punctuator target = new Punctuator(puncChar);
            string expected = "!";
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
            string punctuation = "!";
            Punctuator target = new Punctuator(punctuation);
            string expected = "!";
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
            char punctuation = '.';
            Punctuator target = new Punctuator(punctuation);
            char expected = '.';
            char actual = target.LiteralCharacter;
            Check.That(expected).IsEqualTo(actual);
        }
    }
}
