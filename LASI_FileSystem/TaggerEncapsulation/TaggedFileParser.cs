using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LASI.Algorithm;

namespace LASI.FileSystem
{
    public class TaggedFileParser
    {
        #region Construtors


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
        /// Returns an instance of Document which contains the run time representation of all of the textual construct in the document, for the to analyse.
        /// </summary>
        /// <returns>A traversable, queriable document object defining the run time representation of the tagged file which the TaggedFileParser governs. </returns>
        public virtual Document ConstructDocument() {
            return new Document(ConstructParagraphs());
        }
        public virtual async Task<Document> GetDocumentAsync() {
            return new Document(await GetParagraphsAsync());
        }

        public virtual async Task<IEnumerable<Paragraph>> GetParagraphsAsync() {
            return await Task.Run(() => ConstructParagraphs());
        }

        /// <summary>
        /// Returns the run time representations of the sentences, phrases,and words extracted from the tagged file the TaggedFileParser governs.
        /// </summary>
        /// <returns>The run time constructs which represent the text of the document, aggregated into paragraphs.</returns>
        public virtual IEnumerable<Paragraph> ConstructParagraphs() {
            var result = new List<Paragraph>();

            var data = PreProcessTextData(TaggedInputData);
            foreach (var paragraph in SplitParagraphs(data)) {
                var parsedSentences = new List<Sentence>();
                var sentences = SplitSentences(paragraph);
                foreach (var sent in sentences) {
                    SentenceDelimiter endingPunctuatuin = null;
                    var parsedPhrases = new List<Phrase>();
                    var chunks = from chunk in paragraph.Split(new[] { '[', ']' }, StringSplitOptions.None)
                                 where !String.IsNullOrWhiteSpace(chunk) && !String.IsNullOrEmpty(chunk)
                                 select chunk.Trim();

                    foreach (var chunk in chunks) {

                        char token = FindNextTag(chunk);
                        if (token == ' ') {//A phrase tag is encountered. Such tags are "[ PhraseTAG first words" and do not begin with a slash
                            var currentPhrase = ParsePhrase(
                                new TaggedWordObject {
                                    Tag = chunk.Substring(0, chunk.IndexOf(' ')),
                                    Text = chunk.Substring(chunk.IndexOf(' '))
                                });
                            parsedPhrases.Add(currentPhrase);
                        } else if (token == '/') {//A tagged word, not wrapped in phrase brackets([words]) is encountered
                            var words = ConstructWords(chunk);
                            if (words.Count < 3) {
                                if (words.Last()is Conjunction) {
                                    var currentPhrase = new ConjunctionPhrase(words);
                                    parsedPhrases.Add(currentPhrase);
                                }
                            } else if (words.Last() is SentenceDelimiter) {

                                endingPunctuatuin = words.Last() as SentenceDelimiter;
                            } else {
                                var currentPhrase = new UndeterminedPhrase(words);
                                parsedPhrases.Add(currentPhrase);
                            }

                        }


                    }
                    parsedSentences.Add(new Sentence(parsedPhrases, endingPunctuatuin));
                }
                result.Add(new Paragraph(parsedSentences));
            }

            return result;
        }

        private char FindNextTag(string chunk) {
            var reader2 = (new StringReader(chunk));
            char token = '`';
            while (reader2.Peek() != '/' && reader2.Peek() != ' ') {
                token = (char) reader2.Read();
            }
            token = (char) reader2.Read();
            return token;
        }




        /// <summary>
        /// Pre-processes the data read from the file by replacing some instances of problematic text such as square brackets, with tokens that are easier to reliably parse.
        /// </summary>
        /// <param name="data">The string containing raw SharpNLP tagged-text to process.</param>
        /// <returns>The string containing the processed text.</returns>
        private string PreProcessTextData(string data) {

            data = data.Replace(" [/-LRB-", " LEFT_SQUARE_BRACKET/-LRB-");

            data = data.Replace("]/-RRB- ", "RIGHT_SQUARE_BRACKET/-RRB- ");
            return data;
        }

        /// <summary>
        /// Reads a [NP Square Brack Delimited Phrase Chunk] and returns a phrase tag determined subtype of LASI.Phrase which in turn contains all the run time representations of the individual words within it.
        /// </summary>
        /// <param name="taggedContent">The TextTagPair instance which contains the content of a phrase and its Tag.</param>
        /// <returns></returns>
        private Phrase ParsePhrase(TaggedWordObject taggedContent) {
            var phraseTag = taggedContent.Tag.Trim();
            var composed = ConstructWords(taggedContent.Text);
            switch (phraseTag) {
                case "ADVP":
                    return new AdverbPhrase(composed);
                case "ADJP":
                    return new AdjectivePhrase(composed);
                case "PP":
                    return new PrepositionalPhrase(composed);
                case "PRT":
                    return new ParticlePhrase(composed);
                case "VP":
                    return new VerbPhrase(composed);
                case "NP":
                    return new NounPhrase(composed);
                case "S":
                    return new SimpleDeclarativePhrase(composed);
                case "SINV":
                    return new SimpleDeclarativePhrase(composed);
                case "SBAR":
                    return new PrepositionalPhrase(composed);
                case "SBARQ":
                    return new InterrogativePhrase(composed);
                case "SQ":
                    return new InterrogativePhrase(composed);
                case "CONJP":
                    return new ConjunctionPhrase(composed);
                case "LST":
                    return new RoughListPhrase(composed);
                default: {
                        throw new UnknownPhraseTypeException(phraseTag);
                    }
            }
        }

        /// <summary>
        /// Parses a string of text containing tagged words 
        /// e.g. "LASI/NNP can/MD sniff-out/VBP the/DT problem/NN" 
        /// into a collection of Part of Speech subtyped Word instances which represent them.
        /// </summary>
        /// <param name="wordData">A string containing tagged words.</param>
        /// <returns>The collection of Word objects that is their run time representation.</returns>
        private List<Word> ConstructWords(string wordData) {
            var parsedWords = new List<Word>();
            var elements = wordData.Split(new[] { ' ' });
            var posExtractor = new PosExtractor();

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
        /// <returns>A List of constructor function instances which, when invoked, 
        /// create run time objects which represent each word in the source</returns>
        private List<Func<Word>> GetWordConstructors(string wordData) {
            var result = new List<Func<Word>>();
            var elements = wordData.Split(new[] { ' ', });
            var posExtractor = new PosExtractor();

            var tagParser = new WordTagParser();
            foreach (var tagged in elements) {
                var e = posExtractor.ExtractNextPos(tagged);
                if (e != null) {
                    result.Add(tagParser.ReadWordExpression(e.Value));
                }
            }
            return result;
        }


        /// <summary>
        /// Breaks a string of text containing multiple paragraphs into a collection of strings each representing an individual paragraph.
        /// Paragraphs are delimited using the default regular expression pattern "[\r\n]+[^]*[\r\n]+"
        /// </summary>
        /// <param name="data">A string containing the text to be broken down.</param>
        /// <returns>A collection of strings, each entry corresponding to the entire content of a single paragraph.</returns>
        private IEnumerable<string> SplitParagraphs(string data) {
            return data.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);
        }
        private IEnumerable<string> SplitSentences(string data) {
            var result = new List<string>();
            var delims = new[] { "./.", "!/.", "?/." };
            while (data.Length > 2) {
                var validIndeces = from d in delims
                                   let i = data.IndexOf(d)
                                   where i != -1
                                   orderby i ascending
                                   select i;
                var index = validIndeces.Count() > 0 ? validIndeces.First() : -1;
                if (index != -1) {
                    var sentenceStr = data.Substring(0, index + 3);
                    result.Add(sentenceStr);
                    data = data.Substring(index + 3);
                } else {
                    result.Add(data);
                    break;
                }
            }
            return result;
        }


        #endregion

        #region Properties
        /// <summary>
        /// Gets the path of the tagged file which the TaggedFileParser governs.
        /// </summary>
        private string FilePath {
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

        private string TaggedInputData {
            get;
            set;
        }

        #endregion


    }
}