using LASI.Core.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for ActionReceiverPairTest and is intended
    ///to contain all ActionReceiverPairTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ActionReceiverPairTest
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
        ///A test for op_Inequality
        ///</summary>
        public void op_InequalityTestHelper<TVerbal, TEntity>()
            where TVerbal : IVerbal
            where TEntity : IEntity {
            ActionReceiverPair<TVerbal, TEntity> left = new ActionReceiverPair<TVerbal, TEntity>(); // TODO: Initialize to an appropriate value
            ActionReceiverPair<TVerbal, TEntity> right = new ActionReceiverPair<TVerbal, TEntity>(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (left != right);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void op_InequalityTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call op_InequalityTestHelper<TVerbal, TEntity>() with appropriate t" +
                    "ype parameters.");
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        public void op_EqualityTestHelper<TVerbal, TEntity>()
            where TVerbal : IVerbal
            where TEntity : IEntity {
            ActionReceiverPair<TVerbal, TEntity> left = new ActionReceiverPair<TVerbal, TEntity>(); // TODO: Initialize to an appropriate value
            ActionReceiverPair<TVerbal, TEntity> right = new ActionReceiverPair<TVerbal, TEntity>(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (left == right);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void op_EqualityTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call op_EqualityTestHelper<TVerbal, TEntity>() with appropriate typ" +
                    "e parameters.");
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        public void GetHashCodeTestHelper<TVerbal, TEntity>()
            where TVerbal : IVerbal
            where TEntity : IEntity {
            ActionReceiverPair<TVerbal, TEntity> target = new ActionReceiverPair<TVerbal, TEntity>(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetHashCodeTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call GetHashCodeTestHelper<TVerbal, TEntity>() with appropriate typ" +
                    "e parameters.");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        public void EqualsTestHelper<TVerbal, TEntity>()
            where TVerbal : IVerbal
            where TEntity : IEntity {
            ActionReceiverPair<TVerbal, TEntity> target = new ActionReceiverPair<TVerbal, TEntity>(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void EqualsTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call EqualsTestHelper<TVerbal, TEntity>() with appropriate type par" +
                    "ameters.");
        }

        /// <summary>
        ///A test for ActionReceiverPair`2 Constructor
        ///</summary>
        public void ActionReceiverPairConstructorTestHelper<TVerbal, TEntity>()
            where TVerbal : IVerbal
            where TEntity : IEntity {
            TVerbal action = default(TVerbal); // TODO: Initialize to an appropriate value
            TEntity receiver = default(TEntity); // TODO: Initialize to an appropriate value
            ActionReceiverPair<TVerbal, TEntity> target = new ActionReceiverPair<TVerbal, TEntity>(action, receiver);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void ActionReceiverPairConstructorTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call ActionReceiverPairConstructorTestHelper<TVerbal, TEntity>() wi" +
                    "th appropriate type parameters.");
        }
    }
}
