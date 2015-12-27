using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LASI.Core.Analysis.Heuristics.WordMorphing;
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
            string root = "walk";
            IEnumerable<string> expected = new[] { "walked", "walks", "walking" }.ToList();
            IEnumerable<string> actual;
            actual = VerbMorpher.GetConjugations(root);
            foreach (var f in actual)
                Debug.WriteLine(f);
            Assert.True((from f in expected
                           select actual.Contains(f)).Aggregate(true, (result, b) => result && b));

        }

        /// <summary>
        ///A test for FindRoot
        /// </summary>
        [Fact]
        public void FindRootTest()
        {
            var conjugated = new[] { "walked", "walking", "walks" };
            List<string> expected = new[] { "walk" }.ToList();
            List<string> actual = new List<string>();
            foreach (var c in conjugated)
            {
                actual.AddRange(VerbMorpher.FindRoots(c));

            }
            Assert.True((from f in expected
                           select actual.Contains(f)).Aggregate(true, (result, b) => result && b));


        }

    }
}
