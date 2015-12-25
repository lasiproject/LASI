using NFluent;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for PastParticipleVerbTest and is intended
    ///to contain all PastParticipleVerbTest Unit Tests
    /// </summary>
    public class PastParticipleVerbTest
    {
        /// <summary>
        ///A test for PastParticipleVerb Constructor
        /// </summary>
        [Fact]
        public void PastParticipleVerbConstructorTest()
        {
            string text = "abided";
            PastParticiple target = new PastParticiple(text);
            Check.That(target.Text).IsEqualTo(text);
        }
    }
}
