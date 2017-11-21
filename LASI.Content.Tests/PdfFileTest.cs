using System.IO;
using System.Threading.Tasks;
using LASI.Content.Exceptions;
using LASI.Content.FileConveters;
using LASI.Content.FileTypes;
using NFluent;
using Shared.Test.NFluentExtensions;
using Xunit;

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
        [Fact(Skip = "This test is obsolete. It consistently exhibits a race, due to parallel test execution by different PROCESSES never observed in the application proper, which is a single process.")]
        public void LoadTextTest()
        {
            var target = new PdfFile(TestPdfFilePath);
            var expected = new PdfToTextConverter(target).ConvertFile().LoadText();
            var actual = target.LoadText();
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [Fact(Skip = "This test is obsolete. It consistently exhibits a race, due to parallel test execution by different PROCESSES never observed in the application proper, which is a single process.")]
        public async Task LoadTextAsyncTest()
        {
            var path = TestPdfFilePath;
            var target = new PdfFile(path);
            var converted = await new PdfToTextConverter(target).ConvertFileAsync();
            var expected = await converted.LoadTextAsync();
            var actual = await target.LoadTextAsync();
            Check.That(actual).IsEqualTo(expected);
        }
    }
}
