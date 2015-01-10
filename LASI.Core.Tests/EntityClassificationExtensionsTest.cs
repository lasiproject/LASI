using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core.Heuristics;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for EntityClassificationExtensionsTest and is intended
    ///to contain all EntityClassificationExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EntityClassificationExtensionsTest
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
        ///A test for IsThirdPerson
        ///</summary>
        [TestMethod()]
        public void IsThirdPersonTest() {
            Pronoun pronoun = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsThirdPerson(pronoun);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsThirdPerson
        ///</summary>
        [TestMethod()]
        public void IsThirdPersonTest1() {
            PronounKind kind = new PronounKind(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsThirdPerson(kind);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSecondPerson
        ///</summary>
        [TestMethod()]
        public void IsSecondPersonTest() {
            PronounKind kind = new PronounKind(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsSecondPerson(kind);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSecondPerson
        ///</summary>
        [TestMethod()]
        public void IsSecondPersonTest1() {
            Pronoun pronoun = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsSecondPerson(pronoun);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsReflexive
        ///</summary>
        [TestMethod()]
        public void IsReflexiveTest() {
            Pronoun pronoun = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsReflexive(pronoun);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsReflexive
        ///</summary>
        [TestMethod()]
        public void IsReflexiveTest1() {
            PronounKind kind = new PronounKind(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsReflexive(kind);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsPlural
        ///</summary>
        [TestMethod()]
        public void IsPluralTest() {
            PronounKind kind = new PronounKind(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsPlural(kind);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsPlural
        ///</summary>
        [TestMethod()]
        public void IsPluralTest1() {
            Pronoun pronoun = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsPlural(pronoun);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsNeutralOrUndefined
        ///</summary>
        [TestMethod()]
        public void IsNeutralOrUndefinedTest() {
            Gender gender = new Gender(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsNeutralOrUndefined(gender);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsNeutral
        ///</summary>
        [TestMethod()]
        public void IsNeutralTest() {
            Pronoun pronoun = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsNeutral(pronoun);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsNeutral
        ///</summary>
        [TestMethod()]
        public void IsNeutralTest1() {
            PronounKind kind = new PronounKind(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsNeutral(kind);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsNeutral
        ///</summary>
        [TestMethod()]
        public void IsNeutralTest2() {
            Gender gender = new Gender(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsNeutral(gender);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsMaleOrNeutral
        ///</summary>
        [TestMethod()]
        public void IsMaleOrNeutralTest() {
            Gender gender = new Gender(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsMaleOrNeutral(gender);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsMaleOrFemale
        ///</summary>
        [TestMethod()]
        public void IsMaleOrFemaleTest() {
            Gender gender = new Gender(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsMaleOrFemale(gender);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsMale
        ///</summary>
        [TestMethod()]
        public void IsMaleTest() {
            PronounKind kind = new PronounKind(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsMale(kind);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsMale
        ///</summary>
        [TestMethod()]
        public void IsMaleTest1() {
            Gender gender = new Gender(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsMale(gender);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsMale
        ///</summary>
        [TestMethod()]
        public void IsMaleTest2() {
            Pronoun pronoun = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsMale(pronoun);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsGenderEquivalentTo
        ///</summary>
        [TestMethod()]
        public void IsGenderEquivalentToTest() {
            Pronoun first = null; // TODO: Initialize to an appropriate value
            ProperNoun second = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsGenderEquivalentTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsGenderEquivalentTo
        ///</summary>
        [TestMethod()]
        public void IsGenderEquivalentToTest1() {
            ProperNoun first = null; // TODO: Initialize to an appropriate value
            Pronoun second = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsGenderEquivalentTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsGenderEquivalentTo
        ///</summary>
        [TestMethod()]
        public void IsGenderEquivalentToTest2() {
            IEntity first = null; // TODO: Initialize to an appropriate value
            IEntity second = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsGenderEquivalentTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsGenderEquivalentTo
        ///</summary>
        [TestMethod()]
        public void IsGenderEquivalentToTest3() {
            Pronoun first = null; // TODO: Initialize to an appropriate value
            Pronoun second = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsGenderEquivalentTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsGenderAmbiguous
        ///</summary>
        [TestMethod()]
        public void IsGenderAmbiguousTest() {
            Pronoun pronoun = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsGenderAmbiguous(pronoun);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsGenderAmbiguous
        ///</summary>
        [TestMethod()]
        public void IsGenderAmbiguousTest1() {
            PronounKind kind = new PronounKind(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsGenderAmbiguous(kind);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsFirstPerson
        ///</summary>
        [TestMethod()]
        public void IsFirstPersonTest() {
            Pronoun pronoun = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsFirstPerson(pronoun);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsFirstPerson
        ///</summary>
        [TestMethod()]
        public void IsFirstPersonTest1() {
            PronounKind kind = new PronounKind(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsFirstPerson(kind);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsFemaleOrNeutral
        ///</summary>
        [TestMethod()]
        public void IsFemaleOrNeutralTest() {
            Gender gender = new Gender(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsFemaleOrNeutral(gender);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsFemale
        ///</summary>
        [TestMethod()]
        public void IsFemaleTest() {
            Gender gender = new Gender(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsFemale(gender);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsFemale
        ///</summary>
        [TestMethod()]
        public void IsFemaleTest1() {
            PronounKind kind = new PronounKind(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsFemale(kind);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsFemale
        ///</summary>
        [TestMethod()]
        public void IsFemaleTest2() {
            Pronoun pronoun = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsFemale(pronoun);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsDefined
        ///</summary>
        [TestMethod()]
        public void IsDefinedTest() {
            Gender gender = new Gender(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = EntityClassificationExtensions.IsDefined(gender);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
