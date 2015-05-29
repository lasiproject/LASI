using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for InvertedClauseBeginPhraseTest and is intended
    ///to contain all InvertedClauseBeginPhraseTest Unit Tests
    /// </summary>
    [TestClass]
    public class InvertedClauseBeginPhraseTest
    {
        /// <summary>
        ///A test for InvertedClauseBeginPhrase Constructor
        /// </summary>
        [TestMethod]
        public void InvertedClauseBeginPhraseConstructorTest()
        {
            IEnumerable<Word> composed = null;
            InvertedClauseBeginPhrase target = new InvertedClauseBeginPhrase(composed);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
