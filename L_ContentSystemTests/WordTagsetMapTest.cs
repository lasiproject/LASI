using LASI.ContentSystem.TaggerEncapsulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;

namespace L_ContentSystemTests
{
    
    
    /// <summary>
    ///This is a test class for WordTagsetMapTest and is intended
    ///to contain all WordTagsetMapTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WordTagsetMapTest
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


        internal virtual WordTagsetMap CreateWordTagsetMap() {
            // TODO: Instantiate an appropriate concrete class.
            WordTagsetMap target = null;
            return target;
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest() {
            WordTagsetMap target = CreateWordTagsetMap(); // TODO: Initialize to an appropriate value
            string tag = string.Empty; // TODO: Initialize to an appropriate value
            Func<string, Word> actual;
            actual = target[tag];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest1() {
            WordTagsetMap target = CreateWordTagsetMap(); // TODO: Initialize to an appropriate value
            Func<string, Word> mappedConstructor = null; // TODO: Initialize to an appropriate value
            string actual;
            actual = target[mappedConstructor];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest2() {
            WordTagsetMap target = CreateWordTagsetMap(); // TODO: Initialize to an appropriate value
            Word word = null; // TODO: Initialize to an appropriate value
            string actual;
            actual = target[word];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
