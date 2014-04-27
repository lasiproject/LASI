using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LASI.UnitTests
{


    /// <summary>
    ///This is A test class for PronounTest and is intended
    ///to contain all PronounTest Unit Tests
    ///</summary>
    [TestClass]
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
        ///A test for PersonalPronoun Constructor
        ///</summary>
        [TestMethod]
        public void PronounConstructorTest() {
            string text = "him";
            PersonalPronoun target = new PersonalPronoun(text);
            Assert.IsTrue(target.Text == text, "Text property value correctly initialized via parameter");
            //Assert.IsTrue(from.BoundEntity == null,"Bound Entity property was initialized to NULL");
        }

        /// <summary>
        ///A test for BindPronoun
        ///</summary>
        [TestMethod]
        public void BindPronounTest() {
            string text = "him";
            PersonalPronoun target = new PersonalPronoun(text);
            Pronoun pro = new PersonalPronoun("them");
            target.BindReferencer(pro);
            Assert.IsTrue(target.Referencers.Contains(pro));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
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
        ///A test for BoundEntity
        ///</summary>
        [TestMethod]
        public void BoundEntityTest() {
            string text = "him";
            PersonalPronoun target = new PersonalPronoun(text);
            IEntity expected = new ProperSingularNoun("Aluan");
            IAggregateEntity actual;
            target.BindAsReferringTo(expected);
            actual = target.ReferesTo;
            Assert.IsTrue(actual.Contains(expected));
        }

        /// <summary>
        ///A test for DirectObjectOf
        ///</summary>
        [TestMethod]
        public void DirectObjectOfTest() {
            string text = "him";
            PersonalPronoun target = new PersonalPronoun(text);
            IVerbal expected = new Verb("frightened", VerbForm.Past);
            IVerbal actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected.Text, actual.Text);

        }

        /// <summary>
        ///A test for IndirectObjectOf
        ///</summary>
        [TestMethod]
        public void IndirectObjectOfTest() {
            string text = "him";
            PersonalPronoun target = new PersonalPronoun(text);
            IVerbal expected = new Verb("frightened", VerbForm.Past);
            IVerbal actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IndirectReferences
        ///</summary>
        [TestMethod]
        public void IndirectReferencesTest() {
            string text = "he";
            PersonalPronoun target = new PersonalPronoun(text);
            Pronoun referencer = new PersonalPronoun("himslef");
            target.BindReferencer(referencer);
            Assert.IsTrue(target.Referencers.Contains(referencer));
        }

        /// <summary>
        ///A test for SubjectOf
        ///</summary>
        [TestMethod]
        public void SubjectOfTest() {
            string text = "him";
            PersonalPronoun target = new PersonalPronoun(text);
            IVerbal expected = new Verb("frightened", VerbForm.Past);
            IVerbal actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for EntityKind
        ///</summary>
        [TestMethod]
        public void EntityKindTest() {
            string text = "they";
            PersonalPronoun target = new PersonalPronoun(text);
            EntityKind actual;
            actual = target.EntityKind;
            Assert.AreEqual(EntityKind.Undefined, actual);
            target.BindAsReferringTo(new NounPhrase(new[] { new CommonPluralNoun("apples") }));
            Assert.AreEqual(EntityKind.ThingMultiple, target.EntityKind);
        }

        /// <summary>
        ///A test for PersonalPronoun Constructor
        ///</summary>
        [TestMethod]
        public void PersonalPronounConstructorTest() {
            string text = "her";
            PersonalPronoun target = new PersonalPronoun(text);
            Assert.IsTrue(target.Text == text);
            Assert.IsTrue(target.IsFemale());
            Assert.IsTrue(target.IsThirdPerson());
            Assert.IsTrue(target.EntityKind == EntityKind.PersonFemale);
        }
    }
}
