using LASI.Algorithm;
using LASI.FileSystem.TaggerEncapsulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace LASI.FileSystem
{
    public class TaggedFileParser
    {
        #region Construtors
        /// <summary>
        /// Initialized a new instance of the TaggedFilerParser class to parse the contents of a specific file.
        /// </summary>
        /// <param name="filePath">The absosultePath of the pre-POS-tagged file to parse.</param>
        //public TaggedFileParser(string filePath) {
        //    FilePath = filePath;
        //    TaggededDocumentFile = new TaggedFile(filePath);
        //    TaggedInputData = LoadDocumentFile();
        //}
        /// <summary>
        /// Initialized a new instance of the TaggedFilerParser class to parse the contents of a specific file.
        /// </summary>
        /// <param name="filePath">The wrapper which encapsulates the path information for the pre-POS-tagged file to parse.</param>
        public TaggedFileParser(TaggedFile file) {
            TaggededDocumentFile = file;
            FilePath = TaggededDocumentFile.FullPath;
            TaggedInputData = LoadDocumentFile();
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
        /// Returns an instance of Document which contains the run time representation of all of the textual construct in the document, for the Algorithm to analyse.
        /// </summary>
        /// <returns>A traversable, queriable document object defining the run time representation of the tagged file which the TaggedFileParser governs. </returns>
        public virtual Document LoadDocument() {
            return new Document(LoadParagraphs());
        }
        public virtual async Task<Document> LoadDocumentAsync() {
            return new Document(await LoadParagraphsAsync());
        }

        public virtual async Task<IEnumerable<Paragraph>> LoadParagraphsAsync() {
            return await Task.Run(() => LoadParagraphs());
        }

        /// <summary>
        /// Returns the run time representations of the sentences, phrases,and words extracted from the tagged file the TaggedFileParser governs.
        /// </summary>
        /// <returns>The run time constructs which represent the text of the document, aggregated into paragraphs.</returns>
        public virtual IEnumerable<Paragraph> LoadParagraphs() {
            var results = new List<Paragraph>();

            var data = PreProcessTextData(TaggedInputData);
            foreach (var paragraph in ParseParagraphs(data)) {
                var parsedSentences = new List<Sentence>();
                var sentences = paragraph.Split(new[] { "./.", "!/!", "?/?" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var sent in sentences) {
                    var parsedPhrases = new List<Phrase>();
                    var chunks = from chunk in sent.Split(new[] { "[", "]" }, StringSplitOptions.None).AsParallel()
                                 where !String.IsNullOrWhiteSpace(chunk) && !String.IsNullOrEmpty(chunk)
                                 select chunk.Trim();
                    foreach (var chunk in chunks) {
                        char token = SkipToNextElement(chunk);
                        if (token == ' ') {
                            var currentPhrase = ParsePhrase(new TaggedPhraseObject {
                                Tag = chunk.Substring(0, chunk.IndexOf(' ')),
                                Text = chunk.Substring(chunk.IndexOf(' '))
                            });
                            parsedPhrases.Add(currentPhrase);
                        }
                        else if (token == '/') {
                            var words = CreateWords(chunk);
                            if (words.Count == 1 && words.First() != null)
                                if (words.First().Text == "and" || words.First().Text == "or") {
                                    var currentPhrase = new ConjunctionPhrase(words);
                                    parsedPhrases.Add(currentPhrase);
                                }
                        }

                    }
                    parsedSentences.Add(new Sentence(parsedPhrases));
                }
                results.Add(new Paragraph(parsedSentences));
            }

            return results;
        }

        private static char SkipToNextElement(string chunk) {
            var reader2 = (new StringReader(chunk));
            char token = '`';
            while (reader2.Peek() != '/' && reader2.Peek() != ' ') {
                token = (char)reader2.Read();
            }
            token = (char)reader2.Read();
            return token;
        }




        /// <summary>
        /// Pre-processes the data read from the file by replacing some instances of problematic text such as square brackets, with tokens that are easier to reliably parse.
        /// </summary>
        /// <param name="data">The string containing raw SharpNLP tagged-text to process.</param>
        /// <returns>The string containing the processed text.</returns>
        protected virtual string PreProcessTextData(string data) {

            data = data.Replace(" [/-LRB-", " LEFT_SQUARE_BRACKET/-LRB-");

            data = data.Replace("]/-RRB- ", "RIGHT_SQUARE_BRACKET/-RRB- ");
            return data;
        }

        /// <summary>
        /// Reads a [NP Square Brack Delimited Phrase Chunk] and returns a phrase tag determined subtype of LASI.Phrase which in turn contains all the run time representations of the individual words within it.
        /// </summary>
        /// <param name="taggedContent">The TextTagPair instance which contains the content of a phrase and its Tag.</param>
        /// <returns></returns>
        protected virtual Phrase ParsePhrase(TaggedPhraseObject taggedContent) {
            var phraseTag = taggedContent.Tag.Trim();
            var composed = CreateWords(taggedContent.Text);
            var phraseConstructor = PhraseTagset[phraseTag];
            return phraseConstructor(composed);
        }


        /// <summary>
        /// Parses a string of text containing tagged words 
        /// e.g. "LASI/NNP can/MD sniff-out/VBP the/DT problem/NN" 
        /// into a collection of Part of Speech subtyped Word instances which represent them.
        /// </summary>
        /// <param name="wordData">A string containing tagged words.</param>
        /// <returns>The collection of Word objects that is their run time representation.</returns>
        protected virtual List<Word> CreateWords(string wordData) {
            var parsedWords = new List<Word>();
            var elements = wordData.Split(new[] { ' ', });
            var posExtractor = new WordExtractor();

            var tagParser = new WordTagParser();
            foreach (var tagged in elements) {
                var e = posExtractor.ExtractNextPos(tagged);
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
        /// and returns of a collection containing, for each word, a function which will create a Part of Speech subtyped Word instance
        /// representing that word.
        /// </summary>
        /// <param name="wordData">A string containing tagged words.</param>
        /// <returns>A List of constructor function instances which, when invoked, create run time objects which represent each word in the source</returns>
        protected virtual List<Func<Word>> CreateWordExpressions(string wordData) {
            var wordExpressions = new List<Func<Word>>();
            var elements = wordData.Split(new[] { ' ', });
            var posExtractor = new WordExtractor();

            var tagParser = new WordTagParser();
            foreach (var tagged in elements) {
                var e = posExtractor.ExtractNextPos(tagged);
                if (e != null) {
                    wordExpressions.Add(tagParser.ReadWordExpression(e.Value));
                }
            }
            return wordExpressions;
        }


        /// <summary>
        /// Breaks a string of text containing multiple paragraphs into a collection of strings each representing an individual paragraph.
        /// Paragraphs are delimited using the default regular expression pattern "[\r\n]+[^]*[\r\n]+"
        /// </summary>
        /// <param name="data">A string containing the text to be broken down.</param>
        /// <returns>A collection of strings, each entry corresponding to the entire content of a single paragraph.</returns>
        protected IEnumerable<string> ParseParagraphs(string data) {
            return data.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);


        }



        #endregion

        #region Properties
        /// <summary>
        /// Gets the path of the tagged file which the TaggedFileParser governs.
        /// </summary>
        protected string FilePath {
            get;
            set;
        }
        /// <summary>
        /// Gets the LasiFile object which encapsulates the input file which the TaggedFileParser governs.
        /// </summary>
        public TaggedFile TaggededDocumentFile {
            get;
            protected set;
        }

        protected string TaggedInputData {
            get;
            set;
        }
        private readonly PhraseTagsetMap PhraseTagset = new SharpNLPPhraseTagsetMap();
        #endregion


    }
}
