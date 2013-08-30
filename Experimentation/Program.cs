using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.Lookup;
using LASI.Algorithm.Weighting;
using LASI.ContentSystem;
using System;
using LASI.Algorithm.Patternization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Experimentation.CommandLine
{
    class Program
    {
        static void Main(string[] args) {
            Word.VerboseOutput = true;
            Phrase.VerboseOutput = true;
            LexicalLookup.GetUnstartedLoadingTasks().AsParallel().ForAll(t => t.Wait());

            var doc = Tagger.DocumentFromRaw(new DocXFile(@"C:\Users\Aluan\Desktop\documents\C++_for _LASI.docx"));
            Binder.Bind(doc);

           

            Console.WriteLine(doc.Phrases.Format(w => '\n' + w.ToString()));
            Input.WaitForKey();
        }


        int NumberOfSimilarWords(LASI.Algorithm.DocumentConstructs.Document doc, Noun find) {


            var matches = from word in doc.Words
                          where Matching.From(word).To<bool>()
                          .With<Noun>(n => n.IsSynonymFor(find))
                          .With<IPronoun>(p => p.RefersTo.IsSimilarTo(find))
                          .Result()
                          select word;
            return matches.Count();
        }

    }
}
