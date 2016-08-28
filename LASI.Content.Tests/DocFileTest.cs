using System.IO;
using Xunit;
using NFluent;
using Shared.Test;
using Shared.Test.NFluentExtensions;
using LASI.Content.Exceptions;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for DocFileTest and is intended
    ///to contain all DocFileTest Unit Tests
    /// </summary>
    public class DocFileTest
    {
        const string DOC_TEST_FILE_PATH = @"..\..\MockUserFiles\Draft_Environmental_Assessment.doc";

        /// <summary>
        ///A test for DocFile Constructor
        /// </summary>
        [Fact]
        public void DocFileConstructorTest()
        {
            var target = new DocFile(DOC_TEST_FILE_PATH);
            Check.That(DOC_TEST_FILE_PATH).Satisfies(File.Exists);
            Check.That(target.Extension).IsEqualTo(".doc");
            Check.That(target.FullPath).IsEqualTo(Path.GetFullPath(DOC_TEST_FILE_PATH));
        }
        /// <summary>
        ///A test for DocFile Constructor
        /// </summary>
        [Fact]
        public void DocFileConstructorTest1()
        {
            var wrongFileTypePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment.txt";
            Check.That(wrongFileTypePath).Satisfies(File.Exists);
            Check.ThatCode(() => new DocFile(wrongFileTypePath))
                 .Throws<FileTypeWrapperMismatchException<DocFile>>();
        }
        /// <summary>
        ///A test for DocFile Constructor
        /// </summary>
        [Fact]
        public void DocFileConstructorTest2()
        {
            var invalidPath = Directory.GetCurrentDirectory();//This should never be valid.
            Check.That(invalidPath).DoesNotSatisfy(File.Exists);
            Check.ThatCode(() => new DocFile(invalidPath))
                 .Throws<FileNotFoundException>();
        }
    }
}
