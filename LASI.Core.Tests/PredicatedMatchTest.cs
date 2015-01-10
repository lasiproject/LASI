using LASI.Core.Patternization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for PredicatedMatchTest and is intended
    ///to contain all PredicatedMatchTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PredicatedMatchTest
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
        ///A test for Then
        ///</summary>
        public void ThenTest6Helper<T>()
            where T : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T> target = new PredicatedMatch<T>(accepted, inner); // TODO: Initialize to an appropriate value
            Action<T> action = null; // TODO: Initialize to an appropriate value
            Match<T> expected = null; // TODO: Initialize to an appropriate value
            Match<T> actual;
            actual = target.Then(action);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ThenTest6() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ThenTest6Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Then
        ///</summary>
        public void ThenTest7Helper<T>()
            where T : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T> target = new PredicatedMatch<T>(accepted, inner); // TODO: Initialize to an appropriate value
            Action action = null; // TODO: Initialize to an appropriate value
            Match<T> expected = null; // TODO: Initialize to an appropriate value
            Match<T> actual;
            actual = target.Then(action);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ThenTest7() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ThenTest7Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Then
        ///</summary>
        public void ThenTest8Helper<T, TPattern>()
            where T : class , ILexical
            where TPattern : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T> target = new PredicatedMatch<T>(accepted, inner); // TODO: Initialize to an appropriate value
            Action<TPattern> action = null; // TODO: Initialize to an appropriate value
            Match<T> expected = null; // TODO: Initialize to an appropriate value
            Match<T> actual;
            actual = target.Then<TPattern>(action);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ThenTest8() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ThenTest8Helper<T, TPattern>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Then
        ///</summary>
        public void ThenTest9Helper<T, TPattern>()
            where T : class , ILexical
            where TPattern : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T> target = new PredicatedMatch<T>(accepted, inner); // TODO: Initialize to an appropriate value
            Action action = null; // TODO: Initialize to an appropriate value
            Match<T> expected = null; // TODO: Initialize to an appropriate value
            Match<T> actual;
            actual = target.Then<TPattern>(action);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ThenTest9() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ThenTest9Helper<T, TPattern>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for PredicatedMatch`1 Constructor
        ///</summary>
        public void PredicatedMatchConstructorTest1Helper<T>()
            where T : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T> target = new PredicatedMatch<T>(accepted, inner);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void PredicatedMatchConstructorTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call PredicatedMatchConstructorTest1Helper<T>() with appropriate type par" +
                    "ameters.");
        }

        /// <summary>
        ///A test for Then
        ///</summary>
        public void ThenTestHelper<T, TResult>()
            where T : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T, TResult> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> target = new PredicatedMatch<T, TResult>(accepted, inner); // TODO: Initialize to an appropriate value
            Func<TResult> func = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> actual;
            actual = target.Then(func);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ThenTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ThenTestHelper<T, TResult>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Then
        ///</summary>
        public void ThenTest1Helper<T, TResult>()
            where T : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T, TResult> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> target = new PredicatedMatch<T, TResult>(accepted, inner); // TODO: Initialize to an appropriate value
            TResult resultValue = default(TResult); // TODO: Initialize to an appropriate value
            Match<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> actual;
            actual = target.Then(resultValue);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ThenTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ThenTest1Helper<T, TResult>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Then
        ///</summary>
        public void ThenTest2Helper<T, TResult>()
            where T : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T, TResult> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> target = new PredicatedMatch<T, TResult>(accepted, inner); // TODO: Initialize to an appropriate value
            Func<T, TResult> func = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> actual;
            actual = target.Then(func);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ThenTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ThenTest2Helper<T, TResult>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Then
        ///</summary>
        public void ThenTest3Helper<T, TResult, TPattern>()
            where T : class , ILexical

            where TPattern : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T, TResult> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> target = new PredicatedMatch<T, TResult>(accepted, inner); // TODO: Initialize to an appropriate value
            Func<TPattern, TResult> func = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> actual;
            actual = target.Then<TPattern>(func);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ThenTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ThenTest3Helper<T, TResult, TPattern>() with appropriate type parame" +
                    "ters.");
        }

        /// <summary>
        ///A test for Then
        ///</summary>
        public void ThenTest4Helper<T, TResult, TPattern>()
            where T : class , ILexical

            where TPattern : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T, TResult> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> target = new PredicatedMatch<T, TResult>(accepted, inner); // TODO: Initialize to an appropriate value
            Func<TResult> func = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> actual;
            actual = target.Then<TPattern>(func);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ThenTest4() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ThenTest4Helper<T, TResult, TPattern>() with appropriate type parame" +
                    "ters.");
        }

        /// <summary>
        ///A test for Then
        ///</summary>
        public void ThenTest5Helper<T, TResult, TPattern>()
            where T : class , ILexical

            where TPattern : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T, TResult> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> target = new PredicatedMatch<T, TResult>(accepted, inner); // TODO: Initialize to an appropriate value
            TResult result = default(TResult); // TODO: Initialize to an appropriate value
            Match<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> actual;
            actual = target.Then<TPattern>(result);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ThenTest5() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ThenTest5Helper<T, TResult, TPattern>() with appropriate type parame" +
                    "ters.");
        }

        /// <summary>
        ///A test for Result
        ///</summary>
        public void ResultTestHelper<T, TResult>()
            where T : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T, TResult> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> target = new PredicatedMatch<T, TResult>(accepted, inner); // TODO: Initialize to an appropriate value
            Func<T, TResult> func = null; // TODO: Initialize to an appropriate value
            TResult expected = default(TResult); // TODO: Initialize to an appropriate value
            TResult actual;
            actual = target.Result(func);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ResultTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ResultTestHelper<T, TResult>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Result
        ///</summary>
        public void ResultTest1Helper<T, TResult>()
            where T : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T, TResult> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> target = new PredicatedMatch<T, TResult>(accepted, inner); // TODO: Initialize to an appropriate value
            Func<TResult> func = null; // TODO: Initialize to an appropriate value
            TResult expected = default(TResult); // TODO: Initialize to an appropriate value
            TResult actual;
            actual = target.Result(func);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ResultTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ResultTest1Helper<T, TResult>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Result
        ///</summary>
        public void ResultTest2Helper<T, TResult>()
            where T : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T, TResult> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> target = new PredicatedMatch<T, TResult>(accepted, inner); // TODO: Initialize to an appropriate value
            TResult defaultValue = default(TResult); // TODO: Initialize to an appropriate value
            TResult expected = default(TResult); // TODO: Initialize to an appropriate value
            TResult actual;
            actual = target.Result(defaultValue);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ResultTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ResultTest2Helper<T, TResult>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Result
        ///</summary>
        public void ResultTest3Helper<T, TResult>()
            where T : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T, TResult> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> target = new PredicatedMatch<T, TResult>(accepted, inner); // TODO: Initialize to an appropriate value
            TResult expected = default(TResult); // TODO: Initialize to an appropriate value
            TResult actual;
            actual = target.Result();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ResultTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ResultTest3Helper<T, TResult>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for PredicatedMatch`2 Constructor
        ///</summary>
        public void PredicatedMatchConstructorTestHelper<T, TResult>()
            where T : class , ILexical {
            bool accepted = false; // TODO: Initialize to an appropriate value
            Match<T, TResult> inner = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> target = new PredicatedMatch<T, TResult>(accepted, inner);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void PredicatedMatchConstructorTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call PredicatedMatchConstructorTestHelper<T, TResult>() with appropriate " +
                    "type parameters.");
        }
    }
}
