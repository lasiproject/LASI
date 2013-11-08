using LASI.Core;
using LASI.Core.Binding;
using LASI.Core.ComparativeHeuristics;
using LASI.Core.ComparativeHeuristics.Morphemization;
using LASI.ContentSystem;
using System;
using LASI.Core.Patternization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.DocumentStructures;
using LASI.Utilities;

namespace LASI.Experimentation.CommandLine
{
    using VerbalsSet = System.Collections.Generic.IEnumerable<IVerbal>;


}
class Program
{

    static void Main(string[] args) {
        var doc = Tagger.DocumentFromRaw(new[] { "Hello there you fool." });

        var d = doc.GetEntities().First();
        var k = d.Match()
            .Yield<string>()
            .Case((IEntity e) => e.Text)
            .Result();

        Func<int, int> square = x => x * x;

        var simplePrime =
           from x in square
           select x + x % 2 + 1;
        var primes =
            from i in Enumerable.Range(0, 4)
            select string.Format("{0} => {1}", i, simplePrime(i));

        primes.ToList().ForEach(Console.WriteLine);

        Input.WaitForKey();
    }
}

