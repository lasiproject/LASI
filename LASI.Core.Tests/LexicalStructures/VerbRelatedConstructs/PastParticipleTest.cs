using NFluent;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for PastPrtcplVerbTest and is intended
    ///to contain all PastPrtcplVerbTest Unit Tests
    /// </summary>
    public class PastPrtcplVerbTest
    { 
        /// <summary>
        ///A test for PastPrtcplVerb Constructor
        /// </summary>
        [Fact]
        public void PastPrtcplVerbConstructorTest()
        {
            string text = "gone";
            PastParticiple target = new PastParticiple(text);
            Check.That(target.Text).IsEqualTo(text);
        }
    }
}
