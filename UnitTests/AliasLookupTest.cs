using LASI.Core.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;
using System.Collections.Generic;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for AliasLookupTest and is intended
    ///to contain all AliasLookupTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AliasLookupTest
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
        ///A test for IsAliasFor
        ///</summary>
        [TestMethod()]
        public void IsAliasForTest() {
            IEntity possibleAlias = null; // TODO: Initialize to an appropriate value
            IEntity other = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = AliasLookup.IsAliasFor(possibleAlias, other);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetDefinedAliases
        ///</summary>
        [TestMethod()]
        public void GetDefinedAliasesTest() {
            IEntity aliased = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<string> actual;
            actual = AliasLookup.GetDefinedAliases(aliased);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DefineAlias
        ///</summary>
        [TestMethod()]
        public void DefineAliasTest() {
            string entityText = string.Empty; // TODO: Initialize to an appropriate value
            string aliasText = string.Empty; // TODO: Initialize to an appropriate value
            AliasLookup.DefineAlias(entityText, aliasText);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DefineAlias
        ///</summary>
        public void DefineAliasTest1Helper<TE>()
            where TE : IEntity {
            TE entity = default(TE); // TODO: Initialize to an appropriate value
            TE other = default(TE); // TODO: Initialize to an appropriate value
            AliasLookup.DefineAlias<TE>(entity, other);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void DefineAliasTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TE." +
                    " Please call DefineAliasTest1Helper<TE>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for DefineAlias
        ///</summary>
        [TestMethod()]
        public void DefineAliasTest2() {
            IEntity entity = null; // TODO: Initialize to an appropriate value
            string textualAlias = string.Empty; // TODO: Initialize to an appropriate value
            AliasLookup.DefineAlias(entity, textualAlias);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
