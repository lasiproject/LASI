using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for PronounTest and is intended
    ///to contain all PronounTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PronounTest
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
        ///A test for Pronoun Constructor
        ///</summary>
        [TestMethod()]
        public void PronounConstructorTest() {
            string text = "him";
            Pronoun target = new Pronoun(text);
            Assert.IsTrue(target.Text == text, "Text property value correctly initialized via parameter");
            //Assert.IsTrue(target.BoundEntity == null,"Bound Entity property was initialized to NULL");
        }

        /// <summary>
        ///A test for BindPronoun
        ///</summary>
        [TestMethod()]
        public void BindPronounTest() {
            string text = "him";
            Pronoun target = new Pronoun(text);
            IEntityReferencer pro = new PronounPhrase(new Word[] { new Pronoun("those"), new Preposition("of"), new Pronoun("them") }); // TODO: Initialize to an appropriate value
            target.BindPronoun(pro);
            Assert.IsTrue(target.IndirectReferences.Contains(pro));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Pronoun target = new Pronoun(text); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod()]
        public void op_EqualityTest() {
            Pronoun A = null; // TODO: Initialize to an appropriate value
            Pronoun B = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (A == B);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod()]
        public void op_InequalityTest() {
            Pronoun A = null; // TODO: Initialize to an appropriate value
            Pronoun B = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (A != B);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BoundEntity
        ///</summary>
        [TestMethod()]
        public void BoundEntityTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Pronoun target = new Pronoun(text); // TODO: Initialize to an appropriate value
            IEntity expected = null; // TODO: Initialize to an appropriate value
            IEntity actual;
            target.BoundEntity = expected;
            actual = target.BoundEntity;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DirectObjectOf
        ///</summary>
        [TestMethod()]
        public void DirectObjectOfTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Pronoun target = new Pronoun(text); // TODO: Initialize to an appropriate value
            ITransitiveAction expected = null; // TODO: Initialize to an appropriate value
            ITransitiveAction actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IndirectObjectOf
        ///</summary>
        [TestMethod()]
        public void IndirectObjectOfTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Pronoun target = new Pronoun(text); // TODO: Initialize to an appropriate value
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
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Pronoun target = new Pronoun(text); // TODO: Initialize to an appropriate value
            ICollection<IEntityReferencer> actual;
            actual = target.IndirectReferences;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SubjectOf
        ///</summary>
        [TestMethod()]
        public void SubjectOfTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Pronoun target = new Pronoun(text); // TODO: Initialize to an appropriate value
            IAction expected = null; // TODO: Initialize to an appropriate value
            IAction actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
