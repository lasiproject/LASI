using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.Thesauri;
using System;
using System.Linq;
using LASI.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Aluan_Experimentation
{
    public class Program
    {
        static void Main(string[] args) {



            TestBinders();
            TestThesaurus();

            StdIO.WaitForKey();
        }

        private static void PrintWords() {
            var doc = TaggerUtil.UntaggedToDoc(testText);
            foreach (var r in doc.Phrases)
                foreach (var w in doc.Words)
                    print(w);
        }

        private static void TestBinders() {
            var docString = TaggerUtil.TagString(testText);
            print(docString);
            BindAll(TaggerUtil.TaggedToDoc(docString));
        }

        private static async void TestThesaurus() {
            await ThesaurusManager.LoadAllAsync();
            print("enter verb: ");
            for (var k = Console.ReadLine(); ; ) {
                try {
                    print(ThesaurusManager.VerbThesaurus[k].OrderBy(o => o).Aggregate("", (aggr, s) => s.PadRight(30) + ", " + aggr));
                } catch (ArgumentNullException) {
                    print("no synonyms returned");
                }
                print("enter verb: ");
                k = Console.ReadLine();
            }
        }

        static void BindAll(Document doc) {

            var objectBinder = new ObjectBinder();

            foreach (var sentence in doc.Sentences) {
                new SubjectBinder().Bind(sentence);
                objectBinder.Bind(sentence);
            }
            var nounBinder = new LASI.Algorithm.Binding.InterPhraseWordBinding();
            foreach (var phrase in doc.Phrases.GetNounPhrases())
                nounBinder.InterNounPhrase(phrase);
            foreach (var sentence in doc.Sentences) {
                foreach (var phrase in sentence.Phrases)
                    print(phrase);
                print("\n");
            }

        }

        static void print(object o) {
            Console.WriteLine(o);
        }

        static string testText = @"He who must not be named gave my friend a red speckled dog quickly.";


    }
}