using NFluent;
using Xunit;

namespace LASI.Core.Tests
{
    public class IPrepositionLinkableTest
    {
        /// <summary>
        ///A test for PrepositionOnLeft
        /// </summary>
        [Fact]
        public void PrepositionOnLeftTest()
        {
            IPrepositionLinkable target = new CommonSingularNoun("bacon");
            IPrepositional prepositional = new Preposition("with");
            target.BindLeftPrepositional(prepositional);

            Check.That(target.LeftPrepositional).IsEqualTo(prepositional);
        }

        /// <summary>
        ///A test for PrepositionOnRight
        /// </summary>
        [Fact]
        public void PrepositionOnRightTest()
        {
            IPrepositionLinkable target = new CommonSingularNoun("bacon");
            IPrepositional prepositional = new Preposition("with");

            target.BindRightPrepositional(prepositional);
            Check.That(target.RightPrepositional).IsEqualTo(prepositional);
        }
    }
}
