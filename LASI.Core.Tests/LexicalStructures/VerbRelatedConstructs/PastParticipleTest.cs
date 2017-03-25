using NFluent;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for PastPrtcplVerbTest and is intended
    ///to contain all PastPrtcplVerbTest Unit Tests
    /// </summary>
    public class PastParticipleTest
    {
        /// <summary>
        ///A test for PastPrtcplVerb Constructor
        /// </summary>
        [Fact]
        public void PastPrtcplVerbConstructorTest()
        {
            var text = "gone";
            var target = new PastParticiple(text);
            Check.That(target.Text).IsEqualTo(text);
        }
    }
}
