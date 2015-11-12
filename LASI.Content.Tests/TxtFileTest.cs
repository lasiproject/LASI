using System.IO;
using System.Threading.Tasks;
using NFluent;
using Shared.Test.NFluentExtensions;
using Fact = Xunit.FactAttribute;

namespace LASI.Content.Tests
{
    /// <summary>
    ///This is a test class for TextFileTest and is intended
    ///to contain all TextFileTest Unit Tests
    /// </summary>
    public class TxtFileTest
    {
        private const string VALID_TXT_FILE_PATH = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";

        /// <summary>
        ///A test for TextFile Constructor
        /// </summary>
        [Fact]
        public void TextFileConstructorTest()
        {
            string path = VALID_TXT_FILE_PATH;
            TxtFile target = new TxtFile(path);
            var sfi = new System.IO.FileInfo(path);
            Check.That(sfi.FullName).IsEqualTo(target.FullPath);
            Check.That(sfi.Name).IsEqualTo(target.FileName);
            Check.That(sfi.Extension).IsEqualTo(target.Extension);
        }
        [Fact]
        public void TextFileConstructorTest1()
        {
            string invalidPath = Directory.GetCurrentDirectory();//This should never be valid.
            Check.ThatCode(() => new TxtFile(invalidPath)).Throws<FileNotFoundException>();
        }
        [Fact]
        public void TxtFileConstructorTest2()
        {
            string wrongTypePath = new FileInfo(@"..\..\MockUserFiles\Draft_Environmental_Assessment.pdf").FullName;
            Check.That(wrongTypePath).Satisfies(File.Exists);
            Check.ThatCode(() => new TxtFile(wrongTypePath)).Throws<FileTypeWrapperMismatchException<TxtFile>>();
        }

        /// <summary>
        ///A test for LoadText
        /// </summary>
        [Fact]
        public void LoadTextTest()
        {
            string path = VALID_TXT_FILE_PATH;
            TxtFile target = new TxtFile(path);
            string expected = new StreamReader(path).ReadToEnd();
            string actual;
            actual = target.LoadText();
            Check.That(actual).IsEqualTo(expected);
        }


        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [Fact]
        public async Task LoadTextAsyncTest()
        {
            string path = VALID_TXT_FILE_PATH;
            TxtFile target = new TxtFile(path);
            string expected = new StreamReader(target.FullPath).ReadToEndAsync().Result;
            string actual = null;
            actual = await target.LoadTextAsync();
            Check.That(actual).IsEqualTo(expected);
        }
    }
}
