using LASI;
using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.ContentSystem;
using LASI.ContentSystem.TaggerEncapsulation;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Brittany_Experimentation
{
    class Program
    {
        static void Main(string[] args) {

            TagExampleFile();

            //Keeps the console window open until the escape key is pressed
            Console.WriteLine("Press escape to exit");
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
                Console.WriteLine("Press escape to exit");
            }
        }


        static void TagExampleFile() {
            var document = Tagger.DocumentFromRaw(new TextFile(@"C:\Brittany\Desktop\intest1.txt"));
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