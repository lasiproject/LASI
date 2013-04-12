using LASI.Algorithm;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LASI.Algorithm.FundamentalSyntacticInterfaces;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for PrepositionalPhraseTest and is intended
    ///to contain all PrepositionalPhraseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PrepositionalPhraseTest
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
        ///A test for PrepositionalPhrase Constructor
        ///</summary>
        [TestMethod()]
        public void PrepositionalPhraseConstructorTest() {
            IEnumerable<Word> composedWords = new[] { new Preposition("on") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            Assert.IsTrue(target.Words.Count() == composedWords.Count());
            Assert.IsTrue(target.Text == "on" && target.OnLeftSide == null && target.OnRightSide == null && target.PrepositionalObject == null);

        }

        /// <summary>
        ///A test for BindObjectOfPreposition
        ///</summary>
        [TestMethod()]
        public void BindObjectOfPrepositionTest() {
            IEnumerable<Word> composedWords = new[] { new ToLinker() };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords); // TODO: Initialize to an appropriate value
            ILexical prepositionalObject = new VerbPhrase(new Word[] { new Verb("have", VerbTense.Base) });
            target.BindObjectOfPreposition(prepositionalObject);
            Assert.IsTrue(target.PrepositionalObject == prepositionalObject);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            target.OnLeftSide = new NounPhrase(new[] { new PersonalPronoun("it") });
            target.OnRightSide = new VerbPhrase(new[] { new PresentParticipleGerund("slamming") });
            string expected = String.Format("PrepositionalPhrase \"for\"\n\tleft linked: {0}\n\tright linked: {1}", target.OnLeftSide, target.OnRightSide);
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for PrepositionalRole
        ///</summary>
        [TestMethod()]
        public void PrepositionalRoleTest() {
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            PrepositionalRole expected = PrepositionalRole.Undetermined;
            PrepositionalRole actual;
            actual = target.PrepositionalRole;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for OnLeftSide
        ///</summary>
        [TestMethod()]
        public void OnLeftSideTest() {
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            IPrepositionLinkable expected = new NounPhrase(new[] { new PersonalPronoun("it") });
            IPrepositionLinkable actual;
            target.OnLeftSide = expected;
            actual = target.OnLeftSide;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for OnRightSide
        ///</summary>
        [TestMethod()]
        public void OnRightSideTest() {
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            IPrepositionLinkable expected = new VerbPhrase(new[] { new PresentParticipleGerund("slamming") });
            IPrepositionLinkable actual;
            target.OnRightSide = expected;
            actual = target.OnRightSide;
            Assert.AreEqual(expected, actual);
        }
    }
}
