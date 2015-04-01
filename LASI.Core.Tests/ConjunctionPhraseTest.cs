using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for ConjunctionPhraseTest and is intended
    ///to contain all ConjunctionPhraseTest Unit Tests
    /// </summary>
    [TestClass]
    public class ConjunctionPhraseTest
    {
        /// <summary>
        ///A test for ConjunctionPhrase Constructor
        /// </summary>
        [TestMethod]
        public void ConjunctionPhraseConstructorTest()
        {
            IEnumerable<Word> composedWords = new[] { new Conjunction("or") };
            ConjunctionPhrase target = new ConjunctionPhrase(composedWords);
            Assert.AreEqual(target.Text, string.Join(" ", composedWords.Select(w => w.Text)));
        }

        /// <summary>
        ///A test for JoinedLeft
        /// </summary>
        [TestMethod]
        public void JoinedLeftTest()
        {
            IEnumerable<Word> composedWords = new[] { new Conjunction("and") };
            ConjunctionPhrase target = new ConjunctionPhrase(composedWords);
            ILexical expected = new NounPhrase(new[] { new CommonSingularNoun("cake") });
            ILexical actual;
            target.JoinedLeft = expected;
            actual = target.JoinedLeft;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for JoinedRight
        /// </summary>
        [TestMethod]
        public void JoinedRightTest()
        {
            IEnumerable<Word> composedWords = new[] { new Conjunction("and") };
            ConjunctionPhrase target = new ConjunctionPhrase(composedWords);
            ILexical expected = new CommonPluralNoun("pies");
            ILexical actual;
            target.JoinedRight = expected;
            actual = target.JoinedRight;
            Assert.AreEqual(expected, actual);
        }


    }
}
