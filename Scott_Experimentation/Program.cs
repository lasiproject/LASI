using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.FileSystem;
using SharpNLPTaggingModule;
using LASI.Utilities;

namespace Scott_Experimentation
{
    class Program
    {
        static void Main(string[] args) {
            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Scott\Desktop\TestSentences.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).GetParagraphs();
            var document = new Document(paragraphs);
            List<Word> ListOfWords = (List<Word>)document.Words;
            List<Phrase> ListOf
            //var WordStrings = (from w in ListOfWords where w is Noun select w.Type).ToList();

            for (int x = 0; x < ListOfWords.Count; x++)
            {
                if(ListOfWords[x] is Verb)
                    Console.WriteLine("{0}: {1}", x, ListOfWords[x].Text);
            }


           StdIoUtil.WaitForAnyKey();
        }
    }
}
