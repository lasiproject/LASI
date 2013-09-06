using LASI;
using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.ContentSystem.TaggerEncapsulation;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace LASI.ContentSystem
{
    class TaggedSourceParser : LASI.ContentSystem.TaggerEncapsulation.TagParser
    {
        #region Construtors
        /// <summary>
        /// Initialized a new instance of the TaggedFilerParser class to parse the contents of the specified file.
        /// </summary>
        /// <param name="file">The wrapper which encapsulates the newPath information for the pre-POS-tagged file to parse.</param>
        public TaggedSourceParser(ITaggedTextSource file) {

            TaggedInputData = file.GetText().Trim();
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
            return new Document(LoadParagraphs()) {
                Name = TaggededDocumentFile != null ? TaggededDocumentFile.NameSansExt : "Untitled"
            };
        }



        public override async Task<Document> LoadDocumentAsync() {
            return await Task.Run(() => LoadDocument());
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
                                 where !string.IsNullOrWhiteSpace(s)
                                 select s) {
                var parsedClauses = new List<Clause>();
                var parsedPhrases = new List<Phrase>();
                var chunks = from chunk in sent.Split(new[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries)
                             let s = chunk.Trim()
                             where !string.IsNullOrWhiteSpace(s)
                             select s;
                SentenceEnding sentencePunctuation = null;

                foreach (var s in chunks) {
                    if (!string.IsNullOrWhiteSpace(s) && s.Contains('/')) {
                        char token = SkipToNextElement(s);
                        if (token == ' ') {
                            var currentPhrase = ParsePhrase(new TextTagPair(elementText: s.Substring(s.IndexOf(' ')), elementTag: s.Substring(0, s.IndexOf(' '))));
                            if (currentPhrase.Words.Any(w => !string.IsNullOrWhiteSpace(w.Text)))
                                parsedPhrases.Add(currentPhrase);

                            if (currentPhrase is SubordinateClauseBeginPhrase) {
                                parsedClauses.Add(new Clause(parsedPhrases.Take(parsedPhrases.Count - 1)));
                                parsedPhrases = new List<Phrase>();
                                parsedPhrases.Add(currentPhrase);
                            }

                        } else if (token == '/') {
                            var words = CreateWords(s);
                            if (words.First() != null) {
                                if (words.All(w => w is Conjunction) || (words.Count == 2 && words[0] is Punctuation && words[1] is Conjunction)) {
                                    parsedPhrases.Add(new ConjunctionPhrase(words));
                                } else if (words.Count == 1 && words[0] is SentenceEnding) {
                                    sentencePunctuation = words.First() as SentenceEnding;
                                    parsedClauses.Add(new Clause(parsedPhrases.Take(parsedPhrases.Count)));
                                    parsedPhrases = new List<Phrase>();
                                } else if (words.All(w => w is Punctuation) || words.All(w => w is Punctuation || w is Conjunction)) {
                                    parsedPhrases.Add(new SymbolPhrase(words));
                                } else {
                                    parsedPhrases.Add(new UnknownPhrase(words));
                                }
                            }
                        }
                    }
                }
                parsedSentences.Add(new Sentence(parsedClauses, sentencePunctuation));
            }
            return new Paragraph(parsedSentences, hasEnumElemenets ? ParagraphKind.NumberedOrBullettedContent : ParagraphKind.Default);
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
                token = (char)reader2.Read();
            }
            token = (char)reader2.Read();
            return token;
        }




        /// <summary>
        /// Pre-processes the line read from the file by replacing some instances of problematic text such as square brackets, with tokens that are easier to reliably parse.
        /// </summary>
        /// <param name="data">The string containing raw SharpNLP tagged-text to process.</param>
        /// <returns>The string containing the processed text.</returns>
        protected virtual string PreProcessTextData(string data) {

            data = data.Replace(" [/-LRB-", " LEFT_SQUARE_BRACKET/-LRB-");

            data = data.Replace("]/-RRB- ", "RIGHT_SQUARE_BRACKET/-RRB- ").Replace("<enumeration>", "").Replace("</enumeration>", "");
            return data;
        }/// <summary>
        /// Asynchronously Pre-processes the line read from the file by replacing some instances of problematic text such as square brackets, with tokens that are easier to reliably parse.
        /// </summary>
        /// <param name="data">The string containing raw SharpNLP tagged-text to process.</param>
        /// <returns>The string containing the processed text.</returns>
        protected virtual async Task<string> PreProcessTextDataAsync(string data) {
            return await Task.Run(() => {
                data = data.Replace(" [/-LRB-", " LEFT_SQUARE_BRACKET/-LRB-");

                data = data.Replace("]/-RRB- ", "RIGHT_SQUARE_BRACKET/-RRB- ");
                return data;
            });
        }

        /// <summary>
        /// Reads an [outerNP Square Brack Delimited Phrase Chunk] and returns the start-tag determined subtype of LASI.Algorithm.Phrase which in turn contains all the run time representations of the individual words within it.
        /// </summary>
        /// <param name="taggedPhraseElement">The TextTagPair instance which contains the content of the start and its Tag.</param>
        /// <returns>A LASI.Algorithm.Phrase instance corresponding to the given phrase tag and containing the words within it.</returns>
        protected virtual Phrase ParsePhrase(TextTagPair taggedPhraseElement) {
            var phraseTag = taggedPhraseElement.Tag.Trim();
            var words = CreateWords(taggedPhraseElement.Text);
            try {
                var phraseConstructor = PhraseTagset[phraseTag];
                return phraseConstructor(words);
            }
            catch (UnknownPhraseTagException e) {
                Output.WriteLine("\n{0}\nPhrase Words: {1}\nInstantiating new LASI.Algorithm.UnknownPhrase to compensate", e.Message, words.Format());
                return new UnknownPhrase(words);
            }
            catch (EmptyPhraseTagException e) {
                Output.WriteLine("\n{0}\nPhrase Words: {1}\nInstantiating new LASI.Algorithm.UnknownPhrase to compensate", e.Message, words.Format());
                return new UnknownPhrase(words);
            }
        }
        /// <summary>
        /// Reads an [outerNP Square Brack Delimited Phrase Chunk] and returns the start-tag determined subtype of LASI.Phrase which in turn contains all the run time representations of the individual words within it.
        /// </summary>
        /// <param name="taggedPhraseElement">The TextTagPair instance which contains the content of the Phrase and its Tag.</param>
        /// <returns>A Task&lt;string&gt; which, when awaited, yields a LASI.Algorithm.Phrase instance corresponding to the given phrase tag and containing the words within it. </returns>
        protected virtual async Task<Phrase> ParsePhraseAsync(TextTagPair taggedPhraseElement) {
            var phraseTag = taggedPhraseElement.Tag.Trim();
            var words = await CreateWordsAsync(taggedPhraseElement.Text);
            try {
                var phraseConstructor = PhraseTagset[phraseTag];
                return phraseConstructor(words);
            }
            catch (UnknownPhraseTagException e) {
                Output.WriteLine("\n{0}\nPhrase Words: {1}\nInstantiating new LASI.Algorithm.UnknownPhrase to compensate", e.Message, words.Format());
                return new UnknownPhrase(words);
            }
            catch (EmptyPhraseTagException e) {
                Output.WriteLine("\n{0}\nPhrase Words: {1}\nInstantiating new LASI.Algorithm.UnknownPhrase to compensate", e.Message, words.Format());
                return new UnknownPhrase(words);
            }
        }
        protected virtual Func<Phrase> CreatePhraseExpression(TextTagPair taggedPhraseElement) {
            var phraseTag = taggedPhraseElement.Tag.Trim();
            var wordExprs = CreateWordExpressions(taggedPhraseElement.Text);
            try {
                var phraseConstructor = PhraseTagset[phraseTag];
                return () => phraseConstructor(wordExprs.Select(we => we.Value));
            }
            catch (UnknownPhraseTagException e) {
                Output.WriteLine("\n{0}\nPhrase Words: {1}\nInstantiating new LASI.Algorithm.UnknownPhrase to compensate", e.Message, wordExprs.Format(expr => expr.Value.ToString()));
                return () => new UnknownPhrase(wordExprs.Select(we => we.Value));
            }
            catch (EmptyPhraseTagException e) {
                Output.WriteLine("\n{0}\nPhrase Words: {1}\nInstantiating new LASI.Algorithm.UnknownPhrase to compensate", e.Message, wordExprs.Format(expr => expr.Value.ToString()));
                return () => new UnknownPhrase(wordExprs.Select(we => we.Value));
            }
        }
        protected virtual async Task<List<Word>> CreateWordsAsync(string wordData) {
            return await Task.Run(() => CreateWords(wordData));
        }

        /// <summary>
        /// Parses a string of text containing tagged words 
        /// e.g. "LASI/NNP can/MD sniff-out/VBP the/DT problem/NN" 
        /// into a collection of Part of Speech subtyped wd instances which represent them.
        /// </summary>
        /// <param name="wordData">A string containing tagged words.</param>
        /// <returns>The collection of wd objects that is their run time representation.</returns>
        protected virtual List<Word> CreateWords(string wordData) {
            var parsedWords = new List<Word>();
            var elements = GetTaggedWordLevelTokens(wordData);
            var wordExtractor = new WordExtractor();

            var wordMapper = new WordMapper();
            foreach (var tagged in elements) {
                var e = wordExtractor.ExtractNextPos(tagged);
                if (e != null) {
                    try {
                        parsedWords.Add(wordMapper.CreateWord(e.Value));
                    }
                    catch (UnknownWordTagException x) {
                        Output.WriteLine("\n{0}\nText: {1}\nInstantiating new LASI.Algorithm.UnknownWord to compensate", x.Message, e.GetValueOrDefault().Text);
                        parsedWords.Add(new UnknownWord(e.Value.Text));
                    }
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
        /// and returns of the collection containing, for each word, the function which will create the Part of Speech subtyped wd instance
        /// representing that word.
        /// </summary>
        /// <param name="wordData">A string containing tagged words.</param>
        /// <returns>The List of constructor function instances which, when invoked, create run time objects which represent each word in the source</returns>
        protected virtual List<Lazy<Word>> CreateWordExpressions(string wordData) {
            var wordExpressions = new List<Lazy<Word>>();
            var elements = GetTaggedWordLevelTokens(wordData);
            var posExtractor = new WordExtractor();

            var tagParser = new WordMapper();
            foreach (var tagged in elements) {
                var e = posExtractor.ExtractNextPos(tagged);
                if (e.HasValue) {
                    try {
                        wordExpressions.Add(tagParser.GetLazyWord(e.Value));
                    }
                    catch (UnknownWordTagException x) {
                        Output.WriteLine("\n{0}\nText: {1}\nInstantiating new LASI.Algorithm.UnknownWord to compensate", x.Message, e.GetValueOrDefault().Text);
                        wordExpressions.Add(new Lazy<Word>(() => new UnknownWord(e.Value.Text)));
                    }
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
