using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.RelationshipLookups;
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

            var doc = TaggerUtil.LoadTextFile(new LASI.FileSystem.FileTypes.TextFile(testPath));
            TestRelationshipTable(doc);


            Input.WaitForKey();
        }

        private static void TestRelationshipTable(Document doc) {
            var lookup = new SampleRelationshipLookup(doc);

            Noun n1 = new GenericPluralNoun("cats"), n2 = new GenericPluralNoun("dogs");
            n1.SetRelationshipLookup(lookup);
            Verb relator = new Verb("hate", VerbTense.Base);

            Output.WriteLine(n1.IsRelatedTo(n2).On(relator) ? "success" : "needs tweaking");


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