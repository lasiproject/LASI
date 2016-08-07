using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;
using NFluent;
using Xunit;

namespace LASI.Core.Tests.PatternMatching
{

    public class PhraseAsSequentialPatternMatchingTest
    {
        [Fact]
        public void NounPhraseTest1()
        {
            var actual = false;
            var target = new NounPhrase(new Determiner("The"), new CommonSingularNoun("truth"));
            var expected = true;
            target.MatchSequence()
                .Case((Determiner d, IEntity e) => actual = true);
            Check.That(expected).IsEqualTo(actual);
        }
        [Fact]
        public void NounPhraseTest2()
        {
            var actual = false;
            var target = new NounPhrase(new Determiner("The"), new CommonSingularNoun("truth"));
            var expected = true;
            target.MatchSequence()
                .Case((Determiner d, IDeterminable e) => actual = true);
            Check.That(expected).IsEqualTo(actual);
        }
    }
}
