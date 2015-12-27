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
            string text = "can";
            ModalAuxilary target = new ModalAuxilary(text);
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
            string text = "cannot";
            ModalAuxilary target = new ModalAuxilary(text);
            Check.That(target.Text).IsEqualTo(text);
            Check.That(target.Modifies).IsNull();
        }
    }
}
