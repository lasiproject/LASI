using LASI.Content;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using NFluent;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is A test class for DocToDocXConverterTest and is intended
    ///to contain all DocToDocXConverterTest Unit Tests
    /// </summary>
    public class DocToDocXConverterTest : FileConverterTestBase<DocFile>
    {
        public DocToDocXConverterTest():base("Draft_Environmental_Assessment.doc") { } 

        protected sealed override Func<string, DocFile> SourceFactory => path => new DocFile(path);

        /// <summary>
        ///A test for DocToDocXConverter Constructor
        /// </summary>
        [Fact]
        public void DocToDocXConverterConstructorTest()
        {
            var infile = SourceFile;
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
            var infile = SourceFile;
            var target = new DocToDocXConverter(infile);
            InputFile actual;
            actual = target.ConvertFile();
            Assert.True(File.Exists(actual.FullPath));
        }

        /// <summary>
        ///A test for ConvertFileAsync
        /// </summary>
        [Fact]
        public async Task ConvertFileAsyncCreatesOutputInTheExpectedLocation()
        {
            var infile = SourceFile;
            var target = new DocToDocXConverter(infile);
            InputFile actual;
            actual = await target.ConvertFileAsync();
            Assert.True(File.Exists(actual.FullPath));
        }
    }
}
