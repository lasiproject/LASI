using LASI.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for PdfToTextConverterTest and is intended
    ///to contain all PdfToTextConverterTest Unit Tests
    /// </summary>
    [TestClass]
    public class PdfToTextConverterTest
    {
        private const string TestPdfFilePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment3.pdf";

        /// <summary>
        ///A test for PdfToTextConverter Constructor
        /// </summary>
        [TestMethod]
        public void PdfToTextConverterConstructorTest()
        {
            PdfFile infile = new PdfFile(TestPdfFilePath);
            PdfToTextConverter target = new PdfToTextConverter(infile);
            Assert.AreEqual(infile, target.Original);
        }

        /// <summary>
        ///A test for ConvertFile
        /// </summary>
        [TestMethod]
        public void ConvertFileTest()
        {
            PdfFile infile = new PdfFile(TestPdfFilePath);
            PdfToTextConverter target = new PdfToTextConverter(infile);
            string expected = infile.GetText();
            string actual;
            actual = target.ConvertFile().GetText();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertFileAsync
        /// </summary>
        [TestMethod]
        public void ConvertFileAsyncTest()
        {
            PdfFile infile = new PdfFile(TestPdfFilePath);
            PdfToTextConverter target = new PdfToTextConverter(infile);
            string expected = infile.GetText();
            string actual;
            actual = target.ConvertFileAsync().Result.GetText();
            Assert.AreEqual(expected, actual);
        }
    }
}
