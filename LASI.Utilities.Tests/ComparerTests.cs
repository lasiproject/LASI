using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using NFluent;

namespace LASI.Utilities.Tests
{
    using Test = TestMethodAttribute;
    [TestClass]
    public class ComparerTests
    {
        [Test]
        public void EqualityCreatedDeterminesEqualityAsSpecified()
        {
            var comparer = Equality.Create<string>((x, y) => x.EqualsIgnoreCase(y));
            var first = "hello";
            var second = "HELLO";
            Check.That(comparer.Equals(first, second)).IsTrue();
        }

        [Test]
        public void EqualityCreatedWithoutSpecifyingHasherCausesNonNullsToCollide()
        {
            var comparer = Equality.Create<object>((x, y) => x.Equals(y));
            object[] values = { "hello", 123, new object(), (decimal?)1m };
            Check.That(values.Select(comparer.GetHashCode).Distinct()).HasSize(1);
        }

        [Test]
        public void EqualityCreatedWithoutSpecifyingHasherCausesNullsToCollide()
        {
            var comparer = Equality.Create<object>((x, y) => x.Equals(y));
            var values = Enumerable.Repeat<object>(null, 2);
            Check.That(values.Select(comparer.GetHashCode).Distinct()).HasSize(1);
            Check.That(comparer.GetHashCode(string.Empty)).IsNotEqualTo(comparer.GetHashCode(null));
        }

        [Test]
        public void ComparerCreatedWithExplicitHasherComputesEquivalentHashCodes()
        {
            var comparer = Equality.Create<int>((x, y) => x.Equals(y), x => x < 10 ? x : x % 5);
        }
    }
}