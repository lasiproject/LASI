using LASI.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI_FileSystem_UnitTests
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
        ///A test for AddDocFile
        ///</summary>
        [TestMethod()]
        public void AddDocFileTest() {
            string sourcePath = string.Empty; // TODO: Initialize to an appropriate value
            FileManager.AddDocFile(sourcePath);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddDocXFile
        ///</summary>
        [TestMethod()]
        public void AddDocXFileTest() {
            string sourcePath = string.Empty; // TODO: Initialize to an appropriate value
            FileManager.AddDocXFile(sourcePath);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddFile
        ///</summary>
        [TestMethod()]
        public void AddFileTest() {
            DocFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.AddFile(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddFile
        ///</summary>
        [TestMethod()]
        public void AddFileTest1() {
            DocXFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.AddFile(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddFile
        ///</summary>
        [TestMethod()]
        public void AddFileTest2() {
            TextFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.AddFile(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddFile
        ///</summary>
        [TestMethod()]
        public void AddFileTest3() {
            LasiFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.AddFile(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddLasiFile
        ///</summary>
        [TestMethod()]
        public void AddLasiFileTest() {
            string sourcePath = string.Empty; // TODO: Initialize to an appropriate value
            FileManager.AddLasiFile(sourcePath);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddTextFile
        ///</summary>
        [TestMethod()]
        public void AddTextFileTest() {
            string sourcePath = string.Empty; // TODO: Initialize to an appropriate value
            FileManager.AddTextFile(sourcePath);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
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
        ///A test for ConvertDocFiles
        ///</summary>
        [TestMethod()]
        public void ConvertDocFilesTest() {
            DocFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.ConvertDocFiles(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
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
        ///A test for Initialize
        ///</summary>
        [TestMethod()]
        public void InitializeTest() {
            string projectDir = string.Empty; // TODO: Initialize to an appropriate value
            FileManager.Initialize(projectDir);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TagTextFile
        ///</summary>
        [TestMethod()]
        public void TagTextFileTest() {
            TextFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.TagTextFile(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AnalysisDir
        ///</summary>
        [TestMethod()]
        public void AnalysisDirTest() {
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            FileManager.AnalysisDir = expected;
            actual = FileManager.AnalysisDir;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocFilesDir
        ///</summary>
        [TestMethod()]
        public void DocFilesDirTest() {
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            FileManager.DocFilesDir = expected;
            actual = FileManager.DocFilesDir;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocxFilesDir
        ///</summary>
        [TestMethod()]
        public void DocxFilesDirTest() {
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            FileManager.DocxFilesDir = expected;
            actual = FileManager.DocxFilesDir;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InputFilesDir
        ///</summary>
        [TestMethod()]
        public void InputFilesDirTest() {
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            FileManager.InputFilesDir = expected;
            actual = FileManager.InputFilesDir;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LASIFilesDir
        ///</summary>
        [TestMethod()]
        public void LASIFilesDirTest() {
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            FileManager.LASIFilesDir = expected;
            actual = FileManager.LASIFilesDir;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ProjectDir
        ///</summary>
        [TestMethod()]
        public void ProjectDirTest() {
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            FileManager.ProjectDir = expected;
            actual = FileManager.ProjectDir;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ProjectName
        ///</summary>
        [TestMethod()]
        public void ProjectNameTest() {
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            FileManager.ProjectName = expected;
            actual = FileManager.ProjectName;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ResultsDir
        ///</summary>
        [TestMethod()]
        public void ResultsDirTest() {
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            FileManager.ResultsDir = expected;
            actual = FileManager.ResultsDir;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TextFilesDir
        ///</summary>
        [TestMethod()]
        public void TextFilesDirTest() {
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            FileManager.TextFilesDir = expected;
            actual = FileManager.TextFilesDir;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
