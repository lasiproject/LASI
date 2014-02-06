using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for IDeterminableTest and is intended
    ///to contain all IDeterminableTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IDeterminableTest
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


        internal virtual IDeterminable CreateIDeterminable() {
            // TODO: Instantiate an appropriate concrete class.
            IDeterminable target = null;
            return target;
        }

        /// <summary>
        ///A test for Determiner
        ///</summary>
        [TestMethod()]
        public void DeterminerTest() {
            IDeterminable target = CreateIDeterminable(); // TODO: Initialize to an appropriate value
            Determiner actual;
            actual = target.Determiner;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BindDeterminer
        ///</summary>
        [TestMethod()]
        public void BindDeterminerTest() {
            IDeterminable target = CreateIDeterminable(); // TODO: Initialize to an appropriate value
            Determiner determiner = null; // TODO: Initialize to an appropriate value
            target.BindDeterminer(determiner);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
