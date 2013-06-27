using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Thesauri;
using LASI.Utilities;
using LASI.Utilities.TypedSwitch;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Aluan_Experimentation
{
    public class Program
    {


        static string testPath = @"C:\Users\Aluan\Desktop\.txt\411writtensummary2.txt";

        static void Main(string[] args) {
            Thesaurus.NounThesaurusLoadTask.Wait();
            Output.WriteLine(Thesaurus.LookupNoun("spade").Format());


            //var doc = TaggerUtil.LoadTextFile(new LASI.FileSystem.FileTypes.TextFile(testPath));
            //foreach (var s in doc.Sentences) {

            //    var relevantElements = from w in s.Words
            //                           where w is Adjective || w is Verb || w is Noun
            //                           select w;

            //    Output.WriteLine(relevantElements.Format(w => w.Type.Name, 70));
            //    Output.WriteLine();
            //    Output.WriteLine(s.Words.Format(w => w.Type.Name, 70));
            //    Output.WriteLine(s.Text);

            //    Output.WriteLine("\n\n");
            //}
            Input.WaitForKey();
        }



















        private static void TestWordAndPhraseBindings() {
            var doc = TaggerUtil.LoadTextFile(new LASI.FileSystem.FileTypes.TextFile(testPath));

            new PronounBinder().Bind(doc);
            foreach (var p in doc.Phrases.GetPronounPhrases())
                Output.WriteLine(p);

            PerformAttributeNounPhraseBinding(doc);

            PrintDocument(doc);
        }



        private static void PrintDocument(Document doc) {
            foreach (var r in doc.Phrases) {
                Output.WriteLine(r);
                foreach (var w in r.Words)
                    Output.WriteLine(w);
            }
        }
        private static void PerformAttributeNounPhraseBinding(Document doc) {
            foreach (var s in doc.Sentences) {
                var attributiveBinder = new AttributiveNounPhraseBinder(s);
            }
        }


    }
}