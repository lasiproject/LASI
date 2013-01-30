using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace LASI.DataRepresentation
{
    public class VerbThesaurus : Thesaurus
    {
        /// <summary>
        /// Initializes a new instance of the VerbThesaurus class.
        /// </summary>
        /// <param name="filePath">The path of the WordNet database file containing the sysnonym data for verbs.</param>
        public VerbThesaurus(string filePath = @"..\..\..\ThesaurusDataFiles\data.verb")
            : base(filePath) {
            FilePath = filePath;
            LoadingStatus = FileLoadingState.NotInitiated;
            AssociationData = new SortedList<string, SynonymSet>(13000);
        }

        /// <summary>
        /// Parses the contents of the underlying WordNet database file.
        /// </summary>
        public override void Load() {
            LoadingStatus = FileLoadingState.Initiated;
            using (var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read)) {
                var reader = new StreamReader(fileStream);
                //Discard file header
                for (int i = 0; i < HEADER_LENGTH; ++i)
                    reader.ReadLine();

                while (!reader.EndOfStream) {
                    var data = reader.ReadLine();
                    var synset = BuildSynset(data);
                    AssociationData.Add(synset.IndexCode, synset);
                }


                LoadingStatus = FileLoadingState.Completed;
            }
        }
        /// <summary>
        /// Parses the contents of the underlying WordNet database file Asynchronously in a new thread, returning Task object which represents the state of the ongoing operation.
        /// </summary>
        public override async Task LoadAsync() {
            LoadingStatus = FileLoadingState.Initiated;
            await Task.Run(() => Load());
            LoadingStatus = FileLoadingState.Completed;
        }

        private static SynonymSet BuildSynset(string data) {

            //Console.WriteLine(data);
            var rgx = new Regex(@"[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+[\d]+");

            var sep = data.Split(new[] { '!' }, StringSplitOptions.RemoveEmptyEntries)[0];


            var matches = rgx.Matches(sep);
            //MatchCollection antmat = null;
            var key = matches[0].Value;
            return new SynonymSet {
                IndexCode = key,
                SynIDCodes = (from Match m in matches
                              select m.Value
                           ).ToList()
            };
        }

        public override SynonymSet GetMatches(Word toMatch) {
            return new SynonymSet {
                IndexCode = toMatch.Text,
                SynIDCodes = (from syn in AssociationData[toMatch.Text]
                              where syn != toMatch.Text
                              select syn).Distinct().ToList()

            };

        }

        public override SynonymSet GetMatches(string textualMatch) {
            return new SynonymSet {
                IndexCode = textualMatch,
                SynIDCodes = (from syn in AssociationData[textualMatch]
                              where syn != textualMatch
                              select syn).Distinct().ToList()

            };

        }



        //const string FILE_PATH = @"C:\Users\Aluan\Desktop\dict\data.verb";
        const int HEADER_LENGTH = 30;







    }


}
