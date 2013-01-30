using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.FileSystem;
using SharpNLPTaggingModule;

namespace Aluan_Experimentation
{
    class Program
    {
        static void Main(string[] args) {

            var verbThesaurus = new VerbThesaurus();
            verbThesaurus.Load();
            while (true) {
                Console.Write("enter a word: ");
                var input = Console.ReadLine().Trim();
                if (input == "-1")
                    break;
                foreach (var S in verbThesaurus[input])
                    Console.WriteLine(S);
            }
        }
    }
}
