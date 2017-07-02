using System.Linq;
using System.Collections.Generic;
using NFluent;
using Fact = Xunit.FactAttribute;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is A test class for ParagraphTest and is intended
    ///to contain all ParagraphTest Unit Tests
    /// </summary>
    public class ParagraphTest
    {
        /// <summary>
        ///A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            var phrases1 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("found") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") })
            };
            var sentences = new Sentence[3];
            sentences[0] = new Sentence(phrases1, SentenceEnding.Period);
            var phrases2 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("SNIFd") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("them") })
            };
            sentences[1] = new Sentence(phrases2, SentenceEnding.Period);
            var phrases3 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("Richard") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("did") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("awesome") })
            };
            sentences[2] = new Sentence(phrases3, SentenceEnding.Period);

            var target = new Paragraph(ParagraphKind.Default, sentences);

            var expected = string.Format("LASI.Core.Paragraph: {0} sentences\n\"LASI found TIMIS. LASI SNIFd them. Richard did awesome.\"", sentences.Length);
            string actual;
            actual = target.ToString();
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Text
        /// </summary>
        [Fact]
        public void TextTest()
        {
            var phrases1 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("found")}),
                new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS")})
            };
            var sentences = new Sentence[3];
            sentences[0] = new Sentence(phrases1, SentenceEnding.Period);
            var phrases2 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("SNIFd") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("them") })
            };
            sentences[1] = new Sentence(phrases2, SentenceEnding.Period);
            var phrases3 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("Richard") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("did") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("awesome") })
            };
            sentences[2] = new Sentence(phrases3, SentenceEnding.Period);

            var target = new Paragraph(ParagraphKind.Default, sentences);

            var expected = "LASI found TIMIS. LASI SNIFd them. Richard did awesome.";
            string actual;
            actual = target.Text;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for EstablishParent
        /// </summary>
        [Fact]
        public void EstablishParentTest()
        {
            var phrases1 = new Phrase[] {
                   new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                   new VerbPhrase(new Word[] { new PastTenseVerb("found")}),
                   new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS")})
               };
            var sentences = new Sentence[3];
            sentences[0] = new Sentence(phrases1, SentenceEnding.Period);
            var phrases2 = new Phrase[] {
                   new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                   new VerbPhrase(new Word[] { new PastTenseVerb("SNIFd") }),
                   new NounPhrase(new Word[] { new ProperPluralNoun("them") })
               };
            sentences[1] = new Sentence(phrases2, SentenceEnding.Period);
            var phrases3 = new Phrase[] {
                   new NounPhrase(new Word[] { new ProperSingularNoun("Richard") }),
                   new VerbPhrase(new Word[] { new PastTenseVerb("did") }),
                   new NounPhrase(new Word[] { new ProperPluralNoun("awesome") })
               };
            sentences[2] = new Sentence(phrases3, SentenceEnding.Period);

            Paragraph[] target = { new Paragraph(ParagraphKind.Default, sentences) };
            var parentDoc = new Document(target);
            target[0].EstablishTextualLinks(parentDoc);
            Check.That(target[0].Document).IsEqualTo(parentDoc);
        }

        /// <summary>
        ///A test for Words
        /// </summary>
        [Fact]
        public void WordsTest()
        {
            IEnumerable<Sentence> sentences = new Sentence[] {
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
                            ) }, SentenceEnding.ExclamationPoint),
                        new Sentence(new Clause[]{
                            new Clause(
                                new NounPhrase(
                                    new PersonalPronoun("We")),
                                new VerbPhrase(new ModalAuxilary("must"), new BaseVerb("do")),
                            new NounPhrase(
                                new PersonalPronoun("this")),
                            new AdverbPhrase(new Adverb("quickly")))}, SentenceEnding.ExclamationPoint)
        };
            var paragraphKind = ParagraphKind.Default;
            var target = new Paragraph(paragraphKind, sentences);
            IEnumerable<Word> actual;
            actual = target.Words;
            Check.That(sentences.Words()).ContainsExactly(actual);
        }


        /// <summary>
        ///A test for Phrases
        /// </summary>
        [Fact]
        public void PhrasesTest()
        {
            IEnumerable<Sentence> sentences = new Sentence[] {
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
                            ) }, SentenceEnding.ExclamationPoint),
                        new Sentence(new Clause[]{
                            new Clause(
                                new NounPhrase(
                                    new PersonalPronoun("We")),
                                new VerbPhrase(new ModalAuxilary("must"), new BaseVerb("do")),
                            new NounPhrase(
                                new PersonalPronoun("this")),
                            new AdverbPhrase(new Adverb("quickly")))}, SentenceEnding.ExclamationPoint)
                };
            var paragraphKind = ParagraphKind.Default;
            var target = new Paragraph(paragraphKind, sentences);
            IEnumerable<Phrase> actual;
            actual = target.Phrases;
            Check.That(sentences.Phrases()).ContainsExactly(actual);
        }



        /// <summary>
        ///A test for GetPhrasesAfter
        /// </summary>
        [Fact]
        public void GetPhrasesAfterTest()
        {
            var startAfter = new NounPhrase(new Adjective("blue"), new CommonSingularNoun("team"));
            IEnumerable<Sentence> sentences = new Sentence[] {
                    new Sentence(new Clause[] {
                            new Clause(new NounPhrase(new PersonalPronoun("We")),
                                new VerbPhrase(new ModalAuxilary("must"), new BaseVerb("attack")),
                                startAfter)},
                        SentenceEnding.ExclamationPoint),
                    new Sentence(new Clause[]{new Clause( new Phrase[]{
                            new NounPhrase(new PersonalPronoun("We")),
                            new VerbPhrase(new ModalAuxilary("must"), new BaseVerb("do")),
                            new NounPhrase(new PersonalPronoun("this")),
                            new AdverbPhrase(new Adverb("quickly"))
                        })},
                        SentenceEnding.ExclamationPoint)
                };
            var paragraphKind = ParagraphKind.Default;
            var target = new Paragraph(paragraphKind, sentences);
            var expected = sentences.Phrases().SkipWhile(p => p != startAfter).Skip(1);
            IEnumerable<Phrase> actual;
            actual = target.GetPhrasesAfter(startAfter);

            Check.That(expected).ContainsExactly(actual);
        }

        /// <summary>
        ///A test for Paragraph Constructor
        /// </summary>
        [Fact]
        public void ParagraphConstructorTest()
        {
            IEnumerable<Sentence> sentences = new Sentence[] {
                    new Sentence(new Clause[] {
                        new Clause(
                            new NounPhrase(new PersonalPronoun("We")),
                            new VerbPhrase(new ModalAuxilary("must"), new BaseVerb("attack")),
                            new NounPhrase(new Adjective("blue"), new CommonSingularNoun("team"))
                           )}, SentenceEnding.ExclamationPoint),
                    new Sentence(new Clause[]{
                        new Clause(
                            new NounPhrase(new PersonalPronoun("We")),
                            new VerbPhrase(new ModalAuxilary("must"), new BaseVerb("do")),
                        new NounPhrase(new PersonalPronoun("this")),
                        new AdverbPhrase(new Adverb("quickly")))
                        }, SentenceEnding.ExclamationPoint)
                };
            var paragraphKind = ParagraphKind.Default;
            var target = new Paragraph(paragraphKind, sentences);
            Check.That(paragraphKind).IsEqualTo(target.ParagraphKind);
            Check.That(sentences).ContainsExactly(target.Sentences);
        }
    }
}
