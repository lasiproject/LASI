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
namespace Dustin_Experimentation
{
    class Program
    {
        static void Main(string[] args) {
            string str = TaggerUtil.TagString("Dustin and Aluan were coding for CS 411.");
            Console.WriteLine(str);
            Document doc = TaggerUtil.TaggedToDoc(str);


            SubjectBinder s = new SubjectBinder();
            foreach (Sentence i in doc.Sentences)
            {
                s.bind(i);
                s.display();
            }
            Console.WriteLine("Press escape to exit");
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey())
            {
                Console.WriteLine("Press escape to exit");
            }
        }
    }
}
