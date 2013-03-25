using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;

namespace LASI.Algorithm.Thesauri
{
    public static class ThesaurusManager
    {
        private static readonly string nounThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.noun";
        private static readonly string verbThesaurusFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.verb";
        static ThesaurusManager() {
            NounThesaurus = new NounThesaurus(nounThesaurusFilePath);
            VerbThesaurus = new VerbThesaurus(verbThesaurusFilePath);
        }
        public static void LoadAll() {
            var sw = Stopwatch.StartNew();
            NounThesaurus.Load();
            VerbThesaurus.Load();
            sw.Stop();
            Console.WriteLine("Sync thesaurus loading took {0} milliseconds", sw.ElapsedMilliseconds);
        }
        public static async Task LoadAllAsync() {
            var sw = Stopwatch.StartNew();
            await Task.WhenAll(
                NounThesaurus.LoadAsync().ContinueWith(
                (t) => Console.WriteLine("NounThesausus Loaded")),
                VerbThesaurus.LoadAsync().ContinueWith(
                (t) => Console.WriteLine("VerbThesausus Loaded")));
            sw.Stop();
            Console.WriteLine("Async thesaurus loading took {0} milliseconds", sw.ElapsedMilliseconds);
        }
        public static IEnumerable<string> Lookup(Word word) {
            return VerbThesaurus[word];
        }
        public static IEnumerable<string> Lookup(Verb verb) {
            return VerbThesaurus[verb];
        }
        public static IEnumerable<string> Lookup(Noun noun) {
            throw new NotImplementedException();
        }
        public static NounThesaurus NounThesaurus {
            get;
            private set;
        }

        public static VerbThesaurus VerbThesaurus {
            get;
            private set;
        }
    }
}
