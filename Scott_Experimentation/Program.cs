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


            for (var x = 0; x < 1/*document.Words.Count()*/; x++) {
                for (var y = 0; y < document.Sentences.ElementAt(x).GetWordCount(); y++) {
                    Console.WriteLine("y: {0}, {1}", y, document.Sentences.ElementAt(x).Words.ElementAt(y));
                }
                Console.WriteLine("\n****************** end of sentence ****************\n");
            }
            

            StdIO.WaitForAnyKey();
        }
    }
}
