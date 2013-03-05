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
            string str = TaggerUtil.TagString("Running quickly through the field, Dustin and Aluan were coding for CS 411.");
            Console.WriteLine(str);
            Document doc = TaggerUtil.TaggedToDoc(str);

            for (; ; ) {
                SubjectBinder s = new SubjectBinder();
                s.bind(doc);
                s.display();
                StdIO.WaitForKey();
            }
            //Console.WriteLine("Press escape to exit");
            //for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
            //Console.WriteLine("Press escape to exit");
            //}
        }
    }
}
