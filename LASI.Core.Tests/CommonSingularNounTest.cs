using NFluent;
using Xunit;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is A test class for GenericSingularNounTest and is intended
    ///to contain all GenericSingularNounTest Unit Tests
    /// </summary>
    public class CommonSingularNounTest
    {
        /// <summary>
        /// A test for CommonSingularNoun Constructor
        /// </summary>
        [Fact]
        public void CommonSingularNounConstructorTest()
        {
            string text = "cat";
            CommonSingularNoun target = new CommonSingularNoun(text);
            Check.That(target.Text).IsEqualTo(text);
        }
    }
}