using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for ModalAuxilaryTest and is intended
    ///to contain all ModalAuxilaryTest Unit Tests
    /// </summary>
    [TestClass]
    public class ModalAuxilaryTest
    { 
        /// <summary>
        ///A test for Modifies
        /// </summary>
        [TestMethod]
        public void ModifiesTest() {
            string text = "can";
            ModalAuxilary target = new ModalAuxilary(text);
            IModalityModifiable expected = new BaseVerb("do");
            IModalityModifiable actual;
            target.Modifies = expected;
            actual = target.Modifies;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ModalAuxilary Constructor
        /// </summary>
        [TestMethod]
        public void ModalAuxilaryConstructorTest() {
            string text = "cannot";
            ModalAuxilary target = new ModalAuxilary(text);
            Assert.IsTrue(target.Text == text);
            Assert.IsTrue(target.Modifies == null);
        }
    }
}
