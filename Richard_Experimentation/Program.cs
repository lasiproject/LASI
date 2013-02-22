using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.Utilities;
using LASI.FileSystem;
using SharpNLPTaggingModule;

namespace Richard_Experimentation
{
    class Program
    {
        static void Main(string[] args) {

            //TagExampleFile();

            Action<IEnumerable<int>> printList = L => {
                foreach (var i in L)
                    Console.WriteLine(i);
            };




            Console.WriteLine("enter divisor");

            int divisor = int.Parse(Console.ReadLine());

            var myInts = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };


            var results = from i in myInts
                          where i % divisor == 0
                          select i;


            printList(results);



            //Keeps the console window open until the escape key is pressed
            StdIO.WaitForKey(ConsoleKey.Escape);
        }


        static void TagExampleFile() {

            //Put your windows account name here
            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Richard\Desktop\intest1.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).ConstructParagraphs();
            var document = new Document(paragraphs);

            var para2 = from p in document.Paragraphs
                        select p;
            foreach (var p in para2) {
                var phgrs = from sent in p.Sentences
                            from r in sent.Phrases
                            select r;

                foreach (var r in phgrs) {
                    Console.WriteLine(r);
                }

            }
        }
    }
}
