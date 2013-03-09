using LASI.Algorithm;
using LASI.Algorithm.FundamentalSyntacticInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for VerbPhraseTest and is intended
    ///to contain all VerbPhraseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VerbPhraseTest
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
        ///A test for VerbPhrase Constructor
        ///</summary>
        [TestMethod()]
        public void VerbPhraseConstructorTest() {
            IEnumerable<Word> composedWords = new Word[] { new Verb("run", VerbTense.Base), new Adverb("swiftly"), new Preposition("through") };
            VerbPhrase target = new VerbPhrase(composedWords);
            Assert.IsTrue(composedWords == target.Words);
        }
        /// <summary>
        ///A test for ModifyWith
        ///</summary>
        [TestMethod()]
        public void ModifyWithTest() {
            IEnumerable<Word> composedWords = new Word[] { new Verb("run", VerbTense.Base), new Adverb("swiftly"), new Preposition("through") };
            VerbPhrase target = new VerbPhrase(composedWords);
            IAdverbial adv = new Adverb("daringly");
            target.ModifyWith(adv);
            Assert.IsTrue(target.Modifiers.Contains(adv));
        }

        /// <summary>
        ///A test for BoundSubject
        ///</summary>
        [TestMethod()]
        public void BoundSubjectTest() {
            IEnumerable<Word> composedWords = new Word[] { new Verb("ran", VerbTense.Past), new Adverb("swiftly"), new Preposition("through") };
            VerbPhrase target = new VerbPhrase(composedWords);
            IEntity expected = new PersonalPronoun("he");
            IEntity actual;
            target.BindSubject(expected);
            actual = target.BoundSubjects.First();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Modality
        ///</summary>
        [TestMethod()]
        public void ModalityTest() {
            IEnumerable<Word> composedWords = new Word[] { new Verb("run", VerbTense.Base), new Adverb("swiftly"), new Preposition("through") };
            VerbPhrase target = new VerbPhrase(composedWords);
            ModalAuxilary expected = new ModalAuxilary("cannot");
            ModalAuxilary actual;
            target.Modality = expected;
            actual = target.Modality;
            Assert.AreEqual(expected, actual);
        }



    }
}
