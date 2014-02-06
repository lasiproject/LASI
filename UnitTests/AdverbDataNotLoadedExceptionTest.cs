using LASI.Core.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for AdverbDataNotLoadedExceptionTest and is intended
    ///to contain all AdverbDataNotLoadedExceptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AdverbDataNotLoadedExceptionTest
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
        ///A test for AdverbDataNotLoadedException Constructor
        ///</summary>
        [TestMethod()]
        public void AdverbDataNotLoadedExceptionConstructorTest() {
            string message = string.Empty; // TODO: Initialize to an appropriate value
            Exception inner = null; // TODO: Initialize to an appropriate value
            AdverbDataNotLoadedException target = new AdverbDataNotLoadedException(message, inner);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AdverbDataNotLoadedException Constructor
        ///</summary>
        [TestMethod()]
        public void AdverbDataNotLoadedExceptionConstructorTest1() {
            AdverbDataNotLoadedException target = new AdverbDataNotLoadedException();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
