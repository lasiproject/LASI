using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;
using LASI.Content.Tests.Helpers;
using Shared.Test.Attributes;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for TaggedFileTest and is intended
    ///to contain all TaggedFileTest Unit Tests
    /// </summary>
    [TestClass]
    public class TaggedFileTest
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
        private const string VALID_TAGGED_FILE_PATH = @"..\..\MockUserFiles\Test paragraph about house fires.tagged";
        private const string DIFFERENT_TYPE_FILE_PATH = @"..\..\MockUserFiles\Test paragraph about house fires.txt";


        /// <summary>
        ///A test for TaggedFile Constructor
        /// </summary>
        [TestMethod]
        public void TaggedFileConstructorTest1()
        {
            TaggedFile target = new TaggedFile(VALID_TAGGED_FILE_PATH);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(VALID_TAGGED_FILE_PATH);
            Assert.AreEqual(target.FullPath, fileInfo.FullName);
        }
        [TestMethod]
        [ExpectedFileTypeWrapperMismatchException]
        public void TaggedFileConstructorTest2()
        {
            TaggedFile target = new TaggedFile(DIFFERENT_TYPE_FILE_PATH);
        }
        [TestMethod]
        [ExpectedFileNotFoundException]
        public void TaggedFileConstructorTest3()
        {
            string invalidPath = Directory.GetCurrentDirectory();//This should never be valid.
            TaggedFile target = new TaggedFile(invalidPath);
        }
        /// <summary>
        ///A test for LoadText
        /// </summary>
        [TestMethod]
        public void LoadTextTest()
        {
            TaggedFile target = new TaggedFile(VALID_TAGGED_FILE_PATH);
            string expected = File.ReadAllText(VALID_TAGGED_FILE_PATH);
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

        private static async Task LoadTextAsyncTestHelper()
        {
            TaggedFile target = new TaggedFile(VALID_TAGGED_FILE_PATH);
            Task<string> expected = Task.FromResult(File.ReadAllText(VALID_TAGGED_FILE_PATH));
            Task<string> actual = target.LoadTextAsync();
            Assert.AreEqual(await expected, await actual);
        }
    }
}
