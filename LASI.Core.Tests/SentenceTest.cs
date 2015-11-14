using System.Linq;
using System.Collections.Generic;
using Xunit;
using NFluent;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is A test class for SentenceTest and is intended
    ///to contain all SentenceTest Unit Tests
    /// </summary>
    public class SentenceTest
    {
        /// <summary>
        ///A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            string expected = "LASI.Core.Sentence \"LASI found TIMIS.\"";
            string actual = target.ToString();
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Text
        /// </summary>
        [Fact]
        public void TextTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            string expected = "LASI found TIMIS.";
            string actual = target.Text;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Words
        /// </summary>
        [Fact]
        public void WordsTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            IEnumerable<Word> actual;
            actual = target.Words;
            Check.That(actual).ContainsExactly(phrases.SelectMany(p => p.Words));
        }


        /// <summary>
        ///A test for Phrases
        /// </summary>
        [Fact]
        public void PhrasesTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            IEnumerable<Phrase> actual;
            actual = target.Phrases;
            Check.That(actual).ContainsExactly(phrases);
        }

        /// <summary>
        ///A test for IsInverted
        /// </summary>
        [Fact]
        public void IsInvertedTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            bool expected = false;
            bool actual;
            target.IsInverted = expected;
            actual = target.IsInverted;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Document
        /// </summary>
        [Fact]
        public void DocumentTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            Document actual = new Document(new[] { new Paragraph(ParagraphKind.Default, new[] { target }) });

            Check.That(actual).IsEqualTo(target.Document);
            foreach (var p in phrases)
            {
                Check.That(actual).IsEqualTo(target.Document);
            }
        }



        /// <summary>
        ///A test for GetPhrasesAfter
        /// </summary>
        [Fact]
        public void GetPhrasesAfterTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            Phrase phrase = phrases[1];
            IEnumerable<Phrase> expected = new[] { phrases[2] };
            IEnumerable<Phrase> actual;
            actual = target.GetPhrasesAfter(phrase);
            Check.That(actual).ContainsExactly(expected);
        }

        /// <summary>
        ///A test for EstablishParenthood
        /// </summary>
        [Fact]
        public void EstablishParenthoodTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            Paragraph parent = new Paragraph(ParagraphKind.Default, new[] { target });
            target.EstablishTextualLinks(parent);
            Check.That(parent).IsEqualTo(target.Paragraph);
            foreach (var p in phrases)
            {
                Check.That(parent).IsEqualTo(p.Paragraph);
                Check.That(target).IsEqualTo(p.Sentence);
            }
        }

        /// <summary>
        ///A test for Sentence Constructor
        /// </summary>
        [Fact]
        public void SentenceConstructorTest()
        {
            IEnumerable<Clause> clauses = new Clause[] {
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
                            )};
            SentenceEnding sentenceEnding = SentenceEnding.ExclamationPoint;
            Sentence target = new Sentence(clauses, sentenceEnding);
            Check.That(clauses).ContainsExactly(target.Clauses);
            Check.That(target.Ending).IsEqualTo(sentenceEnding);
            Check.That(target.Text).IsEqualTo(string.Join(" ", clauses.Select(c => c.Text)) + sentenceEnding.Text);
        }


    }
}
