using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;
using LASI.Utilities;
using System.Dynamic;

namespace LASI.Algorithm.Thesauri
{
    public static class Thesaurus
    {
        private static readonly string nounThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.noun";
        private static readonly string verbThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.verb";
        static Thesaurus() {
            NounProvider = new NounThesaurus(nounThesaurusFilePath);
            VerbProvider = new VerbThesaurus(verbThesaurusFilePath);
        }
        public static void LoadAll() {
            // var sw = Stopwatch.StartNew();
            NounProvider.Load();
            VerbProvider.Load();
            //sw.Stop();
            //Console.WriteLine("Sync thesaurus loading took {0} milliseconds", sw.ElapsedMilliseconds);
        }
        public static async Task LoadAllAsync() {
            await LoadAllParallelLinqTest();
        }

        private static async Task LoadAllParallelLinqTest() {
            await Task.Run(() => new ThesaurusBase[] { NounProvider, VerbProvider }.AsParallel().ForAll(t => t.Load()));
        }

        private static async Task LoadAllTaskLevelParallelTest() {
            //var sw = Stopwatch.StartNew();
            await Task.WhenAll(
                NounProvider.LoadAsync().ContinueWith(
                (t) => {
                    Output.WriteLine("NounThesausus Loaded");
                }),
                VerbProvider.LoadAsync().ContinueWith(
                (t) => {
                    Output.WriteLine("VerbThesausus Loaded");
                }));
            //sw.Stop();
            //Console.WriteLine("Async thesaurus loading took {0} milliseconds", sw.ElapsedMilliseconds);
        }
        public static IEnumerable<string> Lookup(Word word) {

            return InternalLookup(word as dynamic);

        }
        public static IEnumerable<string> InternalLookup(Verb verb) {
            return VerbProvider[verb];
        }
        public static IEnumerable<string> InternalLookup(Noun noun) {
            return NounProvider[noun];
        }
        public static IEnumerable<string> InternalLookup(Adverb verb) {
            return AdverbProvider[verb];
        }
        public static IEnumerable<string> InternalLookup(Adjective verb) {
            return AdjectiveProvider[verb];
        }
        public static IEnumerable<string> InternalLookup(Word word) {
            throw new LASI.Algorithm.Thesuari.NoSynonymLookupForTypeException(word) {
            };
        }
        public static NounThesaurus NounProvider {
            get;
            private set;
        }

        public static VerbThesaurus VerbProvider {
            get;
            private set;
        }
        public static AdjectiveThesaurus AdjectiveProvider {
            get;
            private set;
        }
        public static AdverbThesaurus AdverbProvider {
            get;
            private set;
        }



        public static bool IsSynonymFor(this Word word, Word other) {
            return (
                word is Noun && other is Noun ||
                word is Verb && other is Verb ||
                word is Adverb && other is Adverb ||
                word is Adjective && other is Adjective
                )
                && Lookup(other).Contains(word.Text);
        }
    }
}
