using LASI.ContentSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is A test class for FileManagerTest and is intended
    ///to contain all FileManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FileManagerTest
    {
        #region Directory Path Settings
        private const string TEST_MOCK_FILES_RELATIVE_PATH = @"..\..\..\UnitTests\MockUserFiles\";
        private const string TEST_PROJECT_RELATIVE_PATH = @"..\..\..\NewProject";
        private const string TEST_PROJECT_BACKUP_RELATIVE_PATH = @"..\..\..\backup\NewProject";
        private const string TEST_PROJECT_INPUT_RELATIVE_PATH = @"..\..\..\NewProject\Input";
        #endregion

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
            if (Directory.Exists(TEST_PROJECT_INPUT_RELATIVE_PATH))
                Directory.Delete(TEST_PROJECT_INPUT_RELATIVE_PATH, true);
            if (Directory.Exists(TEST_PROJECT_BACKUP_RELATIVE_PATH))
                Directory.Delete(TEST_PROJECT_BACKUP_RELATIVE_PATH, true);
            if (Directory.Exists(TEST_PROJECT_RELATIVE_PATH))
                Directory.Delete(TEST_PROJECT_RELATIVE_PATH, true);

            FileManager.Initialize(TEST_PROJECT_RELATIVE_PATH);
            foreach (var fileInfo in new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles()) {
                switch (fileInfo.Extension) {
                    case ".doc":
                        File.Copy(fileInfo.FullName, TEST_PROJECT_INPUT_RELATIVE_PATH + @"\doc\" + fileInfo.FullName.Substring(fileInfo.FullName.LastIndexOf('\\') + 1), true);
                        break;
                    case ".docx":
                        File.Copy(fileInfo.FullName, TEST_PROJECT_INPUT_RELATIVE_PATH + @"\docx\" + fileInfo.FullName.Substring(fileInfo.FullName.LastIndexOf('\\') + 1), true);
                        break;
                    case ".txt":
                        File.Copy(fileInfo.FullName, TEST_PROJECT_INPUT_RELATIVE_PATH + @"\text\" + fileInfo.FullName.Substring(fileInfo.FullName.LastIndexOf('\\') + 1), true);
                        break;
                    case ".tagged":
                        File.Copy(fileInfo.FullName, TEST_PROJECT_INPUT_RELATIVE_PATH + @"\tagged\" + fileInfo.FullName.Substring(fileInfo.FullName.LastIndexOf('\\') + 1), true);
                        break;
                    default:
                        break;
                }
            }
        }

        private static DocFile[] GetTestDocFiles() {
            return new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles().Where(fi => fi.Extension == "doc").Select(fi => new DocFile(fi.FullName)).ToArray();
        }
        private static DocXFile[] GetTestDocXFiles() {
            return new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles().Where(fi => fi.Extension == "docx").Select(fi => new DocXFile(fi.FullName)).ToArray();
        }
        private static PdfFile[] GetTestPdfFiles() {
            return new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles().Where(fi => fi.Extension == "pdf").Select(fi => new PdfFile(fi.FullName)).ToArray();
        }
        private static TxtFile[] GetTestTxtFiles() {
            return new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles().Where(fi => fi.Extension == "txt").Select(fi => new TxtFile(fi.FullName)).ToArray();
        }
        private static IEnumerable<InputFile> GetAllTestFiles() {
            foreach (var f in GetTestDocFiles())
                yield return f;
            foreach (var f in GetTestDocXFiles())
                yield return f;
            foreach (var f in GetTestPdfFiles())
                yield return f;
            foreach (var f in GetTestTxtFiles())
                yield return f;
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

            var result = FileManager.AddFile(sourcePath, true);
            Assert.IsTrue(File.Exists(FileManager.DocFilesDir + @"\Draft_Environmental_Assessment.doc") && result is DocFile);
        }

        /// <summary>
        ///A test for AddDocXFile
        ///</summary>
        [TestMethod()]
        public void AddDocXFileTest() {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            var result = FileManager.AddFile(sourcePath, true);
            Assert.IsTrue(File.Exists(FileManager.DocxFilesDir + @"\Draft_Environmental_Assessment.docx") && result is DocXFile);
        }

        /// <summary>
        ///A test for AddTextFile
        ///</summary>
        [TestMethod()]
        public void AddTxtFileTest() {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.txt";

            var result = FileManager.AddFile(sourcePath, true);
            Assert.IsTrue(File.Exists(FileManager.TextFilesDir + @"\Draft_Environmental_Assessment.txt") && result is TxtFile);
        }
        /// <summary>
        ///A test for AddPdfFile
        ///</summary>
        [TestMethod()]
        public void AddPdfFileTest() {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.pdf";

            var result = FileManager.AddFile(sourcePath, true);
            Assert.IsTrue(File.Exists(FileManager.PdfFilesDir + @"\Draft_Environmental_Assessment.pdf") && result is PdfFile);
        }
        /// <summary>
        ///A test for AddFile
        ///</summary>
        [TestMethod()]
        public void AddFileTest() {
            string path = string.Empty;
            bool overwrite = true;
            InputFile actual;
            path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.doc";
            actual = FileManager.AddFile(path, overwrite);
            Assert.IsTrue(actual.FileName == "Draft_Environmental_Assessment.doc" && actual is DocFile);

            path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            actual = FileManager.AddFile(path, overwrite);
            Assert.IsTrue(actual.FileName == "Draft_Environmental_Assessment.docx" && actual is DocXFile);

            path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.txt";
            actual = FileManager.AddFile(path, overwrite);
            Assert.IsTrue(actual.FileName == "Draft_Environmental_Assessment.txt" && actual is TxtFile);

            path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.pdf";
            actual = FileManager.AddFile(path, overwrite);
            Assert.IsTrue(actual.FileName == "Draft_Environmental_Assessment.pdf" && actual is PdfFile);
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
            TxtFile[] files = (from F in Directory.EnumerateFiles(FileManager.TextFilesDir)
                               select new TxtFile(F)).ToArray();
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
                         select new TxtFile(F)).ToArray();
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
            FileManager.Initialize(TEST_PROJECT_RELATIVE_PATH);
            string expected = "NewProject";
            string actual;
            actual = FileManager.ProjectName;
            Assert.AreEqual(expected, actual);
        }







        /// <summary>
        ///A test for ConvertAsNeeded
        ///</summary>
        [TestMethod()]
        public void ConvertAsNeededTest() {
            FileManager.ConvertAsNeeded();
            IEnumerable<InputFile> filesUnconverted =
                from file in FileManager.DocFiles
                                        .Concat<InputFile>(FileManager.DocXFiles)
                                        .Concat<InputFile>(FileManager.PdfFiles)
                where FileManager.TxtFiles.Any(tf => tf.NameSansExt == file.NameSansExt)
                select file;
            Assert.IsFalse(filesUnconverted.Any());
        }

        /// <summary>
        ///A test for ConvertAsNeededAsync
        ///</summary>
        [TestMethod()]
        public async void ConvertAsNeededAsyncTest() {
            await FileManager.ConvertAsNeededAsync();
            IEnumerable<InputFile> filesUnconverted =
                from file in FileManager.DocFiles
                                        .Concat<InputFile>(FileManager.DocXFiles)
                                        .Concat<InputFile>(FileManager.PdfFiles)
                where FileManager.TxtFiles.Any(tf => tf.NameSansExt == file.NameSansExt)
                select file;
            Assert.IsFalse(filesUnconverted.Any());
        }

        /// <summary>
        ///A test for ConvertDocToText
        ///</summary>
        [TestMethod()]
        public void ConvertDocToTextTest() {
            DocFile[] files = GetTestDocFiles();
            if (!files.Any())
                Assert.Inconclusive();
            FileManager.ConvertDocToText(files);
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDir + "\\" + F.NameSansExt + ".txt"));
        }


        /// <summary>
        ///A test for ConvertDocToTextAsync
        ///</summary>
        [TestMethod()]
        public async void ConvertDocToTextAsyncTest() {
            DocFile[] files = GetTestDocFiles();
            await FileManager.ConvertDocToTextAsync(files);
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDir + "\\" + F.NameSansExt + ".txt"));
        }


        /// <summary>
        ///A test for ConvertPdfToText
        ///</summary>
        [TestMethod()]
        public void ConvertPdfToTextTest() {
            PdfFile[] files = GetTestPdfFiles();
            FileManager.ConvertPdfToText(files);
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDir + "\\" + F.NameSansExt + ".txt"));
        }

        /// <summary>
        ///A test for ConvertPdfFilesAsync
        ///</summary>
        [TestMethod()]
        public async void ConvertPdfToTextAsyncTest() {
            PdfFile[] files = GetTestPdfFiles();
            await FileManager.ConvertPdfToTextAsync(files);
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDir + "\\" + F.NameSansExt + ".pdf"));
        }


        /// <summary>
        ///A test for DecimateProject
        ///</summary>
        [TestMethod()]
        public void DecimateProjectTest() {
            FileManager.DecimateProject();
            Assert.IsFalse(Directory.Exists(TEST_PROJECT_RELATIVE_PATH));
        }

        /// <summary>
        ///A test for HasSimilarFile
        ///</summary>
        [TestMethod()]
        public void HasSimilarFileTest() {
            foreach (var inputFile in GetAllTestFiles()) {
                Assert.IsTrue(FileManager.HasSimilarFile(inputFile));
            }
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
            TxtFile[] files = null; // TODO: Initialize to an appropriate value
            FileManager.TagTextFiles(files);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for TagTextFilesAsync
        ///</summary>
        [TestMethod()]
        public void TagTextFilesAsyncTest1() {
            TxtFile[] files = null; // TODO: Initialize to an appropriate value
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
            IReadOnlyList<TxtFile> actual;
            actual = FileManager.TxtFiles;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
