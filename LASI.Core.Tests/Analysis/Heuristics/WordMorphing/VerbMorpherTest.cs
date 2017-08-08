using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LASI.Core.Analysis.Heuristics.WordMorphing;
using LASI.Utilities;
using NFluent;
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
            IEnumerable<string> expected = new[] { "walked", "walks", "walking" };
            IEnumerable<string> actual;
            actual = VerbMorpher.GetConjugations(root);
            foreach (var f in actual)
            {
                Debug.WriteLine(f);
            }
            foreach (var e in expected)
            {
                Check.That(e).Contains(actual);
            }
        }

        /// <summary>
        ///A test for FindRoot
        /// </summary>
        [Fact]
        public void FindRootTest()
        {
            var conjugated = new[] { "walked", "walking", "walks" };
            var expected = new[] { "walk" }.ToList();
            var actual = new List<string>();
            foreach (var c in conjugated)
            {
                actual.AddRange(VerbMorpher.FindRoots(c));
            }
            Assert.True(expected.Select(actual.Contains).All());

        }
    }
}
