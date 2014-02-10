using LASI.ContentSystem.Serialization.XML;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LASI.Core;
using System.Xml;
using System.Threading.Tasks;
using LASI.ContentSystem;
using System.Xml.Linq;
using System.Linq;

namespace LASI.Core.Tests
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

        const string OUTPUT_FILE_PATH = @".\xmltest\test\data.xml";
        internal virtual ILexicalWriter<S, T, W> CreateILexicalWriter<S, T, W>()
            where S : class, IEnumerable<T>
            where T : class,ILexical
            where W : XmlWriter {
            return new SimpleLexicalSerializer(OUTPUT_FILE_PATH) as ILexicalWriter<S, T, W>;
        }

        /// <summary>
        ///A test for Write
        ///</summary>
        public void WriteTestHelper<S, T, W>()
            where S : class, IEnumerable<T>
            where T : class, ILexical
            where W : XmlWriter {
            ILexicalWriter<S, T, W> target = CreateILexicalWriter<S, T, W>();
            S elements = (S)Tagger.DocumentFromRaw(new TxtFile(@"..\..\..\TestDocs\cats.txt")).GetAllLexicalConstructs();
            string resultSetTitle = "test xml";
            DegreeOfOutput degreeOfOutput = DegreeOfOutput.Comprehensive;

            target.Write(elements, resultSetTitle, degreeOfOutput);
            using (var reader = new System.IO.StreamReader(OUTPUT_FILE_PATH)) {
                var xd = XDocument.Load(reader);
                var xml = xd.Descendants().DescendantsAndSelf()
                    .Where(e => e.FirstAttribute != null).Select(e => new { Name = e.Name, TextAttribute = e.FirstAttribute.Value }).AsParallel();
                var result = elements.AsParallel().All(lex =>
                        xml.Any(node => lex.Type.Name == node.Name && lex.Text == node.TextAttribute));
                Assert.IsTrue(result);
                Assert.IsTrue(xd.Root.Attribute("Title").Value == resultSetTitle);
            }
        }

        [TestMethod()]
        public void WriteTest() {

            WriteTestHelper<IEnumerable<ILexical>, ILexical, XmlWriter>();


        }
        /// <summary>
        ///A test for WriteAsync
        ///</summary>
        public async void WriteAsyncTestHelper<S, T, W>()
            where S : class,IEnumerable<T>
            where T : class,ILexical
            where W : XmlWriter {
            ILexicalWriter<S, T, W> target = CreateILexicalWriter<S, T, W>();
            S elements = (S)Tagger.DocumentFromRaw(new TxtFile(@"..\..\..\TestDocs\cats.txt")).GetAllLexicalConstructs();
            string resultSetTitle = "test xml";
            DegreeOfOutput degreeOfOutput = DegreeOfOutput.Comprehensive;

            await target.WriteAsync(elements, resultSetTitle, degreeOfOutput);
            using (var reader = new System.IO.StreamReader(OUTPUT_FILE_PATH)) {
                var xd = XDocument.Load(reader);
                var xml = xd.Descendants().DescendantsAndSelf()
                    .Where(e => e.FirstAttribute != null).Select(e => new { Name = e.Name, TextAttribute = e.FirstAttribute.Value }).AsParallel();
                var result = elements.AsParallel().All(lex =>
                        xml.Any(node => lex.Type.Name == node.Name && lex.Text == node.TextAttribute));
                Assert.IsTrue(result);
                Assert.IsTrue(xd.Root.Attribute("Title").Value == resultSetTitle);
            }
        }

        [TestMethod()]
        public void WriteAsyncTest() {
            WriteAsyncTestHelper<IEnumerable<ILexical>, ILexical, XmlWriter>();
        }

    }
}
