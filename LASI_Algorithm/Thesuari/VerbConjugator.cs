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
        private static string exceptionFilePath = System.Configuration.ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "verb.exc";
        static VerbConjugator() {
            LoadExceptionFile(exceptionFilePath);
        }

        private static void LoadExceptionFile(string filePath) {
            using (var reader = new StreamReader(filePath)) {
                while (!reader.EndOfStream) {
                    var keyVal = ProcessLine(reader.ReadLine());
                    //try {
                    if (!exceptionData.ContainsKey(keyVal.Key)) {
                        exceptionData.Add(keyVal.Key, keyVal.Value);
                    }
                    //} catch (ArgumentException ex) {
                    //    Debug.WriteLine(string.Format("Verb: {0} already present\n{1}", keyVal.Key, ex.Message));
                    //    exceptionData[keyVal.Key].AddRange(keyVal.Value);
                    //}
                }
            }
        }

        public static List<string> FindRoot(string conjugated) {
            return TryExtractRoot(conjugated) ?? new List<string> { CheckSpecialFormsList(conjugated) };
        }
        public static IEnumerable<string> GetConjugations(string root) {
            try {
                return exceptionData[root];
            }
            catch (KeyNotFoundException) {
                return TryComputeConjugations(root);
            }
        }

        public static IEnumerable<string> TryComputeConjugations(string root) {
            return TryComputerConjugationFromGrouping(root);
            //var hyphIndex = root.IndexOf('-');
            //List<string> except;
            //var realRoot = hyphIndex > -1 ? root.Substring(0, hyphIndex) : root;
            //exceptionData.TryGetValue(realRoot, out except);
            //if (except != null)
            //    return except;





            //var results = new List<string>();
            //for (var i = VERB_ENDINGS.Length - 1; i >= 0; --i) {
            //    if (realRoot.EndsWith(VERB_ENDINGS[i], StringComparison.OrdinalIgnoreCase) || VERB_ENDINGS[i] == "") {
            //        results.Add(realRoot.Substring(0, realRoot.Length - VERB_ENDINGS[i].Length) + VERB_SUFFICIES[i] + (hyphIndex > -1 ? root.Substring(hyphIndex) : ""));
            //    }
            //}
            //return results;
        }

        public static IEnumerable<string> TryComputerConjugationFromGrouping(string root) {
            var hyphIndex = root.IndexOf('-');
            List<string> except;
            var realRoot = hyphIndex > -1 ? root.Substring(0, hyphIndex) : root;
            exceptionData.TryGetValue(realRoot, out except);
            if (except != null)
                return except;
            return from ending in SuffixEndingPairs.Keys
                   where realRoot.EndsWith(ending, StringComparison.OrdinalIgnoreCase)
                   from suffix in SuffixEndingPairs[ending]
                   select realRoot.Substring(0, realRoot.Length - ending.Length) + suffix;
        }

        static List<string> TryExtractRoot(string search) {
            var results = new List<string>();
            var except = CheckSpecialFormsList(search);
            if (except != null) {
                results.Add(except);
                return results;
            }
            for (var i = VERB_ENDINGS.Length - 1; i >= 0; --i) {
                if (search.EndsWith(VERB_SUFFICIES[i], StringComparison.OrdinalIgnoreCase)) {
                    var possibleRoot = search.Substring(0, search.Length - VERB_SUFFICIES[i].Length);
                    if ((possibleRoot).EndsWith(VERB_ENDINGS[i])) {
                        results.Add(possibleRoot);
                    }
                }
            }
            if (results.Count == 0)
                results.Add(search);
            return results;
        }

        private static string CheckSpecialFormsList(string search) {
            return (from verbExceptKVs in exceptionData
                    where verbExceptKVs.Value.Contains(search)
                    select verbExceptKVs.Key).FirstOrDefault();
        }
        private static KeyValuePair<string, List<string>> ProcessLine(string exceptionLine) {
            var kvstr = exceptionLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new KeyValuePair<string, List<string>>(kvstr.Last(), kvstr.Take(kvstr.Count() - 1).ToList());
        }




        private static readonly Dictionary<string, List<string>> exceptionData = new Dictionary<string, List<string>>();

        private readonly static string[] VERB_SUFFICIES = { "s", "ies", "es", "es", "ed", "ed", "ing", "ing" };
        private readonly static string[] VERB_ENDINGS = { "", "y", "e", "", " e", "", "e", "" };
        private static readonly IDictionary<string, IEnumerable<string>> SuffixEndingPairs = new Dictionary<string, IEnumerable<string>> {
            { "", new []{ "s", "es", "ed", "ing" } },
            { "e", new []{ "es", "ed", "ing"} },
            { "y", new []{ "ies" } },
        };
    }
}
