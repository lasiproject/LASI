using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core.Heuristics;

namespace UnitTests
{


    /// <summary>
    ///This is a test class for IGenderedTest and is intended
    ///to contain all IGenderedTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IGenderedTest
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


        internal virtual IGendered CreateIGendered() {

            IGendered target = new PersonalPronoun("he");
            return target;
        }

        /// <summary>
        ///A test for Gender
        ///</summary>
        [TestMethod()]
        public void GenderTest() {
            IGendered target = CreateIGendered();  
            Gender actual;
            actual = target.Gender;
            Assert.AreEqual(actual,target.Gender);
        }
    }
}
