using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Shared.Test.Attributes;
using NFluent;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for DocFileTest and is intended
    ///to contain all DocFileTest Unit Tests
    /// </summary>
    [TestClass]
    public class DocFileTest
    {
        const string DOC_TEST_FILE_PATH = @"..\..\MockUserFiles\Draft_Environmental_Assessment.doc";

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


        /// <summary>
        ///A test for DocFile Constructor
        /// </summary>
        [TestMethod]
        public void DocFileConstructorTest()
        {
            DocFile target = new DocFile(DOC_TEST_FILE_PATH);
            Assert.IsTrue(File.Exists(DOC_TEST_FILE_PATH));
            Assert.AreEqual(target.Extension, ".doc");
            Assert.AreEqual(target.FullPath, Path.GetFullPath(DOC_TEST_FILE_PATH));
        }
        /// <summary>
        ///A test for DocFile Constructor
        /// </summary>
        [TestMethod]
        public void DocFileConstructorTest1()
        {
            string wrongFileTypePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment4.txt";
            Check.ThatCode(() =>
            {
                DocFile target = new DocFile(wrongFileTypePath);
            }).Throws<FileTypeWrapperMismatchException<DocFile>>();
        }
        /// <summary>
        ///A test for DocFile Constructor
        /// </summary>
        [TestMethod]
        [ExpectedFileNotFoundException]
        public void DocFileConstructorTest2()
        {
            string invalidPath = Directory.GetCurrentDirectory();//This should never be valid.
            Assert.IsFalse(File.Exists(invalidPath));
            DocFile target = new DocFile(invalidPath);
        }
    }
}
