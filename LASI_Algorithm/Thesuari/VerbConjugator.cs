using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesauri
{
    public class VerbConjugator
    {
        public VerbConjugator(string exceptionsFilePath) {
            LoadExceptionFile(exceptionsFilePath);

        }

        private void LoadExceptionFile(string filePath) {
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

        public List<string> FindRoot(string conjugated) {
            return TryExtractRoot(conjugated) ?? new List<string> { CheckSpecialFormsList(conjugated) };
        }
        public List<string> GetConjugations(string root) {
            try {
                return new List<string>(exceptionData[root]);
            } catch (KeyNotFoundException) {
                return TryComputeConjugations(root);
            }
        }

        public List<string> TryComputeConjugations(string root) {
            var hyphIndex = root.IndexOf('-');
            List<string> except;
            var realRoot = hyphIndex > -1 ? root.Substring(0, hyphIndex) : root;
            exceptionData.TryGetValue(realRoot, out except);
            if (except != null)
                return except;





            var results = new List<string>();
            for (var i = 0; i < VERB_ENDINGS.Length; i++) {
                if (realRoot.EndsWith(VERB_ENDINGS[i]) || VERB_ENDINGS[i] == "") {
                    results.Add(realRoot.Substring(0, realRoot.Length - VERB_ENDINGS[i].Length) + VERB_SUFFICIES[i] + (hyphIndex > -1 ? root.Substring(hyphIndex) : ""));
                }
            }
            return results;
        }

        public List<string> TryExtractRoot(string search) {
            var results = new List<string>();
            var except = CheckSpecialFormsList(search);
            if (except != null) {
                results.Add(except);
                return results;
            }
            for (int i = 0; i < VERB_ENDINGS.Length; ++i) {
                if (search.EndsWith(VERB_SUFFICIES[i], StringComparison.InvariantCulture)) {
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

        private string CheckSpecialFormsList(string search) {
            return (from verbExceptKVs in exceptionData
                    where verbExceptKVs.Value.Contains(search)
                    select verbExceptKVs.Key).FirstOrDefault();
        }
        private KeyValuePair<string, List<string>> ProcessLine(string exceptionLine) {
            var kvstr = exceptionLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new KeyValuePair<string, List<string>>(kvstr.Last(), kvstr.Take(kvstr.Count() - 1).ToList());
        }




        private readonly Dictionary<string, List<string>> exceptionData = new Dictionary<string, List<string>>();

        private readonly string[] VERB_SUFFICIES = new[] { "s", "ies", "es", "es", "ed", "ed", "ing", "ing" };
        private readonly string[] VERB_ENDINGS = new[] { "", "y", "e", "", "e", "", "e", "" };
        public override string ToString() {
            return exceptionData.Aggregate("",
                (accumulator, data) => accumulator +=
                String.Format("{0} -> {1}\n",
                data.Key,
                data.Value.Aggregate("",
                (agg, entry) => agg += entry + " ").Trim()));
        }
    }
}
