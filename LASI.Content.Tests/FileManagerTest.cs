using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LASI.Content.FileTypes;
using LASI.Testing.Assertions;
using LASI.Utilities;
using NFluent;
using Xunit;

namespace LASI.Content.Tests
{
    /// <summary>
    ///This is A test class for FileManagerTest and is intended
    ///to contain all FileManagerTest Unit Tests
    /// </summary>
    public class FileManagerTest
    {
        /// <summary>
        ///A test for AddDocFile
        /// </summary>
        [Fact]
        public void AddDocFileTest()
        {
            var result = FileManager.AddFile(TestDocFilePath);

            Check.That(Info(FileManager.DocFilesDirectory + @"\Test paragraph about house fires.doc")).Exists();
            Check.That(result).IsInstanceOf<DocFile>();
        }

        /// <summary>
        ///A test for AddDocXFile
        /// </summary>
        [Fact]
        public void AddDocXFileTest()
        {
            var sourcePath = TestDocXFilePath;
            var result = FileManager.AddFile(sourcePath);

            Check.That(result).IsInstanceOf<DocXFile>()
                .And.Satisfies(result.Exists);
        }

        /// <summary>
        ///A test for AddFile
        /// </summary>
        [Fact]
        public void AddDocFilePathReturnsDocFile()
        {
            var actual = FileManager.AddFile(TestDocFilePath);

            Check.That(actual).IsInstanceOf<DocFile>()
                .And.Satisfies(() => actual.FileName == "Test paragraph about house fires.doc");
        }

        [Fact]
        public void AddDocXFilePathReturnsDocXFile()
        {
            var path = TestDocXFilePath;
            var actual = FileManager.AddFile(path);

            Check.That(actual.FileName).IsEqualTo("Test paragraph about house fires.docx");
            Check.That(actual).IsInstanceOf<DocXFile>();
        }

        [Fact]
        public void AddTxtFilePathReturnsTxtFile()
        {
            var path = @"..\..\MockUserFiles\Test paragraph about house fires.txt";
            var actual = FileManager.AddFile(path);

            Check.That(actual.FileName).IsEqualTo("Test paragraph about house fires.txt");
            Check.That(actual).IsInstanceOf<TxtFile>();
        }

        [Fact]
        public void AddPdfFilePathReturnsPdfFile()
        {
            var path = @"..\..\MockUserFiles\Test paragraph about house fires.pdf";
            var actual = FileManager.AddFile(path);

            Check.That(actual.FileName).IsEqualTo("Test paragraph about house fires.pdf");
            Check.That(actual).IsInstanceOf<PdfFile>();
        }

        /// <summary>
        ///A test for AddPdfFile
        /// </summary>
        [Fact]
        public void AddPdfFileTest()
        {
            var sourcePath = @"..\..\MockUserFiles\Test paragraph about house fires.pdf";
            var result = FileManager.AddFile(sourcePath);

            Check.That(FileManager.PdfFilesDirectory + @"\Test paragraph about house fires.pdf")
                .Satisfies(File.Exists);
            Check.That(result).IsInstanceOf<PdfFile>();
        }

        /// <summary>
        ///A test for AddTextFile
        /// </summary>
        [Fact]
        public void AddTxtFileTest()
        {
            var sourcePath = @"..\..\MockUserFiles\Test paragraph about house fires.txt";
            var result = FileManager.AddFile(sourcePath);

            var txtFileInfo = new FileInfo(FileManager.TxtFilesDirectory + @"\Test paragraph about house fires.txt");

            Check.That(txtFileInfo).Exists();

            Check.That(result).IsInstanceOf<TxtFile>();
        }

        /// <summary>
        ///A test for BackupProject
        /// </summary>
        [Fact]
        public void BackupProjectTest()
        {
            FileManager.BackupProject();

            var backupDirectory = new DirectoryInfo($@"{Directory.GetParent(FileManager.ProjectDirectory).FullName}\backup\{FileManager.ProjectName}");

            Check.That(backupDirectory)
                .Exists();
        }

        /// <summary>
        ///A test for ConvertAsNeededAsync
        /// </summary>
        [Fact(Skip =
            "This test is obsolete. It consistently exhibits a race, due to parallel test execution by different PROCESSES never observed in the application proper, which is a single process.")]
        public async Task ConvertAsNeededAsyncTest()
        {
            foreach (var fileName in AllTestFiles.Select(file => file.Name))
            {
                FileManager.AddFile(fileName);
            }
            await FileManager.ConvertAsNeededAsync();

            var filesUnconverted = FileManager.AllFiles
                .Except(FileManager.TxtFiles)
                .Except(FileManager.TaggedFiles)
                .Where(file => FileManager.TxtFiles.All(tf => tf.NameSansExt != file.NameSansExt));

            Check.That(filesUnconverted).IsEmpty();
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

            Check.That(testProjectPath)
                .DoesNotSatisfy(Directory.Exists);
        }

        /// <summary>
        ///A test for HasSimilarFile
        /// </summary>
        [Fact]
        public void HasSimilarFileTest()
        {
            foreach (var file in AllTestFiles)
            {
                FileManager.AddFile(file.FullPath);
            }
            foreach (var file in AllTestFiles)
            {
                Check.That(file.NameSansExt).Satisfies(FileManager.HasSimilarFile);
                Check.That(file).Satisfies(FileManager.HasSimilarFile);
            }
        }

        /// <summary>
        ///A test for ProjectName
        /// </summary>
        [Fact]
        public void ProjectNameTest()
        {
            FileManager.Initialize(testProjectDirectory);
            var expected = testProjectDirectory.Split(Path.DirectorySeparatorChar).Last();
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
            var file = AllTestFiles.ElementAt(new Random().Next(0, AllTestFiles.Count()));
            FileManager.AddFile(file.FullPath);

            Check.That(file).Satisfies(FileManager.HasSimilarFile);

            FileManager.RemoveFile(file);

            Check.That(file).DoesNotSatisfy(FileManager.HasSimilarFile);
        }

        private IEnumerable<InputFile> AllTestFiles
        {
            get
            {
                foreach (var file in DocFiles)
                {
                    yield return file;
                }
                foreach (var file in DocXFiles)
                {
                    yield return file;
                }
                foreach (var file in PdfFiles)
                {
                    yield return file;
                }
                foreach (var file in TxtFiles)
                {
                    yield return file;
                }
            }
        }

        private static readonly Func<FileInfo, FileInfo> CopyToRunningTestDirectory = fileInfo =>
            fileInfo.CopyTo($@"{testProjectDirectory}\{fileInfo.Name}", overwrite: true);

        private static DocFile[] DocFiles => LoadInputFiles(".doc", path => new DocFile(path));

        private static DocXFile[] DocXFiles => LoadInputFiles(".docx", path => new DocXFile(path));

        private static PdfFile[] PdfFiles => LoadInputFiles(".pdf", path => new PdfFile(path));

        private static TxtFile[] TxtFiles => LoadInputFiles(".txt", path => new TxtFile(path));


        private static TInputFile[] LoadInputFiles<TInputFile>(string extension, Func<string, TInputFile> loadFile) =>
            new DirectoryInfo(MockTestFilesDirectory)
                .EnumerateFiles()
                .Where(file => file.Extension == extension)
                .Select(CopyToRunningTestDirectory)
                .Select(file => file.FullName)
                .Select(loadFile)
                .ToArray();

        public FileManagerTest()
        {
            testMethodWorkingDirectory = $@"{nameof(FileManagerTest)}";
            testProjectDirectory = $@"{testMethodWorkingDirectory}\NewProject";
            FileManager.Initialize(testProjectDirectory);
        }
        private static FileInfo Info(string fileName) => new FileInfo(fileName);

        private readonly string testMethodWorkingDirectory;
        private const string MockTestFilesDirectory = @"..\..\MockUserFiles";
        private const string TestDocFilePath = @"..\..\MockUserFiles\Test paragraph about house fires.doc";
        private static string testProjectDirectory;

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls
        private const string TestDocXFilePath = @"..\..\MockUserFiles\Test paragraph about house fires.docx";

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue) return;
            if (disposing)
            {
                if (Directory.Exists(testMethodWorkingDirectory))
                {
                    //Directory.Delete(testProjectDirectory, recursive: true);
                }
            }

            disposedValue = true;
        }

        void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
