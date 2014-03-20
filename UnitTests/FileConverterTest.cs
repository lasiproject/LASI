using LASI.ContentSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is a test class for FileConverterTest and is intended
    ///to contain all FileConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FileConverterTest
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
        ///A test for ConvertFile
        ///</summary>
        public void ConvertFileTestHelper<TSource, TDestination>()
            where TSource : InputFile
            where TDestination : InputFile {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            FileConverter<TSource, TDestination> target = CreateFileConverter<TSource, TDestination>();
            TDestination actual;
            actual = target.ConvertFile() as TDestination;
            TDestination expected = new TxtFile(sourcePath.Substring(0, sourcePath.LastIndexOf('.')) + ".txt") as TDestination;
            Assert.AreEqual(expected, actual);
        }

        internal virtual FileConverter<TSource, TDestination> CreateFileConverter<TSource, TDestination>()
            where TSource : InputFile
            where TDestination : InputFile {
            // TODO: Instantiate an appropriate concrete class.
            FileConverter<TSource, TDestination> target = new DocxToTextConverter(
                new DocXFile(@"..\..\..\TestDocs\Draft_Environmental_Assessment.docx")) as FileConverter<TSource, TDestination>;

            return target;
        }

        [TestMethod()]
        public void ConvertFileTest() {
            ConvertFileTestHelper<DocXFile, TxtFile>();
        }

        /// <summary>
        ///A test for ConvertFileAsync
        ///</summary>
        public void ConvertFileAsyncTestHelper<TSource, TDestination>()
            where TSource : InputFile
            where TDestination : InputFile {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            FileConverter<TSource, TDestination> target = CreateFileConverter<TSource, TDestination>();
            TDestination actual;
            actual = target.ConvertFileAsync().Result;
            TDestination expected = new TxtFile(sourcePath.Substring(0, sourcePath.LastIndexOf('.')) + ".txt") as TDestination;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void ConvertFileAsyncTest() {
            ConvertFileAsyncTestHelper<DocXFile, TxtFile>();
        }
    }
}
