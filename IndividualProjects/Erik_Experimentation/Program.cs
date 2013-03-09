using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;
using SharpNLPTaggingModule;
using System.IO;
using System.Xml;
using LASI.FileSystem.FileTypes;
using LASI.Algorithm.Binding;
using LASI.Algorithm;
using LASI.FileSystem;
using LASI.Algorithm.Weighting;
using LASI.Algorithm.Thesauri;


namespace Erik_Experimentation
{
    class Program
    {
       
        static void Main(string[] args)
        {

                   var nounTest = new LASI.Algorithm.Thesauri.NounThesaurus(@"..\..\..\..\WordNetThesaurusData\data.noun");
                   nounTest.Load();
                   string key = "give-and-take";
                   nounTest.SearchFor(key);




            //Keeps the console window open until the escape key is pressed
            Console.WriteLine("Press escape to exit");
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
                Console.WriteLine("Press escape to exit");
            }
        }
    }
}
