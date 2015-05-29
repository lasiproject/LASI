using LASI.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is A test class for DocxToTextConverterTest and is intended
    ///to contain all DocxToTextConverterTest Unit Tests
    /// </summary>
    [TestClass]
    public class DocxToTextConverterTest
    {

        private static DocXFile CreateDocXFile()
        {
            string path = @"..\..\..\TestDocs\Draft_Environmental_Assessment.docx";
            DocXFile infile = new DocXFile(path);
            return infile;
        }

        /// <summary>
        ///A test for ConvertFileAsync
        /// </summary>
        [TestMethod]
        public async Task ConvertFileAsyncTest()
        {
            DocXFile infile = CreateDocXFile();
            DocxToTextConverter target = new DocxToTextConverter(infile);
            TxtFile actual;
            actual = await target.ConvertFileAsync();
            Assert.IsTrue(File.Exists(actual.FullPath));
        }
        /// <summary>
        ///A test for ConvertFile
        /// </summary>
        [TestMethod]
        public void ConvertFileTest()
        {
            DocXFile infile = CreateDocXFile();
            DocxToTextConverter target = new DocxToTextConverter(infile);
            TxtFile actual;
            actual = target.ConvertFile();
            Assert.IsTrue(File.Exists(actual.FullPath));
        }
        /// <summary>
        ///A test for DocxToTextConverter Constructor
        /// </summary>
        [TestMethod]
        public void DocxToTextConverterConstructorTest()
        {
            DocXFile infile = CreateDocXFile();
            DocxToTextConverter target = new DocxToTextConverter(infile);
            Assert.AreEqual(target.Original.FullPath, infile.FullPath);
        }
    }
}
