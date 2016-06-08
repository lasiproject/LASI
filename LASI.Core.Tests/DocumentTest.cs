using LASI.Core;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using NFluent;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for DocumentTest and is intended
    ///to contain all DocumentTest Unit Tests
    /// </summary>
    public class DocumentTest
    {
        #region Testing Helpers

        private static Document CreateUnboundUnweightedTestDocument()
        {
            IEnumerable<Paragraph> allParagrpahs = BuildParagraphs();
            return new Document(allParagrpahs);
        }

        private static IEnumerable<Paragraph> BuildParagraphs()
        {
            IEnumerable<Paragraph> allParagrpahs = new[] {
                new Paragraph(ParagraphKind.Default,
                    new Sentence(new Clause[] {
                        new Clause(
                            new NounPhrase(
                                new PersonalPronoun("We")
                            ),
                            new VerbPhrase(
                                new ModalAuxilary("must"),
                                new BaseVerb("attack")
                            ),
                            new NounPhrase(
                                new Adjective("blue"),
                                new CommonSingularNoun("team"))
                            )}, SentenceEnding.ExclamationPoint),
                        new Sentence(new Clause[] {
                            new Clause(
                            new PronounPhrase(
                                new PersonalPronoun("We")),
                            new VerbPhrase(
                                new ModalAuxilary("must"),
                                new BaseVerb("do")
                            ),
                        new NounPhrase(new PersonalPronoun("this")),
                        new AdverbPhrase(new Adverb("quickly")))
                    }, SentenceEnding.ExclamationPoint)
                ),
                new Paragraph(ParagraphKind.Default,
                    new Sentence(new Clause[] {
                        new Clause(
                            new NounPhrase(new PersonalPronoun("We")),
                            new VerbPhrase(new BaseVerb("are")),
                            new AdjectivePhrase(new Adjective("obligated"))),
                        new SubordinateClause(
                            new SubordinateClauseBeginPhrase(new Preposition("because")),
                            new PronounPhrase(new PersonalPronoun("they")),
                            new VerbPhrase(new BaseVerb("are")),
                            new NounPhrase(new CommonPluralNoun("jerks"))
                    )}, SentenceEnding.ExclamationPoint)
                )
            };
            return allParagrpahs.ToList();
        }


        #endregion

        /// <summary>
        ///A test for Document Constructor
        /// </summary>
        [Fact]
        public void DocumentConstructorTest()
        {
            Document doc = CreateUnboundUnweightedTestDocument();
            Assert.True(doc.Words
                .Select((x, index) => x.PreviousWord == doc.Words.ElementAtOrDefault(index - 1) && x.NextWord == doc.Words.ElementAtOrDefault(index + 1)
             ).Aggregate(true, (f, e) => f && e));

            Assert.True(doc.Phrases
                .Select((x, index) => x.Previous == doc.Phrases.ElementAtOrDefault(index - 1) && x.Next == doc.Phrases.ElementAtOrDefault(index + 1)
            ).Aggregate(true, (f, e) => f && e));

            Assert.True(doc != null);
        }

        /// <summary>
        ///A test for GetActions
        /// </summary>
        [Fact]
        public void GetVerbalsTest()
        {
            Document target = CreateUnboundUnweightedTestDocument();
            IEnumerable<IVerbal> expected = new IVerbal[]{
                    new VerbPhrase(new Word[] {
                                new ModalAuxilary("must"),
                                new BaseVerb("attack")
                            }),new BaseVerb("attack"),
                            new VerbPhrase(new Word[] {
                                new ModalAuxilary("must"),
                                new BaseVerb("do")
                            }),new BaseVerb("do"),
                    new VerbPhrase(new[] {new BaseVerb("are")
                }),

            };
            IEnumerable<IVerbal> actual;
            actual = target.Verbals;
            foreach (var e in expected)
            {
                Assert.True(actual.Contains(e, LASI.Utilities.Equality.Create<IVerbal>((a, b) => a.Text == b.Text && a.GetType() == b.GetType())));
            }

        }

        /// <summary>
        ///A test for GetEntities
        /// </summary>
        [Fact]
        public void GetEntitiesTest()
        {

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
            foreach (var e in expected)
            {
                Assert.True(actual.Contains(e, LASI.Utilities.Equality.Create<IEntity>((a, b) => a.Text == b.Text && a.GetType() == b.GetType())));
            }
        }


        /// <summary>
        ///A test for Paragraphs
        /// </summary>
        [Fact]
        public void ParagraphsTest()
        {
            IEnumerable<Paragraph> paragraphsIn = BuildParagraphs();
            Document target = new Document(paragraphsIn);
            IEnumerable<Paragraph> actual = target.Paragraphs.ToList();

            Check.That(paragraphsIn).ContainsExactly(actual);
        }

        /// <summary>
        ///A test for Phrases
        /// </summary>
        [Fact]
        public void PhrasesTest()
        {

            Document target = CreateUnboundUnweightedTestDocument();
            IEnumerable<Phrase> actual;
            actual = target.Phrases;
            var expectedResult = actual
                .Zip(new[] { "We", "must attack", "blue team", "We", "must do", "this", "quickly" },
                (r, s) => r.Text == s)
                .Aggregate(true, (aggr, val) => aggr && val);
            Assert.True(expectedResult);
        }

        /// <summary>
        ///A test for Sentences
        /// </summary>
        [Fact]
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
                                new BaseVerb("attack")
                            }),
                            new NounPhrase(new Word[] {
                                new Adjective("blue"),
                                new CommonSingularNoun("team") }
                                )}
                            )}, SentenceEnding.ExclamationPoint),
                        new Sentence(new Clause[]{new Clause( new Phrase[]{
                            new NounPhrase(new Word[]{
                                new PersonalPronoun("We")}),
                            new VerbPhrase(new Word[] {
                                new ModalAuxilary("must"),
                                new BaseVerb("do")
                            }),
                        new NounPhrase(new Word[]{
                            new PersonalPronoun("this")
                        }),
                        new AdverbPhrase(new Word [] {
                            new Adverb("quickly")
                        })
                    })}, SentenceEnding.ExclamationPoint)
                };

            Document target = new Document(new[] { new Paragraph(ParagraphKind.Default, firstParagraphSentences) });
            IEnumerable<Sentence> actual;
            actual = target.Sentences;
            for (var i = 0; i < actual.Count(); ++i)
            {

                Check.That(firstParagraphSentences.ToList()[i]).IsEqualTo(actual.ToList()[i]);
            }
        }

        /// <summary>
        ///A test for Words
        /// </summary>
        [Fact]
        public void WordsTest()
        {
            Document target = CreateUnboundUnweightedTestDocument();

            var actual = target.Words;
            string[] expectedLexicalMatches = { "We", "must", "attack", "blue", "team", "!", "We", "must", "do", "this", "quickly", "!" };

            var expectedResult = actual.Zip(expectedLexicalMatches, (w, s) => w.Text == s).Aggregate(true, (aggr, val) => aggr && val);

            Check.That(expectedResult).IsTrue();
        }




        /// <summary>
        ///A test for Name
        /// </summary>
        [Fact]
        public void TitleTest()
        {
            IEnumerable<Paragraph> allParagrpahs = BuildParagraphs();
            Document target = new Document("testname", allParagrpahs);
            string expected = "testname";
            string actual;
            actual = target.Name;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for Clauses
        /// </summary>
        [Fact]
        public void ClausesTest()
        {
            Sentence[] firstParagraphSentences = new Sentence[] {
                    new Sentence(new Clause[] {
                        new Clause(
                            new NounPhrase(
                                new PersonalPronoun("We")
                            ),
                            new VerbPhrase(
                                new ModalAuxilary("must"),
                                new BaseVerb("attack")
                            ),
                            new NounPhrase(
                                new Adjective("blue"),
                                new CommonSingularNoun("team")
                                )
                            )}, SentenceEnding.ExclamationPoint),
                        new Sentence(new Clause[] { new Clause(
                            new NounPhrase(
                                new PersonalPronoun("We")
                            ),
                            new VerbPhrase(
                                new ModalAuxilary("must"),
                                new BaseVerb("do")
                            ),
                        new NounPhrase(
                            new PersonalPronoun("this")
                        ),
                        new AdverbPhrase(
                            new Adverb("quickly")
                        ))
                    }, SentenceEnding.ExclamationPoint)
                };

            Document target = new Document(new[] { new Paragraph(ParagraphKind.Default, firstParagraphSentences) });

            IEnumerable<Clause> expected = firstParagraphSentences.SelectMany(s => s.Clauses);
            IEnumerable<Clause> actual;
            actual = target.Clauses;
            Check.That(actual).Contains(expected).Only();
        }

        /// <summary>
        ///A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            Document target = CreateUnboundUnweightedTestDocument();
            string expected = string.Join(string.Empty, target.GetType(), ": ", target.Name, "\nParagraphs:\n", target.Paragraphs.Format());
            string actual;
            actual = target.ToString();
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for Paginate
        /// </summary>
        [Fact]
        public void PaginateTest()
        {
            Document target = CreateUnboundUnweightedTestDocument();
            int lineLength = 80;
            int linesPerPage = 3;

            IEnumerable<Document.Page> actual;

            actual = target.Paginate(lineLength, linesPerPage);
            foreach (var page in actual)
            {
                Assert.True(string.Join(string.Empty, page.Paragraphs.Select(p => p.Text)).Length <= lineLength * linesPerPage);
            }
            Check.That(target.Sentences.Except(actual.SelectMany(page => page.Sentences))).IsEmpty();
            Check.That(target.Paragraphs.Except(actual.SelectMany(page => page.Paragraphs))).IsEmpty();
            Check.That(string.Join(string.Empty, target.Sentences.Select(s => s.Text))).IsEqualTo(string.Join(string.Empty, actual.SelectMany(p => p.Sentences.Select(s => s.Text))));
        }
        /// <summary>
        ///A test for Paginate
        /// </summary>
        [Fact]
        public void PaginateTest1()
        {
            Document target = CreateUnboundUnweightedTestDocument();
            int lineLength = 80;
            int linesPerPage = 1;

            IEnumerable<Document.Page> actual;

            actual = target.Paginate(lineLength, linesPerPage);
            foreach (var page in actual)
            {
                Assert.True(string.Join(string.Empty, page.Paragraphs.Select(p => p.Text)).Length <= lineLength * linesPerPage);
            }
            Check.That(target.Paragraphs.Except(actual.SelectMany(page => page.Paragraphs))).IsEmpty();
            Check.That(target.Sentences.Except(actual.SelectMany(page => page.Sentences))).IsEmpty();
            Check.That(string.Join(string.Empty, target.Sentences.Select(s => s.Text))).IsEqualTo(string.Join(string.Empty, actual.SelectMany(p => p.Sentences.Select(s => s.Text))));

        }



    }
}
