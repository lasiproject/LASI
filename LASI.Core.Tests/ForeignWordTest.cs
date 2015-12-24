using System;
using NFluent;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for ForeignWordTest and is intended
    ///to contain all ForeignWordTest Unit Tests
    /// </summary>
    public class ForeignWordTest
    {
        /// <summary>
        ///A test for ForeignWord Constructor
        /// </summary>
        [Fact]
        public void ForeignWordConstructorTest()
        {
            string text = "Bonjour";
            ForeignWord target = new ForeignWord(text);
            Check.That(target.Text).IsEqualTo(text);
            Check.That(target.UsedAsType).IsNull();
        }

        /// <summary>
        ///A test for UsedAsType
        /// </summary>
        [Fact]
        public void UsedAsTypeTest()
        {
            string text = "Bonjour";
            ForeignWord target = new ForeignWord(text);
            Type expected = typeof(Interjection);
            Type actual;
            target.UsedAsType = expected;
            actual = target.UsedAsType;
            Check.That(expected).IsEqualTo(actual);
        }


    }
}
