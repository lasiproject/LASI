using System;
using System.IO;
using System.Threading.Tasks;
using LASI.Content.FileConverters;
using LASI.Content.FileTypes;
using NFluent;
using Xunit;

namespace LASI.Content.Tests
{

    /// <summary>
    ///This is A test class for DocToDocXConverterTest and is intended
    ///to contain all DocToDocXConverterTest Unit Tests
    /// </summary>
    public class DocToDocXConverterTest : FileConverterBaseTest<DocFile>
    {
        public DocToDocXConverterTest() : base("Draft_Environmental_Assessment.doc") { }

        protected sealed override Func<string, DocFile> SourceFactory => path => new DocFile(path);
        [Fact]
        public void SomeTest()
        {

        }
        /// <summary>
        ///A test for DocToDocXConverter Constructor
        /// </summary>
        [Fact]
        public void DocToDocXConverterConstructorTest()
        {
            var infile = Input;
            var target = new DocToDocXConverter(infile);
            Check.That(target.Original).IsEqualTo(infile);
        }

        ///// <summary>
        /////A test for DocToDocXConverter Constructor
        ///// </summary>
        //[Fact]
        //public void DocToDocXConverterConstructorTest1()
        //{
        //    var infile = SourceFile;
        //    string DocxFilesDir = @"..\..\..\NewProject\input\docx";
        //    DocToDocXConverter target = new DocToDocXConverter(infile, DocxFilesDir);
        //    Check.That(target.Original).IsEqualTo(infile);
        //}


        /// <summary>
        ///A test for ConvertFile
        /// </summary>
        [Fact]
        public void ConvertFileTest()
        {
            var infile = Input;
            var target = new DocToDocXConverter(infile);
            InputFile actual;
            actual = target.ConvertFile();
            Check.That(File.Exists(actual.FullPath)).IsTrue();
        }

        /// <summary>
        ///A test for ConvertFileAsync
        /// </summary>
        [Fact]
        public async Task ConvertFileAsyncCreatesOutputInTheExpectedLocation()
        {
            var infile = Input;
            var target = new DocToDocXConverter(infile);
            InputFile actual;
            actual = await target.ConvertFileAsync();
            Check.That(File.Exists(actual.FullPath)).IsTrue();
        }
    }
}
