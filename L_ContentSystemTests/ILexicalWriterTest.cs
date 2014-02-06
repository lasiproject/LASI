using LASI.ContentSystem.Serialization.XML;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LASI.Core;
using System.Xml;
using System.Threading.Tasks;

namespace L_ContentSystemTests
{
    
    
    /// <summary>
    ///This is a test class for ILexicalWriterTest and is intended
    ///to contain all ILexicalWriterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ILexicalWriterTest
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
        ///A test for Write
        ///</summary>
        public void WriteTestHelper<S, T, W>()
            where S : IEnumerable<T>
            where T : ILexical
            where W : XmlWriter {
            ILexicalWriter<S, T, W> target = CreateILexicalWriter<S, T, W>(); // TODO: Initialize to an appropriate value
            S elements = default(S); // TODO: Initialize to an appropriate value
            string resultSetTitle = string.Empty; // TODO: Initialize to an appropriate value
            DegreeOfOutput degreeOfOutput = new DegreeOfOutput(); // TODO: Initialize to an appropriate value
            target.Write(elements, resultSetTitle, degreeOfOutput);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        internal virtual ILexicalWriter<S, T, W> CreateILexicalWriter<S, T, W>()
            where S : IEnumerable<T>
            where T : ILexical
            where W : XmlWriter {
            // TODO: Instantiate an appropriate concrete class.
            ILexicalWriter<S, T, W> target = null;
            return target;
        }

        [TestMethod()]
        public void WriteTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of S. " +
                    "Please call WriteTestHelper<S, T, W>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for WriteAsync
        ///</summary>
        public void WriteAsyncTestHelper<S, T, W>()
            where S : IEnumerable<T>
            where T : ILexical
            where W : XmlWriter {
            ILexicalWriter<S, T, W> target = CreateILexicalWriter<S, T, W>(); // TODO: Initialize to an appropriate value
            S elements = default(S); // TODO: Initialize to an appropriate value
            string resultSetTitle = string.Empty; // TODO: Initialize to an appropriate value
            DegreeOfOutput degreeOfOutput = new DegreeOfOutput(); // TODO: Initialize to an appropriate value
            Task expected = null; // TODO: Initialize to an appropriate value
            Task actual;
            actual = target.WriteAsync(elements, resultSetTitle, degreeOfOutput);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WriteAsyncTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of S. " +
                    "Please call WriteAsyncTestHelper<S, T, W>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Writer
        ///</summary>
        public void WriterTestHelper<S, T, W>()
            where S : IEnumerable<T>
            where T : ILexical
            where W : XmlWriter {
            ILexicalWriter<S, T, W> target = CreateILexicalWriter<S, T, W>(); // TODO: Initialize to an appropriate value
            XmlWriter actual;
            actual = target.Writer;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WriterTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of S. " +
                    "Please call WriterTestHelper<S, T, W>() with appropriate type parameters.");
        }
    }
}
