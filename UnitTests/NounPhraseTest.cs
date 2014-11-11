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
    [TestClass]
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
        [TestMethod]
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
        [TestMethod]
        public void BindDescriberTest() {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
            IDescriptor adj = new AdjectivePhrase(new Word[] { new CommonSingularNoun("peace"), new PresentParticiple("loving") });
            target.BindDescriptor(adj);
            Assert.IsTrue(target.Descriptors.Contains(adj));
        }

        /// <summary>
        ///A test for BindPronoun
        ///</summary>
        [TestMethod]
        public void BindPronounTest() {
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            Pronoun pro = new PersonalPronoun("they");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referencers.Contains(pro) && pro.RefersTo.Any(e => e == target));
        }


        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest() {
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            Assert.AreEqual(target, target as object);
        }

        /// <summary>
        ///A test for Descriptors
        ///</summary>
        [TestMethod]
        public void DescribedByTest() {
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            Assert.IsTrue(target.Descriptors.Count() == 0);
            IDescriptor adj = new AdjectivePhrase(new CommonSingularNoun("peace"), new PresentParticiple("loving"));
            target.BindDescriptor(adj);
            Assert.IsTrue(target.Descriptors.Contains(adj));
            IDescriptor adj2 = new Adjective("proud");
            target.BindDescriptor(adj2);
            Assert.IsTrue(target.Descriptors.Contains(adj) && target.Descriptors.Contains(adj2));
        }

        /// <summary>
        ///A test for DirectObjectOf
        ///</summary>
        [TestMethod]
        public void DirectObjectOfTest() {
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IVerbal expected = new SimpleVerb("insult");
            IVerbal actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IndirectObjectOf
        ///</summary>
        [TestMethod]
        public void IndirectObjectOfTest() {
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IVerbal expected = new VerbPhrase(new SimpleVerb("gave"), new Adverb("willingly"));
            IVerbal actual;
            target.IndirectObjectOf = expected;
            actual = target.IndirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IndirectReferences
        ///</summary>
        [TestMethod]
        public void IndirectReferencesTest() {
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            Assert.IsFalse(target.Referencers.Any());
            Pronoun pro = new PersonalPronoun("they");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referencers.Contains(pro) && pro.RefersTo.Any(e => e == target));
        }

        /// <summary>
        ///A test for PossessesFor
        ///</summary>
        [TestMethod]
        public void PossesserTest() {
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IEntity expected = new NounPhrase(new ProperSingularNoun("North"), new ProperSingularNoun("America"));
            IPossesser actual;
            target.Possesser = expected;
            actual = target.Possesser;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for SubjectOf
        ///</summary>
        [TestMethod]
        public void SubjectOfTest() {
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IVerbal expected = new SimpleVerb("are");
            IVerbal actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for AddPossession
        ///</summary>
        [TestMethod]
        public void AddPossessionTest() {
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IEntity possession = new NounPhrase(new Adverb("relatively"), new Adjective("affluent"), new CommonPluralNoun("lifestyles"));
            target.AddPossession(possession);
            Assert.IsTrue(target.Possessions.Contains(possession) && possession.Possesser == target);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest() {
            NounPhrase target = new NounPhrase(new ProperSingularNoun("LASI"), new Conjunction("and"), new ProperSingularNoun("Timmy"));
            string expected = "NounPhrase \"LASI and Timmy\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for Referees
        ///</summary>
        [TestMethod]
        public void RefereesTest() {
            NounPhrase target = new NounPhrase(new Determiner("the"), new Adjective("large"), new CommonSingularNoun("elephants"));
            IEnumerable<IReferencer> expected = new IReferencer[] { new RelativePronoun("that"), new PersonalPronoun("it") };
            IEnumerable<IReferencer> actual;
            foreach (var r in expected) {
                target.BindReferencer(r);
            }
            actual = target.Referencers;
            Assert.IsTrue(expected.All(e => actual.Contains(e)));
        }


        /// <summary>
        ///A test for Possessed
        ///</summary>
        [TestMethod]
        public void PossessedTest() {
            NounPhrase target = new NounPhrase(new Adjective("large"), new CommonSingularNoun("elephants"));
            IEnumerable<IPossessable> actual;
            IEnumerable<IPossessable> expected = new[] { new CommonSingularNoun("trunks") };
            actual = target.Possessions;
            foreach (var ip in expected) { target.AddPossession(ip); }
            Assert.IsTrue(expected.All(e => actual.Contains(e)));
        }

        /// <summary>
        ///A test for OuterAttributive
        ///</summary>
        [TestMethod]
        public void OuterAttributiveTest() {
            NounPhrase target = new NounPhrase(new ProperSingularNoun("Catus"));
            NounPhrase expected = new NounPhrase(new ProperSingularNoun("Felis"));
            NounPhrase actual;
            target.OuterAttributive = expected;
            actual = target.OuterAttributive;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for InnerAttributive
        ///</summary>
        [TestMethod]
        public void InnerAttributiveTest() {
            NounPhrase target = new NounPhrase(new ProperSingularNoun("Felis"));
            NounPhrase expected = new NounPhrase(new ProperSingularNoun("Catus"));
            NounPhrase actual;
            target.InnerAttributive = expected;
            actual = target.InnerAttributive;
            Assert.AreEqual(expected, actual);
        }




        /// <summary>
        ///A test for Descriptors
        ///</summary>
        [TestMethod]
        public void DescriptorsTest() {
            NounPhrase target = new NounPhrase(new Determiner("the"), new CommonSingularNoun("elephants"));
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
        [TestMethod]
        public void BindReferencerTest() {
            NounPhrase target = new NounPhrase(new Determiner("the"), new Adjective("large"), new CommonSingularNoun("elephant"));
            IReferencer pro = new RelativePronoun("which");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referencers.All(r => r.RefersTo.Contains(target)));

        }

        /// <summary>
        ///A test for BindDescriptor
        ///</summary>
        [TestMethod]
        public void BindDescriptorTest() {
            NounPhrase target = new NounPhrase(new Determiner("the"), new Adjective("large"), new CommonSingularNoun("elephants"));
            IDescriptor descriptor = new Adjective("hungry");
            target.BindDescriptor(descriptor);
            Assert.IsTrue(target.Descriptors.Contains(descriptor));
            Assert.IsTrue(descriptor.Describes == target);

        }


    }
}
