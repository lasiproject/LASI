using LASI.ContentSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LASI.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for PdfToTextConverterTest and is intended
    ///to contain all PdfToTextConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PdfToTextConverterTest
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
        ///A test for PdfToTextConverter Constructor
        ///</summary>
        [TestMethod()]
        public void PdfToTextConverterConstructorTest() {
            PdfFile infile = null; // TODO: Initialize to an appropriate value
            PdfToTextConverter target = new PdfToTextConverter(infile);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for ConvertFile
        ///</summary>
        [TestMethod()]
        public void ConvertFileTest() {
            PdfFile infile = null; // TODO: Initialize to an appropriate value
            PdfToTextConverter target = new PdfToTextConverter(infile); // TODO: Initialize to an appropriate value
            TxtFile expected = null; // TODO: Initialize to an appropriate value
            TxtFile actual;
            actual = target.ConvertFile();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ConvertFileAsync
        ///</summary>
        [TestMethod()]
        public void ConvertFileAsyncTest() {
            PdfFile infile = null; // TODO: Initialize to an appropriate value
            PdfToTextConverter target = new PdfToTextConverter(infile); // TODO: Initialize to an appropriate value
            Task<TxtFile> expected = null; // TODO: Initialize to an appropriate value
            Task<TxtFile> actual;
            actual = target.ConvertFileAsync();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
