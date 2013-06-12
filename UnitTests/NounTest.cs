using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Algorithm;
 

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is A test class for NounTest and is intended
    ///to contain all NounTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NounTest
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


        internal virtual Noun CreateNoun() {
            Noun target = new GenericSingularNoun("dog");
            return target;
        }

        /// <summary>
        ///A test for AddPossession
        ///</summary>
        [TestMethod()]
        public void AddPossessionTest() {
            Noun target = CreateNoun();
            IEntity possession = new NounPhrase(new[] { new GenericSingularNoun("chew"), new GenericSingularNoun("toy") });
            target.AddPossession(possession);
            Assert.IsTrue(target.Possessed.Contains(possession) && possession.Possesser == target);
        }

        /// <summary>
        ///A test for BindDescriptor
        ///</summary>
        [TestMethod()]
        public void BindDescriberTest() {
            Noun target = CreateNoun();
            IDescriptor adjective = new Adjective("rambunctious");
            target.BindDescriptor(adjective);
            Assert.IsTrue(target.DescribedBy.Contains(adjective) && adjective.Describes == target);
        }

        /// <summary>
        ///A test for BindPronoun
        ///</summary>
        [TestMethod()]
        public void BindPronounTest() {
            Noun target = CreateNoun();
            Pronoun pro = new PersonalPronoun("it");
            target.BindPronoun(pro);
            Assert.IsTrue(target.BoundPronouns.Contains(pro) && pro.BoundEntity == target);
        }

        ///// <summary>
        /////A test for Equals
        /////</summary>
        //[TestMethod()]
        //public void EqualsTest() {
        //    Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
        //    IEntity second = null; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = target.Equals(second);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for DescribedBy
        ///</summary>
        [TestMethod()]
        public void DescribedByTest() {
            Noun target = CreateNoun();

            Assert.IsTrue(target.DescribedBy != null && target.DescribedBy.Count() == 0);
        }

        /// <summary>
        ///A test for DirectObjectOf
        ///</summary>
        [TestMethod()]
        public void DirectObjectOfTest() {
            Noun target = CreateNoun();
            IVerbal expected = new PastTenseVerb("walked");
            IVerbal actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EntityType
        ///</summary>
        [TestMethod()]
        public void EntityTypeTest() {
            Noun target = CreateNoun();
            EntityKind expected = EntityKind.Thing;
            EntityKind actual;
            target.EntityKind = expected;
            actual = target.EntityKind;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IndirectObjectOf
        ///</summary>
        [TestMethod()]
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
        [TestMethod()]
        public void IndirectReferencesTest() {
            Noun target = CreateNoun();
            IEnumerable<IPronoun> actual;
            actual = target.BoundPronouns;
            Assert.IsTrue(actual != null && actual.Count() == 0);
        }

        /// <summary>
        ///A test for Possessed
        ///</summary>
        [TestMethod()]
        public void PossessedTest() {
            Noun target = CreateNoun();
            IEnumerable<IEntity> actual;
            actual = target.Possessed;
            Assert.IsTrue(actual != null && actual.Count() == 0);
        }

        /// <summary>
        ///A test for PossessesFor
        ///</summary>
        [TestMethod()]
        public void PossesserTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IEntity expected = new NounPhrase(new Word[] { new Adjective("Red"), new GenericSingularNoun("Team") });
            IEntity actual;
            target.Possesser = expected;
            actual = target.Possesser;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SubjectOf
        ///</summary>
        [TestMethod()]
        public void SubjectOfTest() {
            Noun target = CreateNoun();
            IVerbal expected = new Verb("runs", VerbTense.SingularPresent);
            IVerbal actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Assert.AreEqual(expected, actual);
        }
    }
}
