using LASI;
using LASI.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.UnitTests
{
    /// <summary>
    ///This is A test class for NounPhraseTest and is intended
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
        ///A test for NounPhrase Constructorf
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
        ///A test for BindDescriptor
        ///</summary>
        [TestMethod()]
        public void BindDescriberTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            IDescriptor adj = new AdjectivePhrase(new Word[] { new CommonSingularNoun("peace"), new PresentParticipleGerund("loving") });
            target.BindDescriptor(adj);
            Assert.IsTrue(target.Descriptors.Contains(adj));
        }

        /// <summary>
        ///A test for BindPronoun
        ///</summary>
        [TestMethod()]
        public void BindPronounTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            Pronoun pro = new PersonalPronoun("they");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referees.Contains(pro) && pro.ReferredTo.Any(e => e == target));
        }


        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords); // TODO: Initialize to an appropriate value
            Assert.AreEqual(target, target as object);
        }

        /// <summary>
        ///A test for Descriptors
        ///</summary>
        [TestMethod()]
        public void DescribedByTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            Assert.IsTrue(target.Descriptors.Count() == 0);
            IDescriptor adj = new AdjectivePhrase(new Word[] { new CommonSingularNoun("peace"), new PresentParticipleGerund("loving") });
            target.BindDescriptor(adj);
            Assert.IsTrue(target.Descriptors.Contains(adj));
            IDescriptor adj2 = new Adjective("proud");
            target.BindDescriptor(adj2);
            Assert.IsTrue(target.Descriptors.Contains(adj) && target.Descriptors.Contains(adj2));
        }

        /// <summary>
        ///A test for DirectObjectOf
        ///</summary>
        [TestMethod()]
        public void DirectObjectOfTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            IVerbal expected = new Verb("insult", VerbForm.Base);
            IVerbal actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IndirectObjectOf
        ///</summary>
        [TestMethod()]
        public void IndirectObjectOfTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            IVerbal expected = new VerbPhrase(new Word[] { new Verb("gave", VerbForm.Base), new Adverb("willingly") });
            IVerbal actual;
            target.IndirectObjectOf = expected;
            actual = target.IndirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IndirectReferences
        ///</summary>
        [TestMethod()]
        public void IndirectReferencesTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            Assert.IsFalse(target.Referees.Any());
            Pronoun pro = new PersonalPronoun("they");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referees.Contains(pro) && pro.ReferredTo.Any(e => e == target));
        }

        /// <summary>
        ///A test for PossessesFor
        ///</summary>
        [TestMethod()]
        public void PossesserTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            IEntity expected = new NounPhrase(new[] { new ProperSingularNoun("North"), new ProperSingularNoun("America") });
            IPossesser actual;
            target.Possesser = expected;
            actual = target.Possesser;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for SubjectOf
        ///</summary>
        [TestMethod()]
        public void SubjectOfTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            IVerbal expected = new Verb("are", VerbForm.Base);
            IVerbal actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for AddPossession
        ///</summary>
        [TestMethod()]
        public void AddPossessionTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            IEntity possession = new NounPhrase(new Word[] { new Adverb("relatively"), new Adjective("affluent"), new CommonPluralNoun("lifestyles") });
            target.AddPossession(possession);
            Assert.IsTrue(target.Possessed.Contains(possession) && possession.Possesser == target);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperSingularNoun("LASI"), new Conjunction("and"), new ProperSingularNoun("Timmy") };
            NounPhrase target = new NounPhrase(composedWords);
            string expected = "NounPhrase \"LASI and Timmy\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for Referees
        ///</summary>
        [TestMethod()]
        public void RefereesTest() {
            IEnumerable<Word> composed = new Word[] { new Determiner("the"), new Adjective("large"), new CommonSingularNoun("elephants") };
            NounPhrase target = new NounPhrase(composed);
            IEnumerable<IReferencer> expected = new IReferencer[] { new RelativePronoun("that"), new PersonalPronoun("it") };
            IEnumerable<IReferencer> actual;
            foreach (var r in expected) {
                target.BindReferencer(r);
            }
            actual = target.Referees;
            Assert.IsTrue(expected.All(e => actual.Contains(e)));
        }


        /// <summary>
        ///A test for Possessed
        ///</summary>
        [TestMethod()]
        public void PossessedTest() {
            IEnumerable<Word> composed = new Word[] { new Adjective("large"), new CommonSingularNoun("elephants") };
            NounPhrase target = new NounPhrase(composed);
            IEnumerable<IPossessable> actual;
            IEnumerable<IPossessable> expected = new[] { new CommonSingularNoun("trunks") };
            actual = target.Possessed;
            foreach (var ip in expected) { target.AddPossession(ip); }
            Assert.IsTrue(expected.All(e => actual.Contains(e)));
        }

        /// <summary>
        ///A test for OuterAttributive
        ///</summary>
        [TestMethod()]
        public void OuterAttributiveTest() {
            NounPhrase target = new NounPhrase(new Word[] { new ProperSingularNoun("Catus") });
            NounPhrase expected = new NounPhrase(new Word[] { new ProperSingularNoun("Felis") });
            NounPhrase actual;
            target.OuterAttributive = expected;
            actual = target.OuterAttributive;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for InnerAttributive
        ///</summary>
        [TestMethod()]
        public void InnerAttributiveTest() {
            NounPhrase target = new NounPhrase(new Word[] { new ProperSingularNoun("Felis") });
            NounPhrase expected = new NounPhrase(new Word[] { new ProperSingularNoun("Catus") });
            NounPhrase actual;
            target.InnerAttributive = expected;
            actual = target.InnerAttributive;
            Assert.AreEqual(expected, actual);
        }




        /// <summary>
        ///A test for Descriptors
        ///</summary>
        [TestMethod()]
        public void DescriptorsTest() {
            IEnumerable<Word> composed = new Word[] { new Determiner("the"), new CommonSingularNoun("elephants") };
            NounPhrase target = new NounPhrase(composed);
            IEnumerable<IDescriptor> actual;
            actual = target.Descriptors;
            Assert.IsTrue(target.Descriptors.None());
            IDescriptor descriptor = new Adjective("large");
            target.BindDescriptor(descriptor);
            Assert.IsTrue(target.Descriptors.Contains(descriptor));
        }



        /// <summary>
        ///A test for BindReferencer
        ///</summary>
        [TestMethod()]
        public void BindReferencerTest() {
            IEnumerable<Word> composed = new Word[] { new Determiner("the"), new Adjective("large"), new CommonSingularNoun("elephant") };
            NounPhrase target = new NounPhrase(composed);
            IReferencer pro = new RelativePronoun("which");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referees.All(r => r.ReferredTo.Contains(target)));

        }

        /// <summary>
        ///A test for BindDescriptor
        ///</summary>
        [TestMethod()]
        public void BindDescriptorTest() {
            IEnumerable<Word> composed = new Word[] { new Determiner("the"), new Adjective("large"), new CommonSingularNoun("elephants") };
            NounPhrase target = new NounPhrase(composed);
            IDescriptor descriptor = new Adjective("hungry");
            target.BindDescriptor(descriptor);
            Assert.IsTrue(target.Descriptors.Contains(descriptor));
            Assert.IsTrue(descriptor.Describes == target);

        }


    }
}
