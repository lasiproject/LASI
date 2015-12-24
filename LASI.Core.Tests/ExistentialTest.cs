using Xunit;
using NFluent;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is a test class for ExistentialTest and is intended
    ///to contain all ExistentialTest Unit Tests
    /// </summary>
    public class ExistentialTest
    {
        /// <summary>
        ///A test for Existential Constructor
        /// </summary>
        [Fact]
        public void ExistentialConstructorTest()
        {
            string text = "there";
            Existential target = new Existential(text);
            Check.That(target.text).IsEqualTo(text);
        }

    }
}
