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



            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\CynosureEPR\Desktop\weight2.txt");
            var tagger2 = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\CynosureEPR\Desktop\weight3.txt");
            var tagged = tagger.ProcessFile();
            var tagged2 = tagger2.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).LoadParagraphs();
            var document = new Document(paragraphs);
            var paragraphs2 = new TaggedFileParser(tagged2).LoadParagraphs();
            var document2 = new Document(paragraphs2);

            List<Document> documentsList = new List<Document>();

            documentsList.Add(document);
            documentsList.Add(document2);

            int numDocs = documentsList.Count();

            foreach (Word w in document.Words)
            {
                Console.WriteLine(w.Text);
            }

            //for (int i = 0; i < documentsList.Count(); i++)
            //{
            //    Document currentDoc = documentsList[i];
            //    List<Word> currentWords = currentDoc.Words.ToList();

            //    foreach (Word w in currentWords)
            //    {
            //        foreach (Word w1 in currentWords)
            //        {
            //            if (w.Text == w1.Text)
            //            {
            //                w1.FrequencyCurrent += 1;
            //                w1.FrequencyAcross += w1.FrequencyCurrent;
            //            }
            //        }


                    

            //    }



            //}

            



            //Keeps the console window open until the escape key is pressed
            Console.WriteLine("Press escape to exit");
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey())
            {
                Console.WriteLine("Press escape to exit");
            }



            

        }
    }
}
