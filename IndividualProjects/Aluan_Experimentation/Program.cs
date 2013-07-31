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


        static string testPath = @"C:\Users\Aluan\Desktop\Documents\ducks.txt";

        static void Main(string[] args) {
            //Load up the document
            var doc = Tagger.DocumentFromRaw(new TextFile(testPath));
            //Bind it
            Task.WaitAll(Binder.GetBindingTasksForDocument(doc).Select(pt => pt.Task).ToArray());

            //Format and output words
            Console.WriteLine(doc.Words.Format(onePerLine: true));
            //Categorize and print each category. 
            GetNounsByAdjectivalClassifiers(doc).ToList().ForEach(Console.WriteLine);

            Input.WaitForKey();
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

            Noun n1 = new GenericPluralNoun("cats"), n2 = new GenericPluralNoun("dogs");
            n1.SetRelationshipLookup(lookup);
            Verb relator = new Verb("hate", VerbTense.Base);

            Output.WriteLine(n1.IsRelatedTo(n2).On(relator) ? "success" : "needs tweaking");


        }



    }
}