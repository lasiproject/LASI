using LASI.Core;
using LASI.Core.Binding;
using LASI.Core.Heuristics;
using LASI.Core.Heuristics.Morphemization;
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

         var d = doc.GetEntities().FirstOrDefault();
        var k = d.Match()
            .Yield<string>()
            ._((IEntity e) => e.Text)
            .Result();
        Output.WriteLine(doc);

        Output.WriteLine(0.To(10).Format());

        Input.WaitForKey();
    }
}

