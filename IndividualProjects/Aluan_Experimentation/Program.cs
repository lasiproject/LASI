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












        static string testPath = @"C:\Users\Aluan\Desktop\TestSentences3.txt";

        static void Main(string[] args) {
            Output.SetToDebug();
            TestWordAndPhraseBindings();


            Input.WaitForKey();
        }





        private static void TestWordAndPhraseBindings() {
            var doc = TaggerUtil.LoadTextFileAsync(new LASI.FileSystem.FileTypes.TextFile(testPath)).Result;

            PerformIntraPhraseBinding(doc);
            PerformSVOBinding(doc);

            foreach (var r in doc.Phrases) {
                Output.WriteLine(r);
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
            Output.WriteLine("enter noun: ");
            for (var k = Console.ReadLine(); ; ) {
                try {
                    Output.WriteLine(ThesaurusManager.NounThesaurus[k].OrderBy(o => o).Aggregate("", (aggr, s) => s.PadRight(30) + ", " + aggr));
                }
                catch (ArgumentNullException) {
                    Output.WriteLine("no synonyms returned");
                }
                Output.WriteLine("enter noun: ");
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
                    Output.WriteLine(phrase);
                Output.WriteLine("\n");
            }

        }



    }
}