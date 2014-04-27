using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is A test class for GenericSingularNounTest and is intended
    ///to contain all GenericSingularNounTest Unit Tests
    ///</summary>
    [TestClass]
    public class CommonSingularNounTest
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
        ///A test for CommonSingularNoun Constructor
        ///</summary>
        [TestMethod]
        public void CommonSingularNounConstructorTest() {
            string text = "LASI"; // TODO: Initialize to an appropriate value
            CommonSingularNoun target = new CommonSingularNoun(text);
            Assert.AreEqual(target.Text, text);
        }
    }
}
