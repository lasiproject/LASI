using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Shared.Test.Assertions;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for ParagraphTest and is intended
    ///to contain all ParagraphTest Unit Tests
    ///</summary>
    [TestClass]
    public class ParagraphTest
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

        #endregion


        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
        {
            Phrase[] phrases1 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("found") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") })
            };
            Sentence[] sentences = new Sentence[3];
            sentences[0] = new Sentence(phrases1, SentenceEnding.Period);
            Phrase[] phrases2 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("SNIFd") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("them") })
            };
            sentences[1] = new Sentence(phrases2, SentenceEnding.Period);
            Phrase[] phrases3 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("Richard") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("did") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("awesome") })
            };
            sentences[2] = new Sentence(phrases3, SentenceEnding.Period);

            Paragraph target = new Paragraph(sentences, ParagraphKind.Default);

            string expected = string.Format("LASI.Core.Paragraph: {0} sentences\n\"LASI found TIMIS. LASI SNIFd them. Richard did awesome.\"", sentences.Length);
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod]
        public void TextTest()
        {
            Phrase[] phrases1 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("found")}),
                new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS")})
            };
            Sentence[] sentences = new Sentence[3];
            sentences[0] = new Sentence(phrases1, SentenceEnding.Period);
            Phrase[] phrases2 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("SNIFd") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("them") })
            };
            sentences[1] = new Sentence(phrases2, SentenceEnding.Period);
            Phrase[] phrases3 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("Richard") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("did") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("awesome") })
            };
            sentences[2] = new Sentence(phrases3, SentenceEnding.Period);

            Paragraph target = new Paragraph(sentences, ParagraphKind.Default);

            string expected = "LASI found TIMIS. LASI SNIFd them. Richard did awesome.";
            string actual;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EstablishParent
        ///</summary>
        [TestMethod]
        public void EstablishParentTest()
        {
            Phrase[] phrases1 = new Phrase[] {
                   new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                   new VerbPhrase(new Word[] { new PastTenseVerb("found")}),
                   new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS")})
               };
            Sentence[] sentences = new Sentence[3];
            sentences[0] = new Sentence(phrases1, SentenceEnding.Period);
            Phrase[] phrases2 = new Phrase[] {
                   new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                   new VerbPhrase(new Word[] { new PastTenseVerb("SNIFd") }),
                   new NounPhrase(new Word[] { new ProperPluralNoun("them") })
               };
            sentences[1] = new Sentence(phrases2, SentenceEnding.Period);
            Phrase[] phrases3 = new Phrase[] {
                   new NounPhrase(new Word[] { new ProperSingularNoun("Richard") }),
                   new VerbPhrase(new Word[] { new PastTenseVerb("did") }),
                   new NounPhrase(new Word[] { new ProperPluralNoun("awesome") })
               };
            sentences[2] = new Sentence(phrases3, SentenceEnding.Period);

            Paragraph[] target = { new Paragraph(sentences, ParagraphKind.Default) };
            Document parentDoc = new Document(target);
            target[0].EstablishParent(parentDoc);
            Assert.AreEqual(target[0].Document, parentDoc);
        }

        /// <summary>
        ///A test for Words
        ///</summary>
        [TestMethod]
        public void WordsTest()
        {
            IEnumerable<Sentence> sentences = new Sentence[] {
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
            ParagraphKind paragraphKind = ParagraphKind.Default;
            Paragraph target = new Paragraph(sentences, paragraphKind);
            IEnumerable<Word> actual;
            actual = target.Words;
            EnumerableAssert.AreSequenceEqual(sentences.Words(), actual);
        }


        /// <summary>
        ///A test for Phrases
        ///</summary>
        [TestMethod]
        public void PhrasesTest()
        {
            IEnumerable<Sentence> sentences = new Sentence[] {
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
            ParagraphKind paragraphKind = ParagraphKind.Default;
            Paragraph target = new Paragraph(sentences, paragraphKind);
            IEnumerable<Phrase> actual;
            actual = target.Phrases;
            EnumerableAssert.AreSequenceEqual(sentences.Phrases(), actual);
        }



        /// <summary>
        ///A test for GetPhrasesAfter
        ///</summary>
        [TestMethod]
        public void GetPhrasesAfterTest()
        {
            var startAfter = new NounPhrase(new Word[] {
                                new Adjective("blue"),
                                new CommonSingularNoun("team") });
            IEnumerable<Sentence> sentences = new Sentence[] {
                    new Sentence(new Clause[] {
                        new Clause(new Phrase[] {
                            new NounPhrase(new Word[] {
                                new PersonalPronoun("We")
                            }),
                            new VerbPhrase(new Word[] {
                                new ModalAuxilary("must"),
                                new BaseVerb("attack")
                            }),
                            startAfter}
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
            ParagraphKind paragraphKind = ParagraphKind.Default;
            Paragraph target = new Paragraph(sentences, paragraphKind);
            IEnumerable<Phrase> expected = sentences.Phrases().SkipWhile(p => p != startAfter).Skip(1);
            IEnumerable<Phrase> actual;
            actual = target.GetPhrasesAfter(startAfter);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }

        /// <summary>
        ///A test for Paragraph Constructor
        ///</summary>
        [TestMethod]
        public void ParagraphConstructorTest()
        {
            IEnumerable<Sentence> sentences = new Sentence[] {
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
            ParagraphKind paragraphKind = ParagraphKind.Default;
            Paragraph target = new Paragraph(sentences, paragraphKind);
            Assert.AreEqual(paragraphKind, target.ParagraphKind);
            EnumerableAssert.AreSequenceEqual(sentences, target.Sentences);
        }
    }

}
