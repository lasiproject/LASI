using LASI.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is A test class for DocxToTextConverterTest and is intended
    ///to contain all DocxToTextConverterTest Unit Tests
    ///</summary>
    [TestClass]
    public class DocxToTextConverterTest
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

        #endregion

        private static DocXFile InitInputFileWrapper() {
            var infile = new DocXFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment.docx");
            return infile;
        }



        /// <summary>
        ///A test for ConvertFileAsync
        ///</summary>
        [TestMethod]
        public async Task ConvertFileAsyncTest() {
            var infile = InitInputFileWrapper();
            DocxToTextConverter target = new DocxToTextConverter(infile);
            InputFile actual;
            actual = await target.ConvertFileAsync();
            Assert.IsTrue(File.Exists(actual.FullPath));
        }

        /// <summary>
        ///A test for DocxToTextConverter Constructor
        ///</summary>
        [TestMethod]
        public void DocxToTextConverterConstructorTest() {
            DocXFile infile = InitInputFileWrapper(); // TODO: Initialize to an appropriate value
            DocxToTextConverter target = new DocxToTextConverter(infile);
            Assert.AreEqual(target.Original.FullPath, infile.FullPath);
        }

        /// <summary>
        ///A test for ConvertFile
        ///</summary>
        [TestMethod]
        public void ConvertFileTest() {
            DocXFile infile = null; // TODO: Initialize to an appropriate value
            DocxToTextConverter target = new DocxToTextConverter(infile); // TODO: Initialize to an appropriate value
            TxtFile expected = null; // TODO: Initialize to an appropriate value
            TxtFile actual;
            actual = target.ConvertFile();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ConvertFileAsync
        ///</summary>
        [TestMethod]
        public void ConvertFileAsyncTest1() {
            DocXFile infile = null; // TODO: Initialize to an appropriate value
            DocxToTextConverter target = new DocxToTextConverter(infile); // TODO: Initialize to an appropriate value
            Task<TxtFile> expected = null; // TODO: Initialize to an appropriate value
            Task<TxtFile> actual;
            actual = target.ConvertFileAsync();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
