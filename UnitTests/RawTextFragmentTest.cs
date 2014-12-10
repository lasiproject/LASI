using LASI.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is a test class for RawTextFragmentTest and is intended
    ///to contain all RawTextFragmentTest Unit Tests
    ///</summary>
    [TestClass]
    public class RawTextFragmentTest
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



        /// <summary>
        ///A test for RawTextFragment Constructor
        ///</summary>
        [TestMethod]
        public void RawTextFragmentConstructorTest() {
            IEnumerable<string> text = new[] { "John enjoyed, with his usual lack of humility, consuming the object in question.", "Some may call him a heathen, but they are mistaken.", "Heathens are far less dangerous than he." };
            string name = "test fragment";
            RawTextFragment target = new RawTextFragment(text, name);
            Assert.AreEqual(target.SourceName, name);
            Assert.AreEqual(target.GetText(), string.Join("\n", text));
        }

        /// <summary>
        ///A test for GetText
        ///</summary>
        [TestMethod]
        public void GetTextTest() {
            IEnumerable<string> text = new[] { "John enjoyed, with his usual lack of humility, consuming the object in question.", "Some may call him a heathen, but they are mistaken.", "Heathens are far less dangerous than he." };
            string name = "test fragment";
            RawTextFragment target = new RawTextFragment(text, name);
            string expected = string.Join("\n", text);
            string actual = target.GetText();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetTextAsync
        ///</summary>
        [TestMethod]
        public void GetTextAsyncTest() {
            IEnumerable<string> text = new[] { "John enjoyed, with his usual lack of humility, consuming the object in question.", "Some may call him a heathen, but they are mistaken.", "Heathens are far less dangerous than he." };
            string name = "test fragment";
            RawTextFragment target = new RawTextFragment(text, name);
            string expected = string.Join("\n", text);
            string actual = target.GetTextAsync().Result;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Implicit
        ///</summary>
        [TestMethod]
        public void op_ImplicitTest() {
            IEnumerable<string> text = new[] { "John enjoyed, with his usual lack of humility, consuming the object in question.", "Some may call him a heathen, but they are mistaken.", "Heathens are far less dangerous than he." };
            string name = "test fragment";
            RawTextFragment fragment = new RawTextFragment(text, name);
            string expected = string.Join("\n", text);
            string actual;
            actual = fragment;
            Assert.AreEqual(expected, actual);
        }
    }
}
