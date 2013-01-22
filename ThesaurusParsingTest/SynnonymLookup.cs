using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ThesaurusParsingTest
{
    static class SynonymLookup
    {
        static SynonymLookup()
        {
            LoadingStatus = FileLoadingState.NotInitiated;
            associationData = new List<RelatedWordSet>(13000);
        }
        public static void Load()
        {
            LoadingStatus = FileLoadingState.Initiated;
            using (var fileStream = new FileStream(FILE_PATH, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fileStream))
                { //Discard file header
                    for (int i = 0; i < FIRST_PARSE_LINE; ++i)
                        reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        var data = reader.ReadLine();
                        associationData.Add(BuildSynset(data));
                    }
                }
            }
            LoadingStatus = FileLoadingState.Completed;
        }

        public static async void LoadAsync()
        {
            LoadingStatus = FileLoadingState.Initiated;
            await Task.Run(() => Load());
            LoadingStatus = FileLoadingState.Completed;
        }

        private static RelatedWordSet BuildSynset(string data)
        {

            //Console.WriteLine(data);
            var rgx = new Regex(@"[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+");

            var sep = data.Split(new[] { '!' }, StringSplitOptions.RemoveEmptyEntries)[0];

            var matches = rgx.Matches(sep);
            MatchCollection antmat = null;

            return new RelatedWordSet {
                SynIDCodes = (from Match m in matches
                              select m.Value
                           ).ToList()
            };
        }

        public static RelatedWordSet GetMatches(string code)
        {
            return new RelatedWordSet {
                SynIDCodes = (from synset in associationData
                              where synset.SynIDCodes.Contains(code)
                              from syn in synset
                              select syn).Distinct().ToList(),

            };

        }
        public static FileLoadingState LoadingStatus
        {
            get;
            private set;
        }

        private static List<RelatedWordSet> associationData;

        public static IReadOnlyList<RelatedWordSet> Assocuations
        {
            get
            {
                return associationData.AsReadOnly();
            }
        }

        const string FILE_PATH = @"C:\Users\Aluan\Desktop\dict\data.verb";
        const int FIRST_PARSE_LINE = 30;


        internal class RelatedWordSet : IReadOnlyCollection<string>
        {
            public List<string> SynIDCodes
            {
                get;
                set;
            }
            public List<string> AntIDCodes
            {
                get;
                set;
            }
            public override string ToString()
            {
                return SynIDCodes.Aggregate("", (str, code) => {
                    return str + "  " + code;
                });
            }


            public int Count
            {
                get
                {
                    return SynIDCodes.Count;
                }
            }

            public IEnumerator<string> GetEnumerator()
            {
                return SynIDCodes.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
    }
}
