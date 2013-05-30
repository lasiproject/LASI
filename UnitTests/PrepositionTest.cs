using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LASI.Algorithm.FundamentalSyntacticInterfaces;

namespace AlgorithmAssemblyUnitTestProject
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
            Assert.IsTrue(target.Text == "into" && target.OnLeftSide == null && target.OnRightSide == null && target.PrepositionalObject == null);
        }

        /// <summary>
        ///A test for BindObjectOfPreposition
        ///</summary>
        [TestMethod()]
        public void BindObjectOfPrepositionTest() {
            string text = "into";
            Preposition target = new Preposition(text);
            ILexical prepositionalObject = new NounPhrase(new Word[] { new Determiner("the"), new GenericSingularNoun("drawer") });
            target.BindObjectOfPreposition(prepositionalObject);
            Assert.IsTrue(target.PrepositionalObject == prepositionalObject);
        }


        /// <summary>
        ///A test for OnLeftSide
        ///</summary>
        [TestMethod()]
        public void OnLeftSideTest() {
            string text = "into";
            Preposition target = new Preposition(text); // TODO: Initialize to an appropriate value
            IPrepositionLinkable expected = new PastTenseVerb("gazed");
            IPrepositionLinkable actual;
            target.OnLeftSide = expected;
            actual = target.OnLeftSide;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for OnRightSide
        ///</summary>
        [TestMethod()]
        public void OnRightSideTest() {
            string text = "inside";
            Preposition target = new Preposition(text);
            IPrepositionLinkable expected = new NounPhrase(new Word[] { new PossessivePronoun("your"), new GenericSingularNoun("soul") });
            IPrepositionLinkable actual;
            target.OnRightSide = expected;
            actual = target.OnRightSide;
            Assert.AreEqual(expected, actual);

        }
        /// <summary>
        ///A test for PrepositionalRole
        ///</summary>
        [TestMethod()]
        public void PrepositionalRoleTest() {
            string text = "inside";
            Particle target = new Particle(text);
            PrepositionalRole expected = PrepositionalRole.Undetermined;
            PrepositionalRole actual;
            actual = target.PrepositionalRole;
            Assert.AreEqual(expected, actual);
        }
    }
}
