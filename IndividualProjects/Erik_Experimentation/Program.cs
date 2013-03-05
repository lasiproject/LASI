using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.FileSystem;
using LASI.Utilities;
using SharpNLPTaggingModule;
using System.IO;

namespace Erik_Experimentation
{
    class Program
    {
        class SynSet
        {
            public string setId;
            public List<string> words = new List<string>();
            public List<string> referencedCodes = new List<string>();
        }
        static void Main(string[] args) {

            Word w = null;
            List<SynSet> synsets = new List<SynSet>();


            var results = from sn in synsets
                          where sn.words.Contains(w.Text)
                          select sn.referencedCodes;
            var referencedFlat = from R in results
                                 from r in R
                                 select r;




            //Keeps the console window open until the escape key is pressed
            Console.WriteLine("Press escape to exit");
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
                Console.WriteLine("Press escape to exit");
            }
        }
    }
}
