using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI.Content.Tests.Helpers;
using Shared.Test.Attributes;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for DocXFileTest and is intended
    ///to contain all DocXFileTest Unit Tests
    /// </summary>
    [TestClass]
    public class DocXFileTest
    {
        /// <summary>
        ///A test for DocXFile Constructor
        /// </summary>
        [TestMethod]
        public void DocXFileConstructorTest()
        {
            string path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            DocXFile target = new DocXFile(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            Assert.AreEqual(System.IO.Path.GetFullPath(path), target.FullPath);
        }
        /// <summary>
        ///A test for DocXFile Constructor
        /// </summary>
        [TestMethod]
        [ExpectedFileTypeWrapperMismatchException]
        public void DocXFileConstructorTest1()
        {
            string path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.txt";
            DocXFile target = new DocXFile(path);
        }
        /// <summary>
        ///A test for DocXFile Constructor
        /// </summary>
        [TestMethod]
        [ExpectedFileNotFoundException]
        public void DocXFileConstructorTest2()
        {
            string invalidPath = System.IO.Directory.GetCurrentDirectory();//This is should never be valid.
            Assert.IsFalse(System.IO.File.Exists(invalidPath));
            DocXFile target = new DocXFile(invalidPath);
        }
    }
}
