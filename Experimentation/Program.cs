using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.Lookup;
using LASI.Algorithm.Lookup.Morphemization;
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
            LexicalLookup.LoadAllData();

            ISet<string> allNouns = LexicalLookup.NounStringDictionary;
            var doc = Tagger.DocumentFromPDF(new PdfFile(@"C:\Users\Dustin\Documents\Was Hitler a Darwinian.pdf"));

            foreach (var p in doc.Words.GetProperNouns())
            {
                if(
            }
            Console.ReadKey();
        }

    }
}
