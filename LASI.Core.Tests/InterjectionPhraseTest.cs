using System.Collections.Generic;
using NFluent;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for InterjectionPhraseTest and is intended
    ///to contain all InterjectionPhraseTest Unit Tests
    /// </summary>
    public class InterjectionPhraseTest
    {
        /// <summary>
        ///A test for InterjectionPhrase Constructor
        /// </summary>
        [Fact]
        public void InterjectionPhraseConstructorTest()
        {
            IEnumerable<Word> composed = new Word[] { new Preposition("by"), new Interjection("jove") };
            var target = new InterjectionPhrase(composed);
            Check.That(target.Text).IsEqualTo("by jove");
            Check.That(target.Words).ContainsExactly(composed);
        }
    }
}
