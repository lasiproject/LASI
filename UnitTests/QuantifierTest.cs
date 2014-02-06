using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for QuantifierTest and is intended
    ///to contain all QuantifierTest Unit Tests
    ///</summary>
    [TestClass()]
    public class QuantifierTest
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
        ///A test for Quantifies
        ///</summary>
        [TestMethod()]
        public void QuantifiesTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Quantifier target = new Quantifier(text); // TODO: Initialize to an appropriate value
            IQuantifiable expected = null; // TODO: Initialize to an appropriate value
            IQuantifiable actual;
            target.Quantifies = expected;
            actual = target.Quantifies;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for QuantifiedBy
        ///</summary>
        [TestMethod()]
        public void QuantifiedByTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Quantifier target = new Quantifier(text); // TODO: Initialize to an appropriate value
            IQuantifier expected = null; // TODO: Initialize to an appropriate value
            IQuantifier actual;
            target.QuantifiedBy = expected;
            actual = target.QuantifiedBy;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Quantifier Constructor
        ///</summary>
        [TestMethod()]
        public void QuantifierConstructorTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Quantifier target = new Quantifier(text);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
