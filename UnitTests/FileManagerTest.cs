using LASI.ContentSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for FileManagerTest and is intended
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
            CleanDirectories();
        }

        private static void CleanDirectories() {
            if (Directory.Exists(@"..\..\..\NewProject\input"))
                Directory.Delete(@"..\..\..\NewProject\input", true);
            if (Directory.Exists(@"..\..\..\backup\NewProject"))
                Directory.Delete(@"..\..\..\backup\NewProject", true);
            if (Directory.Exists(@"..\..\..\NewProject"))
                Directory.Delete(@"..\..\..\NewProject", true);

            FileManager.Initialize(@"..\..\..\NewProject");
            foreach (var fileInfo in new DirectoryInfo(@"..\..\..\UnitTests\MockUserFiles").EnumerateFiles()) {
                switch (fileInfo.Extension) {
                    case ".doc":
                        File.Copy(fileInfo.FullName, @"..\..\..\NewProject\input\doc\" + fileInfo.FullName.Substring(fileInfo.FullName.LastIndexOf('\\') + 1), true);
                        break;
                    case ".docx":
                        File.Copy(fileInfo.FullName, @"..\..\..\NewProject\input\docx\" + fileInfo.FullName.Substring(fileInfo.FullName.LastIndexOf('\\') + 1), true);
                        break;
                    case ".txt":
                        File.Copy(fileInfo.FullName, @"..\..\..\NewProject\input\text\" + fileInfo.FullName.Substring(fileInfo.FullName.LastIndexOf('\\') + 1), true);
                        break;
                    case ".tagged":
                        File.Copy(fileInfo.FullName, @"..\..\..\NewProject\input\tagged\" + fileInfo.FullName.Substring(fileInfo.FullName.LastIndexOf('\\') + 1), true);
                        break;
                    default:
                        break;
                }
            }
        }

        ////  Use ClassCleanup to run code after all tests in A class have run
        [ClassCleanup()]
        public static void MyClassCleanup() {
            Directory.Delete(@"..\..\..\NewProject\Input", true);
        }
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        public void MyTestInitialize() {
            CleanDirectories();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup() {
            //Directory.Delete(@"..\..\..\NewProject\Input\docx", true);
            //Directory.Delete(@"..\..\..\NewProject\Input\doc", true);
            //Directory.Delete(@"..\..\..\NewProject\Input\text", true);
            //Directory.Delete(@"..\..\..\NewProject\Input\tagged", true);
        }
        //
        #endregion


        /// <summary>
        ///A test for AddDocFile
        ///</summary>
        [TestMethod()]
        public void AddDocFileTest() {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.doc";

            FileManager.AddFile(sourcePath, true);
            Assert.IsTrue(File.Exists(FileManager.DocFilesDir + @"\Draft_Environmental_Assessment.doc"));
        }

        /// <summary>
        ///A test for AddDocXFile
        ///</summary>
        [TestMethod()]
        public void AddDocXFileTest() {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            FileManager.AddFile(sourcePath, true);
            Assert.IsTrue(File.Exists(FileManager.DocxFilesDir + @"\Draft_Environmental_Assessment.docx"));
        }

        /// <summary>
        ///A test for AddTextFile
        ///</summary>
        [TestMethod()]
        public void AddTextFileTest() {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.txt";

            FileManager.AddFile(sourcePath, true);
            Assert.IsTrue(File.Exists(FileManager.TextFilesDir + @"\Draft_Environmental_Assessment.txt"));
        }

        /// <summary>
        ///A test for BackupProject
        ///</summary>
        [TestMethod()]
        public void BackupProjectTest() {
            FileManager.BackupProject();
            Assert.IsTrue(Directory.Exists(Directory.GetParent(FileManager.ProjectDir).FullName + @"\backup\" + FileManager.ProjectName));
        }

        /// <summary>
        ///A test for ConvertDocFiles
        ///</summary>
        [TestMethod()]
        public void ConvertDocFilesTest() {
            DocFile[] files = (from F in Directory.EnumerateFiles(FileManager.DocFilesDir)
                               select new DocFile(F)).ToArray();
            FileManager.ConvertDocToText(files);
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.DocxFilesDir + "\\" + F.NameSansExt + ".docx"));
        }

        /// <summary>
        ///A test for ConvertDocFilesAsync
        ///</summary>
        [TestMethod()]
        public async Task ConvertDocFilesAsyncTest() {
            DocFile[] files = (from F in Directory.EnumerateFiles(FileManager.DocFilesDir)
                               select new DocFile(F)).ToArray();

            await FileManager.ConvertDocToTextAsync(files);

            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.DocxFilesDir + "\\" + F.NameSansExt + ".docx"));
        }
        /// <summary>
        ///A test for ConvertDocxToText
        ///</summary>
        [TestMethod()]
        public void ConvertDocxToTextTest() {
            DocXFile[] files = (from F in Directory.EnumerateFiles(FileManager.DocxFilesDir)
                                select new DocXFile(F)).ToArray();
            FileManager.ConvertDocxToText(files);
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.TextFilesDir + "\\" + F.NameSansExt + ".txt"));

        }


        /// <summary>
        ///A test for ConvertDocxToTextAsync
        ///</summary>
        [TestMethod()]
        public async Task ConvertDocxToTextAsyncTest() {
            DocXFile[] files = (from F in Directory.EnumerateFiles(FileManager.DocxFilesDir)
                                select new DocXFile(F)).ToArray();

            await FileManager.ConvertDocxToTextAsync(files);

            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDir + "\\" + F.NameSansExt + ".tagged"));

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
        public void TagTextFilesTest() {
            TextFile[] files = (from F in Directory.EnumerateFiles(FileManager.TextFilesDir)
                                select new TextFile(F)).ToArray();
            FileManager.TagTextFiles(files);
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDir + "\\" + F.NameSansExt + ".tagged"));
        }

        /// <summary>
        ///A test for TagTextFilesAsync
        ///</summary>
        [TestMethod()]
        public async Task TagTextFilesAsyncTest() {
            var files = (from F in Directory.EnumerateFiles(FileManager.TextFilesDir)
                         select new TextFile(F)).ToArray();
            await FileManager.TagTextFilesAsync(files);
            foreach (var F in files) {
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDir + "\\" + F.NameSansExt + ".tagged"));
            }
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
        public void BackupProjectTest1() {
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
        public void ConvertDocxToTextTest1() {
            DocXFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.ConvertDocxToText(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ConvertDocxToTextAsync
        ///</summary>
        [TestMethod()]
        public void ConvertDocxToTextAsyncTest1() {
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
        public void InitializeTest1() {
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
        public void TagTextFilesTest1() {
            TextFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.TagTextFiles(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TagTextFilesAsync
        ///</summary>
        [TestMethod()]
        public void TagTextFilesAsyncTest1() {
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
    }
}
