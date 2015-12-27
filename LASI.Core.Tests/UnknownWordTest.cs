using NFluent;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for UnknownWordTest and is intended
    ///to contain all UnknownWordTest Unit Tests
    /// </summary>
    public class UnknownWordTest
    {
        /// <summary>
        ///A test for UnknownWord Constructor
        /// </summary>
        [Fact]
        public void UnknownWordConstructorTest()
        {
            string text = "qmilgestroph";
            UnknownWord target = new UnknownWord(text);
            Check.That(target.Text).IsEqualTo(text);
        }
    }
}
