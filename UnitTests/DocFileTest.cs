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
        [ExpectedException(typeof(FileTypeWrapperMismatchException))]
        public void DocFileConstructorTest1() {
            string path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.txt";
            DocFile target = new DocFile(path);
        }
        /// <summary>
        ///A test for DocFile Constructor
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void DocFileConstructorTest2() {
            string invalidPath = Directory.GetCurrentDirectory();//This should never be valid.
            Assert.IsFalse(File.Exists(invalidPath));
            DocFile target = new DocFile(invalidPath);
        } 
    }
}
