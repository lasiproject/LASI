using LASI;
using LASI.Core;
using LASI.UnitTests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LASI.UnitTests
{
    /// <summary>
    ///This is A test class for NounTest and is intended
    ///to contain all NounTest Unit Tests
    ///</summary>
    [TestClass]
    public class NounTest
    {
        /// <summary>
        ///A test for AddPossession
        ///</summary>
        [TestMethod]
        public void AddPossessionTest() {
            Noun target = CreateNoun();
            IEntity possession = new NounPhrase(new[] { new CommonSingularNoun("chew"), new CommonSingularNoun("toy") });
            target.AddPossession(possession);
            Assert.IsTrue(target.Possessions.Contains(possession) && possession.Possesser == target);
        }

        /// <summary>
        ///A test for BindDescriptor
        ///</summary>
        [TestMethod]
        public void BindDescriberTest() {
            Noun target = CreateNoun();
            IDescriptor adjective = new Adjective("rambunctious");
            target.BindDescriptor(adjective);
            Assert.IsTrue(target.Descriptors.Contains(adjective) && adjective.Describes == target);
        }

        /// <summary>
        ///A test for BindPronoun
        ///</summary>
        [TestMethod]
        public void BindPronounTest() {
            Noun target = CreateNoun();
            Pronoun pro = new PersonalPronoun("it");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referencers.Contains(pro) && pro.RefersTo.Any(e => e == target));
        }

        /// <summary>
        ///A test for Descriptors
        ///</summary>
        [TestMethod]
        public void DescribedByTest() {
            Noun target = CreateNoun();

            Assert.IsTrue(target.Descriptors != null && target.Descriptors.Count() == 0);
        }

        /// <summary>
        ///A test for DirectObjectOf
        ///</summary>
        [TestMethod]
        public void DirectObjectOfTest() {
            Noun target = CreateNoun();
            IVerbal expected = new PastTenseVerb("walked");
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
            Noun target = CreateNoun();
            IVerbal expected = new PastTenseVerb("gave");
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
            Noun target = CreateNoun();
            IEnumerable<IReferencer> actual;
            actual = target.Referencers;
            Assert.IsTrue(actual != null && actual.Count() == 0);
        }

        /// <summary>
        ///A test for Possessed
        ///</summary>
        [TestMethod]
        public void PossessedTest() {
            Noun target = CreateNoun();
            IEnumerable<IPossessable> actual;
            actual = target.Possessions;
            Assert.IsTrue(actual != null && actual.Count() == 0);
        }

        /// <summary>
        ///A test for PossessesFor
        ///</summary>
        [TestMethod]
        public void PossesserTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IEntity expected = new NounPhrase(new Word[] { new Adjective("Red"), new CommonSingularNoun("Team") });
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
            Noun target = CreateNoun();
            IVerbal expected = new SingularPresentVerb("runs");
            IVerbal actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SuperTaxonomicNoun
        ///</summary>
        [TestMethod]
        public void SuperTaxonomicNounTest() {
            Noun target = CreateNoun();
            Noun expected = new ProperSingularNoun("Highland");
            Noun actual;
            target.PrecedingAdjunctNoun = expected;
            actual = target.PrecedingAdjunctNoun;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SubTaxonomicNoun
        ///</summary>
        [TestMethod]
        public void SubTaxonomicNounTest() {
            Noun target = CreateNoun();
            Noun expected = new CommonSingularNoun("food");
            Noun actual;
            target.FollowingAdjunctNoun = expected;
            actual = target.FollowingAdjunctNoun;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Referees
        ///</summary>
        [TestMethod]
        public void RefereesTest() {
            Noun target = CreateNoun();
            IEnumerable<IReferencer> actual;
            actual = target.Referencers;
            Assert.IsTrue(!actual.Any());
            Pronoun pro = new PersonalPronoun("it");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referencers.Contains(pro));
            Assert.IsTrue(target.Referencers.All(r => r.RefersTo == target || r.RefersTo.Contains(target)));
        }

        /// <summary>
        ///A test for QuantifiedBy
        ///</summary>
        [TestMethod]
        public void QuantifiedByTest() {
            Noun target = CreateNoun();
            IQuantifier expected = new Quantifier("3");
            IQuantifier actual;
            target.QuantifiedBy = expected;
            actual = target.QuantifiedBy;
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.QuantifiedBy.Quantifies == target);
        }

        /// <summary>A test for Descriptors</summary>
        [TestMethod]
        public void DescriptorsTest() {
            Noun target = CreateNoun();

            IEnumerable<IDescriptor> actual;
            actual = target.Descriptors;
            Assert.IsTrue(actual.None());
            IEnumerable<IDescriptor> descriptors = new[] { new Adjective("red"), new Adjective("slow") };

            foreach (var d in descriptors) { target.BindDescriptor(d); }
            actual = target.Descriptors;

            EnumerableAssert.AreSetEqual(actual, descriptors);
        }

        /// <summary>
        ///A test for BindReferencer
        ///</summary>
        [TestMethod]
        public void BindReferencerTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IReferencer pro = new PersonalPronoun("it");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referencers.Contains(pro));
            Assert.IsTrue(target.Referencers.All(r => r.RefersTo == target || r.RefersTo.Contains(target)));
        }

        /// <summary>
        ///A test for BindDeterminer
        ///</summary>
        [TestMethod]
        public void BindDeterminerTest() {
            Noun target = CreateNoun();
            Determiner determiner = new Determiner("the");
            target.BindDeterminer(determiner);
            Assert.AreEqual(target.Determiner, determiner);
            Assert.AreEqual(determiner.Determines, target);
        }

        /// <summary>
        ///A test for BindDescriptor
        ///</summary>
        [TestMethod]
        public void BindDescriptorTest() {
            Noun target = CreateNoun();
            IDescriptor descriptor = new Adjective("red");
            target.BindDescriptor(descriptor);
            Assert.IsTrue(target.Descriptors.Contains(descriptor));
            Assert.AreEqual(descriptor.Describes, target);
        }

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        private Noun CreateNoun() {
            Noun target = new CommonSingularNoun("dog");
            return target;
        }

        private TestContext testContextInstance;
    }
}