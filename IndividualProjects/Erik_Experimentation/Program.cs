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
using System.Text.RegularExpressions;
using LASI.Utilities.TypedSwitch;
using LASI.Algorithm.DocumentConstructs;


namespace Erik_Experimentation
{
    class Program
    {

        static void Main(string[] args) {

            //var nounTest2 = new NounThesaurus(@"..\..\..\..\WordNetThesaurusData\data.noun");
            //nounTest2.Load();
            //string key = "man";
            //HashSet<string> synonyms2;
            //synonyms2 = nounTest2.SearchFor(key);
            //foreach (var s in synonyms2)
            //{
            //    Console.WriteLine(s);
            //}

            //var adjTest = new AdjectiveThesaurus(@"..\..\..\..\WordNetThesaurusData\data.adj");
            //adjTest.Load();
            //string key = "able";
            //adjTest.SearchFor(key);

            //var advTest = new AdverbThesaurus(@"..\..\..\..\WordNetThesaurusData\data.adv");
            //advTest.Load();
            //string key = "vastly";
            //advTest.SearchFor(key);


            //Keeps the console window open until the escape key is pressed
            //Console.WriteLine("Press escape to exit");
            //for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey())
            //{
            //    Console.WriteLine("Press escape to exit");
            //}

            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\CynosureEPR\Desktop\weight2.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).LoadParagraphs();
            var document = new Document(paragraphs);

            //foreach (var i in document.Phrases)
            //{
            //    Console.WriteLine(i);
            //}

            //foreach (var i in document.Words)
            //{
            //    Console.WriteLine(i);
            //}



        }
    }
}
