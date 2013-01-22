using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LASI.FileSystem
{
    public class TaggedFileParser
    {
        #region Construtors
        /// <summary>
        /// Initialized a new instance of the TaggedFilerParser class to parse the contents of a specific file.
        /// </summary>
        /// <param name="filePath">The absosultePath of the pre-POS-tagged file to parse.</param>
        public TaggedFileParser(string filePath) {
            FilePath = filePath;
            TaggededFile = new LasiFile(filePath);
        }
        /// <summary>
        /// Initialized a new instance of the TaggedFilerParser class to parse the contents of a specific file.
        /// </summary>
        /// <param name="filePath">The wrapper which encapsulates the path information for the pre-POS-tagged file to parse.</param>
        public TaggedFileParser(LasiFile file) {
            TaggededFile = file;
            FilePath = TaggededFile.FullPath;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Returns an instance of Algorithm.Document which contains the run time representation of all of the textual construct in the document, for the Algorithm to analyse.
        /// </summary>
        /// <returns>An traversable, queriable, immutable document object defining the run time representation of the tagged file which the TaggedFileParser governs. </returns>
        public virtual Algorithm.Document GetDocument() {
            return new Algorithm.Document(GetParagraphs());
        }

        /// <summary>
        /// Returns the run time representations of the sentences, phrases,and words extracted from the tagged file the TaggedFileParser governs.
        /// </summary>
        /// <returns>The run time constructs which represent the text of the document, aggregated into paragraphs.</returns>
        public virtual IEnumerable<Algorithm.Paragraph> GetParagraphs() {

            var results = new List<Algorithm.Paragraph>();
            using (var reader = new StreamReader(FilePath, Encoding.UTF7)) {
                var data = reader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine("paragraph count = {0}", ParseParagraphs(data).Count());
                data = PreProcessTextData(data);
                foreach (var paragraph in ParseParagraphs(data)) {
                    var parsedOut = new List<Algorithm.Phrase>();
                    var chunks = from chunk in paragraph.Split(new[] { "[", "]" }, StringSplitOptions.None)
                                 where !String.IsNullOrWhiteSpace(chunk) && !String.IsNullOrEmpty(chunk)
                                 select chunk.Trim();
                    var count = 0;
                    foreach (var chunk in chunks) {
                        System.Diagnostics.Debug.WriteLine(count.ToString() + " " + chunk + '\n');
                        count++;
                        var reader2 = (new StringReader(chunk));
                        char tagType = '>';
                        while (reader2.Peek() != '/' && reader2.Peek() != ' ') {
                            tagType = (char) reader2.Read();
                        }
                        tagType = (char) reader2.Read();
                        if (tagType == ' ') {
                            var currentPhrase = ParsePhrase(new TextTagPair {
                                Tag = chunk.Substring(0, chunk.IndexOf(' ')),
                                Text = chunk.Substring(chunk.IndexOf(' '))
                            });
                            parsedOut.Add(currentPhrase);
                        } else if (tagType == '/') {
                            var words = ParseWords(chunk);
                            //parsedOut.Add(new LASI.Algorithm.NounPhrase(words));
                        }
                    }
                    results.Add(new Algorithm.Paragraph(new Algorithm.Sentence(parsedOut)));
                }
            }
            return results;
        }

        /// <summary>
        /// Returns a collection of Algorithm.Phrase constructs which are the run time representations of the phrases in the tagged file which the TaggedFileParser governs.
        /// </summary>
        /// <returns>A collection of Algorithm.Phrase objects which represent the text of the document, aggregated into phrases.</returns>
        public virtual IEnumerable<Algorithm.Phrase> GetPhrases() {

            var parsedOut = new List<Algorithm.Phrase>();
            using (var reader = new StreamReader(FilePath, true)) {
                var data = reader.ReadToEnd();
                //  System.Diagnostics.Debug. WriteLine("paragraph count = {0}", ParseParagraphs(data).Count());
                data = PreProcessTextData(data);
                var chunks = from chunk in data.Split(new[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries)
                             where !String.IsNullOrWhiteSpace(chunk) && !String.IsNullOrEmpty(chunk)
                             select chunk.Trim();
                var count = 0;
                foreach (var chunk in chunks) {

                    //  System.Diagnostics.Debug.WriteLine(count.ToString() + " " + chunk + '\n');

                    count++;
                    var reader2 = (new StringReader(chunk));
                    char currentChar = ' ';
                    while (reader2.Peek() != '/' && reader2.Peek() != ' ') {
                        currentChar = (char) reader2.Read();
                    }
                    currentChar = (char) reader2.Read();
                    if (currentChar == ' ') {
                        var currentPhrase = ParsePhrase(new TextTagPair {
                            Tag = chunk.Substring(0, chunk.IndexOf(' ')),
                            Text = chunk.Substring(chunk.IndexOf(' '))
                        });
                        parsedOut.Add(currentPhrase);
                    } else if (currentChar == '/') {
                        var words = ParseWords(chunk);
                    }
                }
            }
            return parsedOut;
        }

        /// <summary>
        /// Returns a collection which contains the run-time Algorithm.Word constructs which represent the all text in the tagged file which the TaggedFileParser governs.
        /// </summary>
        /// <returns>
        /// A collection of Algorithm.Word objects which represent the text of the document.</returns>
        public virtual IEnumerable<Algorithm.Word> GetWords() {
            return from P in GetPhrases()
                   from W in P.Words
                   select W;
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
        protected virtual Algorithm.Phrase ParsePhrase(TextTagPair taggedContent) {
            // System.Diagnostics.Debug.WriteLine("parsing phrase");
            var phraseTag = taggedContent.Tag.Trim();
            var composed = ParseWords(taggedContent.Text);
            switch (phraseTag) {
                case "ADVP":
                    return new Algorithm.AdverbPhrase(composed);
                case "ADJP":
                    return new Algorithm.AdjectivePhrase(composed);
                case "PP":
                    return new Algorithm.PrepositionalPhrase(composed);
                case "PRT":
                    return new Algorithm.ParticlePhrase(composed);
                case "VP":
                    return new Algorithm.VerbPhrase(composed);
                case "NP":
                    return new Algorithm.NounPhrase(composed);
                case "S":
                    return new Algorithm.SimpleDeclarativePhrase(composed);
                case "SBAR":
                    return new Algorithm.PrepositionalPhrase(composed);
                case "SBARQ":
                    return new Algorithm.InterrogativePhrase(composed);
                case "CONJP":
                    return new Algorithm.InterrogativePhrase(composed);
                case "LST":
                    return new Algorithm.RoughListPhrase(composed);
                default: {
                        throw new UnknownPhraseTypeException(phraseTag);
                    }
            }
        }

        /// <summary>
        /// Parses a string of text containing tagged words e.g. "LASI/NNP can/MD sniff-out/VBP the/DT problem/NN" into a collection of Part of Speech subtyped Word instances which represent them.
        /// </summary>
        /// <param name="wordData">A string containing tagged words.</param>
        /// <returns>The collection of Word objects that is their run time representation.</returns>
        protected virtual List<Algorithm.Word> ParseWords(string wordData) {
            var elements = wordData.Split(new[] { ' ', });
            var posExtractor = new PosExtractor();
            var parsedWords = new List<Algorithm.Word>();
            var tagParser = new WordTagParser();
            foreach (var tagged in elements) {
                var e = posExtractor.ExtractNextPos(tagged);
                if (e != null) {

                    var word = (tagParser.ReadWord(e.Value));
                    parsedWords.Add(word);
                }
            }
            return parsedWords;
        }

        /// <summary>
        /// Breaks a string of text containing multiple paragraphs into a collection of strings each representing an individual paragraph. Paragraphs are delimited using the default regular expression pattern "[\r\n]+[^]*[\r\n]+"
        /// </summary>
        /// <param name="data">A string containing the text to be broken down.</param>
        /// <returns>A collection of strings, each entry corresponding to the entire content of a single paragraph.</returns>
        protected IEnumerable<string> ParseParagraphs(string data) {
            return data.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);


        }

        /// <summary>
        /// Breaks a string of text containing multiple paragraphs into a collection of strings each representing an individual paragraph. Paragraphs are delimited using the given regular expression pattern.
        /// </summary>
        /// <param name="data">A string containing the text to be broken down.</param>
        /// <param name="regexPattern">The regular expression used delimit and split paragraphs.</param>
        /// <returns>A collection of strings, each entry corresponding to the entire content of a single paragraph.</returns>
        protected IEnumerable<string> ParseParagraphs(string data, string regexPattern) {
            var results = Regex.Split(data, regexPattern, RegexOptions.Multiline);
            return results;
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
        public LasiFile TaggededFile {
            get;
            protected set;
        }
        #endregion


    }
}


////Regex rgx = new Regex("[\r\n]+[^]*[\r\n]+");
//var results = Regex.Split(data, @"(x?)\G\A\w+$", RegexOptions.Multiline);
////var matches = rgx.Matches(data);
////var results = new string[matches.Count];
////for (var i = 0; i < matches.Count; ++i)
////    results[i] = matches[i].Value;
//return results.Where(s => !String.IsNullOrWhiteSpace(s));
////return data.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);