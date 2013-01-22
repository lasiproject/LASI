using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ThesaurusParsingTest
{
    static class KeyCodeProvider
    {

        static KeyCodeProvider() {
            LoadingStatus = FileLoadingState.NotInitiated;
            wordCodeMap = new Dictionary<string, string>(10000);
        }
        public static void Load() {

            using (var fileStream = new FileStream(FILE_PATH, FileMode.Open, FileAccess.Read)) {
                LoadingStatus = FileLoadingState.Initiated;
                var reader = new StreamReader(fileStream);
                    //Discard file header
                    for (int i = 0; i < FIRST_PARSE_LINE; ++i)
                        reader.ReadLine();

                    while (!reader.EndOfStream) {
                        string data = reader.ReadLine();
                        var pair = ExtractData(data);
                        wordCodeMap.Add(pair.Key, pair.Value);
                    }
              
                LoadingStatus = FileLoadingState.Completed;
           }
        }
        public static async void LoadAsync() {
            LoadingStatus = FileLoadingState.Initiated;
            await Task.Run(() => Load());
            LoadingStatus = FileLoadingState.Completed;
        }


        /// <summary>
        /// Extract a key value pair from a line of the index file
        /// </summary>
        /// <param name="data">The line of the file to parse</param>
        /// <returns>A pair of values where the word's text is the key and word's 8 digit code is the value</returns>
        private static KeyValuePair<string, string> ExtractData(string data) {
            //Extract the text of the word to use as the key
            int wordDelim = data.IndexOf(' ');
            var wordTextRaw = data.Substring(0, wordDelim);
            var wordText = wordTextRaw.Replace('_', ' ');
            //Extract the 8 digit code
            var rgx = new Regex(@"[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+");
            var matches = rgx.Matches(data);
            var code = matches[0].Value;
            return new KeyValuePair<string, string>(wordText, code);

        }
        public static string GetCode(string key) {

            if (LoadingStatus == FileLoadingState.Completed) {
                try {
                    return wordCodeMap[key];
                } catch (KeyNotFoundException ex) {
                    throw new KeyNotFoundException(String.Format("No synset key code associated with \"{0}\"", key), ex);
                }
            } else {
                throw new FileLoadException("The thesaurus file " + (LoadingStatus ==
                    FileLoadingState.Initiated ?
                    "is still loading" :
                    "is not loaded"));
            }
        }


        private static Dictionary<string, string> wordCodeMap;

        public static IReadOnlyDictionary<string, string> WordCodeMap {
            get {
                return new ReadOnlyDictionary<string, string>(wordCodeMap);
            }
        }
        public static string GetWord(string code) {
            try {
                //    return WordCodeMap.ToLookup(c => c.Value)[code].First().Value;
                var ky = from kv in wordCodeMap
                         where kv.Value == code
                         select kv.Key;
                return ky.First();
            } catch {
                return null;
            }
        }

        public static FileLoadingState LoadingStatus {
            get;
            private set;
        }
        const string FILE_PATH = @"C:\Users\Aluan\Desktop\dict\index.verb";
        const int FIRST_PARSE_LINE = 30;


    }

    enum FileLoadingState : byte
    {
        NotInitiated,
        Initiated,
        Completed,
    }
}
