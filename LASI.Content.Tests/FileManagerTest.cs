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
using Shared.Test.NFluentExtensions;

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
            var sourcePath = @"..\..\MockUserFiles\Test paragraph about house fires.doc";
            var result = FileManager.AddFile(sourcePath);

            Check.That(FileManager.DocFilesDirectory + @"\Test paragraph about house fires.doc").Satisfies(File.Exists);
            Check.That(result).IsInstanceOf<DocFile>();
        }

        /// <summary>
        ///A test for AddDocXFile
        /// </summary>
        [Fact]
        public void AddDocXFileTest()
        {
            var sourcePath = @"..\..\MockUserFiles\Test paragraph about house fires.docx";
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
            var path = @"..\..\MockUserFiles\Test paragraph about house fires.doc";
            var actual = FileManager.AddFile(path);

            Check.That(actual).IsInstanceOf<DocFile>()
                .And.Satisfies(() => actual.FileName == "Test paragraph about house fires.doc");
        }

        [Fact]
        public void AddDocXFilePathReturnsDocXFile()
        {
            var path = @"..\..\MockUserFiles\Test paragraph about house fires.docx";
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

            Check.That(FileManager.TxtFilesDirectory + @"\Test paragraph about house fires.txt")
                 .Satisfies(File.Exists);
            Check.That(result).IsInstanceOf<TxtFile>();
        }

        /// <summary>
        ///A test for BackupProject
        /// </summary>
        [Fact]
        public void BackupProjectTest()
        {
            FileManager.BackupProject();

            Check.That(Directory.GetParent(FileManager.ProjectDirectory).FullName + @"\backup\" + FileManager.ProjectName)
                 .Satisfies(Directory.Exists);
        }

        /// <summary>
        ///A test for ConvertAsNeededAsync
        /// </summary>
        [Fact]
        public async Task ConvertAsNeededAsyncTest()
        {
            AllTestFiles.ToList().ForEach(file => FileManager.AddFile(file.FileName));
            await FileManager.ConvertAsNeededAsync();

            var filesUnconverted = FileManager.AllFiles
                .Except(FileManager.TxtFiles)
                .Except(FileManager.TaggedFiles)
                .Where(file => !FileManager.TxtFiles.Any(tf => tf.NameSansExt == file.NameSansExt));

            Check.That(filesUnconverted).IsEmpty();
        }

        /// <summary>
        ///A test for ConvertAsNeeded
        /// </summary>
        [Fact]
        public void ConvertAsNeededTest()
        {
            AllTestFiles.ToList().ForEach(file => FileManager.AddFile(file.FileName));
            FileManager.ConvertAsNeeded();

            var unconvertedFiles =
                 new IEnumerable<InputFile>[] { FileManager.DocFiles, FileManager.DocXFiles, FileManager.PdfFiles }
                 .Aggregate(Enumerable.Concat)
                 .ExceptBy(FileManager.TxtFiles, file => file.NameSansExt);

            Check.That(unconvertedFiles).IsEmpty();
        }

        /// <summary>
        ///A test for ConvertDocFilesAsync
        /// </summary>
        [Fact]
        public async Task ConvertDocFilesAsyncTest()
        {
            var files = DocFiles;
            await FileManager.ConvertDocToTextAsync(files);

            foreach (var file in files)
            {
                Check.That(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt"))
                     .Satisfies(File.Exists);
            }
        }

        /// <summary>
        ///A test for ConvertDocToTextAsync
        /// </summary>
        [Fact]
        public async Task ConvertDocToTextAsyncTest()
        {
            var files = DocFiles;
            await FileManager.ConvertDocToTextAsync(files);

            foreach (var file in files)
            {
                Check.That(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt"))
                     .Satisfies(File.Exists);
            }
        }

        /// <summary>
        ///A test for ConvertDocToText
        /// </summary>
        [Fact]
        public void ConvertDocToTextTest()
        {
            var files = DocFiles;
            FileManager.ConvertDocToText(files);

            foreach (var file in files)
            {
                Check.That(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt"))
                     .Satisfies(File.Exists);
            }
        }

        /// <summary>
        /// A test for ConvertDocxToTextAsync 
        /// </summary>
        [Fact]
        public async Task ConvertDocxToTextAsyncTest()
        {
            var files = DocXFiles;
            foreach (var path in from file in files select file.FullPath)
            {
                FileManager.AddFile(path);
            }
            Assert.Equal(
                files.Select(file => file.FileName),
                Directory.EnumerateFiles(FileManager.DocxFilesDirectory).Select(file => new DocXFile(file).FileName), StringComparer.OrdinalIgnoreCase
            );

            await FileManager.ConvertDocxToTextAsync(files);

            foreach (var file in files)
            {
                Check.That(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt"))
                     .Satisfies(File.Exists);
            }
        }

        /// <summary>
        ///A test for ConvertDocxToText
        /// </summary>
        [Fact]
        public void ConvertDocxToTextTest()
        {
            var files = DocXFiles;
            FileManager.ConvertDocxToText(files);

            foreach (var file in files)
            {
                Check.That(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt"))
                     .Satisfies(File.Exists);
            }
        }

        /// <summary>
        ///A test for ConvertPdfFilesAsync
        /// </summary>
        [Fact]
        public async Task ConvertPdfToTextAsyncTest()
        {
            var files = PdfFiles;
            await FileManager.ConvertPdfToTextAsync(files);

            foreach (var file in files)
            {
                Check.That(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt"))
                     .Satisfies(File.Exists);
            }
        }

        /// <summary>
        ///A test for ConvertPdfToText
        /// </summary>
        [Fact]
        public void ConvertPdfToTextTest()
        {
            var files = PdfFiles;
            FileManager.ConvertPdfToText(files);

            foreach (var file in files)
            {
                Check.That(Path.Combine(FileManager.TxtFilesDirectory, file.NameSansExt + ".txt"))
                     .Satisfies(File.Exists);
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

        /// <summary>
        ///A test for TagTextFilesAsync
        /// </summary>
        [Fact]
        public async Task TagTextFilesAsyncTest()
        {
            var files = TxtFiles;
            await FileManager.TagTextFilesAsync();

            foreach (var file in files)
            {
                Check.That(Path.Combine(FileManager.TaggedFilesDirectory, file.NameSansExt + ".tagged"))
                     .Satisfies(File.Exists);
            }
        }

        /// <summary>
        ///A test for TagTextFile
        /// </summary>
        [Fact]
        public void TagTextFilesTest()
        {
            var files = TxtFiles;
            FileManager.TagTextFiles(files);

            foreach (var file in files)
            {
                Check.That(Path.Combine(FileManager.TaggedFilesDirectory, file.NameSansExt + ".tagged")).Satisfies(File.Exists);
            }
        }

        private IEnumerable<InputFile> AllTestFiles
        {
            get
            {
                foreach (var file in DocFiles) yield return file;
                foreach (var file in DocXFiles) yield return file;
                foreach (var file in PdfFiles) yield return file;
                foreach (var file in TxtFiles) yield return file;
            }
        }

        private readonly static Func<FileInfo, FileInfo> CopyToRunningTestDirectory = fileInfo => fileInfo.CopyTo($@"{testProjectDirectory}\{fileInfo.Name}", overwrite: true);

        private DocFile[] DocFiles => LoadInputFiles(".doc", path => new DocFile(path));

        private DocXFile[] DocXFiles => LoadInputFiles(".docx", path => new DocXFile(path));

        private PdfFile[] PdfFiles => LoadInputFiles(".pdf", path => new PdfFile(path));

        private TxtFile[] TxtFiles => LoadInputFiles(".txt", path => new TxtFile(path));


        private static TInputFile[] LoadInputFiles<TInputFile>(string extension, Func<string, TInputFile> loadFile) => new DirectoryInfo(MockTestFilesDirectory)
            .EnumerateFiles()
            .Where(file => file.Extension == extension)
            .Select(CopyToRunningTestDirectory)
            .Select(file => file.FullName)
            .Select(loadFile)
            .ToArray();

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
        private const string MockTestFilesDirectory = @"..\..\MockUserFiles";
        private static string testProjectDirectory;

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