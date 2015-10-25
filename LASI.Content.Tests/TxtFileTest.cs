using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;
using Shared.Test.Attributes;
using NFluent;

namespace LASI.Content.Tests
{
    /// <summary>
    ///This is a test class for TextFileTest and is intended
    ///to contain all TextFileTest Unit Tests
    /// </summary>
    [TestClass]
    public class TxtFileTest
    {
        private const string VALID_TXT_FILE_PATH = @"..\..\MockUserFiles\Draft_Environmental_Assessment4.txt";
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

        /// <summary>
        ///A test for TextFile Constructor
        /// </summary>
        [TestMethod]
        public void TextFileConstructorTest()
        {
            string path = VALID_TXT_FILE_PATH;
            TxtFile target = new TxtFile(path);
            var sfi = new System.IO.FileInfo(path);
            Assert.AreEqual(sfi.FullName, target.FullPath);
            Assert.AreEqual(sfi.Name, target.FileName);
            Assert.AreEqual(sfi.Extension, target.Extension);
        }
        [TestMethod]
        [ExpectedFileNotFoundException]
        public void TextFileConstructorTest1()
        {
            string invalidPath = Directory.GetCurrentDirectory();//This should never be valid.
            TxtFile target = new TxtFile(invalidPath);
        }
        [TestMethod]
        public void TxtFileConstructorTest2()
        {
            string wrongTypePath = new FileInfo(@"..\..\MockUserFiles\Draft_Environmental_Assessment3.pdf").FullName;
            Check.ThatCode(() => new TxtFile(wrongTypePath)).Throws<FileTypeWrapperMismatchException<TxtFile>>();
        }

        /// <summary>
        ///A test for LoadText
        /// </summary>
        [TestMethod]
        public void LoadTextTest()
        {
            string path = VALID_TXT_FILE_PATH;
            TxtFile target = new TxtFile(path);
            string expected = new System.IO.StreamReader(path).ReadToEnd();
            string actual;
            actual = target.LoadText();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [TestMethod]
        public void LoadTextAsyncTest()
        {
            LoadTextAsyncTestHelper().Wait();
        }

        private async Task LoadTextAsyncTestHelper()
        {
            string path = VALID_TXT_FILE_PATH;
            TxtFile target = new TxtFile(path);
            string expected = new System.IO.StreamReader(target.FullPath).ReadToEndAsync().Result;
            string actual = null;
            actual = await target.LoadTextAsync();
            Assert.AreEqual(expected, actual);
        }
    }
}
