using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is a test class for ProperNounTest and is intended
    ///to contain all ProperNounTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProperNounTest
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


        internal virtual IEnumerable<ProperNoun> CreateProperNouns() {
            yield return new ProperSingularNoun("Patrick");
            yield return new ProperPluralNoun("Roberts");
            yield return new ProperPluralNoun("James");
            yield return new ProperPluralNoun("Rachels");
        }

        /// <summary>
        ///A test for IsPersonalName
        ///</summary>
        [TestMethod()]
        public void IsPersonalNameTest() {
            foreach (ProperNoun target in CreateProperNouns()) {
                bool actual;
                actual = target.IsPersonalName;
                Assert.IsTrue(actual);
            }
        }
    }
}
