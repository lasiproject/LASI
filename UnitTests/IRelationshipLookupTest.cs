using LASI.Core.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;
using System.Collections.Generic;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for IRelationshipLookupTest and is intended
    ///to contain all IRelationshipLookupTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IRelationshipLookupTest
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
        ///A test for Item
        ///</summary>
        public void ItemTestHelper<TEntity, TVerbal>()
            where TEntity : IEntity
            where TVerbal : IVerbal {
            IRelationshipLookup<TEntity, TVerbal> target = CreateIRelationshipLookup<TEntity, TVerbal>(); // TODO: Initialize to an appropriate value
            TEntity actionPerformer = default(TEntity); // TODO: Initialize to an appropriate value
            Func<TEntity, TEntity, bool> performerComparer = null; // TODO: Initialize to an appropriate value
            TVerbal action = default(TVerbal); // TODO: Initialize to an appropriate value
            Func<TVerbal, TVerbal, bool> actionComparer = null; // TODO: Initialize to an appropriate value
            IEnumerable<TEntity> actual;
            actual = target[actionPerformer, performerComparer, action, actionComparer];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        internal virtual IRelationshipLookup<TEntity, TVerbal> CreateIRelationshipLookup<TEntity, TVerbal>()
            where TEntity : IEntity
            where TVerbal : IVerbal {
            // TODO: Instantiate an appropriate concrete class.
            IRelationshipLookup<TEntity, TVerbal> target = null;
            return target;
        }

        [TestMethod()]
        public void ItemTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TEn" +
                    "tity. Please call ItemTestHelper<TEntity, TVerbal>() with appropriate type param" +
                    "eters.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public void ItemTest1Helper<TEntity, TVerbal>()
            where TEntity : IEntity
            where TVerbal : IVerbal {
            IRelationshipLookup<TEntity, TVerbal> target = CreateIRelationshipLookup<TEntity, TVerbal>(); // TODO: Initialize to an appropriate value
            TEntity actionPerformer = default(TEntity); // TODO: Initialize to an appropriate value
            TVerbal action = default(TVerbal); // TODO: Initialize to an appropriate value
            IEnumerable<TEntity> actual;
            actual = target[actionPerformer, action];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ItemTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TEn" +
                    "tity. Please call ItemTest1Helper<TEntity, TVerbal>() with appropriate type para" +
                    "meters.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public void ItemTest2Helper<TEntity, TVerbal>()
            where TEntity : IEntity
            where TVerbal : IVerbal {
            IRelationshipLookup<TEntity, TVerbal> target = CreateIRelationshipLookup<TEntity, TVerbal>(); // TODO: Initialize to an appropriate value
            TVerbal relater = default(TVerbal); // TODO: Initialize to an appropriate value
            Func<TVerbal, TVerbal, bool> verbalComparer = null; // TODO: Initialize to an appropriate value
            IEnumerable<PerformerReceiverPair<TEntity, TEntity>> actual;
            actual = target[relater, verbalComparer];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ItemTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TEn" +
                    "tity. Please call ItemTest2Helper<TEntity, TVerbal>() with appropriate type para" +
                    "meters.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public void ItemTest3Helper<TEntity, TVerbal>()
            where TEntity : IEntity
            where TVerbal : IVerbal {
            IRelationshipLookup<TEntity, TVerbal> target = CreateIRelationshipLookup<TEntity, TVerbal>(); // TODO: Initialize to an appropriate value
            TEntity performer = default(TEntity); // TODO: Initialize to an appropriate value
            Func<TEntity, TEntity, bool> performerComparer = null; // TODO: Initialize to an appropriate value
            IEnumerable<ActionReceiverPair<TVerbal, TEntity>> actual;
            actual = target[performer, performerComparer];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ItemTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TEn" +
                    "tity. Please call ItemTest3Helper<TEntity, TVerbal>() with appropriate type para" +
                    "meters.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public void ItemTest4Helper<TEntity, TVerbal>()
            where TEntity : IEntity
            where TVerbal : IVerbal {
            IRelationshipLookup<TEntity, TVerbal> target = CreateIRelationshipLookup<TEntity, TVerbal>(); // TODO: Initialize to an appropriate value
            TEntity performer = default(TEntity); // TODO: Initialize to an appropriate value
            IEnumerable<ActionReceiverPair<TVerbal, TEntity>> actual;
            actual = target[performer];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ItemTest4() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TEn" +
                    "tity. Please call ItemTest4Helper<TEntity, TVerbal>() with appropriate type para" +
                    "meters.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public void ItemTest5Helper<TEntity, TVerbal>()
            where TEntity : IEntity
            where TVerbal : IVerbal {
            IRelationshipLookup<TEntity, TVerbal> target = CreateIRelationshipLookup<TEntity, TVerbal>(); // TODO: Initialize to an appropriate value
            TVerbal relator = default(TVerbal); // TODO: Initialize to an appropriate value
            IEnumerable<PerformerReceiverPair<TEntity, TEntity>> actual;
            actual = target[relator];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ItemTest5() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TEn" +
                    "tity. Please call ItemTest5Helper<TEntity, TVerbal>() with appropriate type para" +
                    "meters.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public void ItemTest6Helper<TEntity, TVerbal>()
            where TEntity : IEntity
            where TVerbal : IVerbal {
            IRelationshipLookup<TEntity, TVerbal> target = CreateIRelationshipLookup<TEntity, TVerbal>(); // TODO: Initialize to an appropriate value
            TEntity actionPerformer = default(TEntity); // TODO: Initialize to an appropriate value
            Func<TEntity, TEntity, bool> performerComparer = null; // TODO: Initialize to an appropriate value
            TEntity actionReceiver = default(TEntity); // TODO: Initialize to an appropriate value
            Func<TEntity, TEntity, bool> receiverComparer = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> actual;
            actual = target[actionPerformer, performerComparer, actionReceiver, receiverComparer];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ItemTest6() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TEn" +
                    "tity. Please call ItemTest6Helper<TEntity, TVerbal>() with appropriate type para" +
                    "meters.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        public void ItemTest7Helper<TEntity, TVerbal>()
            where TEntity : IEntity
            where TVerbal : IVerbal {
            IRelationshipLookup<TEntity, TVerbal> target = CreateIRelationshipLookup<TEntity, TVerbal>(); // TODO: Initialize to an appropriate value
            TEntity actionPerformer = default(TEntity); // TODO: Initialize to an appropriate value
            TEntity actionReceiver = default(TEntity); // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> actual;
            actual = target[actionPerformer, actionReceiver];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ItemTest7() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TEn" +
                    "tity. Please call ItemTest7Helper<TEntity, TVerbal>() with appropriate type para" +
                    "meters.");
        }
    }
}
