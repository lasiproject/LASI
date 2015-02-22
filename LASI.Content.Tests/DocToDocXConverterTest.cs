using LASI.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is A test class for DocToDocXConverterTest and is intended
    ///to contain all DocToDocXConverterTest Unit Tests
    /// </summary>
    [TestClass]
    public class DocToDocXConverterTest
    {



        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        /// </summary>
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
        ///A test for DocToDocXConverter Constructor
        /// </summary>
        [TestMethod]
        public void DocToDocXConverterConstructorTest() {
            var infile = CreateTarget();
            DocToDocXConverter target = new DocToDocXConverter(infile);
            Assert.IsTrue(target.Original == infile);
        }

        /// <summary>
        ///A test for DocToDocXConverter Constructor
        /// </summary>
        [TestMethod]
        public void DocToDocXConverterConstructorTest1() {
            var infile = CreateTarget();
            string DocxFilesDir = @"..\..\..\NewProject\input\docx";
            DocToDocXConverter target = new DocToDocXConverter(infile, DocxFilesDir);
            Assert.IsTrue(target.Original == infile);
        }

        private static DocFile CreateTarget() {
            var infile = new DocFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment.doc");

            return infile;
        }

        /// <summary>
        ///A test for ConvertFile
        /// </summary>
        [TestMethod]
        public void ConvertFileTest() {
            var infile = CreateTarget();
            DocToDocXConverter target = new DocToDocXConverter(infile);
            InputFile actual;
            actual = target.ConvertFile();
            Assert.IsTrue(File.Exists(actual.FullPath));
        }

        /// <summary>
        ///A test for ConvertFileAsync
        /// </summary>
        [TestMethod]
        public async Task ConvertFileAsyncTest() {
            var infile = CreateTarget();
            DocToDocXConverter target = new DocToDocXConverter(infile);
            InputFile actual;
            actual = await target.ConvertFileAsync();
            Assert.IsTrue(File.Exists(actual.FullPath));
        }
    }
}
