using NFluent;
using Xunit;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is a test class for ModalAuxilaryTest and is intended
    ///to contain all ModalAuxilaryTest Unit Tests
    /// </summary>
    public class ModalAuxilaryTest
    {
        /// <summary>
        ///A test for Modifies
        /// </summary>
        [Fact]
        public void ModifiesTest()
        {
            var text = "can";
            var target = new ModalAuxilary(text);
            IModalityModifiable expected = new BaseVerb("do");
            IModalityModifiable actual;
            target.Modifies = expected;
            actual = target.Modifies;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for ModalAuxilary Constructor
        /// </summary>
        [Fact]
        public void ModalAuxilaryConstructorTest()
        {
            var text = "cannot";
            var target = new ModalAuxilary(text);
            Check.That(target.Text).IsEqualTo(text);
            Check.That(target.Modifies).IsNull();
        }
    }
}
