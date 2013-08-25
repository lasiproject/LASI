using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.Lookup;
using LASI.Algorithm.Weighting;
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

            var doc = Tagger.DocumentFromRaw(new DocXFile(@"C:\Users\Aluan\Desktop\documents\C++_for _LASI.docx"));
            Binder.Bind(doc);



            Console.WriteLine(doc.Phrases.Format(w => '\n' + w.ToString()));
            Input.WaitForKey();
        }
    }
}
