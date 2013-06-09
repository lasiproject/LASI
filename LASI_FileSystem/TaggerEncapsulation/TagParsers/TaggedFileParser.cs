using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
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
        /// Initialized a new instance of the TaggedFilerParser class to parse the contents of the specified file.
        /// </summary>
        /// <param name="filePath">The wrapper which encapsulates the newPath information for the pre-POS-tagged file to parse.</param>
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
        /// Returns an instance of Document which contains the run time representation of all of the textual construct in the document, for the Algorithm to analyse.
        /// </summary>
        /// <returns>A traversable, queriable document object defining the run time representation of the tagged file which the TaggedFileParser governs. </returns>
        public override Document LoadDocument() {
            return new Document(LoadParagraphs())
            {
                FileName = TaggededDocumentFile != null ? TaggededDocumentFile.NameSansExt : "Untitled"
            };
        }

        /// <summary>
        /// Returns the run time representations of the sentences, componentPhrases,and words extracted from the tagged file the TaggedFileParser governs.
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
            bool hasEnumElemenets;
            var sentences = from s in SplitIntoSentences(paragraph, out hasEnumElemenets)
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
                SentenceDelimiter sentencePunctuation = null;

                foreach (var chunk in chunks) {
                    if (!string.IsNullOrEmpty(chunk) && !string.IsNullOrWhiteSpace(chunk) && chunk.Contains('/')) {
                        char token = SkipToNextElement(chunk);
                        if (token == ' ') {
                            var currentPhrase = ParsePhrase(new TextTagPair
                            {
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
                            if (words.First() != null) {
                                if (words.Count(w => w is Conjunction) == words.Count || (words.Count == 2 && words[0] is Punctuation && words[1] is Conjunction)) {
                                    parsedPhrases.Add(new ConjunctionPhrase(words));
                                } else if (words.Count() == 1 && words.First() is SentenceDelimiter) {
                                    sentencePunctuation = words.First() as SentenceDelimiter;
                                    parsedClauses.Add(new Clause(parsedPhrases.Take(parsedPhrases.Count)));
                                    parsedPhrases = new List<Phrase>();
                                } else if (words.Count(w => w is Punctuation) == words.Count && (words.Count(w => w is Punctuation) + words.Count(w => w is Conjunction)) == words.Count) {
                                    parsedPhrases.Add(new ConjunctionPhrase(words));
                                } else {
                                    parsedPhrases.Add(new UndeterminedPhrase(words));
                                }
                            }
                        }
                    }
                }
                parsedSentences.Add(new Sentence(parsedClauses, sentencePunctuation));
            }
            return new Paragraph(parsedSentences, hasEnumElemenets ? ParagraphKind.EnumerationContent : ParagraphKind.Default);
        }

        private static IEnumerable<string> SplitIntoSentences(string paragraph, out bool containsEnumeratedElemenets) {
            containsEnumeratedElemenets = paragraph.Contains("<enumeration>");
            return paragraph.Split(new[] { 
                "<sentence>", "</sentence>" },
                StringSplitOptions.RemoveEmptyEntries).Select(t => t.Replace("</sentence>", "").Replace("<sentence>", "").Replace("<enumeration>", "").Replace("</enumeration>", ""));

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

            data = data.Replace("]/-RRB- ", "RIGHT_SQUARE_BRACKET/-RRB- ").Replace("<enumeration>", "").Replace("</enumeration>", "");
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
        /// Reads an [outerNP Square Brack Delimited Phrase Chunk] and returns the start-tag determined subtype of LASI.Phrase which in turn contains all the run time representations of the individual words within it.
        /// </summary>
        /// <param name="taggedContent">The TextTagPair instance which contains the content of the start and its Tag.</param>
        /// <returns></returns>
        protected virtual Phrase ParsePhrase(TextTagPair taggedContent) {
            var phraseTag = taggedContent.Tag.Trim();
            var composed = CreateWords(taggedContent.Text);
            Phrase result = null;
            if (phraseTag == "NP" && composed.All(w => w is Adverb)) {
                var phraseConstructor = PhraseTagset["ADVP"];
                result = phraseConstructor(composed);
            } else {
                var phraseConstructor = PhraseTagset[phraseTag];

                result = phraseConstructor(composed);
                if (result is VerbPhrase && result.Words.First() is ToLinker) {
                    result = new InfinitivePhrase(composed.AsEnumerable());
                }
            }
            return result;
        }
        /// <summary>
        /// Reads an [outerNP Square Brack Delimited Phrase Chunk] and returns the start-tag determined subtype of LASI.Phrase which in turn contains all the run time representations of the individual words within it.
        /// </summary>
        /// <param name="taggedContent">The TextTagPair instance which contains the content of the Phrase and its Tag.</param>
        /// <returns></returns>
        protected virtual async Task<Phrase> ParsePhraseAsync(TextTagPair taggedContent) {
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
        /// into a collection of Part of Speech subtyped word instances which represent them.
        /// </summary>
        /// <param name="wordData">A string containing tagged words.</param>
        /// <returns>The collection of word objects that is their run time representation.</returns>
        protected virtual List<Word> CreateWords(string wordData) {
            var parsedWords = new List<Word>();
            var elements = GetTaggedWordLevelTokens(wordData);
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

        private static string[] GetTaggedWordLevelTokens(string wordData) {
            var elements = wordData.Split(new[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            return elements;
        }
        /// <summary>
        /// Parses a string of text containing tagged words,
        /// e.g. "LASI/NNP can/MD sniff-out/VBP the/DT problem/NN",
        /// and returns of the collection containing, for each verb, the function which will create the Part of Speech subtyped word instance
        /// representing that verb.
        /// </summary>
        /// <param name="wordData">A string containing tagged words.</param>
        /// <returns>The List of constructor function instances which, when invoked, create run time objects which represent each verb in the source</returns>
        protected virtual List<Func<Word>> CreateWordExpressions(string wordData) {
            var wordExpressions = new List<Func<Word>>();
            var elements = GetTaggedWordLevelTokens(wordData);
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



        #endregion


    }
}
