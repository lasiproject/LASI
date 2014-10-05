using LASI;
using LASI.Core;
using LASI.Core.DocumentStructures;
using LASI.UnitTests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LASI.UnitTests
{


    /// <summary>
    ///This is A test class for DocumentTest and is intended
    ///to contain all DocumentTest Unit Tests
    ///</summary>
    [TestClass]
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

        private static Document CreateUnboundUnweightedTestDocument() {
            IEnumerable<Paragraph> allParagrpahs = BuildParagraphs();
            return new Document(allParagrpahs);
        }

        private static IEnumerable<Paragraph> BuildParagraphs() {
            IEnumerable<Paragraph> allParagrpahs = new[] {
                new Paragraph(new[] {
                    new Sentence(new Clause[] {
                        new Clause(new Phrase[] {
                            new NounPhrase(new Word[] {
                                new PersonalPronoun("We")
                            }),
                            new VerbPhrase(new Word[] {
                                new ModalAuxilary("must"),
                                new Verb("attack", VerbForm.Base)
                            }),
                            new NounPhrase(new Word[] {
                                new Adjective("blue"),
                                new CommonSingularNoun("team") }
                                )}
                            )}, new SentenceEnding('!')),
                        new Sentence(new Clause[]{new Clause( new Phrase[]{
                            new PronounPhrase(new Word[]{
                                new PersonalPronoun("We")}),
                            new VerbPhrase(new Word[] {
                                new ModalAuxilary("must"),
                                new Verb("do", VerbForm.Base)
                            }),
                        new NounPhrase(new Word[]{
                            new PersonalPronoun("this")
                        }),
                        new AdverbPhrase(new Word [] {
                            new Adverb("quickly")
                        })
                    })}, new SentenceEnding('!'))
                }, ParagraphKind.Default),
                new Paragraph(new[] {
                    new Sentence(new Clause[] {
                        new Clause(new Phrase[] {
                            new NounPhrase(new Word[] {
                                new PersonalPronoun("We")
                            }),
                            new VerbPhrase(new Word[] {
                                new Verb("are", VerbForm.Base)
                            }),
                            new AdjectivePhrase(new[] {
                                new Adjective("obligated")
                            })
                        }),
                        new SubordinateClause(new Phrase[]{
                            new SubordinateClauseBeginPhrase(new Word[] {
                                new Preposition("because")
                            }),
                            new PronounPhrase(new[]{
                                new PersonalPronoun("they")
                            }),
                            new VerbPhrase(new []{
                                new Verb("are",VerbForm.Base)
                            }),
                            new NounPhrase(new []{
                                new CommonPluralNoun("jerks")
                            })
                    })}, new SentenceEnding('!'))
                }, ParagraphKind.Default)
            };
            return allParagrpahs;
        }


        #endregion

        /// <summary>
        ///A test for Document Constructor
        ///</summary>
        [TestMethod]
        public void DocumentConstructorTest() {
            Document doc = CreateUnboundUnweightedTestDocument();
            Assert.IsTrue(doc.Words
                .Select((x, index) => x.PreviousWord == doc.Words.ElementAtOrDefault(index - 1) && x.NextWord == doc.Words.ElementAtOrDefault(index + 1)
             ).Aggregate(true, (f, e) => f && e));
            Assert.IsTrue(doc.Phrases
                .Select((x, index) => x.PreviousPhrase == doc.Phrases.ElementAtOrDefault(index - 1) && x.NextPhrase == doc.Phrases.ElementAtOrDefault(index + 1)
                ).Aggregate(true, (f, e) => f && e));
            Assert.IsTrue(doc != null);
        }

        /// <summary>
        ///A test for GetActions
        ///</summary>
        [TestMethod]
        public void GetVerbalsTest() {
            Document target = CreateUnboundUnweightedTestDocument();
            IEnumerable<IVerbal> expected = new IVerbal[]{
                    new VerbPhrase(new Word[] {
                                new ModalAuxilary("must"),
                                new Verb("attack", VerbForm.Base)
                            }),new Verb("attack", VerbForm.Base),
                            new VerbPhrase(new Word[] {
                                new ModalAuxilary("must"),
                                new Verb("do", VerbForm.Base)
                            }),new Verb("do", VerbForm.Base),
                    new VerbPhrase(new[] {new Verb("are",VerbForm.Base)
                }),

            };
            IEnumerable<IVerbal> actual;
            actual = target.Verbals;
            foreach (var e in expected) {
                Assert.IsTrue(actual.Contains(e, LexicalComparers.Create<IVerbal>((a, b) => a.Text == b.Text && a.GetType() == b.GetType())));
            }

        }

        /// <summary>
        ///A test for GetEntities
        ///</summary>
        [TestMethod]
        public void GetEntitiesTest() {

            Document target = CreateUnboundUnweightedTestDocument();
            IEnumerable<IEntity> expected = new IEntity[]{
                new NounPhrase(new Word[] {
                                new PersonalPronoun("We")
                            }), new PersonalPronoun("We"),
                            new NounPhrase(new Word[] {
                                new Adjective("blue"),
                                new CommonSingularNoun("team") }
                                ),
                             new CommonSingularNoun("team") ,
                             new NounPhrase(new Word[]{
                            new PersonalPronoun("We")}),
                            new PersonalPronoun("We"),
                            new NounPhrase(new Word[]{
                            new PersonalPronoun("this")
                        }),  new PersonalPronoun("this"),
                        new PronounPhrase(new []{new PersonalPronoun("We")}),
                        new PronounPhrase(new []{new PersonalPronoun("they")}),
                        new NounPhrase(new []{new CommonPluralNoun("jerks")}),
            };
            IEnumerable<IEntity> actual;
            actual = target.Entities;
            foreach (var e in expected) {
                Assert.IsTrue(actual.Contains(e, LexicalComparers.Create<IEntity>((a, b) => a.Text == b.Text && a.GetType() == b.GetType())));
            }
        }


        /// <summary>
        ///A test for Paragraphs
        ///</summary>
        [TestMethod]
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
        ///A test for Phrases
        ///</summary>
        [TestMethod]
        public void PhrasesTest() {

            Document target = CreateUnboundUnweightedTestDocument();
            IEnumerable<Phrase> actual;
            actual = target.Phrases;
            var expectedResult = actual
                .Zip(new[] { "We", "must attack", "blue team", "We", "must do", "this", "quickly" },
                (r, s) => r.Text == s)
                .Aggregate(true, (aggr, val) => aggr && val);
            Assert.IsTrue(expectedResult);
        }

        /// <summary>
        ///A test for Sentences
        ///</summary>
        [TestMethod]
        public void SentencesTest() {
            Sentence[] firstParagraphSentences = new Sentence[] {
                    new Sentence(new Clause[] {
                        new Clause(new Phrase[] {
                            new NounPhrase(new Word[] {
                                new PersonalPronoun("We")
                            }),
                            new VerbPhrase(new Word[] {
                                new ModalAuxilary("must"),
                                new Verb("attack", VerbForm.Base)
                            }),
                            new NounPhrase(new Word[] {
                                new Adjective("blue"),
                                new CommonSingularNoun("team") }
                                )}
                            )}, new SentenceEnding('!')),
                        new Sentence(new Clause[]{new Clause( new Phrase[]{
                            new NounPhrase(new Word[]{
                                new PersonalPronoun("We")}),
                            new VerbPhrase(new Word[] {
                                new ModalAuxilary("must"),
                                new Verb("do", VerbForm.Base)
                            }),
                        new NounPhrase(new Word[]{
                            new PersonalPronoun("this")
                        }),
                        new AdverbPhrase(new Word [] {
                            new Adverb("quickly")
                        })
                    })}, new SentenceEnding('!'))
                };

            Document target = new Document(new[] { new Paragraph(firstParagraphSentences, ParagraphKind.Default) });
            IEnumerable<Sentence> actual;
            actual = target.Sentences;
            for (var i = 0; i < actual.Count(); ++i) {

                Assert.AreEqual(firstParagraphSentences.ToList()[i], actual.ToList()[i]);
            }
        }

        /// <summary>
        ///A test for Words
        ///</summary>
        [TestMethod]
        public void WordsTest() {
            Document target = CreateUnboundUnweightedTestDocument();
            IEnumerable<Word> actual;
            actual = target.Words;
            string[] expectedLexicalMatches = new[]{
                "We", "must", "attack", "blue", "team","!", "We", "must", "do", "this", "quickly","!"};
            var expectedResult = actual.Zip(expectedLexicalMatches, (w, s) => w.Text == s).Aggregate(true, (aggr, val) => aggr && val);
            Assert.IsTrue(expectedResult);
        }




        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod]
        public void NameTest() {
            Document target = CreateUnboundUnweightedTestDocument();
            string expected = "testname";
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Clauses
        ///</summary>
        [TestMethod]
        public void ClausesTest() {
            Sentence[] firstParagraphSentences = new Sentence[] {
                    new Sentence(new Clause[] {
                        new Clause(new Phrase[] {
                            new NounPhrase(new Word[] {
                                new PersonalPronoun("We")
                            }),
                            new VerbPhrase(new Word[] {
                                new ModalAuxilary("must"),
                                new Verb("attack", VerbForm.Base)
                            }),
                            new NounPhrase(new Word[] {
                                new Adjective("blue"),
                                new CommonSingularNoun("team") }
                                )}
                            )}, new SentenceEnding('!')),
                        new Sentence(new Clause[]{new Clause( new Phrase[]{
                            new NounPhrase(new Word[]{
                                new PersonalPronoun("We")}),
                            new VerbPhrase(new Word[] {
                                new ModalAuxilary("must"),
                                new Verb("do", VerbForm.Base)
                            }),
                        new NounPhrase(new Word[]{
                            new PersonalPronoun("this")
                        }),
                        new AdverbPhrase(new Word [] {
                            new Adverb("quickly")
                        })
                    })}, new SentenceEnding('!'))
                };

            Document target = new Document(new[] { new Paragraph(firstParagraphSentences, ParagraphKind.Default) });

            IEnumerable<Clause> expected = firstParagraphSentences.SelectMany(s => s.Clauses);
            IEnumerable<Clause> actual;
            actual = target.Clauses;
            AssertHelper.AreSetEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest() {
            Document target = CreateUnboundUnweightedTestDocument();
            string expected = string.Join(string.Empty, target.GetType(), ":  ", target.Name, "\nParagraphs: \n", target.Paragraphs.Format());
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Paginate
        ///</summary>
        [TestMethod]
        public void PaginateTest() {
            Document target = CreateUnboundUnweightedTestDocument();
            int lineLength = 80;
            int linesPerPage = 3;

            IEnumerable<Document.Page> actual;

            actual = target.Paginate(lineLength, linesPerPage);
            foreach (var page in actual) {
                Assert.IsTrue(string.Join(string.Empty, page.Paragraphs.Select(p => p.Text)).Length <= lineLength * linesPerPage);
            }
            AssertHelper.AreSetEqual(target.Sentences, actual.SelectMany(page => page.Sentences));
            AssertHelper.AreSetEqual(target.Paragraphs, actual.SelectMany(page => page.Paragraphs));
            Assert.IsTrue(string.Join(string.Empty, target.Sentences.Select(s => s.Text)) == string.Join(string.Empty, actual.SelectMany(p => p.Sentences.Select(s => s.Text))));
        }
        /// <summary>
        ///A test for Paginate
        ///</summary>
        [TestMethod]
        public void PaginateTest1() {
            Document target = CreateUnboundUnweightedTestDocument();
            int lineLength = 80;
            int linesPerPage = 1;

            IEnumerable<Document.Page> actual;

            actual = target.Paginate(lineLength, linesPerPage);
            foreach (var page in actual) {
                Assert.IsTrue(string.Join(string.Empty, page.Paragraphs.Select(p => p.Text)).Length <= lineLength * linesPerPage);
            }
            AssertHelper.AreSetEqual(target.Paragraphs, actual.SelectMany(page => page.Paragraphs));
            AssertHelper.AreSetEqual(target.Sentences, actual.SelectMany(page => page.Sentences));
            Assert.IsTrue(string.Join(string.Empty, target.Sentences.Select(s => s.Text)) == string.Join(string.Empty, actual.SelectMany(p => p.Sentences.Select(s => s.Text))));

        }



    }
}
