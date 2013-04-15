using LASI.FileSystem;
using LASI.FileSystem.FileTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for DocxToTextConverterTest and is intended
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

        string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
        string txtFilesDir = @"..\..\..\NewProject\input\text";

        public string TxtFilesDir
        {
            get { return txtFilesDir; }
            set { txtFilesDir = value; }
        }
        public string SourcePath
        {
            get { return sourcePath; }
            set { sourcePath = value; }
        } string targetPath = @"..\..\..\NewProject\input\text\Draft_Environmental_Assessment.txt";

        public string TargetPath
        {
            get { return targetPath; }
            set { targetPath = value; }
        }
        /// <summary>
        ///a test for DocxToTextConverter Constructor
        ///</summary>
        [TestMethod()]
        public void DocxToTextConverterConstructorTest()
        {


            DocxToTextConverter target = new DocxToTextConverter(SourcePath, TargetPath);
            Assert.IsTrue(target.Original.FullPath == SourcePath);
        }

        /// <summary>
        ///a test for DocxToTextConverter Constructor
        ///</summary>
        [TestMethod()]
        public void DocxToTextConverterConstructorTest1()
        {
            var infile = InitInputFileWrapper();
            DocxToTextConverter target = new DocxToTextConverter(infile);
            Assert.IsTrue(target.Original.FullPath == infile.FullPath);
        }

        /// <summary>
        ///a test for DocxToTextConverter Constructor
        ///</summary>
        [TestMethod()]
        public void DocxToTextConverterConstructorTest2()
        {
            var infile = InitInputFileWrapper();

            DocxToTextConverter target = new DocxToTextConverter(infile, TxtFilesDir);
            Assert.IsTrue(target.Original.FullPath == infile.FullPath);
        }

        private static DocXFile InitInputFileWrapper()
        {
            var infile = new DocXFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment.docx");
            return infile;
        }



        /// <summary>
        ///a test for ConvertFileAsync
        ///</summary>
        [TestMethod()]
        public async Task ConvertFileAsyncTest()
        {
            var infile = InitInputFileWrapper();
            DocxToTextConverter target = new DocxToTextConverter(infile);
            InputFile actual;
            actual = await target.ConvertFileAsync();
            Assert.IsTrue(File.Exists(actual.FullPath));
        }
    }
}
