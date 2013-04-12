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
            IEnumerable<Paragraph> allParagrpahs = BuildParagraphs();
            return new Document(allParagrpahs);
        }

        private static IEnumerable<Paragraph> BuildParagraphs() {
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
            return allParagrpahs;
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
            IEnumerable<ITransitiveVerbial> expected = new ITransitiveVerbial[]{new VerbPhrase(new Word[] { 
                                new ModalAuxilary("must"),
                                new Verb("attack", VerbTense.Base) 
                            }),new Verb("attack", VerbTense.Base),  new VerbPhrase(new Word[] { 
                                new ModalAuxilary("must"),
                                new Verb("do", VerbTense.Base)
                            }),new Verb("do", VerbTense.Base)};
            IEnumerable<ITransitiveVerbial> actual;
            actual = target.GetActions();
            foreach (var e in expected) {
                Assert.IsTrue(actual.Contains(e, new VerbialEquater()));
            }

        }
        private struct VerbialEquater : IEqualityComparer<ITransitiveVerbial>
        {
            public bool Equals(ITransitiveVerbial a, ITransitiveVerbial b) {
                return a.Text == b.Text && a.GetType() == b.GetType();
            }

            public int GetHashCode(ITransitiveVerbial obj) {
                throw new NotImplementedException();
            }
        }
        private struct EntityEquater : IEqualityComparer<IEntity>
        {
            public bool Equals(IEntity a, IEntity b) {
                return a.Text == b.Text && a.GetType() == b.GetType();
            }

            public int GetHashCode(IEntity obj) {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        ///a test for GetEntities
        ///</summary>
        [TestMethod()]
        public void GetEntitiesTest() {

            Document target = BuildDocumentManually();
            IEnumerable<IEntity> expected = new IEntity[]{
                new NounPhrase(new Word[] {    
                                new PersonalPronoun("We") 
                            }), new PersonalPronoun("We"),
                            new NounPhrase(new Word[] { 
                                new Adjective("blue"), 
                                new GenericSingularNoun("team") }
                                ),
                             new GenericSingularNoun("team") ,
                             new NounPhrase(new Word[]{
                            new PersonalPronoun("We")}),  
                            new PersonalPronoun("We"), 
                            new NounPhrase(new Word[]{  
                            new PersonalPronoun("this")
                        }),  new PersonalPronoun("this")};
            IEnumerable<IEntity> actual;
            actual = target.GetEntities();
            foreach (var e in expected) {
                Assert.IsTrue(actual.Contains(e, new EntityEquater()));
            }
        }





        /// <summary>
        ///a test for SentenceAt
        ///</summary>
        [TestMethod()]
        public void SentenceAtTest() {

            Document target = BuildDocumentManually();
            for (int i = 0; i < target.Sentences.Count(); ++i) {
                Sentence expected = target.Sentences.ToList()[i];
                Sentence actual;
                actual = target.SentenceAt(i);
                Assert.AreEqual(expected, actual);
            }

        }

        /// <summary>
        ///a test for SentenceTextAt
        ///</summary>
        [TestMethod()]
        public void SentenceTextAtTest() {
            Document target = BuildDocumentManually();
            for (int i = 0; i < target.Sentences.Count(); ++i) {
                string expected = target.Sentences.Skip(i).First().Text;
                string actual;
                actual = target.SentenceTextAt(i);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///a test for WordAt
        ///</summary>
        [TestMethod()]
        public void WordAtTest() {

            Document target = BuildDocumentManually();
            for (int i = 0; i < target.Words.Count(); ++i) {
                Word expected = target.Words.Skip(i).First();
                Word actual;
                actual = target.WordAt(i);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///a test for WordTextAt
        ///</summary>
        [TestMethod()]
        public void WordTextAtTest() {

            Document target = BuildDocumentManually();
            for (int i = 0; i < target.Words.Count(); ++i) {
                string expected = target.Words.Skip(i).First().Text;
                string actual;
                actual = target.WordTextAt(i);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///a test for Paragraphs
        ///</summary>
        [TestMethod()]
        public void ParagraphsTest() {
            IEnumerable<Paragraph> paragraphsIn = BuildParagraphs();
            Document target = new Document(paragraphsIn);
            IEnumerable<Paragraph> actual;
            actual = target.Paragraphs;
            for (var i = 0; i < paragraphsIn.Count(); ++i) {

                Assert.AreEqual(paragraphsIn.ToList()[i], actual.ToList()[i]);
            }
        }

        /// <summary>
        ///a test for Phrases
        ///</summary>
        [TestMethod()]
        public void PhrasesTest() {

            Document target = BuildDocumentManually();
            IEnumerable<Phrase> actual;
            actual = target.Phrases;
            var expectedResult = actual.Zip(
                new[] { "We", "must attack", "blue team", "We", "must do", "this", "quickly" },
                (r, s) => r.Text == s).Aggregate(true, (aggr, val) => aggr &= val);
            Assert.IsTrue(expectedResult);
        }

        /// <summary>
        ///a test for Sentences
        ///</summary>
        [TestMethod()]
        public void SentencesTest() {
            Sentence[] firstParagraphSentences = new Sentence[] { 
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
                };

            Document target = new Document(new[] { new Paragraph(firstParagraphSentences) });
            IEnumerable<Sentence> actual;
            actual = target.Sentences;
            for (var i = 0; i < actual.Count(); ++i) {

                Assert.AreEqual(firstParagraphSentences.ToList()[i], actual.ToList()[i]);
            }
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
