using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for ModalTest and is intended
    ///to contain all ModalTest Unit Tests
    /// </summary>
    [TestClass]
    public class ModalTest
    { 
        /// <summary>
        ///A test for ModalAuxilary Constructor
        /// </summary>
        [TestMethod]
        public void ModalConstructorTest() {
            string text = "can";
            ModalAuxilary target = new ModalAuxilary(text);
            Assert.IsTrue(target.Modifies == null && target.Text == text);
        }

        /// <summary>
        ///A test for Modifies
        /// </summary>
        [TestMethod]
        public void ModifiesTest() {
            string text = "can";
            ModalAuxilary target = new ModalAuxilary(text);
            IModalityModifiable expected = new BaseVerb("capitulate");
            IModalityModifiable actual;
            target.Modifies = expected;
            actual = target.Modifies;
            Assert.AreEqual(expected, actual);
        }
    }
}
