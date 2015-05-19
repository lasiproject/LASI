using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Shared.Test.Assertions;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for SentenceTest and is intended
    ///to contain all SentenceTest Unit Tests
    /// </summary>
    [TestClass]
    public class SentenceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        /// </summary>
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


        /// <summary>
        ///A test for ToString
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            string expected = "LASI.Core.Sentence \"LASI found TIMIS.\"";
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Text
        /// </summary>
        [TestMethod]
        public void TextTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            string expected = "LASI found TIMIS.";
            string actual = target.Text;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Words
        /// </summary>
        [TestMethod]
        public void WordsTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            IEnumerable<Word> actual;
            actual = target.Words;
            EnumerableAssert.AreSequenceEqual(phrases.SelectMany(p => p.Words), actual);
        }


        /// <summary>
        ///A test for Phrases
        /// </summary>
        [TestMethod]
        public void PhrasesTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            IEnumerable<Phrase> actual;
            actual = target.Phrases;
            EnumerableAssert.AreSequenceEqual(phrases, actual);
        }

        /// <summary>
        ///A test for IsInverted
        /// </summary>
        [TestMethod]
        public void IsInvertedTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            bool expected = false;
            bool actual;
            target.IsInverted = expected;
            actual = target.IsInverted;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Document
        /// </summary>
        [TestMethod]
        public void DocumentTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            Document actual = new Document(new[] { new Paragraph(ParagraphKind.Default, new[] { target }) });

            Assert.AreEqual(actual, target.Document);
            foreach (var p in phrases)
            {
                Assert.AreEqual(actual, target.Document);
            }
        }



        /// <summary>
        ///A test for GetPhrasesAfter
        /// </summary>
        [TestMethod]
        public void GetPhrasesAfterTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            Phrase phrase = phrases[1];
            IEnumerable<Phrase> expected = new[] { phrases[2] };
            IEnumerable<Phrase> actual;
            actual = target.GetPhrasesAfter(phrase);
            EnumerableAssert.AreSequenceEqual(expected, actual);
        }

        /// <summary>
        ///A test for EstablishParenthood
        /// </summary>
        [TestMethod]
        public void EstablishParenthoodTest()
        {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, SentenceEnding.Period);
            Paragraph parent = new Paragraph(ParagraphKind.Default, new[] { target });
            target.EstablishTextualLinks(parent);
            Assert.AreEqual(parent, target.Paragraph);
            foreach (var p in phrases)
            {
                Assert.AreEqual(parent, p.Paragraph);
                Assert.AreEqual(target, p.Sentence);
            }
        }

        /// <summary>
        ///A test for Sentence Constructor
        /// </summary>
        [TestMethod]
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
            EnumerableAssert.AreSequenceEqual(clauses, target.Clauses);
            Assert.AreEqual(target.Ending, sentenceEnding);
            Assert.AreEqual(target.Text, string.Join(" ", clauses.Select(c => c.Text).ToArray()) + sentenceEnding.Text);
        }


    }
}
