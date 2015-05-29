using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for IModalityModifiableTest and is intended
    ///to contain all IModalityModifiableTest Unit Tests
    /// </summary>
    [TestClass]
    public class IModalityModifiableTest
    {
        internal virtual IModalityModifiable CreateIModalityModifiable()
        {
            IModalityModifiable target = new BaseVerb("laugh");
            return target;
        }

        /// <summary>
        ///A test for Modality
        /// </summary>
        [TestMethod]
        public void ModalityTest()
        {
            IModalityModifiable target = CreateIModalityModifiable();
            ModalAuxilary expected = new ModalAuxilary("might");
            ModalAuxilary actual;
            target.Modality = expected;
            actual = target.Modality;
            Assert.AreEqual(expected, actual);
        }


    }
}
