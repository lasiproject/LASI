using LASI.ContentSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace LASI.Core.Tests
{
    
    
    /// <summary>
    ///This is a test class for FileConverterTest and is intended
    ///to contain all FileConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FileConverterTest
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
        ///A test for ConvertFile
        ///</summary>
        public void ConvertFileTestHelper<TSource, TDestination>()
            where TSource : InputFile
            where TDestination : InputFile {
            FileConverter<TSource, TDestination> target = CreateFileConverter<TSource, TDestination>(); // TODO: Initialize to an appropriate value
            TDestination expected = default(TDestination); // TODO: Initialize to an appropriate value
            TDestination actual;
            actual = target.ConvertFile();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        internal virtual FileConverter<TSource, TDestination> CreateFileConverter<TSource, TDestination>()
            where TSource : InputFile
            where TDestination : InputFile {
            // TODO: Instantiate an appropriate concrete class.
            FileConverter<TSource, TDestination> target = null;
            return target;
        }

        [TestMethod()]
        public void ConvertFileTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TSo" +
                    "urce. Please call ConvertFileTestHelper<TSource, TDestination>() with appropriat" +
                    "e type parameters.");
        }

        /// <summary>
        ///A test for ConvertFileAsync
        ///</summary>
        public void ConvertFileAsyncTestHelper<TSource, TDestination>()
            where TSource : InputFile
            where TDestination : InputFile {
            FileConverter<TSource, TDestination> target = CreateFileConverter<TSource, TDestination>(); // TODO: Initialize to an appropriate value
            Task<TDestination> expected = null; // TODO: Initialize to an appropriate value
            Task<TDestination> actual;
            actual = target.ConvertFileAsync();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ConvertFileAsyncTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TSo" +
                    "urce. Please call ConvertFileAsyncTestHelper<TSource, TDestination>() with appro" +
                    "priate type parameters.");
        }
    }
}
