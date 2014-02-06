using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for PossessivePronounTest and is intended
    ///to contain all PossessivePronounTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PossessivePronounTest
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
        ///A test for PossessesFor
        ///</summary>
        [TestMethod()]
        public void PossessesForTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            PossessivePronoun target = new PossessivePronoun(text); // TODO: Initialize to an appropriate value
            IPossesser expected = null; // TODO: Initialize to an appropriate value
            IPossesser actual;
            target.PossessesFor = expected;
            actual = target.PossessesFor;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Possessed
        ///</summary>
        [TestMethod()]
        public void PossessedTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            PossessivePronoun target = new PossessivePronoun(text); // TODO: Initialize to an appropriate value
            IEnumerable<IPossessable> actual;
            actual = target.Possessed;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            PossessivePronoun target = new PossessivePronoun(text); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddPossession
        ///</summary>
        [TestMethod()]
        public void AddPossessionTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            PossessivePronoun target = new PossessivePronoun(text); // TODO: Initialize to an appropriate value
            IPossessable possession = null; // TODO: Initialize to an appropriate value
            target.AddPossession(possession);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PossessivePronoun Constructor
        ///</summary>
        [TestMethod()]
        public void PossessivePronounConstructorTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            PossessivePronoun target = new PossessivePronoun(text);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
