using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core.Heuristics;

namespace LASI.Core.Tests
{
    
    
    /// <summary>
    ///This is a test class for ISimpleGenderedTest and is intended
    ///to contain all ISimpleGenderedTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ISimpleGenderedTest
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


        internal virtual ISimpleGendered CreateISimpleGendered() {
            // TODO: Instantiate an appropriate concrete class.
            ISimpleGendered target = null;
            return target;
        }

        /// <summary>
        ///A test for Gender
        ///</summary>
        [TestMethod()]
        public void GenderTest() {
            ISimpleGendered target = CreateISimpleGendered(); // TODO: Initialize to an appropriate value
            Gender actual;
            actual = target.Gender;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
