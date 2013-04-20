using LASI.Algorithm;
using LASI.Algorithm.FundamentalSyntacticInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmAssemblyUnitTestProject
{
    /// <summary>
    ///This is a test class for NounPhraseTest and is intended
    ///to contain all NounPhraseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NounPhraseTest
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


        /// <summary>
        ///a test for NounPhrase Constructorf
        ///</summary>
        [TestMethod()]
        public void NounPhraseConstructorTest() {
            IEnumerable<Word> composedWords = new Word[] {
                new ProperPluralNoun("Americans"),
                new Conjunction("and"), 
                new ProperPluralNoun("Canadians") 
            };
            NounPhrase target = new NounPhrase(composedWords);
            Assert.AreEqual(target.Words, composedWords);
        }


        /// <summary>
        ///a test for BindDescriber
        ///</summary>
        [TestMethod()]
        public void BindDescriberTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            IDescriber adj = new AdjectivePhrase(new Word[] { new GenericSingularNoun("peace"), new PresentParticipleGerund("loving") });
            target.BindDescriber(adj);
            Assert.IsTrue(target.DescribedBy.Contains(adj));
        }

        /// <summary>
        ///a test for BindPronoun
        ///</summary>
        [TestMethod()]
        public void BindPronounTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            Pronoun pro = new PersonalPronoun("they");
            target.BindPronoun(pro);
            Assert.IsTrue(target.BoundPronouns.Contains(pro) && pro.BoundEntity == target);
        }


        /// <summary>
        ///a test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords); // TODO: Initialize to an appropriate value
            Assert.AreEqual(target, target as object);
        }

        /// <summary>
        ///a test for DescribedBy
        ///</summary>
        [TestMethod()]
        public void DescribedByTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            Assert.IsTrue(target.DescribedBy.Count() == 0);
            IDescriber adj = new AdjectivePhrase(new Word[] { new GenericSingularNoun("peace"), new PresentParticipleGerund("loving") });
            target.BindDescriber(adj);
            Assert.IsTrue(target.DescribedBy.Contains(adj));
            IDescriber adj2 = new Adjective("proud");
            target.BindDescriber(adj2);
            Assert.IsTrue(target.DescribedBy.Contains(adj) && target.DescribedBy.Contains(adj2));
        }

        /// <summary>
        ///a test for DirectObjectOf
        ///</summary>
        [TestMethod()]
        public void DirectObjectOfTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            ITransitiveVerbial expected = new Verb("insult", VerbTense.Base);
            ITransitiveVerbial actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///a test for IndirectObjectOf
        ///</summary>
        [TestMethod()]
        public void IndirectObjectOfTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            ITransitiveVerbial expected = new VerbPhrase(new Word[] { new Verb("gave", VerbTense.Base), new Adverb("willingly") });
            ITransitiveVerbial actual;
            target.IndirectObjectOf = expected;
            actual = target.IndirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///a test for IndirectReferences
        ///</summary>
        [TestMethod()]
        public void IndirectReferencesTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            Assert.IsTrue(target.BoundPronouns.Count() == 0);
            Pronoun pro = new PersonalPronoun("they");
            target.BindPronoun(pro);
            Assert.IsTrue(target.BoundPronouns.Contains(pro) && pro.BoundEntity == target);
        }

        /// <summary>
        ///a test for PossessesFor
        ///</summary>
        [TestMethod()]
        public void PossesserTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            IEntity expected = new NounPhrase(new[] { new ProperSingularNoun("North"), new ProperSingularNoun("America") });
            IEntity actual;
            target.Possesser = expected;
            actual = target.Possesser;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///a test for SubjectOf
        ///</summary>
        [TestMethod()]
        public void SubjectOfTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            ITransitiveVerbial expected = new Verb("are", VerbTense.Base);
            ITransitiveVerbial actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///a test for AddPossession
        ///</summary>
        [TestMethod()]
        public void AddPossessionTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords); // TODO: Initialize to an appropriate value
            IEntity possession = new NounPhrase(new Word[] { new Adverb("relatively"), new Adjective("affluent"), new GenericPluralNoun("lifestyles") });
            target.AddPossession(possession);
            Assert.IsTrue(target.Possessed.Contains(possession) && possession.Possesser == target);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new ProperSingularNoun("LASI"), new Conjunction("and"), new ProperSingularNoun("Timmy") };
            NounPhrase target = new NounPhrase(composedWords);
            string expected = "NounPhrase \"LASI and Timmy\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);//TODO: Implement Verbosity test also!
        }
    }
}
