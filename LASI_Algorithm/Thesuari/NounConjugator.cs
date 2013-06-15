using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesauri
{
    public class NounConjugator
    {
        public NounConjugator(string exceptionsFilePath) {
            LoadExceptionFile(exceptionsFilePath);

        }


        #region Exception File Processing

        private void LoadExceptionFile(string filePath) {
            using (var reader = new StreamReader(filePath)) {
                while (!reader.EndOfStream) {
                    var keyVal = ProcessLine(reader.ReadLine());

                    if (!exceptionData.ContainsKey(keyVal.Key)) {
                        exceptionData.Add(keyVal.Key, keyVal.Value);
                    }

                }
            }
        }

        private KeyValuePair<string, List<string>> ProcessLine(string exceptionLine) {
            var kvstr = exceptionLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new KeyValuePair<string, List<string>>(kvstr.Last(), kvstr.Take(kvstr.Count() - 1).ToList());
        }

        #endregion

        public List<string> FindRoot(string conjugated) {
            return (TryExtractRoot(conjugated)).AsEnumerable().ToList() ?? new List<string> { CheckSpecialFormsList(conjugated) };
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
            for (var i = 0; i < NOUN_ENDINGS.Length; i++) {
                if (realRoot.EndsWith(NOUN_ENDINGS[i]) || NOUN_ENDINGS[i] == "") {
                    results.Add(realRoot.Substring(0, realRoot.Length - NOUN_ENDINGS[i].Length) + NOUN_SUFFICIES[i] + (hyphIndex > -1 ? root.Substring(hyphIndex) : ""));
                }
            }
            return results;
        }

        //public List<string> TryExtractRoot(string search) {
        //    var result = new List<string>();
        //    string except = CheckSpecialFormsList(search);
        //    if (except != null) {
        //        result.Add(except);
        //        return result;
        //    }
        //    for (int i = 0; i < NOUN_ENDINGS.Length; ++i) {
        //        if (search.EndsWith(NOUN_SUFFICIES[i], StringComparison.InvariantCulture)) {
        //            var possibleRoot = search.Substring(0, search.Length - NOUN_SUFFICIES[i].Length);
        //            if ((possibleRoot).EndsWith(NOUN_ENDINGS[i])) {
        //                result.Add(possibleRoot);
        //            }
        //        }
        //    }
        //    if (result.Count == 0)
        //        result.Add(search);
        //    return result;
        //}


        public List<string> TryExtractRoot(string search) {
            List<string> result = new List<string>(new[] { CheckSpecialFormsList(search) });
            if (result != null) {
                return result;
            }
            for (int i = 0; i < NOUN_SUFFICIES.Length; ++i) {
                if (search.EndsWith(NOUN_SUFFICIES[i])) {
                    result.Add(search.Substring(0, search.Length - NOUN_ENDINGS[i].Length) + NOUN_ENDINGS[i]);
                    break;
                }
            }

            if (result.Count == 0)
                result.Add(search);
            return result;
        }


        private string CheckSpecialFormsList(string search) {
            return (from nounExceptKVs in exceptionData
                    where nounExceptKVs.Value.Contains(search)
                    select nounExceptKVs.Key).FirstOrDefault();
        }




        private readonly Dictionary<string, List<string>> exceptionData = new Dictionary<string, List<string>>();

        public Dictionary<string, List<string>> ExceptionData {
            get {
                return exceptionData;
            }
        }


        private readonly string[] NOUN_SUFFICIES = new[] { "s", "ses", "xes", "zes", "ches", "shes", "men", "ies" };
        private readonly string[] NOUN_ENDINGS = new[] { "", "s", "x", "z", "ch", "sh", "man", "y", };
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
