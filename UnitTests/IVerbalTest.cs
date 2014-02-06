using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for IVerbalTest and is intended
    ///to contain all IVerbalTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IVerbalTest
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


        internal virtual IVerbal CreateIVerbal() {
            // TODO: Instantiate an appropriate concrete class.
            IVerbal target = null;
            return target;
        }

        /// <summary>
        ///A test for PrepositionalToObject
        ///</summary>
        [TestMethod()]
        public void PrepositionalToObjectTest() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            IPrepositional actual;
            actual = target.PrepositionalToObject;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ObjectOfThePreoposition
        ///</summary>
        [TestMethod()]
        public void ObjectOfThePreopositionTest() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            ILexical actual;
            actual = target.ObjectOfThePreoposition;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsPossessive
        ///</summary>
        [TestMethod()]
        public void IsPossessiveTest() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsPossessive;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsClassifier
        ///</summary>
        [TestMethod()]
        public void IsClassifierTest() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsClassifier;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HasSubjectOrObject
        ///</summary>
        [TestMethod()]
        public void HasSubjectOrObjectTest() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            Func<IEntity, bool> predicate = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasSubjectOrObject(predicate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HasSubjectOrObject
        ///</summary>
        [TestMethod()]
        public void HasSubjectOrObjectTest1() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasSubjectOrObject();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HasSubject
        ///</summary>
        [TestMethod()]
        public void HasSubjectTest() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            Func<IEntity, bool> predicate = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasSubject(predicate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HasSubject
        ///</summary>
        [TestMethod()]
        public void HasSubjectTest1() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasSubject();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HasObject
        ///</summary>
        [TestMethod()]
        public void HasObjectTest() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            Func<IEntity, bool> predicate = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasObject(predicate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HasObject
        ///</summary>
        [TestMethod()]
        public void HasObjectTest1() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasObject();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HasIndirectObject
        ///</summary>
        [TestMethod()]
        public void HasIndirectObjectTest() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            Func<IEntity, bool> predicate = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasIndirectObject(predicate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HasIndirectObject
        ///</summary>
        [TestMethod()]
        public void HasIndirectObjectTest1() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasIndirectObject();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HasDirectObject
        ///</summary>
        [TestMethod()]
        public void HasDirectObjectTest() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            Func<IEntity, bool> predicate = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasDirectObject(predicate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HasDirectObject
        ///</summary>
        [TestMethod()]
        public void HasDirectObjectTest1() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasDirectObject();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AttachObjectViaPreposition
        ///</summary>
        [TestMethod()]
        public void AttachObjectViaPrepositionTest() {
            IVerbal target = CreateIVerbal(); // TODO: Initialize to an appropriate value
            IPrepositional prepositional = null; // TODO: Initialize to an appropriate value
            target.AttachObjectViaPreposition(prepositional);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
