using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.RelationshipLookups;
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


        static string testPath = @"C:\Users\Aluan\Desktop\.txt\411writtensummary2.txt";

        static void Main(string[] args) {

            //var doc = TaggerUtil.LoadTextFile(new LASI.FileSystem.FileTypes.TextFile(testPath));
            //TestRelationshipTable(doc);

            TestFullNames();



            Input.WaitForKey();
        }

        private static void TestGender() {
            foreach (var task in LexicalLookup.YetUnloadedResoucesTasks) {
                task.Wait();
                Output.WriteLine(task.Result);
            }
            var overlapResults = new List<string>();
            foreach (var name in LexicalLookup.GenderAmbiguousFirstNames)
                overlapResults.Add(LookupName(name));
            Output.WriteLine(overlapResults.OrderBy(s => s.Contains("Female")).ThenBy(s => s).Format(true));
        }
        private static void TestFullNames() {
            foreach (var task in LexicalLookup.YetUnloadedResoucesTasks) {
                task.Wait();
                Output.WriteLine(task.Result);
            }
            foreach (var ln in LexicalLookup.LastNames) {
                var toCheck = from fn in LexicalLookup.FemaleNames.Concat(LexicalLookup.MaleNames).AsParallel()
                              group fn by fn into g select g.Key into fn
                              select new NounPhrase(new[] { 
                              new ProperSingularNoun(fn),
                              new ProperSingularNoun(ln) 
                          });

                var resultsOfCheck = from pnp in toCheck.AsParallel() group pnp by pnp.IsFullName();

                foreach (var grp in resultsOfCheck.Where(g => !g.Key)) {
                    Output.WriteLine("{0}, {1}", grp.Key, grp.Count());
                }
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
            var doc = TaggerUtil.LoadTextFile(new LASI.FileSystem.FileTypes.TextFile(testPath));

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