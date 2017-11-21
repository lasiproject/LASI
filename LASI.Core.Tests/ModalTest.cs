using NFluent;
using Xunit;

namespace LASI.Core.Tests
{

    /// <summary>
    ///This is A test class for ModalTest and is intended
    ///to contain all ModalTest Unit Tests
    /// </summary>
    public class ModalTest
    {
        /// <summary>
        ///A test for ModalAuxilary Constructor
        /// </summary>
        [Fact]
        public void ModalConstructorTest()
        {
            var text = "can";
            var target = new ModalAuxilary(text);
            Check.That(target.Modifies).IsNull();
            Check.That(target.Text).IsEqualTo(text);
        }

        /// <summary>
        ///A test for Modifies
        /// </summary>
        [Fact]
        public void ModifiesTest()
        {
            var text = "can";
            var target = new ModalAuxilary(text);
            IModalityModifiable expected = new BaseVerb("capitulate");
            IModalityModifiable actual;
            target.Modifies = expected;
            actual = target.Modifies;
            Check.That(actual).IsEqualTo(expected);
        }
    }
}
