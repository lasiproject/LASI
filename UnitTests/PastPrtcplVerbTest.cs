using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is A test class for PastPrtcplVerbTest and is intended
    ///to contain all PastPrtcplVerbTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PastPrtcplVerbTest
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
        ///A test for PastPrtcplVerb Constructor
        ///</summary>
        [TestMethod()]
        public void PastPrtcplVerbConstructorTest() {
            string text = "gone";
            VerbForm pastprt = VerbForm.PastParticiple;
            PastParticipleVerb target = new PastParticipleVerb(text);
            Assert.IsTrue(target.Text == text);
            Assert.IsTrue(target.VerbForm == pastprt);
        }
    }
}
