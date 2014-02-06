using LASI.Core.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;
using System.Collections.Generic;
using LASI.Core.DocumentStructures;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for SampleRelationshipLookupTest and is intended
    ///to contain all SampleRelationshipLookupTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SampleRelationshipLookupTest
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
        [TestMethod()]
        public void ItemTest() {
            IEnumerable<IVerbal> domain = null; // TODO: Initialize to an appropriate value
            SampleRelationshipLookup target = new SampleRelationshipLookup(domain); // TODO: Initialize to an appropriate value
            IEntity actionPerformer = null; // TODO: Initialize to an appropriate value
            Func<IEntity, IEntity, bool> performerComparer = null; // TODO: Initialize to an appropriate value
            IVerbal action = null; // TODO: Initialize to an appropriate value
            Func<IVerbal, IVerbal, bool> actionComparer = null; // TODO: Initialize to an appropriate value
            IEnumerable<IEntity> actual;
            actual = target[actionPerformer, performerComparer, action, actionComparer];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest1() {
            IEnumerable<IVerbal> domain = null; // TODO: Initialize to an appropriate value
            SampleRelationshipLookup target = new SampleRelationshipLookup(domain); // TODO: Initialize to an appropriate value
            IEntity actionPerformer = null; // TODO: Initialize to an appropriate value
            IVerbal action = null; // TODO: Initialize to an appropriate value
            IEnumerable<IEntity> actual;
            actual = target[actionPerformer, action];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest2() {
            IEnumerable<IVerbal> domain = null; // TODO: Initialize to an appropriate value
            SampleRelationshipLookup target = new SampleRelationshipLookup(domain); // TODO: Initialize to an appropriate value
            IVerbal action = null; // TODO: Initialize to an appropriate value
            Func<IVerbal, IVerbal, bool> actionComparer = null; // TODO: Initialize to an appropriate value
            IEnumerable<PerformerReceiverPair<IEntity, IEntity>> actual;
            actual = target[action, actionComparer];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest3() {
            IEnumerable<IVerbal> domain = null; // TODO: Initialize to an appropriate value
            SampleRelationshipLookup target = new SampleRelationshipLookup(domain); // TODO: Initialize to an appropriate value
            IEntity performer = null; // TODO: Initialize to an appropriate value
            Func<IEntity, IEntity, bool> performerComparer = null; // TODO: Initialize to an appropriate value
            IEnumerable<ActionReceiverPair<IVerbal, IEntity>> actual;
            actual = target[performer, performerComparer];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest4() {
            IEnumerable<IVerbal> domain = null; // TODO: Initialize to an appropriate value
            SampleRelationshipLookup target = new SampleRelationshipLookup(domain); // TODO: Initialize to an appropriate value
            IEntity performer = null; // TODO: Initialize to an appropriate value
            IEnumerable<ActionReceiverPair<IVerbal, IEntity>> actual;
            actual = target[performer];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest5() {
            IEnumerable<IVerbal> domain = null; // TODO: Initialize to an appropriate value
            SampleRelationshipLookup target = new SampleRelationshipLookup(domain); // TODO: Initialize to an appropriate value
            IVerbal relator = null; // TODO: Initialize to an appropriate value
            IEnumerable<PerformerReceiverPair<IEntity, IEntity>> actual;
            actual = target[relator];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest6() {
            IEnumerable<IVerbal> domain = null; // TODO: Initialize to an appropriate value
            SampleRelationshipLookup target = new SampleRelationshipLookup(domain); // TODO: Initialize to an appropriate value
            IEntity actionPerformer = null; // TODO: Initialize to an appropriate value
            Func<IEntity, IEntity, bool> performerComparer = null; // TODO: Initialize to an appropriate value
            IEntity actionReceiver = null; // TODO: Initialize to an appropriate value
            Func<IEntity, IEntity, bool> receiverComparer = null; // TODO: Initialize to an appropriate value
            IEnumerable<IVerbal> actual;
            actual = target[actionPerformer, performerComparer, actionReceiver, receiverComparer];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest7() {
            IEnumerable<IVerbal> domain = null; // TODO: Initialize to an appropriate value
            SampleRelationshipLookup target = new SampleRelationshipLookup(domain); // TODO: Initialize to an appropriate value
            IEntity actionPerformer = null; // TODO: Initialize to an appropriate value
            IEntity actionReceiver = null; // TODO: Initialize to an appropriate value
            IEnumerable<IVerbal> actual;
            actual = target[actionPerformer, actionReceiver];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SampleRelationshipLookup Constructor
        ///</summary>
        [TestMethod()]
        public void SampleRelationshipLookupConstructorTest() {
            Document domain = null; // TODO: Initialize to an appropriate value
            SampleRelationshipLookup target = new SampleRelationshipLookup(domain);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SampleRelationshipLookup Constructor
        ///</summary>
        [TestMethod()]
        public void SampleRelationshipLookupConstructorTest1() {
            IEnumerable<Paragraph> domain = null; // TODO: Initialize to an appropriate value
            SampleRelationshipLookup target = new SampleRelationshipLookup(domain);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SampleRelationshipLookup Constructor
        ///</summary>
        [TestMethod()]
        public void SampleRelationshipLookupConstructorTest2() {
            IEnumerable<Sentence> domain = null; // TODO: Initialize to an appropriate value
            SampleRelationshipLookup target = new SampleRelationshipLookup(domain);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SampleRelationshipLookup Constructor
        ///</summary>
        [TestMethod()]
        public void SampleRelationshipLookupConstructorTest3() {
            IEnumerable<IVerbal> domain = null; // TODO: Initialize to an appropriate value
            SampleRelationshipLookup target = new SampleRelationshipLookup(domain);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
