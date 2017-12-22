using NFluent;
using Xunit;

namespace LASI.Core.Tests
{

    /// <summary>
    ///This is A test class for DeterminerTest and is intended
    ///to contain all DeterminerTest Unit Tests
    /// </summary>
    public class DeterminerTest
    {
        /// <summary>
        ///A test for Determiner Constructor
        /// </summary>
        [Fact]
        public void DeterminerConstructorTest()
        {
            var text = "the";
            var target = new Determiner(text);
            Check.That(target.Text).IsEqualTo(text);
            Check.That(target.Determines).IsNull();
        }

        /// <summary>
        ///A test for Determines
        /// </summary>
        [Fact]
        public void DeterminesTest()
        {
            var text = "the";
            var target = new Determiner(text);
            IEntity expected = new CommonSingularNoun("organization");
            IEntity actual;
            target.Determines = expected;
            actual = target.Determines;
            Check.That(actual).IsEqualTo(expected);
        }

    }
}
