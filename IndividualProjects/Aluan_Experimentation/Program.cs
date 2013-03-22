using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.Thesauri;
using LASI.Utilities;
using System;
using System.Collections.Generic;
namespace Aluan_Experimentation
{
    public class Program
    {
        //private static string textFilePath = @"C:\Users\Aluan\Desktop\sec2-2.txt";
        //private static string docxFilePath = @"C:\Users\Aluan\Desktop\sec2-2.docx";
        //private static string taggedFilePath = @"C:\Users\Aluan\Desktop\sec2-2.tagged";
        static string testSentence = @"He ordered the fifth infantry unit under his command to attack at dawn.";

        static void Main(string[] args) {
            var docString = TaggerUtil.TagString(testSentence);
       
            BindAll(TaggerUtil.TaggedToDoc(docString));
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