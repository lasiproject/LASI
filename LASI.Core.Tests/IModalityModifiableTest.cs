using NFluent;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for IModalityModifiableTest and is intended
    ///to contain all IModalityModifiableTest Unit Tests
    /// </summary>
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
        [Fact]
        public void ModalityTest()
        {
            var target = CreateIModalityModifiable();
            var expected = new ModalAuxilary("might");
            ModalAuxilary actual;
            target.Modality = expected;
            actual = target.Modality;
            Check.That(expected).IsEqualTo(actual);
        }


    }
}
