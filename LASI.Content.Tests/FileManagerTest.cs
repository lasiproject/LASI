using LASI.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LASI.Utilities;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is A test class for FileManagerTest and is intended
    ///to contain all FileManagerTest Unit Tests
    ///</summary>
    [TestClass]
    public class FileManagerTest
    {
        #region Directory Path Settings
        private const string TEST_MOCK_FILES_RELATIVE_PATH = @"..\..\MockUserFiles";
        private static string testProjectDirectory = @"..\..\NewProject";
        #endregion

        /// <summary>
        ///A test for AddDocFile
        ///</summary>
        [TestMethod]
        public void AddDocFileTest()
        {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.doc";

            var result = FileManager.AddFile(sourcePath);
            Assert.IsTrue(File.Exists(FileManager.DocFilesDirectory + @"\Draft_Environmental_Assessment.doc") && result is DocFile);
        }

        /// <summary>
        ///A test for AddDocXFile
        ///</summary>
        [TestMethod]
        public void AddDocXFileTest()
        {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            var result = FileManager.AddFile(sourcePath);
            Assert.IsTrue(File.Exists(FileManager.DocxFilesDirectory + @"\Draft_Environmental_Assessment.docx") && result is DocXFile);
        }

        /// <summary>
        ///A test for AddFile
        ///</summary>
        [TestMethod]
        public void AddFileTest()
        {
            string path = string.Empty;
            InputFile actual;
            path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.doc";
            actual = FileManager.AddFile(path);
            Assert.IsTrue(actual.FileName == "Draft_Environmental_Assessment.doc" && actual is DocFile);

            path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            actual = FileManager.AddFile(path);
            Assert.IsTrue(actual.FileName == "Draft_Environmental_Assessment.docx" && actual is DocXFile);

            path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.txt";
            actual = FileManager.AddFile(path);
            Assert.IsTrue(actual.FileName == "Draft_Environmental_Assessment.txt" && actual is TxtFile);

            path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.pdf";
            actual = FileManager.AddFile(path);
            Assert.IsTrue(actual.FileName == "Draft_Environmental_Assessment.pdf" && actual is PdfFile);
        }

        /// <summary>
        ///A test for AddPdfFile
        ///</summary>
        [TestMethod]
        public void AddPdfFileTest()
        {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.pdf";

            var result = FileManager.AddFile(sourcePath);
            Assert.IsTrue(File.Exists(FileManager.PdfFilesDirectory + @"\Draft_Environmental_Assessment.pdf") && result is PdfFile);
        }

        /// <summary>
        ///A test for AddTextFile
        ///</summary>
        [TestMethod]
        public void AddTxtFileTest()
        {
            string sourcePath = @"..\..\..\TestDocs\Draft_Environmental_Assessment.txt";

            var result = FileManager.AddFile(sourcePath);
            Assert.IsTrue(File.Exists(FileManager.TxtFilesDirectory + @"\Draft_Environmental_Assessment.txt") && result is TxtFile);
        }

        /// <summary>
        ///A test for BackupProject
        ///</summary>
        [TestMethod]
        public void BackupProjectTest()
        {
            FileManager.BackupProject();
            Assert.IsTrue(Directory.Exists(Directory.GetParent(FileManager.ProjectDirectory).FullName + @"\backup\" + FileManager.ProjectName));
        }

        /// <summary>
        ///A test for ConvertAsNeededAsync
        ///</summary>
        [TestMethod]
        public void
        ConvertAsNeededAsyncTest()
        {
            ConvertAsNeededAsyncTestHelper().Wait();
        }

        /// <summary>
        ///A test for ConvertAsNeeded
        ///</summary>
        [TestMethod]
        public void ConvertAsNeededTest()
        {
            var files = from fileInfo in new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles()
                        where new[] { ".doc", ".docx", ".pdf", ".txt" }.Contains(fileInfo.Extension, StringComparer.OrdinalIgnoreCase)
                        select fileInfo;
            Assert.IsTrue(files.Any());
            foreach (var fi in files)
            {
                File.Copy(fi.FullName,
                    testProjectDirectory + "\\input" + mapExtToDir(fi.Extension) + fi.FullName.Substring(fi.FullName.LastIndexOf('\\') + 1),
                    overwrite: true);
            }
            FileManager.ConvertAsNeeded();
            var numberOfUnconvertedFiles =
                 new IEnumerable<InputFile>[] { FileManager.DocFiles, FileManager.DocXFiles, FileManager.PdfFiles }
                 .Aggregate(Enumerable.Concat)
                 .ExceptBy(FileManager.TxtFiles, file => file.NameSansExt)
                 .Count();
            Assert.AreEqual(0, numberOfUnconvertedFiles);
        }

        /// <summary>
        ///A test for ConvertDocFilesAsync
        ///</summary>
        [TestMethod]
        public void ConvertDocFilesAsyncTest()
        {
            ConvertDocFilesAsyncTestHelper().Wait();
        }

        private static async Task ConvertDocFilesAsyncTestHelper()
        {
            DocFile[] files = GetTestDocFiles();
            Assert.IsTrue(files.Any());
            await FileManager.ConvertDocToTextAsync(files);

            foreach (var file in files)
                Assert.IsTrue(File.Exists(FileManager.TxtFilesDirectory + "\\" + file.NameSansExt + ".txt"));
        }


        /// <summary>
        ///A test for ConvertDocFiles
        ///</summary>
        [TestMethod]
        public void ConvertDocFilesTest()
        {
            DocFile[] files = GetTestDocFiles();
            Assert.IsTrue(files.Any());

            FileManager.ConvertDocToText(files);
            foreach (var file in files)
                Assert.IsTrue(File.Exists(FileManager.TxtFilesDirectory + "\\" + file.NameSansExt + ".txt"));
        }

        /// <summary>
        ///A test for ConvertDocToTextAsync
        ///</summary>
        [TestMethod]
        public void ConvertDocToTextAsyncTest()
        {
            DocFile[] files = GetTestDocFiles();
            FileManager.ConvertDocToTextAsync(files).Wait();
            Assert.IsTrue(files.Any());
            foreach (var file in files)
                Assert.IsTrue(File.Exists(FileManager.TxtFilesDirectory + "\\" + file.NameSansExt + ".txt"));
        }

        /// <summary>
        ///A test for ConvertDocToText
        ///</summary>
        [TestMethod]
        public void ConvertDocToTextTest()
        {
            DocFile[] files = GetTestDocFiles();
            if (!files.Any())
                Assert.Inconclusive();
            FileManager.ConvertDocToText(files);

            foreach (var file in files)
                Assert.IsTrue(File.Exists(FileManager.TxtFilesDirectory + "\\" + file.NameSansExt + ".txt"));
        }

        /// <summary>
        ///A test for ConvertDocxToTextAsync
        ///</summary>
        [TestMethod]
        public void ConvertDocxToTextAsyncTest()
        {
            ConvertDocxToTextAsyncTestHelper().Wait();
        }

        /// <summary>
        ///A test for ConvertDocxToText
        ///</summary>
        [TestMethod]
        public void ConvertDocxToTextTest()
        {
            DocXFile[] files = GetTestDocXFiles();

            FileManager.ConvertDocxToText(files);
            foreach (var file in files)
                Assert.IsTrue(File.Exists(FileManager.TxtFilesDirectory + "\\" + file.NameSansExt + ".txt"));

        }

        /// <summary>
        ///A test for ConvertPdfFilesAsync
        ///</summary>
        [TestMethod]
        public void ConvertPdfToTextAsyncTest()
        {
            PdfFile[] files = GetTestPdfFiles();
            if (!files.Any())
                Assert.Inconclusive();
            FileManager.ConvertPdfToTextAsync(files).Wait();
            Assert.IsTrue(files.Any());
            foreach (var file in files)
                Assert.IsTrue(File.Exists(FileManager.TxtFilesDirectory + "\\" + file.NameSansExt + ".txt"));
        }

        /// <summary>
        ///A test for ConvertPdfToText
        ///</summary>
        [TestMethod]
        public void ConvertPdfToTextTest()
        {
            PdfFile[] files = GetTestPdfFiles();
            if (!files.Any())
                Assert.Inconclusive();
            FileManager.ConvertPdfToText(files);

            foreach (var file in files)
                Assert.IsTrue(File.Exists(FileManager.TxtFilesDirectory + "\\" + file.NameSansExt + ".txt"));
        }

        /// <summary>
        ///A test for DecimateProject
        ///</summary>
        [TestMethod]
        public void DecimateProjectTest()
        {
            FileManager.Initialize(testProjectDirectory + "decimate_test");
            FileManager.DecimateProject();
            Assert.IsFalse(Directory.Exists(testProjectDirectory + "decimate_test"));
        }

        /// <summary>
        ///A test for HasSimilarFile
        ///</summary>
        [TestMethod]
        public void HasSimilarFileTest()
        {
            foreach (var file in GetAllTestFiles())
            {
                FileManager.AddFile(file.FullPath);
            }
            foreach (var file in GetAllTestFiles())
            {
                Assert.IsTrue(FileManager.HasSimilarFile(file.NameSansExt));
                Assert.IsTrue(FileManager.HasSimilarFile(file));
            }
        }

        /// <summary>
        ///A test for ProjectName
        ///</summary>
        [TestMethod]
        public void ProjectNameTest()
        {
            FileManager.Initialize(testProjectDirectory);
            string expected = testProjectDirectory.Split('\\').Last();
            string actual;
            actual = FileManager.ProjectName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RemoveAllNotIn
        ///</summary>
        [TestMethod]
        public void RemoveAllNotInTest()
        {
            IEnumerable<InputFile> filesToKeep = GetAllTestFiles().Skip(GetAllTestFiles().Count() / 2);
            foreach (var file in GetAllTestFiles()) { FileManager.AddFile(file.FullPath); }
            FileManager.RemoveAllNotIn(filesToKeep);
            foreach (var file in filesToKeep)
            {
                Assert.IsTrue(
                    FileManager.DocFiles.Select(x => x.NameSansExt).Contains(file.NameSansExt) ||
                    FileManager.PdfFiles.Select(x => x.NameSansExt).Contains(file.NameSansExt) ||
                    FileManager.DocXFiles.Select(x => x.NameSansExt).Contains(file.NameSansExt) ||
                    FileManager.TxtFiles.Select(x => x.NameSansExt).Contains(file.NameSansExt) ||
                    FileManager.TaggedFiles.Select(x => x.NameSansExt).Contains(file.NameSansExt));
            }
        }

        /// <summary>
        ///A test for RemoveFile
        ///</summary>
        [TestMethod]
        public void RemoveFileTest()
        {
            InputFile file = GetAllTestFiles().ElementAt(new Random().Next(0, GetAllTestFiles().Count()));
            FileManager.AddFile(file.FullPath);
            Assert.IsTrue(FileManager.HasSimilarFile(file));
            FileManager.RemoveFile(file);
            Assert.IsFalse(FileManager.HasSimilarFile(file));
        }

        /// <summary>
        ///A test for TagTextFilesAsync
        ///</summary>
        [TestMethod]
        public void TagTextFilesAsyncTest()
        {
            TagTextFilesAsyncTestHelper().Wait();
        }

        private static async Task TagTextFilesAsyncTestHelper()
        {
            var files = GetTestTxtFiles();
            Assert.IsTrue(files.Any());

            await FileManager.TagTextFilesAsync(files);
            foreach (var file in files)
            {
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDirectory + "\\" + file.NameSansExt + ".tagged"));
            }
        }


        /// <summary>
        ///A test for TagTextFile
        ///</summary>
        [TestMethod]
        public void TagTextFilesTest()
        {
            TxtFile[] files = GetTestTxtFiles();

            Assert.IsTrue(files.Any());
            FileManager.TagTextFiles(files);
            foreach (var F in files)
                Assert.IsTrue(File.Exists(FileManager.TaggedFilesDirectory + "\\" + F.NameSansExt + ".tagged"));
        }

        private static async Task ConvertAsNeededAsyncTestHelper()
        {
            var files = from fileInfo in new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH).EnumerateFiles()
                        where new[] { ".doc", ".docx", ".pdf", ".txt" }.Contains(fileInfo.Extension, StringComparer.OrdinalIgnoreCase)
                        select fileInfo;
            Assert.IsTrue(files.Any());
            foreach (var fi in files)
            {
                File.Copy(fi.FullName,
                    testProjectDirectory + "\\input" + mapExtToDir(fi.Extension) + fi.FullName.Substring(fi.FullName.LastIndexOf('\\') + 1),
                    overwrite: true);
            }
            await FileManager.ConvertAsNeededAsync();
            IEnumerable<InputFile> filesUnconverted =
                from file in FileManager.AllFiles.Except(FileManager.TxtFiles).Except(FileManager.TaggedFiles)
                where !FileManager.TxtFiles.Any(tf => tf.NameSansExt == file.NameSansExt)
                select file;
            Assert.IsFalse(filesUnconverted.Any());
        }

        private static async Task ConvertDocxToTextAsyncTestHelper()
        {
            DocXFile[] files = (from file in Directory.EnumerateFiles(FileManager.DocxFilesDirectory)
                                select new DocXFile(file)).ToArray();
            files.ToList().ForEach(f => FileManager.AddFile(f.FullPath));

            Assert.IsTrue(files.Any());
            await FileManager.ConvertDocxToTextAsync(files);

            foreach (var file in files)
                Assert.IsTrue(File.Exists(FileManager.TxtFilesDirectory + "\\" + file.NameSansExt + ".txt"));
        }


        private static DocFile[] GetTestDocFiles()
        {
            return new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH)
                .EnumerateFiles()
                .Where(f => f.Extension == ".doc")
                .Select(fi => new DocFile(fi.FullName))
                .ToArray();
        }

        private static DocXFile[] GetTestDocXFiles()
        {
            return new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH)
                .EnumerateFiles()
                .Where(i => i.Extension == ".docx")
                .Select(fi => new DocXFile(fi.FullName))
                .ToArray();
        }

        private static PdfFile[] GetTestPdfFiles()
        {
            return new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH)
                .EnumerateFiles()
                .Where(i => i.Extension == ".pdf")
                .Select(fi => new PdfFile(fi.FullName))
                .ToArray();
        }

        private static TxtFile[] GetTestTxtFiles()
        {
            return new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH)
                .EnumerateFiles()
                .Where(i => i.Extension == ".txt")
                .Select(fi => new TxtFile(fi.FullName))
                .ToArray();
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        private static IEnumerable<InputFile> GetAllTestFiles()
        {
            foreach (var file in GetTestDocFiles())
                yield return file;
            foreach (var file in GetTestDocXFiles())
                yield return file;
            foreach (var file in GetTestPdfFiles())
                yield return file;
            foreach (var file in GetTestTxtFiles())
                yield return file;
        }
        static Func<string, string> mapExtToDir = ext => @"\" + ext.Substring(1) + @"\";
        private TestContext testContextInstance;
        #region Additional test attributes
        ////  Use ClassCleanup to run code after all tests in A class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            FileManager.DecimateProject();
        }
        [TestCleanup]
        public void MyTestCleanup()
        {
            FileManager.DecimateProject();
        }
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            if (Directory.Exists(testProjectDirectory))
            {
                Directory.Delete(testProjectDirectory, recursive: true);
            }
            FileManager.Initialize(testProjectDirectory);
            GetAllTestFiles().ToList().ForEach(file => FileManager.AddFile(file.FullPath));


        }
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            if (Directory.Exists(testProjectDirectory))
            {
                try
                {
                    Directory.Delete(testProjectDirectory, recursive: true);
                }
                catch (IOException)
                {
                    testProjectDirectory += "011";
                }
            }
            FileManager.Initialize(testProjectDirectory);
        }
        #endregion
    }
}
