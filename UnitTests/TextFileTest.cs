using LASI.ContentSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is a test class for TextFileTest and is intended
    ///to contain all TextFileTest Unit Tests
    ///</summary>
    [TestClass]
    public class TextFileTest
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
        ///A test for TextFile Constructor
        ///</summary>
        [TestMethod]
        public void TextFileConstructorTest() {
            string path = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";
            TxtFile target = new TxtFile(path);
            var sfi = new System.IO.FileInfo(path);
            Assert.AreEqual(sfi.FullName, target.FullPath);
            Assert.AreEqual(sfi.Name, target.FileName);
            Assert.AreEqual(sfi.Extension, target.Ext);
        }

        /// <summary>
        ///A test for GetText
        ///</summary>
        [TestMethod]
        public void GetTextTest() {
            string path = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";
            TxtFile target = new TxtFile(path);
            string expected = new System.IO.StreamReader(path).ReadToEnd();
            string actual;
            actual = target.GetText();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetTextAsync
        ///</summary>
        [TestMethod]
        public void GetTextAsyncTest() {
            string path = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";
            TxtFile target = new TxtFile(path);
            string expected = string.Empty;
            string actual = null;
            Task.WaitAll(Task.Run(
                async () => expected = await new System.IO.StreamReader(target.FullPath).ReadToEndAsync()),
                Task.Run(async () => actual = await target.GetTextAsync())
            );
            Assert.AreEqual(expected, actual);
        }
    }
}
