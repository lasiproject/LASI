using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.ComparativeHeuristics;
using LASI.Algorithm.ComparativeHeuristics.Morphemization;
using LASI.ContentSystem;
using System;
using LASI.Algorithm.Patternization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.DocumentStructures;
using LASI.Utilities;

namespace LASI.Experimentation.CommandLine
{
    using VerbalsSet = System.Collections.Generic.IEnumerable<IVerbal>;
     class Program
    {
        static void Main(string[] args) {
            

            var aboutCats = Tagger.DocumentFromRaw(new TextFile(@"C:\Users\dpatrick\Documents\lasipage.txt"));
            Binder.Bind(aboutCats);
            Weighter.Weight(aboutCats);
            
            foreach (var i in aboutCats.Words)
            {
                Console.WriteLine(i);
            }


            Input.WaitForKey();





        }
    }
}
