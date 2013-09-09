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
            foreach (var noun in LexicalLookup.NounStringDictionary) {
                Console.Write(noun + ", ");
            }
            Console.WriteLine();
            Input.WaitForKey();
        }


        int NumberOfSimilarWords(LASI.Algorithm.DocumentConstructs.Document doc, Noun find) {


            var matches = from word in doc.Words
                          where word.Match().Yield<bool>()
                          .Case<Noun>(n => n.IsSynonymFor(find))
                          .Case<IPronoun>(p => p.RefersTo.IsSimilarTo(find))
                          .Result()
                          select word;
            return matches.Count();
        }

    }
}
