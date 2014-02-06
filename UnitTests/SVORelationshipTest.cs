using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for SVORelationshipTest and is intended
    ///to contain all SVORelationshipTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SVORelationshipTest
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
        ///A test for Verbal
        ///</summary>
        [TestMethod()]
        public void VerbalTest() {
            SVORelationship target = new SVORelationship(); // TODO: Initialize to an appropriate value
            IVerbal expected = null; // TODO: Initialize to an appropriate value
            IVerbal actual;
            target.Verbal = expected;
            actual = target.Verbal;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Subject
        ///</summary>
        [TestMethod()]
        public void SubjectTest() {
            SVORelationship target = new SVORelationship(); // TODO: Initialize to an appropriate value
            IAggregateEntity expected = null; // TODO: Initialize to an appropriate value
            IAggregateEntity actual;
            target.Subject = expected;
            actual = target.Subject;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Prepositional
        ///</summary>
        [TestMethod()]
        public void PrepositionalTest() {
            SVORelationship target = new SVORelationship(); // TODO: Initialize to an appropriate value
            ILexical expected = null; // TODO: Initialize to an appropriate value
            ILexical actual;
            target.Prepositional = expected;
            actual = target.Prepositional;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Indirect
        ///</summary>
        [TestMethod()]
        public void IndirectTest() {
            SVORelationship target = new SVORelationship(); // TODO: Initialize to an appropriate value
            IAggregateEntity expected = null; // TODO: Initialize to an appropriate value
            IAggregateEntity actual;
            target.Indirect = expected;
            actual = target.Indirect;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Elements
        ///</summary>
        [TestMethod()]
        public void ElementsTest() {
            SVORelationship target = new SVORelationship(); // TODO: Initialize to an appropriate value
            IEnumerable<ILexical> actual;
            actual = target.Elements;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Direct
        ///</summary>
        [TestMethod()]
        public void DirectTest() {
            SVORelationship target = new SVORelationship(); // TODO: Initialize to an appropriate value
            IAggregateEntity expected = null; // TODO: Initialize to an appropriate value
            IAggregateEntity actual;
            target.Direct = expected;
            actual = target.Direct;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CombinedWeight
        ///</summary>
        [TestMethod()]
        public void CombinedWeightTest() {
            SVORelationship target = new SVORelationship(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            target.CombinedWeight = expected;
            actual = target.CombinedWeight;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod()]
        public void op_InequalityTest() {
            SVORelationship left = null; // TODO: Initialize to an appropriate value
            SVORelationship right = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (left != right);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod()]
        public void op_EqualityTest() {
            SVORelationship left = null; // TODO: Initialize to an appropriate value
            SVORelationship right = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (left == right);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            SVORelationship target = new SVORelationship(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest() {
            SVORelationship target = new SVORelationship(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest() {
            SVORelationship target = new SVORelationship(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest1() {
            SVORelationship target = new SVORelationship(); // TODO: Initialize to an appropriate value
            SVORelationship other = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SVORelationship Constructor
        ///</summary>
        [TestMethod()]
        public void SVORelationshipConstructorTest() {
            SVORelationship target = new SVORelationship();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
