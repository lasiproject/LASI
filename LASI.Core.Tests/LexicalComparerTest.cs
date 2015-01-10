using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for LexicalComparerTest and is intended
    ///to contain all LexicalComparerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LexicalComparerTest
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
        ///A test for Textual
        ///</summary>
        [TestMethod()]
        public void TextualTest() {
            IEqualityComparer<ILexical> actual;
            actual = LexicalComparer.Textual;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        public void CreateTestHelper<TLexical>()
            where TLexical : ILexical {
            Func<TLexical, TLexical, bool> equals = null; // TODO: Initialize to an appropriate value
            IEqualityComparer<TLexical> expected = null; // TODO: Initialize to an appropriate value
            IEqualityComparer<TLexical> actual;
            actual = LexicalComparer.Create<TLexical>(equals);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void CreateTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call CreateTestHelper<TLexical>() with appropriate type parameters" +
                    ".");
        }
    }
}
