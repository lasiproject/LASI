using LASI.ContentSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is A test class for DocxToTextConverterTest and is intended
    ///to contain all DocxToTextConverterTest Unit Tests
    ///</summary>
    [TestClass()]
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

        string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
        string txtFilesDir = @"..\..\..\NewProject\input\text";

        public string TxtFilesDir {
            get { return txtFilesDir; }
            set { txtFilesDir = value; }
        }
        public string SourcePath {
            get { return sourcePath; }
            set { sourcePath = value; }
        }
        string targetPath = @"..\..\..\NewProject\input\text\Draft_Environmental_Assessment.txt";

        public string TargetPath {
            get { return targetPath; }
            set { targetPath = value; }
        }

 



        private static DocXFile InitInputFileWrapper() {
            var infile = new DocXFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment.docx");
            return infile;
        }



        /// <summary>
        ///A test for ConvertFileAsync
        ///</summary>
        [TestMethod()]
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
        [TestMethod()]
        public void DocxToTextConverterConstructorTest() {
            DocXFile infile = InitInputFileWrapper(); // TODO: Initialize to an appropriate value
            DocxToTextConverter target = new DocxToTextConverter(infile);
            Assert.AreEqual(target.Original.FullPath, infile.FullPath);
        }

        /// <summary>
        ///A test for ConvertFile
        ///</summary>
        [TestMethod()]
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
        [TestMethod()]
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
