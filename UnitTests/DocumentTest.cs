using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is A test class for DocumentTest and is intended
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
        //Use ClassCleanup to run code after all tests in A class have run
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

        private static Document BuildDocumentManually()
        {
            IEnumerable<Paragraph> allParagrpahs = BuildParagraphs();
            return new Document(allParagrpahs);
        }

        private static IEnumerable<Paragraph> BuildParagraphs()
        {
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
                            )}, new SentenceDelimiter('!')),
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
                    })}, new SentenceDelimiter('!'))
                })
            };
            return allParagrpahs;
        }


        #endregion

        /// <summary>
        ///A test for Document Constructor
        ///</summary>
        //[TestMethod()]
        public void DocumentConstructorTest()
        {
            Document doc = BuildDocumentManually();
            Assert.IsTrue(doc != null);
        }

        /// <summary>
        ///A test for GetActions
        ///</summary>
        [TestMethod()]
        public void GetActionsTest()
        {

            Document target = BuildDocumentManually();
            IEnumerable<ITransitiveVerbal> expected = new ITransitiveVerbal[]{new VerbPhrase(new Word[] { 
                                new ModalAuxilary("must"),
                                new Verb("attack", VerbTense.Base) 
                            }),new Verb("attack", VerbTense.Base),  new VerbPhrase(new Word[] { 
                                new ModalAuxilary("must"),
                                new Verb("do", VerbTense.Base)
                            }),new Verb("do", VerbTense.Base)};
            IEnumerable<ITransitiveVerbal> actual;
            actual = target.GetActions();
            foreach (var e in expected) {
                Assert.IsTrue(actual.Contains(e, new VerbialEquater()));
            }

        }
        private struct VerbialEquater : IEqualityComparer<ITransitiveVerbal>
        {
            public bool Equals(ITransitiveVerbal a, ITransitiveVerbal b)
            {
                return a.Text == b.Text && a.GetType() == b.GetType();
            }

            public int GetHashCode(ITransitiveVerbal obj)
            {
                throw new NotImplementedException();
            }
        }
        private struct EntityEquater : IEqualityComparer<IEntity>
        {
            public bool Equals(IEntity a, IEntity b)
            {
                return a.Text == b.Text && a.GetType() == b.GetType();
            }

            public int GetHashCode(IEntity obj)
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        ///A test for GetEntities
        ///</summary>
        [TestMethod()]
        public void GetEntitiesTest()
        {

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
        ///A test for Paragraphs
        ///</summary>
        [TestMethod()]
        public void ParagraphsTest()
        {
            IEnumerable<Paragraph> paragraphsIn = BuildParagraphs();
            Document target = new Document(paragraphsIn);
            IEnumerable<Paragraph> actual;
            actual = target.Paragraphs;
            for (var i = 0; i < paragraphsIn.Count(); ++i) {

                Assert.AreEqual(paragraphsIn.ToList()[i], actual.ToList()[i]);
            }
        }

        /// <summary>
        ///A test for Phrases
        ///</summary>
        [TestMethod()]
        public void PhrasesTest()
        {

            Document target = BuildDocumentManually();
            IEnumerable<Phrase> actual;
            actual = target.Phrases;
            var expectedResult = actual.Zip(
                new[] { "We", "must attack", "blue team", "We", "must do", "this", "quickly" },
                (r, s) => r.Text == s).Aggregate(true, (aggr, val) => aggr &= val);
            Assert.IsTrue(expectedResult);
        }

        /// <summary>
        ///A test for Sentences
        ///</summary>
        [TestMethod()]
        public void SentencesTest()
        {
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
                            )}, new SentenceDelimiter('!')),
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
                    })}, new SentenceDelimiter('!'))
                };

            Document target = new Document(new[] { new Paragraph(firstParagraphSentences) });
            IEnumerable<Sentence> actual;
            actual = target.Sentences;
            for (var i = 0; i < actual.Count(); ++i) {

                Assert.AreEqual(firstParagraphSentences.ToList()[i], actual.ToList()[i]);
            }
        }

        /// <summary>
        ///A test for Words
        ///</summary>
        [TestMethod()]
        public void WordsTest()
        {
            Document target = BuildDocumentManually();
            IEnumerable<Word> actual;
            actual = target.Words;
            string[] expectedLexicalMatches = new[]{
                "We", "must", "attack", "blue", "team","!", "We", "must", "do", "this", "quickly","!"};
            var expectedResult = actual.Zip(expectedLexicalMatches, (w, s) => w.Text == s).Aggregate(true, (aggr, val) => aggr &= val);
            Assert.IsTrue(expectedResult);
        }

    }
}
