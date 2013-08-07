using LASI;
using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AlgorithmAssemblyUnitTestProject
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
    }
}
