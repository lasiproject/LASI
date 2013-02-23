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

namespace Scott_Experimentation
{
    class Program
    {
        static void Main(string[] args) {
            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Scott\Desktop\TestSentences.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).LoadParagraphs();
            var document = new Document(paragraphs);

            Word w1 = document.WordAt(5);
            Console.WriteLine("Word at 5: {0}", w1.Text);
            var strng1 = document.WordTextAt(5);
            Console.WriteLine("Word Text at 5: {0}", strng1);

            Sentence sent1 = document.SentenceAt(1);
            Console.WriteLine("Sentence at 1: {0}", sent1.Text);
            string strng2 = document.SentenceTextAt(1);
            Console.WriteLine("Sentence text at 1: {0}", strng2);

            /*
            for (var x = 0; x < document.Words.Count(); x++)
            {
                Console.WriteLine("Word at {0}: {1}", x, document.WordTextAt(x)); 
            }
           */

            StdIO.WaitForAnyKey();
        }
    }
}
