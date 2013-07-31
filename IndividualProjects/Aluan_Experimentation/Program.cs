using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.RelationshipLookups;
using LASI.Algorithm.LexicalLookup;
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
        static void Main(string[] args) {
            //Load up the document
            LoadLookup();

            var doc = Tagger.DocumentFromRaw(
                new RawTextFragment(@"Black cats, fuzzy cats, and hiccuping cats are all silly things that one should avoid. 
                            Silly cats are especially troublesome in that they actually have no morals at all.
                            Of course, this sentence is even sillier than the prior one.",
                "test"));
            //Bind it
            Task.WaitAll(Binder.GetBindingTasksForDocument(doc).Select(pt => pt.Task).ToArray());

            TestRelationshipTable(doc);

            //Format and output words
            Console.WriteLine(doc.Words.Format(onePerLine: true));
            //Categorize and print each category. 
            foreach (var i in GetNounsByAdjectivalClassifiers(doc)) {
                Console.WriteLine(i);
            }
            Input.WaitForKey();
        }

        private static void LoadLookup() {
            Task.WaitAll(LexicalLookup.GetUnstartedLoadingTasks().ToArray());
        }

        private static IEnumerable<string> GetNounsByAdjectivalClassifiers(Document doc) {
            return from i in doc.Words.GetNouns()
                       .Where(n => n.Descriptors.Any())
                       .OrderByDescending(n => n.Text)
                       .Distinct((l, r) => l.Descriptors.SequenceEqual(r.Descriptors, (a, b) => a.Text == b.Text))
                       .Select(n => new { n.Text, det = n.Determiner != null ? n.Determiner.Text : "none", dscrptrs = n.Descriptors })
                   group i by new { i.Text, i.det } into g
                   select string.Format(
                        "det:{0} n: {1} d: {2}",
                        g.First().det,
                        g.Key,
                        g.Format(i => i.dscrptrs.Format(aj => aj.Text))
                   );
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
            Console.WriteLine(related ? string.Format("success:\n{0} is related to {1} on {2}", n1, n2, relator) : "needs work");

        }



    }
}