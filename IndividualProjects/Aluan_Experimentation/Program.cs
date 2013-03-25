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

        static string testText = @"Each year more than 2,500 people die and 12,600 are injured in home fires in the United States, with direct property loss due to home fires estimated at $7.3 billion annually.  Home fires can be prevented!

To protect yourself, it is important to understand the basic characteristics of fire. Fire spreads quickly; there is no time to gather valuables or make a phone call. In just two minutes, a fire can become life-threatening. In five minutes, a residence can be engulfed in flames.

Heat and smoke from fire can be more dangerous than the flames. Inhaling the super-hot air can sear your lungs. Fire produces poisonous gases that make you disoriented and drowsy. Instead of being awakened by a fire, you may fall into a deeper sleep. Asphyxiation is the leading cause of fire deaths, exceeding burns by a three-to-one ratio.";


    }
}