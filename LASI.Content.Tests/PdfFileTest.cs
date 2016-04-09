using System.IO;
using NFluent;
using Shared.Test.NFluentExtensions;
using Xunit;
using LASI.Content.Exceptions;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for PdfFileTest and is intended
    ///to contain all PdfFileTest Unit Tests
    /// </summary>
    public class PdfFileTest
    {
        private const string TestPdfFilePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.pdf";

        /// <summary>
        ///A test for PdfFile Constructor
        /// </summary>
        [Fact]
        public void PdfFileConstructorTest()
        {
            PdfFile target = new PdfFile(TestPdfFilePath);
            FileInfo pdfInfo = new FileInfo(TestPdfFilePath);
            Assert.Equal(pdfInfo.FullName, target.FullPath);
            Assert.Equal(pdfInfo.Name, target.FileName);
            Assert.Equal(pdfInfo.Extension, target.Extension);
        }
        [Fact]
        public void PdfFileConstructorGivenTxtFileThrowsFileTypeMismatchOfPdfFile()
        {
            string invalidPath = Directory.GetCurrentDirectory();
            Check.That(invalidPath).DoesNotSatisfy(File.Exists);
            Check.ThatCode(() => new PdfFile(invalidPath))
                 .Throws<FileNotFoundException>();
        }
        [Fact]
        public void PdfFileConstructorTest2()
        {
            string pathToNonPdfFile = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";
            Check.That(pathToNonPdfFile).Satisfies(File.Exists);
            Check.ThatCode(() => new PdfFile(pathToNonPdfFile))
                 .Throws<FileTypeWrapperMismatchException<PdfFile>>();
        }
        /// <summary>
        ///A test for LoadText
        /// </summary>
        [Fact]
        public void LoadTextTest()
        {
            PdfFile target = new PdfFile(TestPdfFilePath);
            string expected = new PdfToTextConverter(target).ConvertFile().LoadText();
            string actual;
            actual = target.LoadText();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [Fact]
        public void LoadTextAsyncTest()
        {
            string path = TestPdfFilePath;
            PdfFile target = new PdfFile(path);
            string expected = new PdfToTextConverter(target).ConvertFile().LoadText();
            string actual;
            actual = target.LoadTextAsync().Result;
            Assert.Equal(expected, actual);
        }
    }
}
