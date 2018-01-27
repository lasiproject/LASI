using System;
using System.IO;
using System.Threading.Tasks;
using LASI.Content.FileConverters;
using LASI.Content.FileTypes;
using LASI.Testing.Assertions;
using NFluent;
using Xunit;

namespace LASI.Content.Tests
{
    public class DocxToTextConverterTest : FileConverterBaseTest<DocXFile>
    {
        public DocxToTextConverterTest() : base("Draft_Environmental_Assessment.docx") { }

        protected sealed override Func<string, DocXFile> SourceFactory => path => new DocXFile(path);

        /// <summary>
        ///A test for ConvertFileAsync
        /// </summary>
        [Fact]
        public async Task ConvertFileAsyncTest()
        {
            var target = new DocxToTextConverter(Input);
            TxtFile actual;
            actual = await target.ConvertFileAsync();
            Check.That(FileInfo(actual.FullPath)).Satisfies(x => x.Exists);
        }
        /// <summary>
        ///A test for ConvertFile
        /// </summary>
        [Fact]
        public void ConvertFileTest()
        {
            var target = new DocxToTextConverter(Input);
            TxtFile actual;
            actual = target.ConvertFile();
            Check.That(actual.FullPath).Satisfies(File.Exists);
        }
        /// <summary>
        ///A test for DocxToTextConverter Constructor
        /// </summary>
        [Fact]
        public void DocxToTextConverterConstructorTest()
        {
            var target = new DocxToTextConverter(Input);
            Check.That(target.Original.FullPath).IsEqualTo(Input.FullPath);
        }
    }
}
