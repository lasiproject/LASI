using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;

namespace LASI.Core.Tests.PatternMatching
{

    [TestClass]
    public class PhraseAsSequentialPatternMatchingTest
    {
        [TestMethod]
        public void NounPhraseTest1()
        {
            bool actual = false;
            var target = new NounPhrase(new Determiner("The"), new CommonSingularNoun("truth"));
            bool expected = true;
            target.MatchSequence()
                .Case((Determiner d, IEntity e) => actual = true);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void NounPhraseTest2()
        {
            bool actual = false;
            var target = new NounPhrase(new Determiner("The"), new CommonSingularNoun("truth"));
            bool expected = true;
            target.MatchSequence()
                .Case((Determiner d, IDeterminable e) => actual = true);
            Assert.AreEqual(expected, actual);
        }
    }
}
