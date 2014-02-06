using LASI.ContentSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LASI.Core.Tests
{
    
    
    /// <summary>
    ///This is a test class for InputFileTest and is intended
    ///to contain all InputFileTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InputFileTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
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


        internal virtual InputFile CreateInputFile() {
            // TODO: Instantiate an appropriate concrete class.
            InputFile target = null;
            return target;
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest() {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest() {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetText
        ///</summary>
        [TestMethod()]
        public void GetTextTest() {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetText();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetTextAsync
        ///</summary>
        [TestMethod()]
        public void GetTextAsyncTest() {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            Task<string> expected = null; // TODO: Initialize to an appropriate value
            Task<string> actual;
            actual = target.GetTextAsync();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod()]
        public void op_EqualityTest() {
            InputFile left = null; // TODO: Initialize to an appropriate value
            InputFile right = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (left == right);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod()]
        public void op_InequalityTest() {
            InputFile left = null; // TODO: Initialize to an appropriate value
            InputFile right = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (left != right);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Directory
        ///</summary>
        [TestMethod()]
        public void DirectoryTest() {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Directory;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Ext
        ///</summary>
        [TestMethod()]
        public void ExtTest() {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Ext;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FileName
        ///</summary>
        [TestMethod()]
        public void FileNameTest() {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.FileName;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FullPath
        ///</summary>
        [TestMethod()]
        public void FullPathTest() {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.FullPath;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void NameTest() {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Name;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for NameSansExt
        ///</summary>
        [TestMethod()]
        public void NameSansExtTest() {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.NameSansExt;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PathSansExt
        ///</summary>
        [TestMethod()]
        public void PathSansExtTest() {
            InputFile target = CreateInputFile(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.PathSansExt;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
