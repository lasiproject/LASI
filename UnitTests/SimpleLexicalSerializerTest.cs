using LASI.ContentSystem.Serialization.XML;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.IO;
using LASI.Core;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using LASI.ContentSystem;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for SimpleLexicalSerializerTest and is intended
    ///to contain all SimpleLexicalSerializerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SimpleLexicalSerializerTest
    {

        const string OUTPUT_FILE_PATH = @".\xmltest\test\data.xml";
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
        ///A test for SimpleLexicalSerializer Constructor
        ///</summary>
        [TestMethod()]
        public void SimpleLexicalSerializerConstructorTest() {
            XmlWriter target1 = null; // TODO: Initialize to an appropriate value
            SimpleLexicalSerializer target = new SimpleLexicalSerializer(target1);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SimpleLexicalSerializer Constructor
        ///</summary>
        [TestMethod()]
        public void SimpleLexicalSerializerConstructorTest1() {
            string uri = string.Empty; // TODO: Initialize to an appropriate value
            SimpleLexicalSerializer target = new SimpleLexicalSerializer(uri);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SimpleLexicalSerializer Constructor
        ///</summary>
        [TestMethod()]
        public void SimpleLexicalSerializerConstructorTest2() {
            TextWriter textWriter = null; // TODO: Initialize to an appropriate value
            SimpleLexicalSerializer target = new SimpleLexicalSerializer(textWriter);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest() {
            XmlWriter target1 = null; // TODO: Initialize to an appropriate value
            SimpleLexicalSerializer target = new SimpleLexicalSerializer(target1); // TODO: Initialize to an appropriate value
            target.Dispose();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Serialize
        ///</summary>
        [TestMethod()]
        public void SerializeTest() {
            XmlWriter target1 = null; // TODO: Initialize to an appropriate value
            SimpleLexicalSerializer target = new SimpleLexicalSerializer(target1); // TODO: Initialize to an appropriate value
            IEnumerable<ILexical> source = null; // TODO: Initialize to an appropriate value
            string parentElementTitle = string.Empty; // TODO: Initialize to an appropriate value
            DegreeOfOutput degreeOfOutput = new DegreeOfOutput(); // TODO: Initialize to an appropriate value
            XElement expected = null; // TODO: Initialize to an appropriate value
            XElement actual;
            actual = target.Serialize(source, parentElementTitle, degreeOfOutput);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SerializeSequence
        ///</summary>
        [TestMethod()]
        public void SerializeSequenceTest() {
            XmlWriter target1 = null; // TODO: Initialize to an appropriate value
            SimpleLexicalSerializer target = new SimpleLexicalSerializer(target1); // TODO: Initialize to an appropriate value
            IEnumerable<ILexical> source = null; // TODO: Initialize to an appropriate value
            string documentTitle = string.Empty; // TODO: Initialize to an appropriate value
            DegreeOfOutput degreeOfOutput = new DegreeOfOutput(); // TODO: Initialize to an appropriate value
            XDocument expected = null; // TODO: Initialize to an appropriate value
            XDocument actual;
            actual = target.SerializeSequence(source, documentTitle, degreeOfOutput);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Write
        ///</summary>
        [TestMethod()]
        public void WriteTest() {
            SimpleLexicalSerializer target = new SimpleLexicalSerializer(OUTPUT_FILE_PATH);
            IEnumerable<ILexical> source = Tagger.DocumentFromRaw(new TxtFile(@"..\..\..\TestDocs\cats.txt")).GetAllLexicalConstructs();
            string title = "test xml";
            DegreeOfOutput degreeOfOutput = DegreeOfOutput.Comprehensive;

            target.Write(source, title, degreeOfOutput);
            using (var reader = new System.IO.StreamReader(OUTPUT_FILE_PATH)) {
                var xd = XDocument.Load(reader);
                var xml = xd.Descendants().DescendantsAndSelf()
                    .Where(e => e.FirstAttribute != null).Select(e => new { Name = e.Name, TextAttribute = e.FirstAttribute.Value }).AsParallel();
                var result = source.AsParallel().All(lex =>
                        xml.Any(node => lex.Type.Name == node.Name && lex.Text == node.TextAttribute));
                Assert.IsTrue(result);
                Assert.IsTrue(xd.Root.Attribute("Title").Value == title);
            }
        }

        /// <summary>
        ///A test for WriteAsync
        ///</summary>
        [TestMethod()]
        public async void WriteAsyncTest() {
            SimpleLexicalSerializer target = new SimpleLexicalSerializer(OUTPUT_FILE_PATH);
            IEnumerable<ILexical> source = Tagger.DocumentFromRaw(new TxtFile(@"..\..\..\TestDocs\cats.txt")).GetAllLexicalConstructs();
            string title = "test xml";
            DegreeOfOutput degreeOfOutput = DegreeOfOutput.Comprehensive;

            await target.WriteAsync(source, title, degreeOfOutput);
            using (var reader = new System.IO.StreamReader(OUTPUT_FILE_PATH)) {
                var xd = XDocument.Load(reader);
                var xml = xd.Descendants().DescendantsAndSelf()
                    .Where(e => e.FirstAttribute != null).Select(e => new { Name = e.Name, TextAttribute = e.FirstAttribute.Value }).AsParallel();
                var result = source.AsParallel().All(lex =>
                        xml.Any(node => lex.Type.Name == node.Name && lex.Text == node.TextAttribute));
                Assert.IsTrue(result);
                Assert.IsTrue(xd.Root.Attribute("Title").Value == title);
            }
        }
    }
}
