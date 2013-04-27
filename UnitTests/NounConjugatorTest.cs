using LASI.Algorithm.Thesauri;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for NounConjugatorTest and is intended
    ///to contain all NounConjugatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NounConjugatorTest
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
        /// constructs and returns the noun conjugator used for all tests
        /// </summary>
        private static NounConjugator CreateNounConjugator() {
            return new NounConjugator(ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "noun.exc");
        }

        /// <summary>
        ///A test for NounConjugator Constructor
        ///</summary>
        [TestMethod()]
        public void NounConjugatorConstructorTest() {
            NounConjugator target = CreateNounConjugator();
            Assert.IsTrue(
        }


        /// <summary>
        ///A test for FindRoot
        ///</summary>
        [TestMethod()]
        public void FindRootTest() {
            string exceptionsFilePath = string.Empty; // TODO: Initialize to an appropriate value
            NounConjugator target = new NounConjugator(exceptionsFilePath); // TODO: Initialize to an appropriate value
            string conjugated = string.Empty; // TODO: Initialize to an appropriate value
            List<string> expected = null; // TODO: Initialize to an appropriate value
            List<string> actual;
            actual = target.FindRoot(conjugated);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetConjugations
        ///</summary>
        [TestMethod()]
        public void GetConjugationsTest() {
            string exceptionsFilePath = string.Empty; // TODO: Initialize to an appropriate value
            NounConjugator target = new NounConjugator(exceptionsFilePath); // TODO: Initialize to an appropriate value
            string root = string.Empty; // TODO: Initialize to an appropriate value
            List<string> expected = null; // TODO: Initialize to an appropriate value
            List<string> actual;
            actual = target.GetConjugations(root);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            string exceptionsFilePath = string.Empty; // TODO: Initialize to an appropriate value
            NounConjugator target = new NounConjugator(exceptionsFilePath); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TryComputeConjugations
        ///</summary>
        [TestMethod()]
        public void TryComputeConjugationsTest() {
            string exceptionsFilePath = string.Empty; // TODO: Initialize to an appropriate value
            NounConjugator target = new NounConjugator(exceptionsFilePath); // TODO: Initialize to an appropriate value
            string root = string.Empty; // TODO: Initialize to an appropriate value
            List<string> expected = null; // TODO: Initialize to an appropriate value
            List<string> actual;
            actual = target.TryComputeConjugations(root);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TryExtractRoot
        ///</summary>
        [TestMethod()]
        public void TryExtractRootTest() {
            string exceptionsFilePath = string.Empty; // TODO: Initialize to an appropriate value
            NounConjugator target = new NounConjugator(exceptionsFilePath); // TODO: Initialize to an appropriate value
            string search = string.Empty; // TODO: Initialize to an appropriate value
            List<string> expected = null; // TODO: Initialize to an appropriate value
            List<string> actual;
            actual = target.TryExtractRoot(search);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
