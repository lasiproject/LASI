using LASI.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is a test class for IUntaggedTextSourceTest and is intended
    ///to contain all IUntaggedTextSourceTest Unit Tests
    ///</summary>
    [TestClass]
    public class IRawTextSourceTest
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

        internal virtual IRawTextSource CreateIRawTextSource() {
            IRawTextSource target = new RawTextFragment(lines, "test fragment");
            return target;
        }



        /// <summary>
        ///A test for GetText
        ///</summary>
        [TestMethod]
        public void GetTextTest() {
            string text = string.Join("\n", lines);
            IRawTextSource target = new RawTextFragment(text, "test fragment");
            string expected = text;
            string actual;
            actual = target.GetText();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetTextAsync
        ///</summary>
        [TestMethod]
        public void GetTextAsyncTest() {
            string text = string.Join("\n", lines);
            IRawTextSource target = new RawTextFragment(text, "test fragment");
            string expected = text;
            string actual;
            actual = target.GetTextAsync().Result;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod]
        public void NameTest() {
            string text = string.Join("\n", lines);
            IRawTextSource target = new RawTextFragment(text, "test fragment");

            string actual;
            string expected = "test fragment";
            actual = target.SourceName;
            Assert.AreEqual(expected, actual);

        }
        private static readonly string[] lines = { "John enjoyed, with his usual lack of humility, consuming the object in question.", "Some may call him a heathen, but they are mistaken.", "Heathens are far less dangerous than he." };

    }
}
