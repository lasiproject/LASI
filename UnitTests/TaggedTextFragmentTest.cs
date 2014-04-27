using LASI.ContentSystem;
using LASI.ContentSystem.TaggerEncapsulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is a test class for TaggedTextFragmentTest and is intended
    ///to contain all TaggedTextFragmentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TaggedTextFragmentTest
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
        ///A test for TaggedTextFragment Constructor
        ///</summary>
        [TestMethod()]
        public void TaggedTextFragmentConstructorTest() {
            var lines = Tagger.TaggedFromRaw(new[] {
                "This is a test which i will not regret.",
                "While it may yield me, in the context of the system at large, only ",
                "a little confidence, each test makes everything else that much better."
            });
            string name = "Test Fragment";
            TaggedTextFragment target = new TaggedTextFragment(lines, name);
            Assert.AreEqual(target.SourceName, name);
        }

        /// <summary>
        ///A test for GetText
        ///</summary>
        [TestMethod()]
        public void GetTextTest() {
            var lines = Tagger.TaggedFromRaw(new[] {
                "This is a test which i will not regret.",
                "While it may yield me, in the context of the system at large, only ",
                "a little confidence, each test makes everything else that much better."
            });
            string name = "Test Fragment";
            TaggedTextFragment target = new TaggedTextFragment(lines, name);
            string expected = string.Join("\n", lines);
            string actual;
            actual = target.GetText();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetTextAsync
        ///</summary>
        [TestMethod()]
        public void GetTextAsyncTest() {
            var lines = Tagger.TaggedFromRaw(new[] {
                "This is a test which i will not regret.",
                "While it may yield me, in the context of the system at large, only ",
                "a little confidence, each test makes everything else that much better."
            });
            string name = "Test Fragment";
            TaggedTextFragment target = new TaggedTextFragment(lines, name);
            string expected = string.Join("\n", lines);
            string actual = null;
            Task.WaitAll(Task.Run(async () => actual = await target.GetTextAsync()));
            Assert.AreEqual(expected, actual);
        }
    }
}
