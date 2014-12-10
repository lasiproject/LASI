using LASI.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.IO;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is a test class for DocFileTest and is intended
    ///to contain all DocFileTest Unit Tests
    ///</summary>
    [TestClass]
    public class DocFileTest
    {
        const string DOC_TEST_FILE_PATH = @"..\..\MockUserFiles\Draft_Environmental_Assessment1.doc";

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
        ///A test for DocFile Constructor
        ///</summary>
        [TestMethod]
        public void DocFileConstructorTest() {
            DocFile target = new DocFile(DOC_TEST_FILE_PATH);
            Assert.IsTrue(File.Exists(DOC_TEST_FILE_PATH));
            Assert.AreEqual(target.Ext, DocFile.EXTENSION);
            Assert.AreEqual(target.FullPath, Path.GetFullPath(DOC_TEST_FILE_PATH));
        }
        /// <summary>
        ///A test for DocFile Constructor
        ///</summary>
        [TestMethod]
        public void DocFileConstructorTest1() {
            string path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.txt";
            try {
                DocFile target = new DocFile(path);
                Assert.Fail("Instantiation with mismatched extension succeeded.");
            } catch (FileTypeWrapperMismatchException e) {
                TestContext.WriteLine("Expected exception thrown: {0}", e.GetType().FullName);
            }
        }
        /// <summary>
        ///A test for DocFile Constructor
        ///</summary>
        [TestMethod]
        public void DocFileConstructorTest2() {
            string invalidPath = Directory.GetCurrentDirectory();//This should never be valid.
            Assert.IsFalse(File.Exists(invalidPath));
            try {
                DocFile target = new DocFile(invalidPath);
                Assert.Fail("Instantiation with invalid path succeeded.");
            } catch (FileNotFoundException e) {
                TestContext.WriteLine("Expected exception thrown: {0}", e.GetType().FullName);
            }
        }
        ///// <summary>
        /////A test for GetText
        /////</summary>
        //[TestMethod]
        //public void GetTextTest() {
        //    System.IO.Directory.CreateDirectory(@"..\..\DocFileTest\GetTestText");
        //    DocFile target = new DocFile(DOC_TEST_FILE_PATH);
        //    string conversionLocation = Path.GetDirectoryName(DOC_TEST_FILE_PATH) + @"\DocFileTest\GetTestText\";
        //    string expected = new DocxToTextConverter(new DocToDocXConverter(target, conversionLocation).ConvertFile()).ConvertFile().GetText();
        //    string actual;
        //    actual = target.GetText();
        //    Assert.AreEqual(expected, actual);
        //}

        ///// <summary>
        /////A test for GetTextAsync
        /////</summary>
        //[TestMethod]
        //public void GetTextAsyncTest() {
        //    System.IO.Directory.CreateDirectory(@"..\..\DocFileTest_GetTextAsyncTest");
        //    DocFile target = new DocFile(DOC_TEST_FILE_PATH);
        //    string path = new System.IO.FileInfo(DOC_TEST_FILE_PATH).CopyTo(@".\DocFileTest_GetTextAsyncTest\Draft_Environmental_Assessment.doc", overwrite: true).FullName;
        //    string conversionLocation = Path.GetDirectoryName(DOC_TEST_FILE_PATH) + @"\DocFileTest\GetTestText\";
        //    string expected = new DocxToTextConverter(new DocToDocXConverter(target, conversionLocation).ConvertFile()).ConvertFile().GetText();
        //    string actual;
        //    actual = target.GetTextAsync().Result;
        //    Assert.AreEqual(expected, actual);
        //}
    }
}
