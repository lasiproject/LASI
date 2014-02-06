using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for RelativePronounTest and is intended
    ///to contain all RelativePronounTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RelativePronounTest
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
        ///A test for Subordinates
        ///</summary>
        [TestMethod()]
        public void SubordinatesTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            ILexical expected = null; // TODO: Initialize to an appropriate value
            ILexical actual;
            target.Subordinates = expected;
            actual = target.Subordinates;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SubjectOf
        ///</summary>
        [TestMethod()]
        public void SubjectOfTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            IVerbal expected = null; // TODO: Initialize to an appropriate value
            IVerbal actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Referees
        ///</summary>
        [TestMethod()]
        public void RefereesTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            IEnumerable<IReferencer> actual;
            actual = target.Referees;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Possesser
        ///</summary>
        [TestMethod()]
        public void PossesserTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            IPossesser expected = null; // TODO: Initialize to an appropriate value
            IPossesser actual;
            target.Possesser = expected;
            actual = target.Possesser;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Possessed
        ///</summary>
        [TestMethod()]
        public void PossessedTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            IEnumerable<IPossessable> actual;
            actual = target.Possessed;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IndirectObjectOf
        ///</summary>
        [TestMethod()]
        public void IndirectObjectOfTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            IVerbal expected = null; // TODO: Initialize to an appropriate value
            IVerbal actual;
            target.IndirectObjectOf = expected;
            actual = target.IndirectObjectOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DirectObjectOf
        ///</summary>
        [TestMethod()]
        public void DirectObjectOfTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            IVerbal expected = null; // TODO: Initialize to an appropriate value
            IVerbal actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Descriptors
        ///</summary>
        [TestMethod()]
        public void DescriptorsTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            IEnumerable<IDescriptor> actual;
            actual = target.Descriptors;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BindReferencer
        ///</summary>
        [TestMethod()]
        public void BindReferencerTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            IReferencer pro = null; // TODO: Initialize to an appropriate value
            target.BindReferencer(pro);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BindDescriptor
        ///</summary>
        [TestMethod()]
        public void BindDescriptorTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            IDescriptor descriptor = null; // TODO: Initialize to an appropriate value
            target.BindDescriptor(descriptor);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BindAsReference
        ///</summary>
        [TestMethod()]
        public void BindAsReferenceTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            IEntity target1 = null; // TODO: Initialize to an appropriate value
            target.BindAsReference(target1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddPossession
        ///</summary>
        [TestMethod()]
        public void AddPossessionTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text); // TODO: Initialize to an appropriate value
            IPossessable possession = null; // TODO: Initialize to an appropriate value
            target.AddPossession(possession);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RelativePronoun Constructor
        ///</summary>
        [TestMethod()]
        public void RelativePronounConstructorTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            RelativePronoun target = new RelativePronoun(text);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
