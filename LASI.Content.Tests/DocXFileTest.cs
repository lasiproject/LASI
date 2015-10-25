using Shared.Test.Attributes;
using NFluent;
using Xunit;
using System.IO;

namespace LASI.Content.Tests
{
    /// <summary>
    /// This is a test class for DocXFileTest and is intended to contain all DocXFileTest Unit Tests
    /// </summary>
    public class DocXFileTest
    {
        /// <summary>
        /// A test for DocXFile Constructor
        /// </summary>
        [Fact]
        public void DocXFileConstructorTest()
        {
            string path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            DocXFile target = new DocXFile(path);
            Check.That(File.Exists(path)).IsTrue();
            Check.That(Path.GetFullPath(path)).Equals(target.FullPath);
        }

        /// <summary>
        /// A test for DocXFile Constructor
        /// </summary>
        [Fact]
        public void DocXFileConstructorTest1()
        {
            string path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.txt";
            Check.ThatCode(() => new DocXFile(path)).Throws<FileTypeWrapperMismatchException<DocXFile>>();
        }

        /// <summary>
        /// A test for DocXFile Constructor
        /// </summary>
        [Fact]
        public void DocXFileConstructorTest2()
        {
            string invalidPath = Directory.GetCurrentDirectory();//This is should never be valid.
            Check.That(File.Exists(invalidPath)).IsFalse();
            Check.ThatCode(() => new DocXFile(invalidPath)).Throws<FileNotFoundException>();
        }
    }
}