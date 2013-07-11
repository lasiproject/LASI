using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Lookup
{
    public static class VerbConjugator
    {




        public static IEnumerable<string> GetConjugations(string root) {

            var hyphIndex = root.IndexOf('-');

            var realRoot = hyphIndex > -1 ? root.Substring(0, hyphIndex) : root;
            var postHyphenInvariant = hyphIndex > -1 ? root.Substring(hyphIndex) : string.Empty;

            var results = new List<string>();
            var except = exceptionData.ContainsKey(realRoot) ? exceptionData[realRoot] : null;

            if (except != null && except.Any()) {
                results.AddRange(except.Select(e => e + postHyphenInvariant));
                return results;
            }
            results.AddRange(from ending in EndingSuffixPairs.Keys
                             where ending == "" || realRoot.EndsWith(ending, StringComparison.OrdinalIgnoreCase)
                             from suffix in EndingSuffixPairs[ending]
                             select realRoot.Substring(0, realRoot.Length - ending.Length) + suffix + postHyphenInvariant);

            return results.Distinct();

        }

        public static string FindRoot(string search) {

            return CheckSpecialForms(search).FirstOrDefault() ?? BuildLexicalForms(search).FirstOrDefault() ?? search;

        }

        private static IEnumerable<string> BuildLexicalForms(string search) {
            var hyphIndex = search.IndexOf('-');
            var postHyphenInvariant = hyphIndex > -1 ? search.Substring(hyphIndex) : string.Empty;
            var results = new List<string>();
            for (var i = VERB_ENDINGS.Length - 1; i >= 0; --i) {
                if (search.EndsWith(VERB_SUFFICIES[i], StringComparison.OrdinalIgnoreCase)) {
                    var possibleRoot = search.Substring(0, search.Length - VERB_SUFFICIES[i].Length);
                    if (string.IsNullOrEmpty(VERB_ENDINGS[i]) || (possibleRoot).EndsWith(VERB_ENDINGS[i])) {
                        results.Add(possibleRoot);
                        return results.Select(r => r + postHyphenInvariant);
                    }
                }

            }
            return results.Select(r => r + postHyphenInvariant).DefaultIfEmpty();
        }

        private static IEnumerable<string> CheckSpecialForms(string search) {
            return from verbExceptKVs in exceptionData
                   where verbExceptKVs.Value.Contains(search)
                   from v in verbExceptKVs.Value
                   select v;
        }




        private readonly static string[] VERB_SUFFICIES = { "s", "ies", "es", "es", "ed", "ed", "ing", "ing" };
        private readonly static string[] VERB_ENDINGS = { "", "y", "e", "", " e", "", "e", "" };
        private static readonly IDictionary<string, IEnumerable<string>> EndingSuffixPairs = new Dictionary<string, IEnumerable<string>> {
            { "", new []{ "s",  "es",  "ed", "ing" } },
            { "e", new []{ "es", "ed", "ing"} },    
            { "y", new []{ "ies" } },
        };




        private static string exceptionFilePath = System.Configuration.ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "verb.exc";
        static VerbConjugator() {
            LoadExceptionFile(exceptionFilePath);
        }

        private static void LoadExceptionFile(string filePath) {
            using (var reader = new StreamReader(filePath)) {
                var exceptions = from line in reader.ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 select line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Replace('_', '-'));

                exceptionData = (from items in exceptions
                                 from i in items
                                 select new
                                 {
                                     i,
                                     items
                                 } into kvp
                                 group kvp by kvp.i into g
                                 select new KeyValuePair<string, IEnumerable<string>>(g.Key, from i in g
                                                                                             from e in i.items
                                                                                             select e)
                                 ).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            }
        }


        private static Dictionary<string, IEnumerable<string>> exceptionData = new Dictionary<string, IEnumerable<string>>();


    }
    internal static class CharExtensions
    {
        internal static bool IsConsonant(this char c) {
            return !c.IsVowel();
        }
        internal static bool IsVowel(this char c) {
            var lc = char.ToLower(c);
            return lc == 'a' || lc == 'e' || lc == 'i' || lc == 'o' || lc == 'u' || lc == 'y';
        }
    }
}
