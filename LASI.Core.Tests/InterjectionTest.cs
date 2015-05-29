using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for InterjectionTest and is intended
    ///to contain all InterjectionTest Unit Tests
    /// </summary>
    [TestClass]
    public class InterjectionTest
    {
        /// <summary>
        /// A test for Interjection Constructor
        /// </summary>
        [TestMethod]
        public void InterjectionConstructorTest()
        {
            string text = "ha";
            Interjection target = new Interjection(text);
            Assert.AreEqual(target.Text, text);
        }
    }
}
