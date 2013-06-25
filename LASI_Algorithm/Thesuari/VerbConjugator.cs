using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesauri
{
    public static class VerbConjugator
    {

        public static IEnumerable<string> FindRoot(string conjugated) {
            var k = TryExtractRoot(conjugated);
            return k.Any() ? k.Distinct() : new[] { conjugated };
        }


        public static IEnumerable<string> TryComputeConjugations(string root) {
            return TryComputerConjugationFromGrouping(root);

        }

        private static IEnumerable<string> TryComputerConjugationFromGrouping(string root) {
            var hyphIndex = root.IndexOf('-');

            var realRoot = hyphIndex > -1 ? root.Substring(0, hyphIndex) : root;
            var postHyphenInvariant = hyphIndex > -1 ? root.Substring(hyphIndex) : string.Empty;

            var results = new List<string>();
            IEnumerable<string> except;
            exceptionData.TryGetValue(realRoot, out except);

            if (except != null && except.Any()) {
                results.AddRange(except.Select(e => e + postHyphenInvariant));
                return results;
            }
            results.AddRange(from ending in SuffixEndingPairs.Keys
                             where !string.IsNullOrEmpty(ending)
                             where realRoot.EndsWith(ending, StringComparison.OrdinalIgnoreCase)
                             from suffix in SuffixEndingPairs[ending]
                             select realRoot.Substring(0, realRoot.Length - ending.Length) + suffix + postHyphenInvariant);
            return results.Any() ? results.Distinct() : (from suffix in SuffixEndingPairs[""]
                                                         select realRoot + suffix + postHyphenInvariant).Distinct();

        }

        private static List<string> TryExtractRoot(string search) {
            var hyphIndex = search.IndexOf('-');
            var realLookup = hyphIndex > -1 ? search.Substring(0, hyphIndex) : search;
            var postHyphenInvariant = hyphIndex > -1 ? search.Substring(hyphIndex) : string.Empty;
            var results = new List<string>();
            var except = CheckSpecialFormsList(search);
            if (except != null && except.Any()) {
                return except.ToList();
            }
            for (var i = VERB_ENDINGS.Length - 1; i >= 0; --i) {
                if (search.EndsWith(VERB_SUFFICIES[i], StringComparison.OrdinalIgnoreCase)) {
                    var possibleRoot = search.Substring(0, search.Length - VERB_SUFFICIES[i].Length);
                    if ((possibleRoot).EndsWith(VERB_ENDINGS[i])) {
                        results.Add(possibleRoot);
                        return results;
                    }
                }
            }

            return results.Select(r => r + postHyphenInvariant).ToList();
        }

        private static IEnumerable<string> CheckSpecialFormsList(string search) {
            return from verbExceptKVs in exceptionData
                   where verbExceptKVs.Value.Contains(search)
                   from v in verbExceptKVs.Value
                   select v;
        }




        private readonly static string[] VERB_SUFFICIES = { "s", "ies", "es", "es", "ed", "ed", "ing", "ing" };
        private readonly static string[] VERB_ENDINGS = { "", "y", "e", "", " e", "", "e", "" };
        private static readonly IDictionary<string, IEnumerable<string>> SuffixEndingPairs = new Dictionary<string, IEnumerable<string>> {
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
                exceptions = from line in reader.ReadToEnd().Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
                             select ProcessLine(line);

            }
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
        private static IEnumerable<string> ProcessLine(string exceptionLine) {
            var kvstr = exceptionLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(r => r.Replace('_', '-'));
            return kvstr;
        }

        private static IEnumerable<IEnumerable<string>> exceptions = new List<IEnumerable<string>>();
        private static Dictionary<string, IEnumerable<string>> exceptionData = new Dictionary<string, IEnumerable<string>>();


    }
}
