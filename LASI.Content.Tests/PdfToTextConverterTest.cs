using LASI.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for PdfToTextConverterTest and is intended
    ///to contain all PdfToTextConverterTest Unit Tests
    ///</summary>
    [TestClass]
    public class PdfToTextConverterTest
    {

        private const string TEST_PDF_FILE_PATH = @"..\..\MockUserFiles\Draft_Environmental_Assessment3.pdf";

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
        [TestMethod]
        public void PdfToTextConverterConstructorTest() {
            PdfFile infile = new PdfFile(TEST_PDF_FILE_PATH);
            PdfToTextConverter target = new PdfToTextConverter(infile);
            Assert.AreEqual(infile, target.Original);
        }

        /// <summary>
        ///A test for ConvertFile
        ///</summary>
        [TestMethod]
        public void ConvertFileTest() {
            PdfFile infile = new PdfFile(TEST_PDF_FILE_PATH);
            PdfToTextConverter target = new PdfToTextConverter(infile);
            string expected = infile.GetText();
            string actual;
            actual = target.ConvertFile().GetText();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertFileAsync
        ///</summary>
        [TestMethod]
        public void ConvertFileAsyncTest() {
            PdfFile infile = new PdfFile(TEST_PDF_FILE_PATH);
            PdfToTextConverter target = new PdfToTextConverter(infile);
            string expected = infile.GetText();
            string actual;
            actual = target.ConvertFileAsync().Result.GetText();
            Assert.AreEqual(expected, actual);
        }
    }
}
