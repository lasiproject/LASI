﻿using LASI.Content;
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
            string sourcePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.doc";
            var result = FileManager.AddFile(sourcePath);

            Check.That(FileManager.DocFilesDirectory + @"\Draft_Environmental_Assessment.doc").Satisfies(File.Exists);
            Check.That(result).IsInstanceOf<DocFile>();
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
        public void AddDocFilePathReturnsDocFile()
        {
            string path = string.Empty;
            InputFile actual;
            path = @"..\..\MockUserFiles\Draft_Environmental_Assessment.doc";
            actual = FileManager.AddFile(path);

            Check.That(actual.FileName).IsEqualTo("Draft_Environmental_Assessment.doc");
            Check.That(actual).IsInstanceOf<DocFile>();
        }

        [Fact]
        public void AddDocXFilePathReturnsDocXFile()
        {
            var path = @"..\..\MockUserFiles\Draft_Environmental_Assessment.docx";
            var actual = FileManager.AddFile(path);

            Check.That(actual.FileName).IsEqualTo("Draft_Environmental_Assessment.docx");
            Check.That(actual).IsInstanceOf<DocXFile>();
        }

        [Fact]
        public void AddTxtFilePathReturnsTxtFile()
        {
            var path = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";
            var actual = FileManager.AddFile(path);

            Check.That(actual.FileName).IsEqualTo("Draft_Environmental_Assessment.txt");
            Check.That(actual).IsInstanceOf<TxtFile>();
        }

        [Fact]
        public void AddPdfFilePathReturnsPdfFile()
        {
            var path = @"..\..\MockUserFiles\Draft_Environmental_Assessment.pdf";
            var actual = FileManager.AddFile(path);

            Check.That(actual.FileName).IsEqualTo("Draft_Environmental_Assessment.pdf");
            Check.That(actual).IsInstanceOf<PdfFile>();
        }

        /// <summary>
        ///A test for AddPdfFile
        /// </summary>
        [Fact]
        public void AddPdfFileTest()
        {
            string sourcePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.pdf";
            var result = FileManager.AddFile(sourcePath);

            Check.That(FileManager.PdfFilesDirectory + @"\Draft_Environmental_Assessment.pdf")
                 .Satisfies(File.Exists);
            Check.That(result).IsInstanceOf<PdfFile>();
        }

        /// <summary>
        ///A test for AddTextFile
        /// </summary>
        [Fact]
        public void AddTxtFileTest()
        {
            string sourcePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";
            var result = FileManager.AddFile(sourcePath);

            Check.That(FileManager.TxtFilesDirectory + @"\Draft_Environmental_Assessment.txt")
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
            this.GetAllTestFiles().ToList().ForEach(file => FileManager.AddFile(file.FileName));
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
            GetAllTestFiles().ToList().ForEach(file => FileManager.AddFile(file.FileName));
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
            DocFile[] files = GetTestDocFiles();
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
            DocFile[] files = GetTestDocFiles();
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
            DocFile[] files = GetTestDocFiles();
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
            DocXFile[] files = GetTestDocXFiles();
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
            DocXFile[] files = GetTestDocXFiles();
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
            PdfFile[] files = GetTestPdfFiles();
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
            PdfFile[] files = GetTestPdfFiles();
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
            foreach (var file in GetAllTestFiles())
            {
                FileManager.AddFile(file.FullPath);
            }
            foreach (var file in GetAllTestFiles())
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
            string expected = testProjectDirectory.Split(Path.DirectorySeparatorChar).Last();
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
            var files = GetTestTxtFiles();
            await FileManager.TagTextFilesAsync(files);

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
            TxtFile[] files = GetTestTxtFiles();
            FileManager.TagTextFiles(files);

            foreach (var file in files)
            {
                Check.That(Path.Combine(FileManager.TaggedFilesDirectory, file.NameSansExt + ".tagged"))
                     .Satisfies(File.Exists);
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