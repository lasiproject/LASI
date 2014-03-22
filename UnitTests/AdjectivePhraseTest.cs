using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is A test class for AdjectivePhraseTest and is intended
    ///to contain all AdjectivePhraseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AdjectivePhraseTest
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
        //Use ClassCleanup to run code after all tests in A class have run
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
        ///A test for AdjectivePhrase Constructor
        ///</summary>
        [TestMethod()]
        public void AdjectivePhraseConstructorTest() {
            IEnumerable<Word> composedWords = new Word[] { new Adjective("soft"), new Adjective("smooth"), new Adjective("silky") }; // TODO: Initialize to an appropriate value
            AdjectivePhrase target = new AdjectivePhrase(composedWords);
            Assert.AreEqual(target.Words, composedWords);
        }

        /// <summary>
        ///A test for Modifiers
        ///</summary>
        [TestMethod()]
        public void AdverbialModifiersTest() {
            IEnumerable<Word> composedWords = new Word[] { new Adjective("soft"), new Conjunction("and"), new Adjective("silky") };
            AdjectivePhrase target = new AdjectivePhrase(composedWords);
            IEnumerable<IAdverbial> actual;
            actual = target.AdverbialModifiers;
            Assert.IsTrue(actual.None());
            IAdverbial modifier = new Adverb("very");
            target.ModifyWith(modifier);
            Assert.IsTrue(target.AdverbialModifiers.Contains(modifier));

        }

        /// <summary>
        ///A test for Describes
        ///</summary>
        [TestMethod()]
        public void DescribesTest() {
            IEnumerable<Word> composedWords = new Word[] { new Adverb("very"), new Adjective("tall") };
            AdjectivePhrase target = new AdjectivePhrase(composedWords);
            IEntity expected = new CommonSingularNoun("tree");
            IEntity actual;
            target.Describes = expected;
            actual = target.Describes;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ModifyWith
        ///</summary>
        [TestMethod()]
        public void ModifyWithTest() {
            IEnumerable<Word> composedWords = new Word[] { new Adjective("tall") };
            AdjectivePhrase target = new AdjectivePhrase(composedWords);
            IAdverbial adv = new Adverb("overly");
            target.ModifyWith(adv);
            Assert.IsTrue(target.AdverbialModifiers.Contains(adv));
        }


    }
}
