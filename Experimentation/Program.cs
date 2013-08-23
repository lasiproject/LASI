using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.Lookup;
using LASI.ContentSystem;
using System;
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

            var doc = Tagger.DocumentFromRaw(new TextFile(@"C:\Users\Aluan\Desktop\documents\ducks.txt"));
            Binder.Bind(doc);

            Console.WriteLine(doc.Words.Format(w=>'\n'+w.ToString()));
            Input.WaitForKey();
        }
    }
}
