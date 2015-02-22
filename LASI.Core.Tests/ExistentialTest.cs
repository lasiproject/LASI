using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for ExistentialTest and is intended
    ///to contain all ExistentialTest Unit Tests
    /// </summary>
    [TestClass]
    public class ExistentialTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }


        /// <summary>
        ///A test for Existential Constructor
        /// </summary>
        [TestMethod]
        public void ExistentialConstructorTest()
        {
            string text = "there";
            Existential target = new Existential(text);
            Assert.AreEqual(target.Text, text);
        }

    }
}
