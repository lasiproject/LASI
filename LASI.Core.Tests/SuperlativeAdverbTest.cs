using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for SuperlativeAdverbTest and is intended
    ///to contain all SuperlativeAdverbTest Unit Tests
    /// </summary>
    public class SuperlativeAdverbTest
    { 
        /// <summary>
        ///A test for SuperlativeAdverb Constructor
        /// </summary>
        [Fact]
        public void SuperlativeAdverbConstructorTest()
        {
            var text = "worthiest";
            var target = new SuperlativeAdverb(text);
            Assert.Equal(text, target.Text);
            Assert.True(target.Modifies == null);
        }
    }
}
