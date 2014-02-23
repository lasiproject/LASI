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
        private const string TEST_MOCK_FILES_RELATIVE_PATH = @"..\..\MockUserFiles";
        private const string TEST_PROJECT_RELATIVE_PATH = @"..\..\NewProject";
        private const string TEST_PROJECT_BACKUP_RELATIVE_PATH = @"..\..\backup\NewProject";
        private const string TEST_PROJECT_INPUT_RELATIVE_PATH = @"..\..\NewProject\Input";
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

            //FileManager.Initialize(TEST_PROJECT_RELATIVE_PATH);

        }


        static Func<string, string> mapExtToDir = ext => @"\" + ext.Substring(1) + @"\";


        private static DocFile[] GetTestDocFiles() {
            return new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles().Where(fi => fi.Extension == ".doc").Select(fi => new DocFile(fi.FullName)).ToArray();
        }
        private static DocXFile[] GetTestDocXFiles() {
            return new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles().Where(fi => fi.Extension == ".docx").Select(fi => new DocXFile(fi.FullName)).ToArray();
        }
        private static PdfFile[] GetTestPdfFiles() {
            return new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles().Where(fi => fi.Extension == ".pdf").Select(fi => new PdfFile(fi.FullName)).ToArray();
        }
        private static TxtFile[] GetTestTxtFiles() {
            return new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles().Where(fi => fi.Extension == ".txt").Select(fi => new TxtFile(fi.FullName)).ToArray();
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
        //[ClassCleanup()]
        //public static void MyClassCleanup() {

        //}
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize() {
            FileManager.Initialize(TEST_PROJECT_RELATIVE_PATH);
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup() {

        }

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
            Assert.IsTrue(File.Exists(FileManager.TxtFilesDir + @"\Draft_Environmental_Assessment.txt") && result is TxtFile);
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
            DocFile[] files = GetTestDocFiles();
            Assert.IsTrue(files.Any());

            FileManager.ConvertDocToText(files);
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.DocxFilesDir + "\\" + F.NameSansExt + ".docx"));
        }

        /// <summary>
        ///A test for ConvertDocFilesAsync
        ///</summary>
        [TestMethod()]
        public void ConvertDocFilesAsyncTest() {
            DocFile[] files = (from F in Directory.EnumerateFiles(FileManager.DocFilesDir)
                               select new DocFile(F)).ToArray();
            Assert.IsTrue(files.Any());
            files.ToList().ForEach(f => FileManager.AddFile(f.FullPath));

            Task.Run(async () => await FileManager.ConvertDocToTextAsync(files)).Wait();

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
            Assert.IsTrue(files.Any());
            files.ToList().ForEach(f => FileManager.AddFile(f.FullPath));

            FileManager.ConvertDocxToText(files);
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.TxtFilesDir + "\\" + F.NameSansExt + ".txt"));

        }


        /// <summary>
        ///A test for ConvertDocxToTextAsync
        ///</summary>
        [TestMethod()]
        public void ConvertDocxToTextAsyncTest() {
            DocXFile[] files = (from F in Directory.EnumerateFiles(FileManager.DocxFilesDir)
                                select new DocXFile(F)).ToArray();
            files.ToList().ForEach(f => FileManager.AddFile(f.FullPath));

            Assert.IsTrue(files.Any());
            Task.Run(async () => await FileManager.ConvertDocxToTextAsync(files)).Wait();

            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDir + "\\" + F.NameSansExt + ".tagged"));

        }

        /// <summary>
        ///A test for Initialize
        ///</summary>
        //[TestMethod()]
        //public void InitializeTest() {ws
        //    string projectDir = TEST_PROJECT_RELATIVE_PATH;
        //    FileManager.Initialize(projectDir);
        //    Assert.IsTrue(Directory.Exists(projectDir));
        //}

        /// <summary>
        ///A test for TagTextFile
        ///</summary>
        [TestMethod()]
        public void TagTextFilesTest() {
            TxtFile[] files = (from F in Directory.EnumerateFiles(FileManager.TxtFilesDir)
                               select new TxtFile(F)).ToArray();
            files.ToList().ForEach(f => FileManager.AddFile(f.FullPath));

            Assert.IsTrue(files.Any());
            FileManager.TagTextFiles(files);
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDir + "\\" + F.NameSansExt + ".tagged"));
        }

        /// <summary>
        ///A test for TagTextFilesAsync
        ///</summary>
        [TestMethod()]
        public void TagTextFilesAsyncTest() {
            var files = (from F in Directory.EnumerateFiles(FileManager.TxtFilesDir)
                         select new TxtFile(F)).ToArray();
            files.ToList().ForEach(f => FileManager.AddFile(f.FullPath));

            Assert.IsTrue(files.Any());
            Task.Run(async () => await FileManager.TagTextFilesAsync(files)).Wait();
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
            string expected = TEST_PROJECT_RELATIVE_PATH.Split('\\').Last();
            string actual;
            actual = FileManager.ProjectName;
            Assert.AreEqual(expected, actual);
        }







        /// <summary>
        ///A test for ConvertAsNeeded
        ///</summary>
        [TestMethod()]
        public void ConvertAsNeededTest() {
            var files = from fi in new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles()
                        where new[] { ".doc", ".docx", ".pdf", ".txt" }.Contains(fi.Extension, StringComparer.OrdinalIgnoreCase)
                        select fi;
            Assert.IsTrue(files.Any());
            foreach (var fi in files) {
                File.Copy(fi.FullName,
                    TEST_PROJECT_INPUT_RELATIVE_PATH + mapExtToDir(fi.Extension) + fi.FullName.Substring(fi.FullName.LastIndexOf('\\') + 1),
                    overwrite: true);
            }
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
        public void ConvertAsNeededAsyncTest() {
            var files = from fi in new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles()
                        where new[] { ".doc", ".docx", ".pdf", ".txt" }.Contains(fi.Extension, StringComparer.OrdinalIgnoreCase)
                        select fi;
            Assert.IsTrue(files.Any());
            foreach (var fi in files) {
                File.Copy(fi.FullName,
                    TEST_PROJECT_INPUT_RELATIVE_PATH + mapExtToDir(fi.Extension) + fi.FullName.Substring(fi.FullName.LastIndexOf('\\') + 1),
                    overwrite: true);
            }
            Task.Run(async () => await FileManager.ConvertAsNeededAsync());
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
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDir + "\\" + F.NameSansExt + ".tagged"));
        }


        /// <summary>
        ///A test for ConvertDocToTextAsync
        ///</summary>
        [TestMethod()]
        public void ConvertDocToTextAsyncTest() {
            DocFile[] files = GetTestDocFiles();
            Task.Run(async () => await FileManager.ConvertDocToTextAsync(files)).Wait();
            Assert.IsTrue(files.Any());
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
            Assert.IsTrue(files.Any());
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDir + "\\" + F.NameSansExt + ".txt"));
        }

        /// <summary>
        ///A test for ConvertPdfFilesAsync
        ///</summary>
        [TestMethod()]
        public void ConvertPdfToTextAsyncTest() {
            PdfFile[] files = GetTestPdfFiles();
            Task.Run(async () => await FileManager.ConvertPdfToTextAsync(files)).Wait();
            Assert.IsTrue(files.Any());
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
            foreach (var f in GetAllTestFiles()) { FileManager.AddFile(f.FullPath, overwrite: true); }
            foreach (var f in GetAllTestFiles()) {
                Assert.IsTrue(FileManager.HasSimilarFile(f.NameSansExt));
                Assert.IsTrue(FileManager.HasSimilarFile(f));

            }
        }



        /// <summary>
        ///A test for RemoveAllNotIn
        ///</summary>
        [TestMethod()]
        public void RemoveAllNotInTest() {
            IEnumerable<InputFile> filesToKeep = GetAllTestFiles().Skip(GetAllTestFiles().Count() / 2);
            foreach (var f in GetAllTestFiles()) { FileManager.AddFile(f.FullPath, overwrite: true); }
            FileManager.RemoveAllNotIn(filesToKeep);
            foreach (var f in filesToKeep) {
                Assert.IsTrue(
                    FileManager.DocFiles.Select(x => x.NameSansExt).Contains(f.NameSansExt) ||
                    FileManager.PdfFiles.Select(x => x.NameSansExt).Contains(f.NameSansExt) ||
                    FileManager.DocXFiles.Select(x => x.NameSansExt).Contains(f.NameSansExt) ||
                    FileManager.TxtFiles.Select(x => x.NameSansExt).Contains(f.NameSansExt) ||
                    FileManager.TaggedFiles.Select(x => x.NameSansExt).Contains(f.NameSansExt));
            }
        }


        /// <summary>
        ///A test for RemoveFile
        ///</summary>
        [TestMethod()]
        public void RemoveFileTest() {
            InputFile file = GetAllTestFiles().ElementAt(new Random().Next(0, GetAllTestFiles().Count()));
            FileManager.AddFile(file.FullPath);
            Assert.IsTrue(FileManager.HasSimilarFile(file));
            FileManager.RemoveFile(file);
            Assert.IsFalse(FileManager.HasSimilarFile(file));
        }





    }
}
