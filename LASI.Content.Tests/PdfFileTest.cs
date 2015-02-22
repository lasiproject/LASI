using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using LASI.Content.Tests.Helpers;
using  Shared.Test.Attributes;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for PdfFileTest and is intended
    ///to contain all PdfFileTest Unit Tests
    /// </summary>
    [TestClass]
    public class PdfFileTest
    {
        private const string TEST_PDF_FILE_PATH = @"..\..\MockUserFiles\Draft_Environmental_Assessment3.pdf";
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        #endregion


        /// <summary>
        ///A test for PdfFile Constructor
        /// </summary>
        [TestMethod]
        public void PdfFileConstructorTest()
        {
            PdfFile target = new PdfFile(TEST_PDF_FILE_PATH);
            FileInfo pdfInfo = new FileInfo(TEST_PDF_FILE_PATH);
            Assert.AreEqual(pdfInfo.FullName, target.FullPath);
            Assert.AreEqual(pdfInfo.Name, target.FileName);
            Assert.AreEqual(pdfInfo.Extension, target.Extension);
        }
        [TestMethod]
        [ExpectedFileNotFoundException]
        public void PdfFileConstructorTest1()
        {
            string invalidPath = Directory.GetCurrentDirectory();
            PdfFile target = new PdfFile(invalidPath);
        }
        [TestMethod]
        [ExpectedFileTypeWrapperMismatchException]
        public void PdfFileConstructorTest2()
        {
            string textFilePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment3.txt";
            PdfFile target = new PdfFile(textFilePath);
        }
        /// <summary>
        ///A test for GetText
        /// </summary>
        [TestMethod]
        public void GetTextTest()
        {
            PdfFile target = new PdfFile(TEST_PDF_FILE_PATH);
            string expected = new PdfToTextConverter(target).ConvertFile().GetText();
            string actual;
            actual = target.GetText();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetTextAsync
        /// </summary>
        [TestMethod]
        public void GetTextAsyncTest()
        {
            string path = TEST_PDF_FILE_PATH;
            PdfFile target = new PdfFile(path);
            string expected = new PdfToTextConverter(target).ConvertFile().GetText();
            string actual;
            actual = target.GetTextAsync().Result;
            Assert.AreEqual(expected, actual);
        }
    }
}
