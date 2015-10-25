using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.Core.Tests
{
    using Test = TestMethodAttribute;
    /// <summary>
    ///This is a test class for ExistentialTest and is intended
    ///to contain all ExistentialTest Unit Tests
    /// </summary>
    [TestClass]
    public class ExistentialTest
    {
        /// <summary>
        ///A test for Existential Constructor
        /// </summary>
        [Test]
        public void ExistentialConstructorTest()
        {
            string text = "there";
            Existential target = new Existential(text);
            Assert.AreEqual(target.Text, text);
        }

    }
}
