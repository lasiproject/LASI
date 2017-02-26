using NFluent;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for InterjectionTest and is intended
    ///to contain all InterjectionTest Unit Tests
    /// </summary>
    public class InterjectionTest
    {
        /// <summary>
        /// A test for Interjection Constructor
        /// </summary>
        [Fact]
        public void InterjectionConstructorTest()
        {
            var text = "ha";
            var target = new Interjection(text);
            Check.That(target.Text).IsEqualTo(text);
        }
    }
}
