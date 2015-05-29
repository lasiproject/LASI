﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using LASI.Content.Tests.Helpers;
using Shared.Test.Attributes;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for PdfFileTest and is intended
    ///to contain all PdfFileTest Unit Tests
    /// </summary>
    [TestClass]
    public class PdfFileTest
    {
        private const string TestPdfFilePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment3.pdf";

        /// <summary>
        ///A test for PdfFile Constructor
        /// </summary>
        [TestMethod]
        public void PdfFileConstructorTest()
        {
            PdfFile target = new PdfFile(TestPdfFilePath);
            FileInfo pdfInfo = new FileInfo(TestPdfFilePath);
            Assert.AreEqual(pdfInfo.FullName, target.FullPath);
            Assert.AreEqual(pdfInfo.Name, target.FileName);
            Assert.AreEqual(pdfInfo.Extension, target.Extension);
        }
        [TestMethod]
        [ExpectedFileNotFoundException]
        public void PdfFileConstructorTest1()
        {
            string invalidPath = Directory.GetCurrentDirectory();
            PdfFile target = new PdfFile(invalidPath);
        }
        [TestMethod]
        [ExpectedFileTypeWrapperMismatchException]
        public void PdfFileConstructorTest2()
        {
            string pathToNonPdfFile = @"..\..\MockUserFiles\Draft_Environmental_Assessment3.txt";
            PdfFile target = new PdfFile(pathToNonPdfFile);
        }
        /// <summary>
        ///A test for LoadText
        /// </summary>
        [TestMethod]
        public void LoadTextTest()
        {
            PdfFile target = new PdfFile(TestPdfFilePath);
            string expected = new PdfToTextConverter(target).ConvertFile().LoadText();
            string actual;
            actual = target.LoadText();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [TestMethod]
        public void LoadTextAsyncTest()
        {
            string path = TestPdfFilePath;
            PdfFile target = new PdfFile(path);
            string expected = new PdfToTextConverter(target).ConvertFile().LoadText();
            string actual;
            actual = target.LoadTextAsync().Result;
            Assert.AreEqual(expected, actual);
        }
    }
}
