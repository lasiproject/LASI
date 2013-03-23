using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.Thesauri;
using LASI.Utilities;
using System;
using System.Linq;
using System.Collections.Generic;
namespace Aluan_Experimentation
{
    public class Program
    {
        //private static string textFilePath = @"C:\Users\Aluan\Desktop\sec2-2.txt";
        //private static string docxFilePath = @"C:\Users\Aluan\Desktop\sec2-2.docx";
        //private static string taggedFilePath = @"C:\Users\Aluan\Desktop\sec2-2.tagged";
        //static string testSentence = @"He ordered the fifth infantry unit under his command to attack at dawn.";

        static void Main(string[] args) {
            print("enter verb: ");
            for (var k = Console.ReadLine(); ; ) {
                try {
                    print(Thesauri.VerbThesaurus[k].OrderBy(o=>o).Aggregate("", (aggr, s) => s.PadRight(30)+", "+aggr));
                } catch (ArgumentNullException) {
                    print("no synonyms returned");
                }
                print("enter verb: ");
                k = Console.ReadLine();
            }


            //var docString = TaggerUtil.TagString(testSentence);

            //BindAll(TaggerUtil.TaggedToDoc(docString));
        }

        static void BindAll(Document doc) {
            var subjectBinder = new SubjectBinder();
            var objectBinder = new ObjectBinder();

            foreach (var sentence in doc.Sentences) {
                subjectBinder.Bind(sentence);
                objectBinder.Bind(sentence);
            }
            foreach (var phrase in doc.Phrases)
                print(phrase);

        }

        static void print(object o) {
            Console.WriteLine(o);
        }
    }
}