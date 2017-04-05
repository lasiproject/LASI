using System.IO;
using NFluent;
using Shared.Test.NFluentExtensions;
using Xunit;
using LASI.Content.Exceptions;
using System.Threading.Tasks;

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
            var target = new PdfFile(TestPdfFilePath);
            var pdfInfo = new FileInfo(TestPdfFilePath);
            Check.That(pdfInfo.FullName).IsEqualTo(target.FullPath);
            Check.That(pdfInfo.Name).IsEqualTo(target.FileName);
            Check.That(pdfInfo.Extension).IsEqualTo(target.Extension);
        }
        [Fact]
        public void PdfFileConstructorGivenTxtFileThrowsFileTypeMismatchOfPdfFile()
        {
            var invalidPath = Directory.GetCurrentDirectory();
            Check.That(invalidPath).DoesNotSatisfy(File.Exists);
            Check.ThatCode(() => new PdfFile(invalidPath))
                 .Throws<FileNotFoundException>();
        }
        [Fact]
        public void PdfFileConstructorTest2()
        {
            var pathToNonPdfFile = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";
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
            var target = new PdfFile(TestPdfFilePath);
            var expected = new PdfToTextConverter(target).ConvertFile().LoadText();
            var actual = target.LoadText();
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [Fact]
        public async Task LoadTextAsyncTest()
        {
            var path = TestPdfFilePath;
            var target = new PdfFile(path);
            var converted = await new PdfToTextConverter(target).ConvertFileAsync();
            var expected = await converted.LoadTextAsync();
            var actual = await target.LoadTextAsync();
            Check.That(expected).IsEqualTo(actual);
        }
    }
}
