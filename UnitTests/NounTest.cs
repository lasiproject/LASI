using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for NounTest and is intended
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


        internal virtual Noun CreateNoun() {
            // TODO: Instantiate an appropriate concrete class.
            Noun target = null;
            return target;
        }

        /// <summary>
        ///A test for AddPossession
        ///</summary>
        [TestMethod()]
        public void AddPossessionTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IEntity possession = null; // TODO: Initialize to an appropriate value
            target.AddPossession(possession);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BindDescriber
        ///</summary>
        [TestMethod()]
        public void BindDescriberTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IDescriber adjective = null; // TODO: Initialize to an appropriate value
            target.BindDescriber(adjective);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BindPronoun
        ///</summary>
        [TestMethod()]
        public void BindPronounTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            Pronoun pro = null; // TODO: Initialize to an appropriate value
            target.BindPronoun(pro);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IEntity other = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DescribedBy
        ///</summary>
        [TestMethod()]
        public void DescribedByTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IEnumerable<IDescriber> actual;
            actual = target.DescribedBy;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DirectObjectOf
        ///</summary>
        [TestMethod()]
        public void DirectObjectOfTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            ITransitiveAction expected = null; // TODO: Initialize to an appropriate value
            ITransitiveAction actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EntityType
        ///</summary>
        [TestMethod()]
        public void EntityTypeTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            EntityKind expected = new EntityKind(); // TODO: Initialize to an appropriate value
            EntityKind actual;
            target.EntityKind = expected;
            actual = target.EntityKind;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IndirectObjectOf
        ///</summary>
        [TestMethod()]
        public void IndirectObjectOfTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            ITransitiveAction expected = null; // TODO: Initialize to an appropriate value
            ITransitiveAction actual;
            target.IndirectObjectOf = expected;
            actual = target.IndirectObjectOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IndirectReferences
        ///</summary>
        [TestMethod()]
        public void IndirectReferencesTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IEnumerable<Pronoun> actual;
            actual = target.BoundPronouns;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Possessed
        ///</summary>
        [TestMethod()]
        public void PossessedTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IEnumerable<IEntity> actual;
            actual = target.Possessed;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Possesser
        ///</summary>
        [TestMethod()]
        public void PossesserTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IEntity expected = null; // TODO: Initialize to an appropriate value
            IEntity actual;
            target.Possesser = expected;
            actual = target.Possesser;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SubjectOf
        ///</summary>
        [TestMethod()]
        public void SubjectOfTest() {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IAction expected = null; // TODO: Initialize to an appropriate value
            IAction actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
