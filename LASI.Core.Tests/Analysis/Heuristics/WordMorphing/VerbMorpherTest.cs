using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LASI.Core.Analysis.Heuristics.WordMorphing;
using LASI.Utilities;
using Xunit;

namespace LASI.Core.Analysis.WordMorphing.Tests
{
    /// <summary>
    ///This is A test class for VerbConjugatorTest and is intended
    ///to contain all VerbConjugatorTest Unit Tests
    /// </summary>
    public class VerbMorpherTest
    {
        /// <summary>
        ///A test for GetLexicalForms
        /// </summary>
        [Fact]
        public void GetConjugationsTest()
        {
            var root = "walk";
            var expected = new[] {"walked", "walks", "walking"};
            var actual = VerbMorpher.GetConjugations(root);
            foreach (var a in actual)
            {
                Debug.WriteLine(a);
            }
            foreach (var e in expected)
            {
                Assert.Contains(actual, a => a == e);
            }
        }

        /// <summary>
        ///A test for FindRoot
        /// </summary>
        [Fact]
        public void FindRootTest()
        {
            var conjugated = new[] {"walked", "walking", "walks"};
            var expected = new[] {"walk"}.ToList();
            var actual = new List<string>();
            foreach (var c in conjugated)
            {
                actual.AddRange(VerbMorpher.FindRoots(c));
            }
            Assert.True((from f in expected
                         select actual.Contains(f)).Aggregate(true, (result, b) => result && b));
        }
    }
}