using LASI.Core.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for PerformerReceiverPairTest and is intended
    ///to contain all PerformerReceiverPairTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PerformerReceiverPairTest
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
        public void op_InequalityTestHelper<TPerformer, TReceiver>()
            where TPerformer : IEntity
            where TReceiver : IEntity {
            PerformerReceiverPair<TPerformer, TReceiver> left = new PerformerReceiverPair<TPerformer, TReceiver>(); // TODO: Initialize to an appropriate value
            PerformerReceiverPair<TPerformer, TReceiver> right = new PerformerReceiverPair<TPerformer, TReceiver>(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (left != right);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void op_InequalityTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TPe" +
                    "rformer. Please call op_InequalityTestHelper<TPerformer, TReceiver>() with appro" +
                    "priate type parameters.");
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        public void op_EqualityTestHelper<TPerformer, TReceiver>()
            where TPerformer : IEntity
            where TReceiver : IEntity {
            PerformerReceiverPair<TPerformer, TReceiver> left = new PerformerReceiverPair<TPerformer, TReceiver>(); // TODO: Initialize to an appropriate value
            PerformerReceiverPair<TPerformer, TReceiver> right = new PerformerReceiverPair<TPerformer, TReceiver>(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (left == right);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void op_EqualityTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TPe" +
                    "rformer. Please call op_EqualityTestHelper<TPerformer, TReceiver>() with appropr" +
                    "iate type parameters.");
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        public void GetHashCodeTestHelper<TPerformer, TReceiver>()
            where TPerformer : IEntity
            where TReceiver : IEntity {
            PerformerReceiverPair<TPerformer, TReceiver> target = new PerformerReceiverPair<TPerformer, TReceiver>(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void GetHashCodeTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TPe" +
                    "rformer. Please call GetHashCodeTestHelper<TPerformer, TReceiver>() with appropr" +
                    "iate type parameters.");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        public void EqualsTestHelper<TPerformer, TReceiver>()
            where TPerformer : IEntity
            where TReceiver : IEntity {
            PerformerReceiverPair<TPerformer, TReceiver> target = new PerformerReceiverPair<TPerformer, TReceiver>(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void EqualsTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TPe" +
                    "rformer. Please call EqualsTestHelper<TPerformer, TReceiver>() with appropriate " +
                    "type parameters.");
        }

        /// <summary>
        ///A test for PerformerReceiverPair`2 Constructor
        ///</summary>
        public void PerformerReceiverPairConstructorTestHelper<TPerformer, TReceiver>()
            where TPerformer : IEntity
            where TReceiver : IEntity {
            TPerformer performer = default(TPerformer); // TODO: Initialize to an appropriate value
            TReceiver receiver = default(TReceiver); // TODO: Initialize to an appropriate value
            PerformerReceiverPair<TPerformer, TReceiver> target = new PerformerReceiverPair<TPerformer, TReceiver>(performer, receiver);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void PerformerReceiverPairConstructorTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TPe" +
                    "rformer. Please call PerformerReceiverPairConstructorTestHelper<TPerformer, TRec" +
                    "eiver>() with appropriate type parameters.");
        }
    }
}
