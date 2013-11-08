using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests
{


    /// <summary>
    ///This is a test class for ConjunctionPhraseTest and is intended
    ///to contain all ConjunctionPhraseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConjunctionPhraseTest
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
        ///A test for ConjunctionPhrase Constructor
        ///</summary>
        [TestMethod()]
        public void ConjunctionPhraseConstructorTest() {
            IEnumerable<Word> composedWords = new[] { new Conjunction("or") };
            ConjunctionPhrase target = new ConjunctionPhrase(composedWords);
            Assert.AreEqual(target.Text, string.Join(" ", composedWords.Select(w => w.Text)));
        }

        /// <summary>
        ///A test for JoinedLeft
        ///</summary>
        [TestMethod()]
        public void JoinedLeftTest() {
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
        ///</summary>
        [TestMethod()]
        public void JoinedRightTest() {
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
