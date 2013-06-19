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
            Output.WriteLine(Thesaurus.LookupNoun("bull's-eye").Format());
            Input.WaitForKey();
        }



        //private static IEnumerable<IVerbalSubject> GetActionPerformers(Document doc, IVerbal action, IEntity performer) {
        //    var doers = from verb in doc.Words.GetVerbs().WithSubject(subject => subject.IsSimilarTo(performer))
        //                where verb.IsSimilarTo(action)
        //                from actionPerformer in verb.Subjects
        //                select actionPerformer;
        //    return doers;
        //}














        private static void TestNounConjugator() {

            Output.WriteLine(NounConjugator.GetLexicalForms("cat").Format());
            Output.WriteLine(NounConjugator.GetLexicalForms("woman").Format());
            Output.WriteLine(NounConjugator.GetLexicalForms("banana").Format());
        }





        private static void TestWordAndPhraseBindings() {
            var doc = TaggerUtil.LoadTextFile(new LASI.FileSystem.FileTypes.TextFile(testPath));
            var wd = (doc);

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