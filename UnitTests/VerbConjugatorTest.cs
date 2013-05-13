using LASI.Algorithm.Thesauri;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is entity test class for VerbConjugatorTest and is intended
    ///to contain all VerbConjugatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VerbConjugatorTest
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
        //Use ClassCleanup to run code after all tests in entity class have run
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
        ///A test for TryExtractRoot
        ///</summary>
        [TestMethod()]
        public void TryGetExtractRootTest() {
            string exceptionsFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "verb.exc";
            VerbConjugator target = new VerbConjugator(exceptionsFilePath);
            string expected = "walk";
            foreach (var search in new[] { "walk", "walked", "walks", "walking" }) {
                List<string> actual;
                actual = target.TryExtractRoot(search);
                Assert.IsTrue(actual.Contains(expected));
            }
        }

        /// <summary>
        ///A test for TryComputeConjugations
        ///</summary>
        [TestMethod()]
        public void TryComputeConjugationsTest() {
            string exceptionsFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "verb.exc";
            VerbConjugator target = new VerbConjugator(exceptionsFilePath);
            string root = "walk";
            List<string> expected = new[] { "walks", "walking", "walked" }.ToList();
            List<string> actual;
            actual = target.TryComputeConjugations(root);
            foreach (var conjugation in expected) {
                Assert.IsTrue(actual.Contains(conjugation));
            }
        }




        /// <summary>
        ///A test for GetConjugations
        ///</summary>
        [TestMethod()]
        public void GetConjugationsTest() {
            string exceptionsFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "verb.exc";
            VerbConjugator target = new VerbConjugator(exceptionsFilePath);
            string root = "walk";
            List<string> expected = new[] { "walked", "walks", "walking" }.ToList();
            List<string> actual;
            actual = target.GetConjugations(root);
            foreach (var f in actual)
                Debug.WriteLine(f);
            Assert.IsTrue((from f in expected
                           select actual.Contains(f)).Aggregate(true, (aggr, tf) => aggr &= tf));

        }

        /// <summary>
        ///A test for FindRoot
        ///</summary>
        [TestMethod()]
        public void FindRootTest() {
            string exceptionsFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "verb.exc";
            VerbConjugator target = new VerbConjugator(exceptionsFilePath);
            var conjugated = new[] { "walked", "walking", "walks" };
            List<string> expected = new[] { "walk" }.ToList();
            List<string> actual = new List<string>();
            foreach (var c in conjugated) {
                actual.AddRange(target.FindRoot(c));

            }
            Assert.IsTrue((from f in expected
                           select actual.Contains(f)).Aggregate(true, (aggr, tf) => aggr &= tf)); 


        }


        //    /// <summary>
        //    ///A test for VerbConjugator Constructor
        //    ///</summary>
        //    [TestMethod()]
        //    public void VerbConjugatorConstructorTest() {
        //        string exceptionsFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "verb.exc";
        //        VerbConjugator target = new VerbConjugator(exceptionsFilePath);
        //        Assert.Inconclusive("TODO: Implement code to verify target");
        //    }
        //}
    }
}
