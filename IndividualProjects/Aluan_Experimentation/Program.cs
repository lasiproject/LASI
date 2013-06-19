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
        //static string text = @"You need to kill that mother fucker because he took away the fun.";

        static void Main(string[] args) {
            Thesaurus.NounThesaurusLoadTask.Wait();
            var syns = Thesaurus.LookupNoun("feline");
            foreach (var syn in syns) {
                Output.WriteLine(syn);
            }
            Output.WriteLine(syns.Count());
            Input.WaitForKey();
        }

        private static void GroupingByBehaviorAndKindExample(Document doc) {

        }


        private static IEnumerable<IVerbalSubject> GetActionPerformers(Document doc, IVerbal action, IEntity performer) {
            var doers = from verb in doc.Words.GetVerbs().WithSubject(subject => subject.IsSimilarTo(performer))
                        where verb.IsSimilarTo(action)
                        from actionPerformer in verb.Subjects
                        select actionPerformer;
            return doers;
        }














        private static void TestNounConjugator() {
            NounConjugator conjugator = new NounConjugator(ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "noun.exc");
            Output.WriteLine(conjugator);
            var conjugations = conjugator.GetConjugations("cat");
            var bases = conjugator.TryExtractRoot("women");
            conjugations.ForEach(c => Output.WriteLine(c));
            bases.ForEach(c => Output.WriteLine(c));
        }





        private static void TestWordAndPhraseBindings() {
            var doc = TaggerUtil.LoadTextFile(new LASI.FileSystem.FileTypes.TextFile(testPath));
            var wd = (doc);
            PerformSVOBinding(doc);
            new PronounBinder().Bind(doc);
            foreach (var p in doc.Phrases.GetPronounPhrases())
                Output.WriteLine(p);

            PerformAttributeNounPhraseBinding(doc);






            PrintDocument(doc);
        }

        private static void PerformSVOBinding(Document doc) {
            throw new NotImplementedException();
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