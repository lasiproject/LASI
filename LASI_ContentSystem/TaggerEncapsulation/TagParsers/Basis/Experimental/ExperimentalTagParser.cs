using LASI;
using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.ContentSystem.TaggerEncapsulation.TagParsers.Experiment.Support
{
    class ExperimentalTagParser : TagParser
    {
        public ExperimentalTagParser(TaggedFile file) {
            TaggededDocumentFile = file;
            FilePath = TaggededDocumentFile.FullPath;
            TaggedInputData = LoadDocumentFile();
        }
        public ExperimentalTagParser(string taggedText) {
            TaggedInputData = taggedText;
        }
        private string LoadDocumentFile() {
            using (var reader = new StreamReader(FilePath, Encoding.UTF8)) {
                return reader.ReadToEnd();
            }
        }
        public override Document LoadDocument() {
            throw new NotImplementedException();
        }
        public override async Task<Document> LoadDocumentAsync() {
            return await Task.Run(() => LoadDocument());
        }


        public override IEnumerable<Paragraph> LoadParagraphs() {

            var textParagraphs = ParseParagraphs(TaggedInputData);
            foreach (var paraText in textParagraphs) {
                Paragraph para = CreateParagraph(paraText);
            }


            throw new NotImplementedException();
        }

        private Paragraph CreateParagraph(string paraText) {
            var sentences = BreakSentences(paraText);
            foreach (var sentence in sentences) {
                Sentence sent = CreateSentence(sentence);
            }
            throw new NotImplementedException();
        }

        private Sentence CreateSentence(string sentence) {
            var clauses = new List<Clause>();
            var begin = sentence.IndexOfAny(new[] { ' ', '\r', '\t', '\n' });
            for (; ; ) {

                var len = FindEndOfClause(sentence.Substring(begin));
                clauses.Add(CreateClause(sentence.Substring(begin, len)));
                begin = len;

            }

            throw new NotImplementedException();
        }

        private Clause CreateClause(string data) {
            Console.WriteLine(data);
            var phrases = new List<Phrase>();
            while (data.Contains("(")) {
                var currentPhraseString = new PhraseExtractor().ExtractAndConsume(ref data);
                Console.WriteLine(currentPhraseString);
            }
            throw new NotImplementedException();
        }

        private int FindEndOfClause(string data) {
            var clauseTagDelims = new[] { "(S", "(SBAR", "SBARQ", "(SQ", "(SINV" };
            var indeces = from D in clauseTagDelims
                          let I = data.IndexOf(D)
                          where I > -1
                          orderby I ascending
                          select I;
            if (indeces.Any()) {
                return indeces.First();
            } else {
                return data.Length;
            }
        }

        private IEnumerable<string> BreakSentences(string paraText) {
            return paraText.Split(new[] { "(TOP" }, StringSplitOptions.RemoveEmptyEntries).AsEnumerable().Select(s => s.Trim());
        }

        #region Fields
        internal static readonly IDictionary<string, NodeTeir> map = new Dictionary<string, NodeTeir> { 
            { "(S", NodeTeir.Clause },
            { "(SBAR", NodeTeir.Clause }, 
            { "SBARQ", NodeTeir.Clause },
            {  "(SQ", NodeTeir.Clause },
            { "(SINV", NodeTeir.Clause },
            { "VP", NodeTeir.Phrase },
            { "NP", NodeTeir.Phrase },
            { "PP", NodeTeir.Phrase },
            { "ADVP", NodeTeir.Phrase },
            { "ADJP", NodeTeir.Phrase },
            { "PRT", NodeTeir.Phrase },
            { "CONJP", NodeTeir.Phrase },
            { "S", NodeTeir.Phrase },
            { "SINV", NodeTeir.Phrase },
            { "SQ", NodeTeir.Phrase },
            { "SBARQ", NodeTeir.Phrase },
            { "SBAR", NodeTeir.Phrase },
            { "LST", NodeTeir.Phrase },
            { "INTJ", NodeTeir.Phrase },
            { "CC", NodeTeir.Word },
            { ",", NodeTeir.Word },     
            { ";", NodeTeir.Word },
            { ":", NodeTeir.Word },
            { "CD", NodeTeir.Word },
            { "DT", NodeTeir.Word },
            { "EX", NodeTeir.Word },
            { "FW", NodeTeir.Word },
            { "IN", NodeTeir.Word },
            { "JJ", NodeTeir.Word },
            { "JJR", NodeTeir.Word },
            { "JJS", NodeTeir.Word },
            { "LS", NodeTeir.Word },
            { "-LRB-", NodeTeir.Word },
            { "-RRB-", NodeTeir.Word },
            { "''", NodeTeir.Word },
            { "MD", NodeTeir.Word },
            { "NN", NodeTeir.Word },
            { "NNS", NodeTeir.Word },
            { "NNP", NodeTeir.Word },
            { "NNPS", NodeTeir.Word },
            { "PDT", NodeTeir.Word },
            { "POS", NodeTeir.Word },
            { "PRP", NodeTeir.Word },
            { "PRP$", NodeTeir.Word },
            { "RB", NodeTeir.Word },
            { "RBR", NodeTeir.Word },
            { "RBS", NodeTeir.Word },
            { "VB", NodeTeir.Word },
            { "VBD", NodeTeir.Word },
            { "VBG", NodeTeir.Word },
            { "VBN", NodeTeir.Word },
            { "VBP", NodeTeir.Word },
            { "VBZ", NodeTeir.Word },
            { "WDT", NodeTeir.Word },
            { "WP", NodeTeir.Word },
            { "WP$", NodeTeir.Word },
            { "WRB", NodeTeir.Word },
            { "RP", NodeTeir.Word },
            { "SYM", NodeTeir.Word },
            { "TO", NodeTeir.Word },
            { "UH", NodeTeir.Word },
        };

        #endregion

        #region enums
        internal enum NodeTeir { Word, Phrase, Clause }
        #endregion
    }
    internal static class NodeStringExtensions
    {
        static bool IsClauseLevel(this string nodeKind) { return ExperimentalTagParser.map[nodeKind] == ExperimentalTagParser.NodeTeir.Clause; }
        static bool IsPhraseLevel(this string nodeKind) { return ExperimentalTagParser.map[nodeKind] == ExperimentalTagParser.NodeTeir.Phrase; }
        static bool IsWordLevel(this string nodeKind) { return ExperimentalTagParser.map[nodeKind] == ExperimentalTagParser.NodeTeir.Word; }
    }
}
