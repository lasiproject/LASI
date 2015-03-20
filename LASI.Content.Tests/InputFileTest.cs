using LASI.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for InputFileTest and is intended
    ///to contain all InputFileTest Unit Tests
    /// </summary>
    [TestClass]
    public class InputFileTest
    {
        const string TEXT_TEST_FILE_PATH = @"..\..\MockUserFiles\Draft_Environmental_Assessment4.txt";
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        internal virtual InputFile CreateInputFile()
        {
            return new TxtFile(TEXT_TEST_FILE_PATH);
        }

        /// <summary>
        ///A test for Equals
        /// </summary>
        [TestMethod]
        public void EqualsTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment2.docx";
            InputFile target = new DocXFile(relativePath);
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
            obj = new DocXFile(relativePath);
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
            TxtFile other = new TxtFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment4.txt");
            expected = false;
            actual = target.Equals(other);
            InputFile other1 = new DocXFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment2.docx");
            expected = true;
            actual = target.Equals(other1);
            DocXFile other2 = new DocXFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment2.docx");
            expected = true;
            actual = target.Equals(other2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetHashCode
        /// </summary>
        [TestMethod]
        public void GetHashCodeTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment2.docx";
            InputFile target = new DocXFile(relativePath);
            int expected = new DocXFile(relativePath).GetHashCode();
            int actual;
            actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetText
        /// </summary>
        [TestMethod]
        public void GetTextTest()
        {
            InputFile target = CreateInputFile();
            string expected = string.Empty;
            using (var reader = new System.IO.StreamReader(target.FullPath))
            {
                expected = reader.ReadToEnd();
            }
            string actual;
            actual = target.GetText();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetTextAsync
        /// </summary>
        [TestMethod]
        public void GetTextAsyncTest()
        {
            InputFile target = CreateInputFile();
            string expected = string.Empty;
            string actual = null;
            Task.WaitAll(Task.Run(
                async () => expected = await new System.IO.StreamReader(target.FullPath).ReadToEndAsync()),
                Task.Run(async () => actual = await target.GetTextAsync())
            );
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            string expected = string.Format("{0}: {1} in: {2}", target.GetType(), target.FileName, target.Directory);
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Equality
        /// </summary>
        [TestMethod]
        public void op_EqualityTest()
        {
            InputFile left = new TxtFile(TEXT_TEST_FILE_PATH);
            InputFile right = null;
            bool expected = false;
            bool actual;
            actual = (left == right);
            Assert.AreEqual(expected, actual);

            right = new DocXFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment2.docx");
            expected = false;
            actual = (left == right);
            Assert.AreEqual(expected, actual);
            right = new TxtFile(TEXT_TEST_FILE_PATH);
            expected = true;
            actual = (left == right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Inequality
        /// </summary>
        [TestMethod]
        public void op_InequalityTest()
        {
            InputFile left = new TxtFile(TEXT_TEST_FILE_PATH);
            InputFile right = null;
            bool expected = true;
            bool actual;
            actual = (left != right);
            Assert.AreEqual(expected, actual);

            right = new DocXFile(@"..\..\MockUserFiles\Draft_Environmental_Assessment2.docx");
            expected = true;
            actual = (left != right);
            Assert.AreEqual(expected, actual);
            right = new TxtFile(TEXT_TEST_FILE_PATH);
            expected = false;
            actual = (left != right);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Directory
        /// </summary>
        [TestMethod]
        public void DirectoryTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment2.docx";
            InputFile target = new DocXFile(relativePath);
            string expected = new System.IO.FileInfo(relativePath).Directory.FullName + "\\";
            string actual;
            actual = target.Directory;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Extension
        /// </summary>
        [TestMethod]
        public void ExtTest()
        {
            var fullPath = @"..\..\MockUserFiles\Draft_Environmental_Assessment2.docx";
            InputFile target = new DocXFile(fullPath);
            string expected = ".docx";
            string actual;
            actual = target.Extension;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FileName
        /// </summary>
        [TestMethod]
        public void FileNameTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment3.pdf";
            InputFile target = new PdfFile(relativePath);
            string expected = "Draft_Environmental_Assessment3.pdf";
            string actual;
            actual = target.FileName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FullPath
        /// </summary>
        [TestMethod]
        public void FullPathTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment2.docx";
            InputFile target = new DocXFile(relativePath);
            string actual;
            actual = target.FullPath;
            Assert.AreEqual(System.IO.Path.GetFullPath(relativePath), actual);
        }        /// <summary>
                 ///A test for FullPath
                 /// </summary>
        [TestMethod]
        public void FullPathTest1()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment2.docx";
            InputFile target = new DocXFile(relativePath);
            string actual;
            actual = target.FullPath;
            Assert.AreEqual(System.IO.Path.GetFullPath(relativePath), actual);
        }

        /// <summary>
        ///A test for Name
        /// </summary>
        [TestMethod]
        public void NameTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment1.doc";
            InputFile target = new DocFile(relativePath);
            string expected = "Draft_Environmental_Assessment1";
            string actual;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NameSansExt
        /// </summary>
        [TestMethod]
        public void NameSansExtTest()
        {
            var relativePath = @"..\..\MockUserFiles\Draft_Environmental_Assessment4.txt";
            InputFile target = new TxtFile(relativePath);
            string expected = "Draft_Environmental_Assessment4";
            string actual;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PathSansExt
        /// </summary>
        [TestMethod]
        public void PathSansExtTest()
        {
            var absolutePath = System.IO.Path.GetFullPath(@"..\..\MockUserFiles\Draft_Environmental_Assessment3.pdf");
            InputFile target = new PdfFile(absolutePath);
            string expected = System.IO.Path.GetDirectoryName(absolutePath) + @"\Draft_Environmental_Assessment3";
            string actual;
            actual = target.PathSansExt;
            Assert.AreEqual(expected, actual);
        }
    }
}
