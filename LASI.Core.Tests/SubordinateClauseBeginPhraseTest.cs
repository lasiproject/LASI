using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for SubordinateClauseBeginPhraseTest and is intended
    ///to contain all SubordinateClauseBeginPhraseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SubordinateClauseBeginPhraseTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Subordinates
        ///</summary>
        [TestMethod()]
        public void SubordinatesTest() {
            IEnumerable<Word> composed = null; // TODO: Initialize to an appropriate value
            SubordinateClauseBeginPhrase target = new SubordinateClauseBeginPhrase(composed); // TODO: Initialize to an appropriate value
            ILexical expected = null; // TODO: Initialize to an appropriate value
            ILexical actual;
            target.Subordinates = expected;
            actual = target.Subordinates;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EndOfClause
        ///</summary>
        [TestMethod()]
        public void EndOfClauseTest() {
            IEnumerable<Word> composed = null; // TODO: Initialize to an appropriate value
            SubordinateClauseBeginPhrase target = new SubordinateClauseBeginPhrase(composed); // TODO: Initialize to an appropriate value
            Punctuator expected = null; // TODO: Initialize to an appropriate value
            Punctuator actual;
            target.EndOfClause = expected;
            actual = target.EndOfClause;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SubordinateClauseBeginPhrase Constructor
        ///</summary>
        [TestMethod()]
        public void SubordinateClauseBeginPhraseConstructorTest() {
            IEnumerable<Word> composed = null; // TODO: Initialize to an appropriate value
            SubordinateClauseBeginPhrase target = new SubordinateClauseBeginPhrase(composed);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
