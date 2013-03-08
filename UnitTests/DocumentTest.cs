using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for DocumentTest and is intended
    ///to contain all DocumentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DocumentTest
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
        ///A test for Document Constructor
        ///</summary>
        [TestMethod()]
        public void DocumentConstructorTest() {
            IEnumerable<Paragraph> allParagrpahs = new[] { new Paragraph(new[] { new Sentence(new Clause[] { new Clause(new Phrase[] { new NounPhrase(new Word[] { new PersonalPronoun("We") }), new VerbPhrase(new Word[] { new Modal("must"), new Verb("attack", VerbTense.Base) }), new NounPhrase(new Word[] { new Adjective("blue"), new GenericSingularNoun("team") }) }) }) }) };
            Document target = new Document(allParagrpahs);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetActions
        ///</summary>
        [TestMethod()]
        public void GetActionsTest() {
            IEnumerable<Paragraph> allParagrpahs = null; // TODO: Initialize to an appropriate value
            Document target = new Document(allParagrpahs); // TODO: Initialize to an appropriate value
            IEnumerable<ITransitiveAction> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<ITransitiveAction> actual;
            actual = target.GetActions();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetEntities
        ///</summary>
        [TestMethod()]
        public void GetEntitiesTest() {
            IEnumerable<Paragraph> allParagrpahs = null; // TODO: Initialize to an appropriate value
            Document target = new Document(allParagrpahs); // TODO: Initialize to an appropriate value
            IEnumerable<IEntity> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<IEntity> actual;
            actual = target.GetEntities();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PrintByPhraseLinkage
        ///</summary>
        [TestMethod()]
        public void PrintByPhraseLinkageTest() {
            IEnumerable<Paragraph> allParagrpahs = null; // TODO: Initialize to an appropriate value
            Document target = new Document(allParagrpahs); // TODO: Initialize to an appropriate value
            target.PrintByPhraseLinkage();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PrintByWordLinkage
        ///</summary>
        [TestMethod()]
        public void PrintByWordLinkageTest() {
            IEnumerable<Paragraph> allParagrpahs = null; // TODO: Initialize to an appropriate value
            Document target = new Document(allParagrpahs); // TODO: Initialize to an appropriate value
            target.PrintByWordLinkage();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SentenceAt
        ///</summary>
        [TestMethod()]
        public void SentenceAtTest() {
            IEnumerable<Paragraph> allParagrpahs = null; // TODO: Initialize to an appropriate value
            Document target = new Document(allParagrpahs); // TODO: Initialize to an appropriate value
            int loc = 0; // TODO: Initialize to an appropriate value
            Sentence expected = null; // TODO: Initialize to an appropriate value
            Sentence actual;
            actual = target.SentenceAt(loc);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SentenceTextAt
        ///</summary>
        [TestMethod()]
        public void SentenceTextAtTest() {
            IEnumerable<Paragraph> allParagrpahs = null; // TODO: Initialize to an appropriate value
            Document target = new Document(allParagrpahs); // TODO: Initialize to an appropriate value
            int loc = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.SentenceTextAt(loc);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for WordAt
        ///</summary>
        [TestMethod()]
        public void WordAtTest() {
            IEnumerable<Paragraph> allParagrpahs = null; // TODO: Initialize to an appropriate value
            Document target = new Document(allParagrpahs); // TODO: Initialize to an appropriate value
            int loc = 0; // TODO: Initialize to an appropriate value
            Word expected = null; // TODO: Initialize to an appropriate value
            Word actual;
            actual = target.WordAt(loc);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for WordTextAt
        ///</summary>
        [TestMethod()]
        public void WordTextAtTest() {
            IEnumerable<Paragraph> allParagrpahs = null; // TODO: Initialize to an appropriate value
            Document target = new Document(allParagrpahs); // TODO: Initialize to an appropriate value
            int loc = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.WordTextAt(loc);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Paragraphs
        ///</summary>
        [TestMethod()]
        public void ParagraphsTest() {
            IEnumerable<Paragraph> allParagrpahs = null; // TODO: Initialize to an appropriate value
            Document target = new Document(allParagrpahs); // TODO: Initialize to an appropriate value
            IEnumerable<Paragraph> actual;
            actual = target.Paragraphs;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Phrases
        ///</summary>
        [TestMethod()]
        public void PhrasesTest() {
            IEnumerable<Paragraph> allParagrpahs = null; // TODO: Initialize to an appropriate value
            Document target = new Document(allParagrpahs); // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> actual;
            actual = target.Phrases;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Sentences
        ///</summary>
        [TestMethod()]
        public void SentencesTest() {
            IEnumerable<Paragraph> allParagrpahs = null; // TODO: Initialize to an appropriate value
            Document target = new Document(allParagrpahs); // TODO: Initialize to an appropriate value
            IEnumerable<Sentence> actual;
            actual = target.Sentences;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Words
        ///</summary>
        [TestMethod()]
        public void WordsTest() {
            IEnumerable<Paragraph> allParagrpahs = null; // TODO: Initialize to an appropriate value
            Document target = new Document(allParagrpahs); // TODO: Initialize to an appropriate value
            IEnumerable<Word> actual;
            actual = target.Words;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
