using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for PronounTest and is intended
    ///to contain all PronounTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PersonalPronounTest
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
        ///a test for PersonalPronoun Constructor
        ///</summary>
        [TestMethod()]
        public void PronounConstructorTest() {
            string text = "him";
            PersonalPronoun target = new PersonalPronoun(text);
            Assert.IsTrue(target.Text == text, "Text property value correctly initialized via parameter");
            //Assert.IsTrue(target.BoundEntity == null,"Bound Entity property was initialized to NULL");
        }

        /// <summary>
        ///a test for BindPronoun
        ///</summary>
        [TestMethod()]
        public void BindPronounTest() {
            string text = "him";
            PersonalPronoun target = new PersonalPronoun(text);
            Pronoun pro = new PersonalPronoun("them");
            target.BindPronoun(pro);
            Assert.IsTrue(target.BoundPronouns.Contains(pro));
        }

        /// <summary>
        ///a test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest() {
            string text = "her";
            PersonalPronoun target = new PersonalPronoun(text);
            object obj = target as object;
            bool expected = true;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }



     

        /// <summary>
        ///a test for BoundEntity
        ///</summary>
        [TestMethod()]
        public void BoundEntityTest() {
            string text = "him";
            PersonalPronoun target = new PersonalPronoun(text);
            IEntity expected = new ProperSingularNoun("Aluan");
            IEntity actual;
            target.BoundEntity = expected;
            actual = target.BoundEntity;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///a test for DirectObjectOf
        ///</summary>
        [TestMethod()]
        public void DirectObjectOfTest() {
            string text = "him";
            PersonalPronoun target = new PersonalPronoun(text);
            ITransitiveVerbial expected = new Verb("frightened", VerbTense.Past);
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
            string text = "him";
            PersonalPronoun target = new PersonalPronoun(text);
            ITransitiveVerbial expected = new Verb("frightened", VerbTense.Past);
            ITransitiveVerbial actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///a test for IndirectReferences
        ///</summary>
        [TestMethod()]
        public void IndirectReferencesTest() {
            string text = "he";
            PersonalPronoun target = new PersonalPronoun(text);
            Pronoun referencer = new PersonalPronoun("himslef");
            target.BindPronoun(referencer);
            Assert.IsTrue(target.BoundPronouns.Contains(referencer));
        }

        /// <summary>
        ///a test for SubjectOf
        ///</summary>
        [TestMethod()]
        public void SubjectOfTest() {
            string text = "him";
            PersonalPronoun target = new PersonalPronoun(text);
            ITransitiveVerbial expected = new Verb("frightened", VerbTense.Past);
            ITransitiveVerbial actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);

        }
    }
}
