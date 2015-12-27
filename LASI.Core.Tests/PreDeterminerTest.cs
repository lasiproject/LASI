using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for PreDeterminerTest and is intended
    ///to contain all PreDeterminerTest Unit Tests
    /// </summary>
    public class PreDeterminerTest
    { 
        /// <summary>
        ///A test for PreDeterminer Constructor
        /// </summary>
        [Fact]
        public void PreDeterminerConstructorTest() {
            string text = "both";
            PreDeterminer target = new PreDeterminer(text);
            Assert.Equal(text, target.Text);
        }
    }
}
