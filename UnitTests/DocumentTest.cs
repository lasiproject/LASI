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

        #region Testing Helpers

        private static Document BuildDocumentManually() {
            IEnumerable<Paragraph> allParagrpahs = new Paragraph[] { 
                new Paragraph(new Sentence[] { 
                    new Sentence(new Clause[] {
                        new Clause(new Phrase[] { 
                            new NounPhrase(new Word[] {    
                                new PersonalPronoun("We") 
                            }),
                            new VerbPhrase(new Word[] { 
                                new ModalAuxilary("must"),
                                new Verb("attack", VerbTense.Base) 
                            }),
                            new NounPhrase(new Word[] { 
                                new Adjective("blue"), 
                                new GenericSingularNoun("team") }
                                )}
                            )}, new SentencePunctuation('!')),
                        new Sentence(new Clause[]{new Clause( new Phrase[]{
                            new NounPhrase(new Word[]{
                                new PersonalPronoun("We")}),
                            new VerbPhrase(new Word[] { 
                                new ModalAuxilary("must"),
                                new Verb("do", VerbTense.Base)
                            }),
                        new NounPhrase(new Word[]{  
                            new PersonalPronoun("this")
                        }),
                        new AdverbPhrase(new Word [] {
                            new Adverb("quickly")
                        })
                    })}, new SentencePunctuation('!'))
                })
            };
            return new Document(allParagrpahs);
        }


        #endregion

        /// <summary>
        ///a test for Document Constructor
        ///</summary>
        //[TestMethod()]
        public void DocumentConstructorTest() {
            Document doc = BuildDocumentManually();
            Assert.IsTrue(doc != null);
        }

        /// <summary>
        ///a test for GetActions
        ///</summary>
        [TestMethod()]
        public void GetActionsTest() {

            Document target = BuildDocumentManually();
            IEnumerable<ITransitiveVerbial> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<ITransitiveVerbial> actual;
            actual = target.GetActions();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///a test for GetEntities
        ///</summary>
        [TestMethod()]
        public void GetEntitiesTest() {

            Document target = BuildDocumentManually();
            IEnumerable<IEntity> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<IEntity> actual;
            actual = target.GetEntities();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///a test for PrintByPhraseLinkage
        ///</summary>
        [TestMethod()]
        public void PrintByPhraseLinkageTest() {

            Document target = BuildDocumentManually();
            target.PrintByPhraseLinkage();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///a test for PrintByWordLinkage
        ///</summary>
        [TestMethod()]
        public void PrintByWordLinkageTest() {

            Document target = BuildDocumentManually();
            target.PrintByWordLinkage();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///a test for SentenceAt
        ///</summary>
        [TestMethod()]
        public void SentenceAtTest() {

            Document target = BuildDocumentManually();
            int loc = 0; // TODO: Initialize to an appropriate value
            Sentence expected = null; // TODO: Initialize to an appropriate value
            Sentence actual;
            actual = target.SentenceAt(loc);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///a test for SentenceTextAt
        ///</summary>
        [TestMethod()]
        public void SentenceTextAtTest() {

            Document target = BuildDocumentManually();
            int loc = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.SentenceTextAt(loc);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///a test for WordAt
        ///</summary>
        [TestMethod()]
        public void WordAtTest() {

            Document target = BuildDocumentManually();
            int loc = 0; // TODO: Initialize to an appropriate value
            Word expected = null; // TODO: Initialize to an appropriate value
            Word actual;
            actual = target.WordAt(loc);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///a test for WordTextAt
        ///</summary>
        [TestMethod()]
        public void WordTextAtTest() {

            Document target = BuildDocumentManually();
            int loc = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.WordTextAt(loc);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///a test for Paragraphs
        ///</summary>
        [TestMethod()]
        public void ParagraphsTest() {

            Document target = BuildDocumentManually();
            IEnumerable<Paragraph> actual;
            actual = target.Paragraphs;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///a test for Phrases
        ///</summary>
        [TestMethod()]
        public void PhrasesTest() {

            Document target = BuildDocumentManually();
            IEnumerable<Phrase> actual;
            actual = target.Phrases;
            var expectedResult = actual.Zip(new[] { "We", "must attack", "blue team", "We", "must do", "this", "quickly" }, (r, s) => r.Text == s).Aggregate(true, (aggr, val) => aggr &= val);
            Assert.IsTrue(expectedResult);
        }

        /// <summary>
        ///a test for Sentences
        ///</summary>
        [TestMethod()]
        public void SentencesTest() {
            Document target = BuildDocumentManually();
            IEnumerable<Sentence> actual;
            actual = target.Sentences;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///a test for Words
        ///</summary>
        [TestMethod()]
        public void WordsTest() {
            Document target = BuildDocumentManually();
            IEnumerable<Word> actual;
            actual = target.Words;
            string[] expectedLexicalMatches = new[]{
                "We", "must", "attack", "blue", "team", "We", "must", "do", "this", "quickly"};
            var expectedResult = actual.Zip(expectedLexicalMatches, (w, s) => w.Text == s).Aggregate(true, (aggr, val) => aggr &= val);
            Assert.IsTrue(expectedResult);
        }
    }
}
