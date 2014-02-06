using LASI.ContentSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace L_ContentSystemTests
{
    
    
    /// <summary>
    ///This is a test class for FileManagerTest and is intended
    ///to contain all FileManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FileManagerTest
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
        ///A test for ConvertDocToTextAsync
        ///</summary>
        [TestMethod()]
        public void ConvertDocToTextAsyncTest() {
            DocFile[] files = null; // TODO: Initialize to an appropriate value
            Task expected = null; // TODO: Initialize to an appropriate value
            Task actual;
            actual = FileManager.ConvertDocToTextAsync(files);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ConvertDocxToText
        ///</summary>
        [TestMethod()]
        public void ConvertDocxToTextTest() {
            DocXFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.ConvertDocxToText(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ConvertDocxToTextAsync
        ///</summary>
        [TestMethod()]
        public void ConvertDocxToTextAsyncTest() {
            DocXFile[] files = null; // TODO: Initialize to an appropriate value
            Task expected = null; // TODO: Initialize to an appropriate value
            Task actual;
            actual = FileManager.ConvertDocxToTextAsync(files);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ConvertPdfFilesAsync
        ///</summary>
        [TestMethod()]
        public void ConvertPdfFilesAsyncTest() {
            PdfFile[] files = null; // TODO: Initialize to an appropriate value
            Task expected = null; // TODO: Initialize to an appropriate value
            Task actual;
            actual = FileManager.ConvertPdfFilesAsync(files);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ConvertPdfToText
        ///</summary>
        [TestMethod()]
        public void ConvertPdfToTextTest() {
            PdfFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.ConvertPdfToText(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DecimateProject
        ///</summary>
        [TestMethod()]
        public void DecimateProjectTest() {
            FileManager.DecimateProject();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for HasSimilarFile
        ///</summary>
        [TestMethod()]
        public void HasSimilarFileTest() {
            InputFile inputFile = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = FileManager.HasSimilarFile(inputFile);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HasSimilarFile
        ///</summary>
        [TestMethod()]
        public void HasSimilarFileTest1() {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = FileManager.HasSimilarFile(filePath);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Initialize
        ///</summary>
        [TestMethod()]
        public void InitializeTest() {
            string projectDir = string.Empty; // TODO: Initialize to an appropriate value
            FileManager.Initialize(projectDir);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RemoveAllNotIn
        ///</summary>
        [TestMethod()]
        public void RemoveAllNotInTest() {
            IEnumerable<InputFile> filesToKeep = null; // TODO: Initialize to an appropriate value
            FileManager.RemoveAllNotIn(filesToKeep);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RemoveAllNotIn
        ///</summary>
        [TestMethod()]
        public void RemoveAllNotInTest1() {
            IEnumerable<string> filesToKeep = null; // TODO: Initialize to an appropriate value
            FileManager.RemoveAllNotIn(filesToKeep);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RemoveFile
        ///</summary>
        [TestMethod()]
        public void RemoveFileTest() {
            InputFile file = null; // TODO: Initialize to an appropriate value
            FileManager.RemoveFile(file);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RemoveFile
        ///</summary>
        [TestMethod()]
        public void RemoveFileTest1() {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            FileManager.RemoveFile(filePath);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TagTextFiles
        ///</summary>
        [TestMethod()]
        public void TagTextFilesTest() {
            TextFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.TagTextFiles(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TagTextFilesAsync
        ///</summary>
        [TestMethod()]
        public void TagTextFilesAsyncTest() {
            TextFile[] files = null; // TODO: Initialize to an appropriate value
            Task expected = null; // TODO: Initialize to an appropriate value
            Task actual;
            actual = FileManager.TagTextFilesAsync(files);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocFiles
        ///</summary>
        [TestMethod()]
        public void DocFilesTest() {
            IReadOnlyList<DocFile> actual;
            actual = FileManager.DocFiles;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocXFiles
        ///</summary>
        [TestMethod()]
        public void DocXFilesTest() {
            IReadOnlyList<DocXFile> actual;
            actual = FileManager.DocXFiles;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PdfFiles
        ///</summary>
        [TestMethod()]
        public void PdfFilesTest() {
            IReadOnlyList<PdfFile> actual;
            actual = FileManager.PdfFiles;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TaggedFiles
        ///</summary>
        [TestMethod()]
        public void TaggedFilesTest() {
            IReadOnlyList<TaggedFile> actual;
            actual = FileManager.TaggedFiles;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TextFiles
        ///</summary>
        [TestMethod()]
        public void TextFilesTest() {
            IReadOnlyList<TextFile> actual;
            actual = FileManager.TextFiles;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddFile
        ///</summary>
        [TestMethod()]
        public void AddFileTest() {
            string path = string.Empty; // TODO: Initialize to an appropriate value
            bool overwrite = false; // TODO: Initialize to an appropriate value
            InputFile expected = null; // TODO: Initialize to an appropriate value
            InputFile actual;
            actual = FileManager.AddFile(path, overwrite);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BackupProject
        ///</summary>
        [TestMethod()]
        public void BackupProjectTest() {
            FileManager.BackupProject();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ConvertAsNeeded
        ///</summary>
        [TestMethod()]
        public void ConvertAsNeededTest() {
            FileManager.ConvertAsNeeded();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ConvertAsNeededAsync
        ///</summary>
        [TestMethod()]
        public void ConvertAsNeededAsyncTest() {
            Task expected = null; // TODO: Initialize to an appropriate value
            Task actual;
            actual = FileManager.ConvertAsNeededAsync();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ConvertDocToText
        ///</summary>
        [TestMethod()]
        public void ConvertDocToTextTest() {
            DocFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.ConvertDocToText(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
