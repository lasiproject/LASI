using LASI.Core.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LASI.Core;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for LookupTest and is intended
    ///to contain all LookupTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LookupTest
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
        ///A test for ScrabbleDictionary
        ///</summary>
        [TestMethod()]
        public void ScrabbleDictionaryTest() {
            IEnumerable<string> actual;
            actual = Lookup.ScrabbleDictionary;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSynonymFor
        ///</summary>
        [TestMethod()]
        public void IsSynonymForTest() {
            Adverb word = null; // TODO: Initialize to an appropriate value
            Adverb other = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Lookup.IsSynonymFor(word, other);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSynonymFor
        ///</summary>
        [TestMethod()]
        public void IsSynonymForTest1() {
            Noun first = null; // TODO: Initialize to an appropriate value
            Noun second = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Lookup.IsSynonymFor(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSynonymFor
        ///</summary>
        [TestMethod()]
        public void IsSynonymForTest2() {
            Adjective word = null; // TODO: Initialize to an appropriate value
            Adjective other = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Lookup.IsSynonymFor(word, other);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSynonymFor
        ///</summary>
        [TestMethod()]
        public void IsSynonymForTest3() {
            Verb first = null; // TODO: Initialize to an appropriate value
            Verb second = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Lookup.IsSynonymFor(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest() {
            Adverb first = null; // TODO: Initialize to an appropriate value
            Adverb second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest1() {
            AdverbPhrase first = null; // TODO: Initialize to an appropriate value
            Adverb second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest2() {
            Adverb first = null; // TODO: Initialize to an appropriate value
            AdverbPhrase second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest3() {
            AdverbPhrase first = null; // TODO: Initialize to an appropriate value
            AdverbPhrase second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest4() {
            IVerbal first = null; // TODO: Initialize to an appropriate value
            IVerbal second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest5() {
            Noun first = null; // TODO: Initialize to an appropriate value
            Noun second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest6() {
            VerbPhrase first = null; // TODO: Initialize to an appropriate value
            Verb second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest7() {
            Verb first = null; // TODO: Initialize to an appropriate value
            VerbPhrase second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest8() {
            VerbPhrase first = null; // TODO: Initialize to an appropriate value
            VerbPhrase second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest9() {
            IEntity first = null; // TODO: Initialize to an appropriate value
            IEntity second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest10() {
            IAggregateEntity first = null; // TODO: Initialize to an appropriate value
            IAggregateEntity second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest11() {
            Noun first = null; // TODO: Initialize to an appropriate value
            NounPhrase second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest12() {
            NounPhrase first = null; // TODO: Initialize to an appropriate value
            Noun second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest13() {
            NounPhrase first = null; // TODO: Initialize to an appropriate value
            NounPhrase second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest14() {
            Verb first = null; // TODO: Initialize to an appropriate value
            Verb second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest15() {
            Adjective first = null; // TODO: Initialize to an appropriate value
            Adjective second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest16() {
            IDescriptor first = null; // TODO: Initialize to an appropriate value
            IDescriptor second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest17() {
            AdjectivePhrase first = null; // TODO: Initialize to an appropriate value
            Adjective second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest18() {
            Adjective first = null; // TODO: Initialize to an appropriate value
            AdjectivePhrase second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest19() {
            AdjectivePhrase first = null; // TODO: Initialize to an appropriate value
            AdjectivePhrase second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsSimilarTo
        ///</summary>
        [TestMethod()]
        public void IsSimilarToTest20() {
            IAdverbial first = null; // TODO: Initialize to an appropriate value
            IAdverbial second = null; // TODO: Initialize to an appropriate value
            SimilarityResult expected = new SimilarityResult(); // TODO: Initialize to an appropriate value
            SimilarityResult actual;
            actual = Lookup.IsSimilarTo(first, second);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsMaleFull
        ///</summary>
        [TestMethod()]
        public void IsMaleFullTest() {
            NounPhrase name = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Lookup.IsMaleFull(name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsMaleFirst
        ///</summary>
        [TestMethod()]
        public void IsMaleFirstTest() {
            ProperNoun proper = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Lookup.IsMaleFirst(proper);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsLastName
        ///</summary>
        [TestMethod()]
        public void IsLastNameTest() {
            ProperNoun proper = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Lookup.IsLastName(proper);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsFirstName
        ///</summary>
        [TestMethod()]
        public void IsFirstNameTest() {
            ProperNoun proper = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Lookup.IsFirstName(proper);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsFemaleFull
        ///</summary>
        [TestMethod()]
        public void IsFemaleFullTest() {
            NounPhrase name = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Lookup.IsFemaleFull(name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsFemaleFirst
        ///</summary>
        [TestMethod()]
        public void IsFemaleFirstTest() {
            ProperNoun proper = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = Lookup.IsFemaleFirst(proper);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSynonyms
        ///</summary>
        [TestMethod()]
        public void GetSynonymsTest() {
            Noun noun = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> actual;
            actual = Lookup.GetSynonyms(noun);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSynonyms
        ///</summary>
        [TestMethod()]
        public void GetSynonymsTest1() {
            Verb verb = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> actual;
            actual = Lookup.GetSynonyms(verb);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSynonyms
        ///</summary>
        [TestMethod()]
        public void GetSynonymsTest2() {
            Adjective adjective = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> actual;
            actual = Lookup.GetSynonyms(adjective);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSynonyms
        ///</summary>
        [TestMethod()]
        public void GetSynonymsTest3() {
            Adverb adverb = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> actual;
            actual = Lookup.GetSynonyms(adverb);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetGender
        ///</summary>
        [TestMethod()]
        public void GetGenderTest() {
            IEntity entity = null; // TODO: Initialize to an appropriate value
            Gender expected = new Gender(); // TODO: Initialize to an appropriate value
            Gender actual;
            actual = Lookup.GetGender(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
