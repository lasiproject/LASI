using LASI;
using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.Binding.Experimental;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Lookup;
using LASI.Algorithm.RelationshipLookups;
using LASI.ContentSystem;
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
        static void Main(string[] args) {

            LoadLookup().Wait();


            var docs = from filePath in args
                       select Tagger.DocumentFromRaw(new TextFile(filePath));
            foreach (var doc in docs) {
                TestGenderRecognition(doc);
                foreach (var sentence in doc.Sentences) {
                    new ClauseSeperatingMultiBranchingBinder().Bind(sentence.Words);
                }
                Task.WaitAll(Binder.GetBindingTasksForDocument(doc).Select(pt => pt.Task).ToArray());
                GetNounsByAdjectivalClassifiers(doc);
                Input.WaitForAnyKey("Press any key to continue to the next document.");
                Output.WriteLine("\n\n\n");
            }
            Input.WaitForKey();
        }

        private static void TestGenderRecognition(Document doc) {
            foreach (var pnp in from np in doc.Phrases.GetNounPhrases()
                                let firstWord = np.Words.GetProperNouns().FirstOrDefault() as ProperNoun
                                let lastWord = np.Words.GetProperNouns().LastOrDefault() as ProperNoun
                                let result = new { NP = np, Gender = np.GetNameGender() }
                                where result.Gender != NameGender.UNDEFINED
                                select result) {
                Output.WriteLine("Name: {0}\nLikely Gender: {1}", pnp.NP.Text, pnp.Gender);
            }
        }

        private async static Task LoadLookup() {

            var tasks = LexicalLookup.GetUnstartedLoadingTasks().ToList();
            while (tasks.Any()) {
                var currentTask = await Task.WhenAny(tasks);
                Output.WriteLine(await currentTask);
                tasks.Remove(currentTask);
            }
        }

        private static IEnumerable<string> GetNounsByAdjectivalClassifiers(Document doc) {
            var result = from i in doc.Words.GetNouns()
                        .Where(n => n.Descriptors.Any())
                        .OrderByDescending(n => n.Text)
                        .Distinct((l, r) => l.Descriptors.SequenceEqual(r.Descriptors, (a, b) => a.Text == b.Text))
                        .Select(n => new { Noun = n.Text, Determiner = n.Determiner != null ? n.Determiner.Text : "none", Discriptors = n.Descriptors })
                         group i by new { i.Noun, i.Determiner } into g
                         select string.Format("{0} Discriptors = {1}", g.Key, g.First().Discriptors.Format(SeqFormatDelim.Curly, aj => aj.Text));
            foreach (var i in result) {
                Output.WriteLine(i);
            }
            return result;
        }


        private static async Task LoadThesaurus() {
            foreach (var task in LexicalLookup.GetUnstartedLoadingTasks()) {
                task.Wait();
                Output.WriteLine(task.Result);
            }
            var tasks = LexicalLookup.GetUnstartedLoadingTasks().ToList();
            while (tasks.Any()) {
                var currentTask = await Task.WhenAny(tasks);
                Output.WriteLine(await currentTask);
                tasks.Remove(currentTask);
            }
        }

        private static string LookupName(string name) {
            return string.Format("{0} is in the set of {1} names", name, name.IsFemaleName() ? "Female" : name.IsMaleName() ? "Male" : "Ambiguous");
        }

        private static void TestRelationshipTable(Document doc) {
            var lookup = new SampleRelationshipLookup(doc);

            Noun n1 = new GenericPluralNoun("cats"), n2 = new GenericPluralNoun("things");
            n1.SetRelationshipLookup(lookup);
            Verb relator = new Verb("are", VerbTense.Base);

            bool related = n1.IsRelatedTo(n2).On(relator);
            Output.WriteLine(related ? string.Format("success:\n{0} is related to {1} on {2}", n1, n2, relator) : "needs work");

        }



    }
}