using LASI;
using LASI.Core;
using LASI.Core.DocumentStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for SentenceTest and is intended
    ///to contain all SentenceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SentenceTest
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


        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, new SentenceEnding('.'));
            string expected = "LASI.Core.DocumentStructures.Sentence \"LASI found TIMIS.\"";
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod()]
        public void TextTest() {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, new SentenceEnding('.'));
            string expected = "LASI found TIMIS.";
            string actual = target.Text;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Words
        ///</summary>
        [TestMethod()]
        public void WordsTest() {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases,new SentenceEnding('.'));  
            IEnumerable<Word> actual;
            actual = target.Words;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
 

        /// <summary>
        ///A test for Phrases
        ///</summary>
        [TestMethod()]
        public void PhrasesTest() {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, new SentenceEnding('.'));
            IEnumerable<Phrase> actual;
            actual = target.Phrases;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsInverted
        ///</summary>
        [TestMethod()]
        public void IsInvertedTest() {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, new SentenceEnding('.'));
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.IsInverted = expected;
            actual = target.IsInverted;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Document
        ///</summary>
        [TestMethod()]
        public void DocumentTest() {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, new SentenceEnding('.'));
            Document actual;
            actual = target.Document;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        

        /// <summary>
        ///A test for GetPhrasesAfter
        ///</summary>
        [TestMethod()]
        public void GetPhrasesAfterTest() {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, new SentenceEnding('.'));
            Phrase phrase = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> actual;
            actual = target.GetPhrasesAfter(phrase);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EstablishParenthood
        ///</summary>
        [TestMethod()]
        public void EstablishParenthoodTest() {
            Phrase[] phrases = new Phrase[] { new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), new VerbPhrase(new Word[] { new PastTenseVerb("found") }), new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") }) };
            Sentence target = new Sentence(phrases, new SentenceEnding('.'));
            Paragraph parent = null; // TODO: Initialize to an appropriate value
            target.EstablishParenthood(parent);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Sentence Constructor
        ///</summary>
        [TestMethod()]
        public void SentenceConstructorTest() {
            IEnumerable<Clause> clauses = null; // TODO: Initialize to an appropriate value
            SentenceEnding sentencePunctuation = null; // TODO: Initialize to an appropriate value
            Sentence target = new Sentence(clauses, sentencePunctuation);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

       
    }
}
