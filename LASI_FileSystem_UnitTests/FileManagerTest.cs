using LASI.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) {
            FileManager.Initialize(@"..\..\..\NewProject");
        }

        ////  Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup() {
        //    Directory.Delete(@"..\..\..\NewProject\Input", true);
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
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.doc";

            FileManager.AddDocFile(sourcePath);
            Assert.IsTrue(File.Exists(FileManager.DocFilesDir + @"\\Draft_Environmental_Assessment.doc"));
        }

        /// <summary>
        ///A test for AddDocXFile
        ///</summary>
        [TestMethod()]
        public void AddDocXFileTest() {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            FileManager.AddDocXFile(sourcePath);
            Assert.IsTrue(File.Exists(FileManager.DocxFilesDir + @"\\Draft_Environmental_Assessment.docx"));
        }

        /// <summary>
        ///A test for AddFile
        ///</summary>
        [TestMethod()]
        public void AddFileTest() {
            DocFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.AddFiles(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddFile
        ///</summary>
        [TestMethod()]
        public void AddFileTest1() {
            DocXFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.AddFiles(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddFile
        ///</summary>
        [TestMethod()]
        public void AddFileTest2() {
            TextFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.AddFiles(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddFile
        ///</summary>
        [TestMethod()]
        public void AddFileTest3() {
            TaggedFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.AddFiles(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddLasiFile
        ///</summary>
        [TestMethod()]
        public void AddTaggedFileTest() {

            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.tagged";
            FileManager.AddLasiFile(sourcePath);
            Assert.IsTrue(File.Exists(FileManager.TaggedFilesDir + @"\\Draft_Environmental_Assessment.tagged"));
        }

        /// <summary>
        ///A test for AddTextFile
        ///</summary>
        [TestMethod()]
        public void AddTextFileTest() {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.txt";

            FileManager.AddTextFile(sourcePath);
            Assert.IsTrue(File.Exists(FileManager.TextFilesDir + @"\\Draft_Environmental_Assessment.txt"));
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
            DocFile[] files = (from F in Directory.EnumerateFiles(FileManager.DocFilesDir)
                               select new DocFile(F)).ToArray();
            FileManager.ConvertDocFiles(files);
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.DocxFilesDir + "\\" + F.NameSansExt + ".docx"));
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
            string projectDir = @"..\..\..\NewProject";
            FileManager.Initialize(projectDir);
            Assert.IsTrue(Directory.Exists(projectDir));
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
        ///A test for ProjectName
        ///</summary>
        [TestMethod()]
        public void ProjectNameTest() {
            FileManager.Initialize(@"..\..\..\NewProject");
            string expected = "NewProject";
            string actual;
            actual = FileManager.ProjectName;
            Assert.AreEqual(expected, actual);
        }

    }
}
