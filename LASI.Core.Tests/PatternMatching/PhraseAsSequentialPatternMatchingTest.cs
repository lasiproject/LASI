﻿using LASI.Core.Heuristics.Binding.Experimental.SequentialPatterns;
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
            target.MatchSequence()
                .Case((Determiner d, IEntity e) => actual = true);
            Check.That(actual).IsTrue();
        }

        [Fact]
        public void NounPhraseTest2()
        {
            var actual = false;
            var target = new NounPhrase(new Determiner("The"), new CommonSingularNoun("truth"));
            target.MatchSequence()
                .Case((Determiner d, IDeterminable e) => actual = true);
            Check.That(actual).IsTrue();
        }
    }
}