using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for SingleQuoteTest and is intended
    ///to contain all SingleQuoteTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SingleQuoteTest
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
        ///A test for PairWith
        ///</summary>
        [TestMethod()]
        public void PairWithTest() {
            SingleQuote target = new SingleQuote(); // TODO: Initialize to an appropriate value
            SingleQuote complement = null; // TODO: Initialize to an appropriate value
            target.PairWith(complement);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SingleQuote Constructor
        ///</summary>
        [TestMethod()]
        public void SingleQuoteConstructorTest() {
            SingleQuote target = new SingleQuote();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
