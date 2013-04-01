using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.Thesauri;
using System;
using System.Linq;
using LASI.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using LASI.Utilities.TypedSwitch;
namespace Aluan_Experimentation
{
    public class Program
    {










        static string tagtest = @"I enjoy jumping whereas he enjoys climbing.";

        static string testPath = @"C:\Users\Aluan\Desktop\test1.txt";

        static void Main(string[] args) {

            //TestWordAndPhraseBindings();

            TestThesaurus();

            StdIO.WaitForKey();
        }

        private static void TestWordAndPhraseBindings() {
            var doc = TaggerUtil.LoadTextFileAsync(new LASI.FileSystem.FileTypes.TextFile(testPath)).Result;

            PerformIntraPhraseBinding(doc);
            PerformSVOBinding(doc);

            foreach (var r in doc.Phrases) {
                print(r);
            }
        }

        private static void PerformSVOBinding(Document doc) {
            foreach (var s in doc.Sentences) {
                var subjectBinder = new SubjectBinder();
                var objectBinder = new ObjectBinder();
                try {
                    subjectBinder.Bind(s);
                }
                catch (NullReferenceException) {
                }
                try {
                    objectBinder.Bind(s);
                }
                catch (InvalidStateTransitionException) {
                }
                catch (VerblessPhrasalSequenceException) {
                }
            }
        }

        private static void PerformIntraPhraseBinding(Document doc) {
            foreach (var r in doc.Phrases) {
                var wordBinder = new InterPhraseWordBinding();
                new Switch(r)
                .Case<NounPhrase>(np => {
                    wordBinder.IntraNounPhrase(np);
                })
                .Case<VerbPhrase>(vp => {
                    wordBinder.IntraVerbPhrase(vp);
                })
                .Default(a => {
                });
            }
        }




        private static void TestThesaurus() {
            ThesaurusManager.LoadAll();
            print("enter noun: ");
            for (var k = Console.ReadLine(); ; ) {
                try {
                    print(ThesaurusManager.NounThesaurus[k].OrderBy(o => o).Aggregate("", (aggr, s) => s.PadRight(30) + ", " + aggr));
                }
                catch (ArgumentNullException) {
                    print("no synonyms returned");
                }
                print("enter noun: ");
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
                nounBinder.IntraNounPhrase(phrase);
            foreach (var sentence in doc.Sentences) {
                foreach (var phrase in sentence.Phrases)
                    print(phrase);
                print("\n");
            }

        }

        static void print(object o) {
            Console.WriteLine(o);
        }



    }
}