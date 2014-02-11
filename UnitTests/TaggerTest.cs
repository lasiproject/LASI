using LASI.ContentSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core.DocumentStructures;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaggerInterop;

namespace LASI.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for TaggerTest and is intended
    ///to contain all TaggerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TaggerTest
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
        ///A test for DocumentFromDoc
        ///</summary>
        [TestMethod()]
        public void DocumentFromDocTest() {
            DocFile doc = null; // TODO: Initialize to an appropriate value
            Document expected = null; // TODO: Initialize to an appropriate value
            Document actual;
            actual = Tagger.DocumentFromDoc(doc);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocumentFromDocX
        ///</summary>
        [TestMethod()]
        public void DocumentFromDocXTest() {
            DocXFile docx = null; // TODO: Initialize to an appropriate value
            Document expected = null; // TODO: Initialize to an appropriate value
            Document actual;
            actual = Tagger.DocumentFromDocX(docx);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocumentFromPDF
        ///</summary>
        [TestMethod()]
        public void DocumentFromPDFTest() {
            PdfFile pdf = null; // TODO: Initialize to an appropriate value
            Document expected = null; // TODO: Initialize to an appropriate value
            Document actual;
            actual = Tagger.DocumentFromPDF(pdf);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocumentFromRaw
        ///</summary>
        [TestMethod()]
        public void DocumentFromRawTest() {
            IEnumerable<string> strs = null; // TODO: Initialize to an appropriate value
            Document expected = null; // TODO: Initialize to an appropriate value
            Document actual;
            actual = Tagger.DocumentFromRaw(strs);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocumentFromRaw
        ///</summary>
        [TestMethod()]
        public void DocumentFromRawTest1() {
            TxtFile txt = null; // TODO: Initialize to an appropriate value
            Document expected = null; // TODO: Initialize to an appropriate value
            Document actual;
            actual = Tagger.DocumentFromRaw(txt);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocumentFromRaw
        ///</summary>
        [TestMethod()]
        public void DocumentFromRawTest2() {
            IUntaggedTextSource textSource = null; // TODO: Initialize to an appropriate value
            Document expected = null; // TODO: Initialize to an appropriate value
            Document actual;
            actual = Tagger.DocumentFromRaw(textSource);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocumentFromRawAsync
        ///</summary>
        [TestMethod()]
        public void DocumentFromRawAsyncTest() {
            TxtFile txt = null; // TODO: Initialize to an appropriate value
            Task<Document> expected = null; // TODO: Initialize to an appropriate value
            Task<Document> actual;
            actual = Tagger.DocumentFromRawAsync(txt);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocumentFromRawAsync
        ///</summary>
        [TestMethod()]
        public void DocumentFromRawAsyncTest1() {
            IUntaggedTextSource textSource = null; // TODO: Initialize to an appropriate value
            Task<Document> expected = null; // TODO: Initialize to an appropriate value
            Task<Document> actual;
            actual = Tagger.DocumentFromRawAsync(textSource);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocumentFromTagged
        ///</summary>
        [TestMethod()]
        public void DocumentFromTaggedTest() {
            ITaggedTextSource tagged = null; // TODO: Initialize to an appropriate value
            Document expected = null; // TODO: Initialize to an appropriate value
            Document actual;
            actual = Tagger.DocumentFromTagged(tagged);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocumentFromTagged
        ///</summary>
        [TestMethod()]
        public void DocumentFromTaggedTest1() {
            IEnumerable<string> tagged = null; // TODO: Initialize to an appropriate value
            Document expected = null; // TODO: Initialize to an appropriate value
            Document actual;
            actual = Tagger.DocumentFromTagged(tagged);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocumentFromTaggedAsync
        ///</summary>
        [TestMethod()]
        public void DocumentFromTaggedAsyncTest() {
            ITaggedTextSource tagged = null; // TODO: Initialize to an appropriate value
            Task<Document> expected = null; // TODO: Initialize to an appropriate value
            Task<Document> actual;
            actual = Tagger.DocumentFromTaggedAsync(tagged);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DocumentFromTaggedAsync
        ///</summary>
        [TestMethod()]
        public void DocumentFromTaggedAsyncTest1() {
            TaggedFile taggedFile = null; // TODO: Initialize to an appropriate value
            Task<Document> expected = null; // TODO: Initialize to an appropriate value
            Task<Document> actual;
            actual = Tagger.DocumentFromTaggedAsync(taggedFile);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TaggedFromRaw
        ///</summary>
        [TestMethod()]
        public void TaggedFromRawTest() {
            IEnumerable<string> strs = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = Tagger.TaggedFromRaw(strs);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TaggedFromRaw
        ///</summary>
        [TestMethod()]
        public void TaggedFromRawTest1() {
            IUntaggedTextSource textSource = null; // TODO: Initialize to an appropriate value
            ITaggedTextSource expected = null; // TODO: Initialize to an appropriate value
            ITaggedTextSource actual;
            actual = Tagger.TaggedFromRaw(textSource);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TaggedFromRawAsync
        ///</summary>
        [TestMethod()]
        public void TaggedFromRawAsyncTest() {
            IUntaggedTextSource textSource = null; // TODO: Initialize to an appropriate value
            Task<ITaggedTextSource> expected = null; // TODO: Initialize to an appropriate value
            Task<ITaggedTextSource> actual;
            actual = Tagger.TaggedFromRawAsync(textSource);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TaggerMode
        ///</summary>
        [TestMethod()]
        public void TaggerModeTest() {
            TaggerMode expected = new TaggerMode(); // TODO: Initialize to an appropriate value
            TaggerMode actual;
            Tagger.TaggerMode = expected;
            actual = Tagger.TaggerMode;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
