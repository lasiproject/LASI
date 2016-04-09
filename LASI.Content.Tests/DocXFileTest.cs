using NFluent;
using Xunit;
using System.IO;
using Shared.Test.NFluentExtensions;
using LASI.Content.Exceptions;

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
            Check.That(path).Satisfies(File.Exists);
            Check.That(Path.GetFullPath(path)).IsEqualTo(target.FullPath);
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
            Check.That(invalidPath).DoesNotSatisfy(File.Exists);
            Check.ThatCode(() => new DocXFile(invalidPath)).Throws<FileNotFoundException>();
        }
    }
}