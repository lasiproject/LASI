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
    /// </summary>
    [TestClass]
    public class DocxToTextConverterTest
    {

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

        private static DocXFile InitDocFile()
        {
            string path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            DocXFile infile = new DocXFile(path);
            return infile;
        }



        /// <summary>
        ///A test for ConvertFileAsync
        /// </summary>
        [TestMethod]
        public async Task ConvertFileAsyncTest()
        {
            DocXFile infile = InitDocFile();
            DocxToTextConverter target = new DocxToTextConverter(infile);
            TxtFile actual;
            actual = await target.ConvertFileAsync();
            Assert.IsTrue(File.Exists(actual.FullPath));
        }
        /// <summary>
        ///A test for ConvertFile
        /// </summary>
        [TestMethod]
        public void ConvertFileTest()
        {
            DocXFile infile = InitDocFile();
            DocxToTextConverter target = new DocxToTextConverter(infile); // TODO: Initialize to an appropriate value
            TxtFile actual;
            actual = target.ConvertFile();
            Assert.IsTrue(File.Exists(actual.FullPath));
        }
        /// <summary>
        ///A test for DocxToTextConverter Constructor
        /// </summary>
        [TestMethod]
        public void DocxToTextConverterConstructorTest()
        {
            DocXFile infile = InitDocFile(); // TODO: Initialize to an appropriate value
            DocxToTextConverter target = new DocxToTextConverter(infile);
            Assert.AreEqual(target.Original.FullPath, infile.FullPath);
        }
    }
}
