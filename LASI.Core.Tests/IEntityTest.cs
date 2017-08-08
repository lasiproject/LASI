using NFluent;
using Xunit;

namespace LASI.Core.Tests
{
    /// <summary>
    /// This is a test class for IEntityTest and is intended to contain all IEntityTest Unit Tests
    /// </summary>
    public class IEntityTest
    {
        /// <summary>
        ///A test for EntityKind
        /// </summary>
        [Fact]
        public void EntityKindTest()
        {
            IEntity target = new CommonSingularNoun("cat");

            Check.That(target.EntityKind).IsEqualTo(EntityKind.Thing);
        }
    }
}