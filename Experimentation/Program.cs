using LASI.ContentSystem;
using LASI.Core;
using LASI.Core.Binding;
using LASI.Core.DocumentStructures;
using LASI.Core.Heuristics;
using LASI.Core.Heuristics.Morphemization;
using LASI.Core.Patternization;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Experimentation.CommandLine
{
    using VerbalsSet = System.Collections.Generic.IEnumerable<IVerbal>;
    class Program
    {

        static void Main(string[] args) {
            var doc = Tagger.DocumentFromRaw(new[] { "Hello there you fool." });

            var d = doc.GetEntities().FirstOrDefault();
            var k = d.Match()
                .Yield<string>()
                .With((IEntity e) => e.Text)
                .Result();
            Output.WriteLine(doc);

            Output.WriteLine(0.To(10).Format());

            Input.WaitForKey();
        }
    }


}

