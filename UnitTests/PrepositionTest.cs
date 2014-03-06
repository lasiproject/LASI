using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;


namespace LASI.UnitTests
{


    /// <summary>
    ///This is A test class for PrepositionTest and is intended
    ///to contain all PrepositionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PrepositionTest
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
        ///A test for Preposition Constructor
        ///</summary>
        [TestMethod()]
        public void PrepositionConstructorTest() {
            string text = "into";
            Preposition target = new Preposition(text);
            Assert.IsTrue(target.Text == "into" && target.ToTheLeftOf == null && target.ToTheRightOf == null && target.BoundObject == null);
        }

        /// <summary>
        ///A test for BindObjectOfPreposition
        ///</summary>
        [TestMethod()]
        public void BindObjectOfPrepositionTest() {
            string text = "into";
            Preposition target = new Preposition(text);
            ILexical prepositionalObject = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("drawer") });
            target.BindObject(prepositionalObject);
            Assert.IsTrue(target.BoundObject == prepositionalObject);
        }


        /// <summary>
        ///A test for OnLeftSide
        ///</summary>
        [TestMethod()]
        public void OnLeftSideTest() {
            string text = "into";
            Preposition target = new Preposition(text);
            ILexical expected = new PastTenseVerb("gazed");
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for OnRightSide
        ///</summary>
        [TestMethod()]
        public void OnRightSideTest() {
            string text = "into";
            Preposition target = new Preposition(text);
            ILexical expected = new NounPhrase(new Word[] {
                new PossessivePronoun("your"), new CommonSingularNoun("soul") });
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Assert.AreEqual(expected, actual);

        }


        /// <summary>
        ///A test for ToTheRightOf
        ///</summary>
        [TestMethod()]
        public void ToTheRightOfTest() {
            string text = "into";
            Preposition target = new Preposition(text);
            ILexical expected = new PastTenseVerb("gazed");
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToTheLeftOf
        ///</summary>
        [TestMethod()]
        public void ToTheLeftOfTest() {
            string text = "inside";
            Preposition target = new Preposition(text);
            ILexical expected = new NounPhrase(new Word[] {
                new PossessivePronoun("your"), new CommonSingularNoun("soul") });
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Subordinates
        ///</summary>
        [TestMethod()]
        public void SubordinatesTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Preposition target = new Preposition(text); // TODO: Initialize to an appropriate value
            ILexical expected = null; // TODO: Initialize to an appropriate value
            ILexical actual;
            target.Subordinates = expected;
            actual = target.Subordinates;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Role
        ///</summary>
        [TestMethod()]
        public void RoleTest() {
            string text = "over";
            Preposition target = new Preposition(text);
            PrepositionRole expected = PrepositionRole.SpatialSpecifier;
            PrepositionRole actual;
            target.Role = expected;
            actual = target.Role;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            string text = "with";
            Preposition target = new Preposition(text);
            string expected = "Preposition \"with\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for BindObject
        ///</summary>
        [TestMethod()]
        public void BindObjectTest() {
            string text = "with";
            Preposition target = new Preposition(text);
            ILexical prepositionalObject = new PersonalPronoun("them");
            target.BindObject(prepositionalObject);
            Assert.AreEqual(prepositionalObject, target.BoundObject);
        }


    }
}
