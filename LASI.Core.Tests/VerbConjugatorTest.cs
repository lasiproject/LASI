using LASI.Core.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LASI.Core.Tests
{


	/// <summary>
	///This is A test class for VerbConjugatorTest and is intended
	///to contain all VerbConjugatorTest Unit Tests
	///</summary>
	[TestClass]
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
        ///A test for GetLexicalForms
        ///</summary>
        [TestMethod]
        public void GetConjugationsTest() {
            string root = "walk";
            IEnumerable<string> expected = new[] { "walked", "walks", "walking" }.ToList();
            IEnumerable<string> actual;
            actual = VerbMorpher.GetConjugations(root);
            foreach (var f in actual)
                Debug.WriteLine(f);
            Assert.IsTrue((from f in expected
                           select actual.Contains(f)).Aggregate(true, (result, b) => result && b));

        }

        /// <summary>
        ///A test for FindRoot
        ///</summary>
        [TestMethod]
        public void FindRootTest() {
            var conjugated = new[] { "walked", "walking", "walks" };
            List<string> expected = new[] { "walk" }.ToList();
            List<string> actual = new List<string>();
            foreach (var c in conjugated) {
                actual.AddRange(VerbMorpher.FindRoots(c));

            }
            Assert.IsTrue((from f in expected
                           select actual.Contains(f)).Aggregate(true, (result, b) => result && b));


        }

    }
}
