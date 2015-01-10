using LASI.Core.Patternization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for MatchTest and is intended
    ///to contain all MatchTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MatchTest
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
        ///A test for Yield
        ///</summary>
        public void YieldTestHelper<T, TResult>()
            where T : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T> target = new Match<T>(value); // TODO: Initialize to an appropriate value
            Match<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> actual;
            actual = target.Yield<TResult>();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void YieldTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call YieldTestHelper<T, TResult>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for With
        ///</summary>
        public void WithTest3Helper<T, TPattern>()
            where T : class , ILexical
            where TPattern : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T> target = new Match<T>(value); // TODO: Initialize to an appropriate value
            Action<TPattern> action = null; // TODO: Initialize to an appropriate value
            Match<T> expected = null; // TODO: Initialize to an appropriate value
            Match<T> actual;
            actual = target.With<TPattern>(action);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WithTest3Helper<T, TPattern>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for With
        ///</summary>
        public void WithTest4Helper<T, TPattern>()
            where T : class , ILexical
            where TPattern : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T> target = new Match<T>(value); // TODO: Initialize to an appropriate value
            Action action = null; // TODO: Initialize to an appropriate value
            Match<T> expected = null; // TODO: Initialize to an appropriate value
            Match<T> actual;
            actual = target.With<TPattern>(action);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithTest4() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WithTest4Helper<T, TPattern>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for When
        ///</summary>
        public void WhenTest3Helper<T>()
            where T : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T> target = new Match<T>(value); // TODO: Initialize to an appropriate value
            bool condition = false; // TODO: Initialize to an appropriate value
            PredicatedMatch<T> expected = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T> actual;
            actual = target.When(condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WhenTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WhenTest3Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for When
        ///</summary>
        public void WhenTest4Helper<T, TPattern>()
            where T : class , ILexical
            where TPattern : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T> target = new Match<T>(value); // TODO: Initialize to an appropriate value
            Func<TPattern, bool> predicate = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T> expected = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T> actual;
            actual = target.When<TPattern>(predicate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WhenTest4() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WhenTest4Helper<T, TPattern>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for When
        ///</summary>
        public void WhenTest5Helper<T>()
            where T : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T> target = new Match<T>(value); // TODO: Initialize to an appropriate value
            Func<T, bool> predicate = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T> expected = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T> actual;
            actual = target.When(predicate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WhenTest5() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WhenTest5Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Default
        ///</summary>
        public void DefaultTestHelper<T>()
            where T : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T> target = new Match<T>(value); // TODO: Initialize to an appropriate value
            Action<T> action = null; // TODO: Initialize to an appropriate value
            target.Default(action);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void DefaultTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call DefaultTestHelper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Default
        ///</summary>
        public void DefaultTest1Helper<T>()
            where T : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T> target = new Match<T>(value); // TODO: Initialize to an appropriate value
            Action action = null; // TODO: Initialize to an appropriate value
            target.Default(action);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void DefaultTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call DefaultTest1Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Match`1 Constructor
        ///</summary>
        public void MatchConstructorTest1Helper<T>()
            where T : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T> target = new Match<T>(value);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void MatchConstructorTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call MatchConstructorTest1Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for With
        ///</summary>
        public void WithTestHelper<T, TResult, TPattern>()
            where T : class , ILexical

            where TPattern : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> target = new Match<T, TResult>(value); // TODO: Initialize to an appropriate value
            TResult result = default(TResult); // TODO: Initialize to an appropriate value
            Match<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> actual;
            actual = target.With<TPattern>(result);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WithTestHelper<T, TResult, TPattern>() with appropriate type paramet" +
                    "ers.");
        }

        /// <summary>
        ///A test for With
        ///</summary>
        public void WithTest1Helper<T, TResult, TPattern>()
            where T : class , ILexical

            where TPattern : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> target = new Match<T, TResult>(value); // TODO: Initialize to an appropriate value
            Func<TPattern, TResult> func = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> actual;
            actual = target.With<TPattern>(func);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WithTest1Helper<T, TResult, TPattern>() with appropriate type parame" +
                    "ters.");
        }

        /// <summary>
        ///A test for With
        ///</summary>
        public void WithTest2Helper<T, TResult, TPattern>()
            where T : class , ILexical

            where TPattern : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> target = new Match<T, TResult>(value); // TODO: Initialize to an appropriate value
            Func<TResult> func = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> actual;
            actual = target.With<TPattern>(func);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WithTest2Helper<T, TResult, TPattern>() with appropriate type parame" +
                    "ters.");
        }

        /// <summary>
        ///A test for When
        ///</summary>
        public void WhenTestHelper<T, TResult>()
            where T : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> target = new Match<T, TResult>(value); // TODO: Initialize to an appropriate value
            bool condition = false; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> actual;
            actual = target.When(condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WhenTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WhenTestHelper<T, TResult>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for When
        ///</summary>
        public void WhenTest1Helper<T, TResult, TPattern>()
            where T : class , ILexical

            where TPattern : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> target = new Match<T, TResult>(value); // TODO: Initialize to an appropriate value
            Func<TPattern, bool> predicate = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> actual;
            actual = target.When<TPattern>(predicate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WhenTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WhenTest1Helper<T, TResult, TPattern>() with appropriate type parame" +
                    "ters.");
        }

        /// <summary>
        ///A test for When
        ///</summary>
        public void WhenTest2Helper<T, TResult>()
            where T : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> target = new Match<T, TResult>(value); // TODO: Initialize to an appropriate value
            Func<T, bool> predicate = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> expected = null; // TODO: Initialize to an appropriate value
            PredicatedMatch<T, TResult> actual;
            actual = target.When(predicate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WhenTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WhenTest2Helper<T, TResult>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Result
        ///</summary>
        public void ResultTestHelper<T, TResult>()
            where T : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> target = new Match<T, TResult>(value); // TODO: Initialize to an appropriate value
            TResult expected = default(TResult); // TODO: Initialize to an appropriate value
            TResult actual;
            actual = target.Result();
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
            T value = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> target = new Match<T, TResult>(value); // TODO: Initialize to an appropriate value
            TResult defaultValue = default(TResult); // TODO: Initialize to an appropriate value
            TResult expected = default(TResult); // TODO: Initialize to an appropriate value
            TResult actual;
            actual = target.Result(defaultValue);
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
            T value = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> target = new Match<T, TResult>(value); // TODO: Initialize to an appropriate value
            Func<T, TResult> func = null; // TODO: Initialize to an appropriate value
            TResult expected = default(TResult); // TODO: Initialize to an appropriate value
            TResult actual;
            actual = target.Result(func);
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
            T value = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> target = new Match<T, TResult>(value); // TODO: Initialize to an appropriate value
            Func<TResult> func = null; // TODO: Initialize to an appropriate value
            TResult expected = default(TResult); // TODO: Initialize to an appropriate value
            TResult actual;
            actual = target.Result(func);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ResultTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ResultTest3Helper<T, TResult>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Match`2 Constructor
        ///</summary>
        public void MatchConstructorTestHelper<T, TResult>()
            where T : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T, TResult> target = new Match<T, TResult>(value);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void MatchConstructorTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call MatchConstructorTestHelper<T, TResult>() with appropriate type param" +
                    "eters.");
        }
    }
}
