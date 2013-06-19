using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesauri
{
    public static class NounConjugator
    {
        private static string exceptionFilePath = System.Configuration.ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "noun.exc";
        static NounConjugator() {
            LoadExceptionFile(exceptionFilePath);

        }




        public static IEnumerable<string> GetLexicalForms(string search) {
            var root = FindRoot(search);
            return TryComputeConjugations(root);
        }

        private static List<string> TryComputeConjugations(string root) {
            var hyphenIndex = root.IndexOf('-');


            List<string> except;
            exceptionData.TryGetValue(root, out except);
            if (except != null) {
                except.Add(root);
                return except;
            } else {
                var results = new List<string>();
                for (var i = NOUN_ENDINGS.Length - 1; i > -1; i--) {
                    if (root.EndsWith(NOUN_ENDINGS[i]) || NOUN_ENDINGS[i] == "") {
                        results.Add(root.Substring(0, root.Length - NOUN_ENDINGS[i].Length) + NOUN_SUFFICIES[i]);
                        break;
                    }
                }
                results.Add(root);
                return results;
            }
        }



        public static string FindRoot(string NounText) {
            List<string> result = CheckSpecialFormsList(NounText);
            if (result.Any()) {
                return result.First();
            } else {
                for (int i = 0; i < NOUN_SUFFICIES.Length; ++i) {
                    if (NounText.EndsWith(NOUN_SUFFICIES[i])) {
                        result.Add(NounText.Substring(0, NounText.Length - NOUN_ENDINGS[i].Length) + NOUN_ENDINGS[i]);
                        break;
                    }
                }
            }

            if (result.Count == 0)
                result.Add(NounText);
            return result.First();
        }


        private static List<string> CheckSpecialFormsList(string search) {
            return (from nounExceptKVs in exceptionData
                    where nounExceptKVs.Value.Contains(search)
                    select nounExceptKVs.Key).ToList();
        }










        //public override string ToString() {
        //    return exceptionData.Aggregate("",
        //        (accumulator, data) => accumulator +=
        //        String.Format("{0} -> {1}\n",
        //        data.Key,
        //        data.Value.Aggregate("",
        //        (agg, entry) => agg += entry + " ").Trim()));
        //}


        #region Exception File Processing

        private static void LoadExceptionFile(string filePath) {
            using (var reader = new StreamReader(filePath)) {
                while (!reader.EndOfStream) {
                    var keyVal = ProcessLine(reader.ReadLine());
                    exceptionData[keyVal.Key] = keyVal.Value;
                }
            }
        }

        private static KeyValuePair<string, List<string>> ProcessLine(string exceptionLine) {
            var kvstr = exceptionLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new KeyValuePair<string, List<string>>(kvstr.Last(), kvstr.Take(kvstr.Count() - 1).ToList());
        }
        private static readonly Dictionary<string, List<string>> exceptionData = new Dictionary<string, List<string>>();
        private static readonly string[] NOUN_SUFFICIES = new[] { "s", "ses", "xes", "zes", "ches", "shes", "men", "ies" };
        private static readonly string[] NOUN_ENDINGS = new[] { "", "s", "x", "z", "ch", "sh", "man", "y", };
        #endregion

    }
}
