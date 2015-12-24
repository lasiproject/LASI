using Xunit;
using NFluent;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for PastTenseVerbTest and is intended
    ///to contain all PastTenseVerbTest Unit Tests
    /// </summary>
    public class PastTenseVerbTest
    {
        /// <summary>
        ///A test for PastTenseVerb Constructor
        /// </summary>
        [Fact]
        public void PastTenseVerbConstructorTest()
        {
            string text = "had";
            PastTenseVerb target = new PastTenseVerb(text);
            Check.That(target.Text).IsEqualTo(text);
        }

    }
}
