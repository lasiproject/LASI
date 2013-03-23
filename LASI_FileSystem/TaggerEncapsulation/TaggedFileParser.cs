using LASI.Algorithm;
using LASI.FileSystem.FileTypes;
using LASI.FileSystem.TaggerEncapsulation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace LASI.FileSystem
{
    public class TaggedFileParser : LASI.FileSystem.TaggerEncapsulation.TagParser
    {
        #region Construtors
        /// <summary>
        /// Initialized a new instance of the TaggedFilerParser class to parse the contents of a specific file.
        /// </summary>
        /// <param name="filePath">The wrapper which encapsulates the path information for the pre-POS-tagged file to parse.</param>
        public TaggedFileParser(TaggedFile file) {
            TaggededDocumentFile = file;
            FilePath = TaggededDocumentFile.FullPath;
            TaggedInputData = LoadDocumentFile().Trim();
        }
        public TaggedFileParser(string taggedText) {
            TaggedInputData = taggedText;
        }


        #endregion

        #region Methods

        private string LoadDocumentFile() {
            using (var reader = new StreamReader(FilePath, Encoding.UTF8)) {
                return reader.ReadToEnd();
            }
        }
        /// <summary>
        /// Returns an instance of ParentDocument which contains the run time representation of all of the textual construct in the document, for the Algorithm to analyse.
        /// </summary>
        /// <returns>a traversable, queriable document object defining the run time representation of the tagged file which the TaggedFileParser governs. </returns>
        public override Document LoadDocument() {
            return new Document(LoadParagraphs());
        }

        /// <summary>
        /// Returns the run time representations of the sentences, phrases,and words extracted from the tagged file the TaggedFileParser governs.
        /// </summary>
        /// <returns>The run time constructs which represent the text of the document, aggregated into paragraphs.</returns>
        public override IEnumerable<Paragraph> LoadParagraphs() {

            var data = PreProcessTextData(TaggedInputData.Trim());
            var results = new List<Paragraph>();
            foreach (var paragraph in ParseParagraphs(data))
                results.Add(BuildParagraph(paragraph));

            return results;
        }

        public override async Task<IEnumerable<Paragraph>> LoadParagraphsAsync() {

            return await Task.Run(() => LoadParagraphs());

        }
        private async Task<Paragraph> BuildParagraphAsync(string paragraph) {
            return await Task.Run(() => BuildParagraph(paragraph));
        }
        private Paragraph BuildParagraph(string paragraph) {
            var parsedSentences = new List<Sentence>();
            var sentences = from s in SplitIntoSentences(paragraph)
                            select s.Trim();
            foreach (var sent in from s in sentences
                                 where !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s)
                                 select s) {

                var parsedClauses = new List<Clause>();
                var parsedPhrases = new List<Phrase>();
                var chunks = from chunk in sent.Split(new[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries)
                             let c = chunk.Trim()
                             where !String.IsNullOrWhiteSpace(c) && !String.IsNullOrEmpty(c)
                             select c;
                SentencePunctuation sentencePunctuation = null;

                foreach (var chunk in chunks) {
                    if (!string.IsNullOrEmpty(chunk) && !string.IsNullOrWhiteSpace(chunk) && chunk.Contains('/')) {
                        char token = SkipToNextElement(chunk);
                        if (token == ' ') {
                            var currentPhrase = ParsePhrase(new TaggedPhraseObject {
                                Text = chunk.Substring(chunk.IndexOf(' ')),
                                Tag = chunk.Substring(0, chunk.IndexOf(' '))
                            });
                            if (currentPhrase.Words.Count(w => !string.IsNullOrWhiteSpace(w.Text)) > 0)
                                parsedPhrases.Add(currentPhrase);

                            if (currentPhrase is SubordinateClauseBeginPhrase) {
                                parsedClauses.Add(new Clause(parsedPhrases.Take(parsedPhrases.Count - 1)));
                                parsedPhrases = new List<Phrase>();
                                parsedPhrases.Add(currentPhrase);
                            }

                        } else if (token == '/') {
                            var words = CreateWords(chunk);
                            if (words.First() != null)
                                if (words.Count(w => w is Conjunction) == words.Count) {
                                    var currentPhrase = new ConjunctionPhrase(words);
                                    parsedPhrases.Add(currentPhrase);
                                } else if (words.Count() == 1 && words.First() is SentencePunctuation) {
                                    sentencePunctuation = words.First() as SentencePunctuation;
                                    parsedClauses.Add(new Clause(parsedPhrases.Take(parsedPhrases.Count)));
                                    parsedPhrases = new List<Phrase>();
                                } else {

                                    //parsedPhrases.Add(new UndeterminedPhrase(words));
                                }
                        }

                    }
                    //if (parsedClauses.Find(fff => fff.Text != "") != null)

                }
                parsedSentences.Add(new Sentence(parsedClauses, sentencePunctuation));
            }
            return new Paragraph(parsedSentences);
        }

        private static IEnumerable<string> SplitIntoSentences(string paragraph) {
            return paragraph.Split(new[] { "<sentence>", "</sentence>" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static char SkipToNextElement(string chunk) {
            var reader2 = (new StringReader(chunk));
            char token = '~';
            while (reader2.Peek() != ' ' && reader2.Peek() != '/') {
                token = (char) reader2.Read();
            }
            token = (char) reader2.Read();
            return token;
        }




        /// <summary>
        /// Pre-processes the line read from the file by replacing some instances of problematic text such as square brackets, with tokens that are easier to reliably parse.
        /// </summary>
        /// <param name="line">The string containing raw SharpNLP tagged-text to process.</param>
        /// <returns>The string containing the processed text.</returns>
        protected virtual string PreProcessTextData(string data) {

            data = data.Replace(" [/-LRB-", " LEFT_SQUARE_BRACKET/-LRB-");

            data = data.Replace("]/-RRB- ", "RIGHT_SQUARE_BRACKET/-RRB- ");
            return data;
        }/// <summary>
        /// Asynchronously Pre-processes the line read from the file by replacing some instances of problematic text such as square brackets, with tokens that are easier to reliably parse.
        /// </summary>
        /// <param name="line">The string containing raw SharpNLP tagged-text to process.</param>
        /// <returns>The string containing the processed text.</returns>
        protected virtual async Task<string> PreProcessTextDataAsync(string data) {
            return await Task.Run(() => {
                data = data.Replace(" [/-LRB-", " LEFT_SQUARE_BRACKET/-LRB-");

                data = data.Replace("]/-RRB- ", "RIGHT_SQUARE_BRACKET/-RRB- ");
                return data;
            });
        }

        /// <summary>
        /// Reads a [NP Square Brack Delimited Phrase Chunk] and returns a entity tag determined subtype of LASI.Phrase which in turn contains all the run time representations of the individual words within it.
        /// </summary>
        /// <param name="taggedContent">The TextTagPair instance which contains the content of a entity and its Tag.</param>
        /// <returns></returns>
        protected virtual Phrase ParsePhrase(TaggedPhraseObject taggedContent) {
            var phraseTag = taggedContent.Tag.Trim();
            var composed = CreateWords(taggedContent.Text);
            var phraseConstructor = PhraseTagset[phraseTag];
            return phraseConstructor(composed);
        }
        /// <summary>
        /// Reads a [NP Square Brack Delimited Phrase Chunk] and returns a entity tag determined subtype of LASI.Phrase which in turn contains all the run time representations of the individual words within it.
        /// </summary>
        /// <param name="taggedContent">The TextTagPair instance which contains the content of a entity and its Tag.</param>
        /// <returns></returns>
        protected virtual async Task<Phrase> ParsePhraseAsync(TaggedPhraseObject taggedContent) {
            var phraseTag = taggedContent.Tag.Trim();
            var composed = await CreateWordsAsync(taggedContent.Text);
            var phraseConstructor = PhraseTagset[phraseTag];
            return phraseConstructor(composed);
        }
        protected virtual async Task<List<Word>> CreateWordsAsync(string wordData) {
            return await Task.Run(() => CreateWords(wordData));
        }

        /// <summary>
        /// Parses a string of text containing tagged words 
        /// e.g. "LASI/NNP can/MD sniff-out/VBP the/DT problem/NN" 
        /// into a collection of Part of Speech subtyped Word instances which represent them.
        /// </summary>
        /// <param name="wordData">a string containing tagged words.</param>
        /// <returns>The collection of Word objects that is their run time representation.</returns>
        protected virtual List<Word> CreateWords(string wordData) {
            var parsedWords = new List<Word>();
            var elements = wordData.Split(new[] { ' ', }, StringSplitOptions.RemoveEmptyEntries);
            var wordExtractor = new WordExtractor();

            var tagParser = new WordMapper();
            foreach (var tagged in elements) {
                var e = wordExtractor.ExtractNextPos(tagged);
                if (e != null) {

                    var word = (tagParser.CreateWord(e.Value));
                    parsedWords.Add(word);
                }
            }
            return parsedWords;
        }
        /// <summary>
        /// Parses a string of text containing tagged words,
        /// e.g. "LASI/NNP can/MD sniff-out/VBP the/DT problem/NN",
        /// and returns of a collection containing, for each w, a function which will create a Part of Speech subtyped Word instance
        /// representing that w.
        /// </summary>
        /// <param name="wordData">a string containing tagged words.</param>
        /// <returns>a List of constructor function instances which, when invoked, create run time objects which represent each w in the source</returns>
        protected virtual List<Func<Word>> CreateWordExpressions(string wordData) {
            var wordExpressions = new List<Func<Word>>();
            var elements = wordData.Split(new[] { ' ', }, StringSplitOptions.RemoveEmptyEntries);
            var posExtractor = new WordExtractor();

            var tagParser = new WordMapper();
            foreach (var tagged in elements) {
                var e = posExtractor.ExtractNextPos(tagged);
                if (e != null) {
                    wordExpressions.Add(tagParser.ReadWordExpression(e.Value));
                }
            }
            return wordExpressions;
        }


        #endregion

        #region Properties

        private readonly WordTagsetMap WordTagset = new SharpNLPWordTagsetMap();

        private readonly PhraseTagsetMap PhraseTagset = new SharpNLPPhraseTagsetMap();

        private static readonly Dictionary<string, int> textToNumberMap = new Dictionary<string, int> {
        { "zero", 0 },
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
        { "ten", 10 },
        { "eleven", 11 },
        { "twelve", 12 },
        { "thirteen", 13 },
        { "fourteen", 14 },
        { "fifteen", 15 },
        { "sixteen", 16 },
        { "seventeen", 17 },
        { "eighteen", 18 },
        { "nineteen", 19 },
        { "twenty", 20 },
        { "twenty-one", 21 },
        { "twenty-two", 22 },
        { "twenty-three", 23 },
        { "twenty-four", 24 },
        { "twenty-five", 25 },
        { "twenty-six", 26 },
        { "twenty-seven", 27 },
        { "twenty-eight", 28 },
        { "twenty-nine", 29 },
        { "thirty", 30 },
        { "thirty-one", 31 },
        { "thirty-two", 32 },
        { "thirty-three", 33 },
        { "thirty-four", 34 },
        { "thirty-five", 35 },
        { "thirty-six", 36 },
        { "thirty-seven", 37 },
        { "thirty-eight", 38 },
        { "thirty-nine", 39 },
        { "forty", 40 },
        { "forty-one", 41 },
        { "forty-two", 42 },
        { "forty-three", 43 },
        { "forty-four", 44 },
        { "forty-five", 45 },
        { "forty-six", 46 },
        { "forty-seven", 47 },
        { "forty-eight", 48 },
        { "forty-nine", 49 },
        { "fifty", 50 },
        { "fifty-one", 51 },
        { "fifty-two", 52 },
        { "fifty-three", 53 },
        { "fifty-four", 54 },
        { "fifty-five", 55 },
        { "fifty-six", 56 },
        { "fifty-seven", 57 },
        { "fifty-eight", 58 },
        { "fifty-nine", 59 },
        { "sixty", 60 },
        { "sixty-one", 61 },
        { "sixty-two", 62 },
        { "sixty-three", 63 },
        { "sixty-four", 64 },
        { "sixty-five", 65 },
        { "sixty-six", 66 },
        { "sixty-seven", 67 },
        { "sixty-eight", 68 },
        { "sixty-nine", 69 },
        { "seventy", 70 },
        { "seventy-one", 71 },
        { "seventy-two", 72 },
        { "seventy-three", 73 },
        { "seventy-four", 74 },
        { "seventy-five", 75 },
        { "seventy-six", 76 },
        { "seventy-seven", 77 },
        { "seventy-eight", 78 },
        { "seventy-nine", 79 },
        { "eighty", 80 },
        { "eighty-one", 81 },
        { "eighty-two", 82 },
        { "eighty-three", 83 },
        { "eighty-four", 84 },
        { "eighty-five", 85 },
        { "eighty-six", 86 },
        { "eighty-seven", 87 },
        { "eighty-eight", 88 },
        { "eighty-nine", 89 },
        { "ninety", 90 },
        { "ninety-one", 91 },
        { "ninety-two", 92 },
        { "ninety-three", 93 },
        { "ninety-four", 94 },
        { "ninety-five", 95 },
        { "ninety-six", 96 },
        { "ninety-seven", 97 },
        { "ninety-eight", 98 },
        { "ninety-nine", 99 },};

        #endregion


    }
}
