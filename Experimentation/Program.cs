using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.LexicalLookup;
using LASI.Algorithm.LexicalLookup.Morphemization;
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
    using LASI.Algorithm.Weighting;
    class Program
    {
        static void Main(string[] args)
        {
            Output.SetToFile(@"C:\Users\Aluan\Desktop\log1.txt");
            Word.VerboseOutput = true;
            Phrase.VerboseOutput = true;
            Task.WaitAll(Lookup.GetLoadingTasks().ToArray());



            var aboutCats = Tagger.DocumentFromRaw(new TextFile(@"C:\Users\Aluan\Desktop\documents\ducks.txt"));
            Binder.Bind(aboutCats);
            //Weighter.Weight(aboutCats);


            foreach (var word in aboutCats.Words.OfType<Symbol>()) { Console.WriteLine(word); }



            //Func<Document, IEntity, VerbalsSet> associatedVerbs =
            //    (d, e) => d.GetActions().WithSubjectOrObject(subOrObj => subOrObj.IsSimilarTo(e));

            //var results = associatedVerbs(aboutCats, new CommonSingularNoun("feline"));
            //foreach (var v in results.Select(v => v.Subjects.Format(s => s.Text) + " -> " + v.Text + " -> " + v.DirectObjects.Format(s => s.Text) + " -> " + v.IndirectObjects.Format(s => s.Text))) { Console.WriteLine(v); }







            Input.WaitForKey();





        }
    }
}
