using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests
{


    /// <summary>
    ///This is a test class for IDirectObjectTakerTest and is intended
    ///to contain all IDirectObjectTakerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IDirectObjectTakerTest
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


        internal virtual IDirectObjectTaker CreateIDirectObjectTaker() {
            IDirectObjectTaker target = new Verb("slay", VerbForm.Base);
            return target;
        }

        /// <summary>
        ///A test for BindDirectObject
        ///</summary>
        [TestMethod()]
        public void BindDirectObjectTest() {
            IDirectObjectTaker target = CreateIDirectObjectTaker();
            IEntity directObject = new PersonalPronoun("them");
            target.BindDirectObject(directObject);
            Assert.IsTrue(target.DirectObjects.Any(o => o == directObject));
        }

        /// <summary>
        ///A test for AggregateDirectObject
        ///</summary>
        [TestMethod()]
        public void AggregateDirectObjectTest() {
            IDirectObjectTaker target = CreateIDirectObjectTaker();
            IAggregateEntity actual;
            actual = target.AggregateDirectObject;
            Assert.IsTrue(target.AggregateDirectObject.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for DirectObjects
        ///</summary>
        [TestMethod()]
        public void DirectObjectsTest() {
            IDirectObjectTaker target = CreateIDirectObjectTaker();
            IEnumerable<IEntity> actual;
            actual = target.DirectObjects;
            Assert.AreEqual(target.DirectObjects, actual);
        }
    }
}
