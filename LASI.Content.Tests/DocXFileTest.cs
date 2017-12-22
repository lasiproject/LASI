using System.IO;
using LASI.Content.Exceptions;
using LASI.Content.FileTypes;
using NFluent;
using Shared.Test.NFluentExtensions;
using Xunit;

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
            var path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            var target = new DocXFile(path);
            Check.That(path).Satisfies(File.Exists);
            Check.That(Path.GetFullPath(path)).IsEqualTo(target.FullPath);
        }

        /// <summary>
        /// A test for DocXFile Constructor
        /// </summary>
        [Fact]
        public void DocXFileConstructorTest1()
        {
            var path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.txt";
            Check.ThatCode(() => new DocXFile(path)).Throws<FileTypeWrapperMismatchException<DocXFile>>();
        }

        /// <summary>
        /// A test for DocXFile Constructor
        /// </summary>
        [Fact]
        public void DocXFileConstructorTest2()
        {
            var invalidPath = Directory.GetCurrentDirectory();//This is should never be valid.
            Check.That(invalidPath).DoesNotSatisfy(File.Exists);
            Check.ThatCode(() => new DocXFile(invalidPath)).Throws<FileNotFoundException>();
        }
    }
}
