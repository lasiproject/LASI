using LASI.ContentSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LASI.Core.Tests
{
    
    
    /// <summary>
    ///This is a test class for DocXFileTest and is intended
    ///to contain all DocXFileTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DocXFileTest
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
        ///A test for DocXFile Constructor
        ///</summary>
        [TestMethod()]
        public void DocXFileConstructorTest() {
            string absolutePath = string.Empty; // TODO: Initialize to an appropriate value
            DocXFile target = new DocXFile(absolutePath);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GetText
        ///</summary>
        [TestMethod()]
        public void GetTextTest() {
            string absolutePath = string.Empty; // TODO: Initialize to an appropriate value
            DocXFile target = new DocXFile(absolutePath); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetText();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetTextAsync
        ///</summary>
        [TestMethod()]
        public void GetTextAsyncTest() {
            string absolutePath = string.Empty; // TODO: Initialize to an appropriate value
            DocXFile target = new DocXFile(absolutePath); // TODO: Initialize to an appropriate value
            Task<string> expected = null; // TODO: Initialize to an appropriate value
            Task<string> actual;
            actual = target.GetTextAsync();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
