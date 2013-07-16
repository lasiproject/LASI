using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.RelationshipLookups;
using LASI.Algorithm.LexicalInformationProviders;
using LASI.Utilities;
using LASI.Utilities.TypedSwitch;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using LASI.ContentSystem;

namespace Aluan_Experimentation
{
    public class Program
    {


        static string testPath = @"C:\Users\Aluan\Desktop\ducks.txt";

        static void Main(string[] args) {

            var doc = Tagger.DocumentFromRaw(new TextFile(testPath));



            Input.WaitForKey();
        }

        private static void TestGender() {
            LoadThesaurus().Wait();
            var overlapResults = new List<string>();
            foreach (var name in LexicalLookup.GenderAmbiguousFirstNames)
                overlapResults.Add(LookupName(name));
            Output.WriteLine(overlapResults.OrderBy(s => s.Contains("Female")).ThenBy(s => s).Format(true));
        }
        private static void TestFullNames() {
            LoadThesaurus().Wait();

            LexicalLookup.LastNames.AsParallel().AsUnordered().WithExecutionMode(ParallelExecutionMode.ForceParallelism).ForAll(ln => {
                var falseResults = from fn in LexicalLookup.FemaleNames.Union(LexicalLookup.MaleNames).AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                                   group fn by fn into g
                                   select g.Key into fn
                                   select new NounPhrase(new[] { 
                                            new ProperSingularNoun(fn),
                                            new ProperSingularNoun(ln) 
                                   }) into pnp
                                   where !pnp.IsFullName() select pnp;
                foreach (var pnp in falseResults) {
                    Output.WriteLine(pnp);
                }
            });
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

            Noun n1 = new GenericPluralNoun("cats"), n2 = new GenericPluralNoun("dogs");
            n1.SetRelationshipLookup(lookup);
            Verb relator = new Verb("hate", VerbTense.Base);

            Output.WriteLine(n1.IsRelatedTo(n2).On(relator) ? "success" : "needs tweaking");


        }


        private static void TestWordAndPhraseBindings() {
            var doc = Tagger.DocumentFromRaw(new LASI.ContentSystem.TextFile(testPath));

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
                new AttributiveNounPhraseBinder().Bind(s);
            }
        }


    }
}