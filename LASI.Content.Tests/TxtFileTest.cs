using System.IO;
using System.Threading.Tasks;
using NFluent;
using Shared.Test.NFluentExtensions;
using Fact = Xunit.FactAttribute;
using LASI.Content.Exceptions;

namespace LASI.Content.Tests
{
    /// <summary>
    ///This is a test class for TextFileTest and is intended
    ///to contain all TextFileTest Unit Tests
    /// </summary>
    public class TxtFileTest
    {
        private const string ValidTxtFilePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";

        /// <summary>
        ///A test for TextFile Constructor
        /// </summary>
        [Fact]
        public void TextFileConstructorTest()
        {
            var target = new TxtFile(ValidTxtFilePath);
            var fileInfo = new FileInfo(ValidTxtFilePath);
            Check.That(fileInfo.FullName).IsEqualTo(target.FullPath);
            Check.That(fileInfo.Name).IsEqualTo(target.FileName);
            Check.That(fileInfo.Extension).IsEqualTo(target.Extension);
        }
        [Fact]
        public void TextFileConstructorTest1()
        {
            var invalidPath = Directory.GetCurrentDirectory();//This should never be valid.
            Check.ThatCode(() => new TxtFile(invalidPath)).Throws<FileNotFoundException>();
        }
        [Fact]
        public void TxtFileConstructorTest2()
        {
            var wrongTypePath = new FileInfo(@"..\..\MockUserFiles\Draft_Environmental_Assessment.pdf").FullName;
            Check.That(wrongTypePath).Satisfies(File.Exists);
            Check.ThatCode(() => new TxtFile(wrongTypePath)).Throws<FileTypeWrapperMismatchException<TxtFile>>();
        }

        /// <summary>
        ///A test for LoadText
        /// </summary>
        [Fact]
        public void LoadTextTest()
        {
            var target = new TxtFile(ValidTxtFilePath);
            var expected = new StreamReader(ValidTxtFilePath).ReadToEnd();
            var actual = target.LoadText();
            Check.That(actual).IsEqualTo(expected);
        }


        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [Fact]
        public async Task LoadTextAsyncTest()
        {
            var target = new TxtFile(ValidTxtFilePath);
            var expected = await new StreamReader(target.FullPath).ReadToEndAsync();
            var actual = await target.LoadTextAsync();
            Check.That(actual).IsEqualTo(expected);
        }
    }
}
