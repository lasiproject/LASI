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


        static string testPath = @"C:\Users\Aluan\Desktop\411writtensummary2.txt";

        static void Main(string[] args) {

            Thesaurus.VerbThesaurusLoadTask.Wait();
            Output.WriteLine(Thesaurus.LookupVerb("fuck").OrderBy(f => f).Format(70));
            //var results = new[] { Thesaurus.LookupVerb("fuck").OrderBy(s=>s).ToArray(), 
            //    Thesaurus.LookupVerb("fucks").OrderBy(s=>s).ToArray(),
            //    Thesaurus.LookupVerb("fucked").OrderBy(s=>s).ToArray(),
            //    Thesaurus.LookupVerb("fucking").OrderBy(s=>s).ToArray() };
            //var test = true;

            //for (int j = 0; j < results[0].Length; j++) {
            //    test &= results[0][j] == results[1][j] && results[0][j] == results[2][j] && results[0][j] == results[3][j];

            //}
            //Output.WriteLine(test);
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