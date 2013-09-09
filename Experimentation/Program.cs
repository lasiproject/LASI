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

            LexicalLookup.NounStringDictionary.GroupBy(s => s[0]).ToList().ForEach(g => Console.WriteLine(g.Format()));
            Input.WaitForKey();
        }

    }
}
