using LASI;
using LASI.Core;
using LASI.Core.DocumentStructures;
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
    internal class TaggedSourceParser : LASI.ContentSystem.TaggerEncapsulation.TagParser
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

        /// <summary>
        /// Returns a LASI.Algorithm.DocumentConstructs.Document instance representating of all of the textual constructs in the source.
        /// </summary>
        /// <returns>A traversable, queriable LASI.Algorithm.DocumentConstructs.Document instance defining representing the textual constructs of the tagged file which the TaggedSourceParser governs. 
        /// </returns>
        public override Document LoadDocument() {
            return new Document(LoadParagraphs()) {
                Name = TaggededDocumentFile != null ? TaggededDocumentFile.NameSansExt : "Untitled"
            };
        }



        public override async Task<Document> LoadDocumentAsync() {
            return await Task.Run(() => LoadDocument());
        }
        /// <summary>
        /// Returns the strongly typed representations of the sentences, componentPhrases,and words extracted from the tagged file the TaggedSourceParser governs.
        /// </summary>
        /// <returns>The stringly typed constructs which represent the text of the document, aggregated into paragraphs.</returns>
        public override IEnumerable<Paragraph> LoadParagraphs() {

            var data = PreProcessText(TaggedInputData.Trim());
            var results = new List<Paragraph>();
            foreach (var paragraph in ParseParagraphs(data))
                results.Add(BuildParagraph(paragraph));

            return results;
        }

        private Paragraph BuildParagraph(string paragraph) {
            var parsedSentences = new List<Sentence>();
            bool hasBulletOrHeading;
            var sentences = from s in SplitIntoSentences(paragraph, out hasBulletOrHeading)
                            select s.Trim();
            foreach (var sent in from s in sentences
                                 where s.IsNotWsOrNull()
                                 select s) {
                var parsedClauses = new List<Clause>();
                var parsedPhrases = new List<Phrase>();
                var chunks = from chunk in sent.SplitRemoveEmpty("[", "]")
                             let s = chunk.Trim()
                             where s.IsNotWsOrNull()
                             select s;
                SentenceEnding sentencePunctuation = null;

                foreach (var s in chunks) {
                    if (s.IsNotWsOrNull() && s.Contains('/')) {
                        char token = SkipToNextElement(s);
                        if (token == ' ') {
                            var currentPhrase = ParsePhrase(new TaggedText(text: s.Substring(s.IndexOf(' ')), tag: s.Substring(0, s.IndexOf(' '))));
                            if (currentPhrase.Words.Any(w => w.Text.IsNotWsOrNull()))
                                parsedPhrases.Add(currentPhrase);

                            if (currentPhrase is SubordinateClauseBeginPhrase) {
                                parsedClauses.Add(new Clause(parsedPhrases.Take(parsedPhrases.Count - 1)));
                                parsedPhrases = new List<Phrase>();
                                parsedPhrases.Add(currentPhrase);
                            }

                        } else if (token == '/') {
                            var words = CreateWords(s);
                            if (words.First() != null) {
                                if (words.Any(w => w is DoubleQuote || w is SingleQuote)) {
                                    parsedPhrases.Add(new SymbolPhrase(words));
                                    parsedClauses.Add(new Clause(parsedPhrases.Take(parsedPhrases.Count)));
                                    parsedPhrases = new List<Phrase>();
                                } else {
                                    if (words.All(w => w is Conjunction) || (words.Count == 2 && words[0] is Punctuator && words[1] is Conjunction)) {
                                        parsedPhrases.Add(new ConjunctionPhrase(words));
                                    } else if (words.Count == 1 && words[0] is SentenceEnding) {
                                        sentencePunctuation = words.First() as SentenceEnding;
                                        parsedClauses.Add(new Clause(parsedPhrases.Take(parsedPhrases.Count)));
                                        parsedPhrases = new List<Phrase>();
                                    } else if (words.All(w => w is Punctuator) || words.All(w => w is Punctuator || w is Conjunction)) {
                                        parsedPhrases.Add(new SymbolPhrase(words));
                                    } else {
                                        parsedPhrases.Add(new UnknownPhrase(words));
                                    }
                                }
                            }
                        }
                    }
                }
                parsedSentences.Add(new Sentence(parsedClauses, sentencePunctuation));
            }
            return new Paragraph(parsedSentences, hasBulletOrHeading ? ParagraphKind.NumberedOrBullettedContent : ParagraphKind.Default);
        }

        private static IEnumerable<string> SplitIntoSentences(string paragraph, out bool hasBulletOrHeading) {
            hasBulletOrHeading = paragraph.Contains("<enumeration>");
            return paragraph.SplitRemoveEmpty("<sentence>", "</sentence>").Select(s => s.RemoveElements("<enumeration>", "</enumeration>"));
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
        protected virtual string PreProcessText(string data) {

            data = data.Replace(" [/-LRB-", " LEFT_SQUARE_BRACKET/-LRB-");

            data = data.Replace("]/-RRB- ", "RIGHT_SQUARE_BRACKET/-RRB- ").RemoveElements("<enumeration>", "</enumeration>");
            return data;
        }/// <summary>
        /// Asynchronously Pre-processes the line read from the file by replacing some instances of problematic text such as square brackets, with tokens that are easier to reliably parse.
        /// </summary>
        /// <param name="data">The string containing raw SharpNLP tagged-text to process.</param>
        /// <returns>The string containing the processed text.</returns>
        protected virtual async Task<string> PreProcessTextAsync(string data) {
            return await Task.Run(() => {
                data = data.Replace(" [/-LRB-", " LEFT_SQUARE_BRACKET/-LRB-");

                data = data.Replace("]/-RRB- ", "RIGHT_SQUARE_BRACKET/-RRB- ");
                return data;
            });
        }

        /// <summary>
        /// Reads an [NP Square Brack Delimited Phrase Chunk] and returns the start-tag determined subtype of LASI.Algorithm.Phrase which in turn contains all the Part of Speech subtyped LASI.Algorithm.Word instances of the individual words within the chunk.
        /// </summary>
        /// <param name="taggedPhraseElement">The TextTagPair instance which contains the content of the start and its Tag.</param>
        /// <returns>A LASI.Algorithm.Phrase instance corresponding to the given phrase tag and containing the words within it.</returns>
        protected virtual Phrase ParsePhrase(TaggedText taggedPhraseElement) {
            var phraseTag = taggedPhraseElement.Tag.Trim();
            var words = CreateWords(taggedPhraseElement.Text);
            return parsePhrase(phraseTag, words);
        }
        /// <summary>
        /// Reads an [NP Square Brack Delimited Phrase Chunk] and returns the start-tag determined subtype of LASI.Phrase which in turn contains all the Part of Speech subtyped LASI.Algorithm.Word instances of the individual words within the chunk.
        /// </summary>
        /// <param name="taggedPhraseElement">The TextTagPair instance which contains the content of the Phrase and its Tag.</param>
        /// <returns>A Task&lt;string&gt; which, when awaited, yields a LASI.Algorithm.Phrase instance corresponding to the given phrase tag and containing the words within it. </returns>
        protected virtual async Task<Phrase> ParsePhraseAsync(TaggedText taggedPhraseElement) {
            var phraseTag = taggedPhraseElement.Tag.Trim();
            var words = await CreateWordsAsync(taggedPhraseElement.Text);
            return parsePhrase(phraseTag, words);
        }

        private Phrase parsePhrase(string phraseTag, List<Word> words) {
            try {
                var phraseConstructor = phraseTagset[phraseTag];
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
        protected virtual Func<Phrase> CreatePhraseExpression(TaggedText taggedPhraseElement) {
            var phraseTag = taggedPhraseElement.Tag.Trim();
            var wordExprs = CreateWordExpressions(taggedPhraseElement.Text);
            try {
                var phraseConstructor = phraseTagset[phraseTag];
                return () => phraseConstructor(wordExprs.Select(we => we.Value));
            }
            catch (UnknownPhraseTagException e) {
                Output.WriteLine("\n{0}\nPhrase Words: {1}\nInstantiating new Func<LASI.Algorithm.UnknownPhrase> to compensate", e.Message, wordExprs.Format(expr => expr.Value.ToString()));
                return () => new UnknownPhrase(wordExprs.Select(we => we.Value));
            }
            catch (EmptyPhraseTagException e) {
                Output.WriteLine("\n{0}\nPhrase Words: {1}\nInstantiating new Func<LASI.Algorithm.UnknownPhrase> to compensate", e.Message, wordExprs.Format(expr => expr.Value.ToString()));
                return () => new UnknownPhrase(wordExprs.Select(we => we.Value));
            }
        }
        protected virtual async Task<List<Word>> CreateWordsAsync(string wordData) {
            return await Task.Run(() => CreateWords(wordData));
        }

        /// <summary>
        /// Parses a string of text containing tagged words 
        /// e.g. "LASI/NNP can/MD sniff-out/VBP the/DT problem/NN" 
        /// into a collection of Part of Speech subtyped LASI.Algorithm.Word instances which represent them.
        /// </summary>
        /// <param name="text">A string containing tagged words from which to instantiate LASI.Algorithm.Word instances.</param>
        /// <returns>The collection of Part of Speech subtyped LASI.Algorithm.Word instances each corresponding to a tagged word element.</returns>
        protected virtual List<Word> CreateWords(string text) {
            var parsedWords = new List<Word>();
            var wordExtractor = new TaggedWordExtractor();

            var wordMapper = new WordFactory(wordTagset);
            foreach (var element in GetTaggedWordStrings(text)) {
                TaggedText? textTagPair = wordExtractor.Extract(element);
                if (textTagPair.HasValue) {
                    try {
                        parsedWords.Add(wordMapper.Create(textTagPair.Value));
                    }
                    catch (UnknownWordTagException x) {
                        Output.WriteLine("\n{0}\nText: {1}\nInstantiating new LASI.Algorithm.UnknownWord to compensate\nAttempting to parse data: {2}", x.Message, textTagPair.GetValueOrDefault().Text, element);
                        parsedWords.Add(new UnknownWord(textTagPair.Value.Text));
                    }
                }
            }
            return parsedWords;
        }

        private static string[] GetTaggedWordStrings(string wordData) {
            var elements = wordData.SplitRemoveEmpty(' ', '\r', '\n', '\t');
            return elements;
        }
        /// <summary>
        /// Parses a string of text containing tagged words,
        /// e.g. "LASI/NNP can/MD sniff-out/VBP the/DT problem/NN",
        /// and returns of the collection containing, for each word, the function which will create the Part of Speech subtyped word instance
        /// representing that word.
        /// </summary>
        /// <param name="wordData">A string containing tagged words.</param>
        /// <returns>The List of constructor function instances which, when invoked, create the instances LASI.Algorithm.Word which represent each word in the source</returns>
        protected virtual List<Lazy<Word>> CreateWordExpressions(string wordData) {
            var wordExpressions = new List<Lazy<Word>>();
            var elements = GetTaggedWordStrings(wordData);
            var posExtractor = new TaggedWordExtractor();
            var tagParser = new WordFactory(wordTagset);
            foreach (var element in elements) {
                TaggedText? textTagPair = posExtractor.Extract(element);
                if (textTagPair.HasValue) {
                    try {
                        wordExpressions.Add(new Lazy<Word>(() => tagParser.Create(textTagPair.Value)));
                    }
                    catch (UnknownWordTagException e) {
                        Output.WriteLine("\n{0}\nText: {1}\nInstantiating new Lazy<LASI.Algorithm.UnknownWord> to compensate\nAttempting to parse data: {2}", e.Message, textTagPair.GetValueOrDefault().Text, element);
                        wordExpressions.Add(new Lazy<Word>(() => new UnknownWord(textTagPair.Value.Text)));
                    }
                }
            }
            return wordExpressions;
        }


        #endregion

        #region Fields

        private readonly WordTagsetMap wordTagset = new SharpNLPWordTagsetMap();

        private readonly PhraseTagsetMap phraseTagset = new SharpNLPPhraseTagsetMap();



        #endregion

    }
}
