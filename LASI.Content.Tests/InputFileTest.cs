using System.Threading.Tasks;
using LASI.Content.FileTypes;
using NFluent;
using Xunit;

namespace LASI.Content.Tests
{
    /// <summary>
    ///This is a test class for InputFileTest and is intended
    ///to contain all InputFileTest Unit Tests
    /// </summary>
    public class InputFileTest
    {
        const string TestTextFilePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";

        internal virtual InputFile CreateInputFile() => new TxtFile(TestTextFilePath);

        /// <summary>
        ///A test for Equals
        /// </summary>
        [Fact]
        public void EqualsTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.docx";
            InputFile target = new DocXFile(relativePath);
            object obj = null;
            var expected = false;
            bool actual;
            actual = target.Equals(obj);
            Check.That(actual).IsEqualTo(expected);
            obj = new DocXFile(relativePath);
            expected = true;
            actual = target.Equals(obj);
            Check.That(actual).IsEqualTo(expected);
            var other = new TxtFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment.txt");
            expected = false;
            actual = target.Equals(other);
            InputFile other1 = new DocXFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment.docx");
            expected = true;
            actual = target.Equals(other1);
            var other2 = new DocXFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment.docx");
            expected = true;
            actual = target.Equals(other2);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for GetHashCode
        /// </summary>
        [Fact]
        public void GetHashCodeTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.docx";
            InputFile target = new DocXFile(relativePath);
            var expected = new DocXFile(relativePath).GetHashCode();
            int actual;
            actual = target.GetHashCode();
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for LoadText
        /// </summary>
        [Fact]
        public void LoadTextTest()
        {
            var target = CreateInputFile();
            var expected = string.Empty;
            using (var reader = new System.IO.StreamReader(target.FullPath))
            {
                expected = reader.ReadToEnd();
            }
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
            var target = CreateInputFile();
            var expected = string.Empty;
            string actual = null;

            expected = await new System.IO.StreamReader(target.FullPath).ReadToEndAsync();
            actual = await target.LoadTextAsync();
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            var target = CreateInputFile(); // TODO: Initialize to an appropriate value
            var expected = string.Format("{0}: {1} in: {2}", target.GetType(), target.FileName, target.Directory);
            string actual;
            actual = target.ToString();
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for op_Equality
        /// </summary>
        [Fact]
        public void op_EqualityTest()
        {
            InputFile left = new TxtFile(TestTextFilePath);
            InputFile right = null;
            var expected = false;
            bool actual;
            actual = (left == right);
            Check.That(actual).IsEqualTo(expected);

            right = new DocXFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment.docx");
            expected = false;
            actual = (left == right);
            Check.That(actual).IsEqualTo(expected);
            right = new TxtFile(TestTextFilePath);
            expected = true;
            actual = (left == right);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for op_Inequality
        /// </summary>
        [Fact]
        public void op_InequalityTest()
        {
            InputFile left = new TxtFile(TestTextFilePath);
            InputFile right = null;
            var expected = true;
            bool actual;
            actual = (left != right);
            Check.That(actual).IsEqualTo(expected);

            right = new DocXFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment.docx");
            expected = true;
            actual = (left != right);
            Check.That(actual).IsEqualTo(expected);
            right = new TxtFile(TestTextFilePath);
            expected = false;
            actual = (left != right);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Directory
        /// </summary>
        [Fact]
        public void DirectoryTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.docx";
            InputFile target = new DocXFile(relativePath);
            var expected = new System.IO.FileInfo(relativePath).Directory.FullName + "\\";
            string actual;
            actual = target.Directory;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Extension
        /// </summary>
        [Fact]
        public void ExtTest()
        {
            var fullPath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.docx";
            InputFile target = new DocXFile(fullPath);
            var expected = ".docx";
            string actual;
            actual = target.Extension;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for FileName
        /// </summary>
        [Fact]
        public void FileNameTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.pdf";
            InputFile target = new PdfFile(relativePath);
            var expected = "Draft_Environmental_Assessment.pdf";
            string actual;
            actual = target.FileName;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for FullPath
        /// </summary>
        [Fact]
        public void FullPathTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.docx";
            InputFile target = new DocXFile(relativePath);
            string actual;
            var expected = System.IO.Path.GetFullPath(relativePath);
            actual = target.FullPath;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for FullPath
        /// </summary>
        [Fact]
        public void FullPathTest1()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.docx";
            InputFile target = new DocXFile(relativePath);
            var expected = System.IO.Path.GetFullPath(relativePath);
            var actual = target.FullPath;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Name
        /// </summary>
        [Fact]
        public void NameTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.doc";
            InputFile target = new DocFile(relativePath);
            var expected = "Draft_Environmental_Assessment";
            string actual;
            actual = target.Name;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for NameSansExt
        /// </summary>
        [Fact]
        public void NameSansExtTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";
            InputFile target = new TxtFile(relativePath);
            var expected = "Draft_Environmental_Assessment";
            string actual;
            actual = target.Name;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for PathSansExt
        /// </summary>
        [Fact]
        public void PathSansExtTest()
        {
            var absolutePath = System.IO.Path.GetFullPath(@"..\..\MockUserFiles\Draft_Environmental_Assessment.pdf");
            InputFile target = new PdfFile(absolutePath);
            var expected = System.IO.Path.GetDirectoryName(absolutePath) + @"\Draft_Environmental_Assessment";
            string actual;
            actual = target.PathSansExt;
            Check.That(actual).IsEqualTo(expected);
        }
    }
}
