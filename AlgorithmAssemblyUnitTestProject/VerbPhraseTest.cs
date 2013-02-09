using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for DetermineHeadWord
        ///</summary>
        [TestMethod()]
        public void DetermineHeadWordTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            target.DetermineHeadWord();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ModifyWith
        ///</summary>
        [TestMethod()]
        public void ModifyWithTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            IAdverbial adv = null; // TODO: Initialize to an appropriate value
            target.ModifyWith(adv);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BoundSubject
        ///</summary>
        [TestMethod()]
        public void BoundSubjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            IEntity expected = null; // TODO: Initialize to an appropriate value
            IEntity actual;
            target.BoundSubject = expected;
            actual = target.BoundSubject;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Modality
        ///</summary>
        [TestMethod()]
        public void ModalityTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            Modal expected = null; // TODO: Initialize to an appropriate value
            Modal actual;
            target.Modality = expected;
            actual = target.Modality;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Modifiers
        ///</summary>
        [TestMethod()]
        public void ModifiersTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            VerbPhrase target = new VerbPhrase(composedWords); // TODO: Initialize to an appropriate value
            List<IAdverbial> expected = null; // TODO: Initialize to an appropriate value
            List<IAdverbial> actual;
            target.Modifiers = expected;
            actual = target.Modifiers;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
