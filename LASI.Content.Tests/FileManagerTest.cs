using LASI.Content;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LASI.Utilities;
using Shared.Test.Assertions;
using Xunit;
using NFluent;
using LASI.Content.Tests.Extensions;

namespace LASI.Content.Tests
{
    /// <summary>
    ///This is A test class for FileManagerTest and is intended
    ///to contain all FileManagerTest Unit Tests
    /// </summary>
    public class FileManagerTest : IDisposable
    {

        /// <summary>
        ///A test for AddDocFile
        /// </summary>
        [Fact]
        public void AddDocFileTest()
        {
            string sourcePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.doc";

            var result = FileManager.AddFile(sourcePath);
            Assert.True(File.Exists(FileManager.DocFilesDirectory + @"\Draft_Environmental_Assessment.doc") && result is DocFile);
        }

        /// <summary>
        ///A test for AddDocXFile
        /// </summary>
        [Fact]
        public void AddDocXFileTest()
        {
            string sourcePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.docx";
            var result = FileManager.AddFile(sourcePath);
            Check.That(result.Exists()).IsTrue();
            Check.That(result).IsInstanceOf<DocXFile>();
        }

        /// <summary>
        ///A test for AddFile
        /// </summary>
        [Fact]
        public void AddFileTest()
        {
            string path = string.Empty;
            InputFile actual;
            path = @"..\..\MockUserFiles\Draft_Environmental_Assessment.doc";
            actual = FileManager.AddFile(path);
            Assert.True(actual.FileName == "Draft_Environmental_Assessment.doc" && actual is DocFile);

            path = @"..\..\MockUserFiles\Draft_Environmental_Assessment.docx";
            actual = FileManager.AddFile(path);
            Assert.True(actual.FileName == "Draft_Environmental_Assessment.docx" && actual is DocXFile);

            path = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";
            actual = FileManager.AddFile(path);
            Assert.True(actual.FileName == "Draft_Environmental_Assessment.txt" && actual is TxtFile);

            path = @"..\..\MockUserFiles\Draft_Environmental_Assessment.pdf";
            actual = FileManager.AddFile(path);
            Assert.True(actual.FileName == "Draft_Environmental_Assessment.pdf" && actual is PdfFile);
        }

        /// <summary>
        ///A test for AddPdfFile
        /// </summary>
        [Fact]
        public void AddPdfFileTest()
        {
            string sourcePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.pdf";

            var result = FileManager.AddFile(sourcePath);
            Assert.True(File.Exists(FileManager.PdfFilesDirectory + @"\Draft_Environmental_Assessment.pdf") && result is PdfFile);
        }

        /// <summary>
        ///A test for AddTextFile
        /// </summary>
        [Fact]
        public void AddTxtFileTest()
        {
            string sourcePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";

            var result = FileManager.AddFile(sourcePath);
            Assert.True(File.Exists(FileManager.TxtFilesDirectory + @"\Draft_Environmental_Assessment.txt") && result is TxtFile);
        }

        /// <summary>
        ///A test for BackupProject
        /// </summary>
        [Fact]
        public void BackupProjectTest()
        {
            FileManager.BackupProject();
            Check.That(Directory.GetParent(FileManager.ProjectDirectory).FullName + @"\backup\" + FileManager.ProjectName).Satisfies(Directory.Exists);
        }

        /// <summary>
        ///A test for ConvertAsNeededAsync
        /// </summary>
        [Fact]
        public async Task ConvertAsNeededAsyncTest()
        {
            var files = GetAllTestFiles();

            foreach (var file in files)
            {
                Check.That(file.Exists()).IsTrue();
            }
            Check.That(files).Not.IsEmpty();

            files.ToList().ForEach(file => FileManager.AddFile(file.FileName));

            await FileManager.ConvertAsNeededAsync();
            IEnumerable<InputFile> filesUnconverted =
                from file in FileManager.AllFiles.Except(FileManager.TxtFiles).Except(FileManager.TaggedFiles)
                where !FileManager.TxtFiles.Any(tf => tf.NameSansExt == file.NameSansExt)
                select file;
            Check.That(filesUnconverted).IsEmpty();
        }

        /// <summary>
        ///A test for ConvertAsNeeded
        /// </summary>
        [Fact]
        public void ConvertAsNeededTest()
        {
            var files = from input in GetAllTestFiles()
                        select input;
            Assert.True(files.Any());

            files.ToList().ForEach(file => FileManager.AddFile(file.FileName));

            FileManager.ConvertAsNeeded();
            var numberOfUnconvertedFiles =
                 new IEnumerable<InputFile>[] { FileManager.DocFiles, FileManager.DocXFiles, FileManager.PdfFiles }
                 .Aggregate(Enumerable.Concat)
                 .ExceptBy(FileManager.TxtFiles, file => file.NameSansExt)
                 .Count();
            Check.That(numberOfUnconvertedFiles).IsZero();
        }

        /// <summary>
        ///A test for ConvertDocFilesAsync
        /// </summary>
        [Fact]
        public async Task ConvertDocFilesAsyncTest()
        {
            DocFile[] files = GetTestDocFiles();
            Assert.True(files.Any());
            await FileManager.ConvertDocToTextAsync(files);

            foreach (var file in files)
            {
                Assert.True(File.Exists(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt")));
            }
        }

        ///// <summary>
        /////A test for ConvertDocFiles
        ///// </summary>
        //[TestMethod]
        //public void ConvertDocFilesTest()
        //{
        //    DocFile[] files = GetTestDocFiles();
        //    Assert.True(files.Any());
        //    try
        //    {
        //        FileManager.ConvertDocToText(files);
        //        foreach (var file in files)
        //        {
        //            Assert.True(File.Exists(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt")));
        //        }
        //    }
        //    catch (IOException e) when (e.Message.StartsWith("The process cannot access the file '") && e.Message.EndsWith("' because it is being used by another process."))
        //    {
        //        Assert.Inconclusive(e.Message);
        //    }

        //}

        /// <summary>
        ///A test for ConvertDocToTextAsync
        /// </summary>
        [Fact]
        public async Task ConvertDocToTextAsyncTest()
        {
            DocFile[] files = GetTestDocFiles();
            Assert.True(files.Any());
            await FileManager.ConvertDocToTextAsync(files);
            foreach (var file in files)
            {
                Assert.True(File.Exists(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt")));
            }
        }

        /// <summary>
        ///A test for ConvertDocToText
        /// </summary>
        [Fact]
        public void ConvertDocToTextTest()
        {
            DocFile[] files = GetTestDocFiles();
            Assert.True(files.Any());

            FileManager.ConvertDocToText(files);
            foreach (var file in files)
            {
                Assert.True(File.Exists(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt")));
            }
        }

        /// <summary>
        /// A test for ConvertDocxToTextAsync 
        /// </summary>
        [Fact]
        public async Task ConvertDocxToTextAsyncTest()
        {
            DocXFile[] files = GetTestDocXFiles();
            Assert.True(files.Any());
            foreach (var path in from file in files select file.FullPath)
            {
                FileManager.AddFile(path);
            }
            EnumerableAssert.AreSetEqual(
                files.Select(f => f.FileName),
                from file in Directory.EnumerateFiles(FileManager.DocxFilesDirectory)
                select new DocXFile(file).FileName, StringComparer.OrdinalIgnoreCase
            );

            await FileManager.ConvertDocxToTextAsync(files);

            foreach (var file in files)
            {
                Assert.True(File.Exists(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt")));
            }
        }

        /// <summary>
        ///A test for ConvertDocxToText
        /// </summary>
        [Fact]
        public void ConvertDocxToTextTest()
        {
            DocXFile[] files = GetTestDocXFiles();

            FileManager.ConvertDocxToText(files);
            foreach (var file in files)
            {
                Assert.True(File.Exists(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt")));
            }
        }

        /// <summary>
        ///A test for ConvertPdfFilesAsync
        /// </summary>
        [Fact]
        public async Task ConvertPdfToTextAsyncTest()
        {
            PdfFile[] files = GetTestPdfFiles();

            await FileManager.ConvertPdfToTextAsync(files);
            Assert.True(files.Any());
            foreach (var file in files)
            {
                Assert.True(File.Exists(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt")));
            }
        }

        /// <summary>
        ///A test for ConvertPdfToText
        /// </summary>
        [Fact]
        public void ConvertPdfToTextTest()
        {
            PdfFile[] files = GetTestPdfFiles();

            FileManager.ConvertPdfToText(files);
            foreach (var file in files)
            {
                Assert.True(File.Exists(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt")));
            }
        }

        /// <summary>
        ///A test for DecimateProject
        /// </summary>
        [Fact]
        public void DecimateProjectTest()
        {
            var testProjectPath = testProjectDirectory + "\\decimate_test";
            FileManager.Initialize(testProjectPath);
            FileManager.DecimateProject();
            Assert.False(Directory.Exists(testProjectPath));
        }

        /// <summary>
        ///A test for HasSimilarFile
        /// </summary>
        [Fact]
        public void HasSimilarFileTest()
        {
            foreach (var file in GetAllTestFiles())
            {
                FileManager.AddFile(file.FullPath);
            }
            foreach (var file in GetAllTestFiles())
            {
                Assert.True(FileManager.HasSimilarFile(file.NameSansExt));
                Assert.True(FileManager.HasSimilarFile(file));
            }
        }

        /// <summary>
        ///A test for ProjectName
        /// </summary>
        [Fact]
        public void ProjectNameTest()
        {
            FileManager.Initialize(testProjectDirectory);
            string expected = testProjectDirectory.Split('\\').Last();
            string actual;
            actual = FileManager.ProjectName;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for RemoveFile
        /// </summary>
        [Fact]
        public void RemoveFileTest()
        {
            InputFile file = GetAllTestFiles().ElementAt(new Random().Next(0, GetAllTestFiles().Count()));
            FileManager.AddFile(file.FullPath);
            Assert.True(FileManager.HasSimilarFile(file));
            FileManager.RemoveFile(file);
            Assert.False(FileManager.HasSimilarFile(file));
        }

        /// <summary>
        ///A test for TagTextFilesAsync
        /// </summary>
        [Fact]
        public async Task TagTextFilesAsyncTest()
        {
            var files = GetTestTxtFiles();
            Assert.True(files.Any());

            await FileManager.TagTextFilesAsync(files);
            foreach (var file in files)
            {
                Assert.True(File.Exists(Path.Combine(FileManager.TaggedFilesDirectory, file.NameSansExt + ".tagged")));
            }
        }

        /// <summary>
        ///A test for TagTextFile
        /// </summary>
        [Fact]
        public void TagTextFilesTest()
        {
            TxtFile[] files = GetTestTxtFiles();

            Assert.True(files.Any());
            FileManager.TagTextFiles(files);
            foreach (var file in files)
            {
                Assert.True(File.Exists(Path.Combine(FileManager.TaggedFilesDirectory, file.NameSansExt + ".tagged")));
            }
        }

        private IEnumerable<InputFile> GetAllTestFiles()
        {
            foreach (var file in GetTestDocFiles()) yield return file;
            foreach (var file in GetTestDocXFiles()) yield return file;
            foreach (var file in GetTestPdfFiles()) yield return file;
            foreach (var file in GetTestTxtFiles()) yield return file;
        }

        private FileInfo CopyToRunningTestDirectory(FileInfo fileInfo) => fileInfo.CopyTo($@"{testProjectDirectory}\{fileInfo.Name}", overwrite: true);

        private DocFile[] GetTestDocFiles() => new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH)
                .EnumerateFiles()
                .Where(f => f.Extension == ".doc")
                .Select(CopyToRunningTestDirectory)
                .Select(f => new DocFile(f.FullName))
                .ToArray();

        private DocXFile[] GetTestDocXFiles() => new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH)
                .EnumerateFiles()
                .Where(i => i.Extension == ".docx")
                .Select(CopyToRunningTestDirectory)
                .Select(f => new DocXFile(f.FullName))
                .ToArray();

        private PdfFile[] GetTestPdfFiles() => new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH)
                .EnumerateFiles()
                .Where(i => i.Extension == ".pdf")
                .Select(CopyToRunningTestDirectory)
                .Select(f => new PdfFile(f.FullName))
                .ToArray();

        private TxtFile[] GetTestTxtFiles() => new DirectoryInfo(TEST_MOCK_FILES_RELATIVE_PATH)
                .EnumerateFiles()
                .Where(f => f.Extension == ".txt")
                .Select(CopyToRunningTestDirectory)
                .Select(f => new TxtFile(f.FullName))
                .ToArray();



        private static Func<string, string> MapExtToDir = ext => @"\" + ext.Substring(1) + @"\";

        #region Setup and Teardown

        public FileManagerTest()
        {
            TestOffset += typeof(FileManagerTest).GetMethods().Count(m => m.CustomAttributes.Any(a => a.AttributeType.Name == typeof(FactAttribute).Name));
            testMethodWorkingDirectory = $@"{nameof(FileManagerTest)}\{TestOffset}";
            Directory.CreateDirectory(testMethodWorkingDirectory);
            testProjectDirectory = $@"{testMethodWorkingDirectory}\NewProject";
            FileManager.Initialize(testProjectDirectory);
        }

        private static int TestOffset;
        private readonly string testMethodWorkingDirectory;
        private const string TEST_MOCK_FILES_RELATIVE_PATH = @"..\..\MockUserFiles";
        private string testProjectDirectory;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Directory.Exists(testMethodWorkingDirectory))
                    {
                        //Directory.Delete(testProjectDirectory, recursive: true);
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        #endregion Setup and Teardown
    }
}