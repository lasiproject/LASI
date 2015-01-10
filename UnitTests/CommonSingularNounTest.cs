using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is A test class for GenericSingularNounTest and is intended
    ///to contain all GenericSingularNounTest Unit Tests
    ///</summary>
    [TestClass]
    public class CommonSingularNounTest
    {
        /// <summary>
        /// A test for CommonSingularNoun Constructor
        ///</summary>
        [TestMethod]
        public void CommonSingularNounConstructorTest() {
            string text = "cat";
            CommonSingularNoun target = new CommonSingularNoun(text);
            Assert.AreEqual(target.Text, text);
        }

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        private TestContext testContextInstance;
    }
}