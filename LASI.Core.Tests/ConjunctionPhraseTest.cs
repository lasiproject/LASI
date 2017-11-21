using System.Linq;
using System.Collections.Generic;
using NFluent;
using Xunit;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is a test class for ConjunctionPhraseTest and is intended
    ///to contain all ConjunctionPhraseTest Unit Tests
    /// </summary>
    public class ConjunctionPhraseTest
    {
        /// <summary>
        ///A test for ConjunctionPhrase Constructor
        /// </summary>
        [Fact]
        public void ConjunctionPhraseConstructorTest()
        {
            IEnumerable<Word> composedWords = new[] { new Conjunction("or") };
            var target = new ConjunctionPhrase(composedWords);
            Check.That(target.Text).IsEqualTo(string.Join(" ", composedWords.Select(w => w.Text)));
        }

        /// <summary>
        ///A test for JoinedLeft
        /// </summary>
        [Fact]
        public void JoinedLeftTest()
        {
            IEnumerable<Word> composedWords = new[] { new Conjunction("and") };
            var target = new ConjunctionPhrase(composedWords);
            ILexical expected = new NounPhrase(new[] { new CommonSingularNoun("cake") });
            ILexical actual;
            target.JoinedLeft = expected;
            actual = target.JoinedLeft;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for JoinedRight
        /// </summary>
        [Fact]
        public void JoinedRightTest()
        {
            IEnumerable<Word> composedWords = new[] { new Conjunction("and") };
            var target = new ConjunctionPhrase(composedWords);
            ILexical expected = new CommonPluralNoun("pies");
            ILexical actual;
            target.JoinedRight = expected;
            actual = target.JoinedRight;
            Check.That(actual).IsEqualTo(expected);
        }
    }
}
