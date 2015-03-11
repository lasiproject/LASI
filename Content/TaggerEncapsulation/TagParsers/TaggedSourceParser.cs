using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LASI;
using LASI.Content.Tagging;
using LASI.Core;
using LASI.Utilities;

namespace LASI.Content
{
    internal class TaggedSourceParser : LASI.Content.Tagging.TagParser
    {
        #region Construtors

        /// <summary>
        /// Initialized a new instance of the TaggedFilerParser class to parse the contents of the
        /// specified file.
        /// </summary>
        /// <param name="file">
        /// The wrapper which encapsulates the newPath information for the pre-POS-tagged file to parse.
        /// </param>
        public TaggedSourceParser(ITaggedTextSource file)
        {
            TaggedInputData = file.GetText().Trim();
        }

        #endregion Construtors

        #region Methods

        /// <summary>
        /// Returns a LASI.Algorithm.DocumentConstructs.Document instance representating of all of
        /// the textual constructs in the source.
        /// </summary>
        /// <returns>
        /// A traversable, queriable LASI.Algorithm.DocumentConstructs.Document instance defining
        /// representing the textual constructs of the tagged file which the TaggedSourceParser governs.
        /// </returns>
        public override Document LoadDocument()
        {
            return LoadDocument();
        }

        public virtual Document LoadDocument(string title)
        {
            return new Document(
                paragraphs: LoadParagraphs(),
                title: title ?? TaggedDocumentFile?.NameSansExt ?? "Untitled");
        }

        public virtual async Task<Document> LoadDocumentAsync(string title)
        {
            return await Task.Run(() => LoadDocument(title));
        }

        public override async Task<Document> LoadDocumentAsync()
        {
            return await Task.Run(() => LoadDocument());
        }

        /// <summary>
        /// Returns the strongly typed representations of the sentences, componentPhrases,and words
        /// extracted from the tagged file the TaggedSourceParser governs.
        /// </summary>
        /// <returns>
        /// The stringly typed constructs which represent the text of the document, aggregated into paragraphs.
        /// </returns>
        public override IEnumerable<Paragraph> LoadParagraphs()
        {
            var data = PreProcessText(TaggedInputData.Trim());
            var results = new List<Paragraph>();
            foreach (var paragraph in ParseParagraphs(data))
                results.Add(BuildParagraph(paragraph));

            return results;
        }

        /// <summary>
        /// Pre-processes the line read from the file by replacing some instances of problematic
        /// text such as square brackets, with tokens that are easier to reliably parse.
        /// </summary>
        /// <param name="text">The string containing raw SharpNLP tagged-text to process.</param>
        /// <returns>The string containing the processed text.</returns>
        protected virtual string PreProcessText(string text)
        {
            return text.Replace(" [/-LRB-", " LEFT_SQUARE_BRACKET/-LRB-")
                .Replace("]/-RRB- ", "RIGHT_SQUARE_BRACKET/-RRB- ")
                .RemoveSubstrings("<enumeration>", "</enumeration>");
        }

        /// <summary>
        /// Asynchronously Pre-processes the line read from the file by replacing some instances of
        /// problematic text such as square brackets, with tokens that are easier to reliably parse.
        /// </summary>
        /// <param name="text">The string containing raw SharpNLP tagged-text to process.</param>
        /// <returns>The string containing the processed text.</returns>
        protected virtual async Task<string> PreProcessTextAsync(string text)
        {
            return await Task.Run(() => PreProcessText(text));
        }


        protected virtual Func<Phrase> CreatePhraseExpression(TaggedText taggedPhraseElement)
        {
            var phraseTag = taggedPhraseElement.Tag.Trim();
            var lazyWords = CreateWordExpressions(taggedPhraseElement.Text);
            try
            {
                var phraseConstructor = phraseTagset[phraseTag];
                return () => phraseConstructor(lazyWords.Select(lazy => lazy.Value));
            }
            catch (UnknownPhraseTagException e)
            {
                Logger.Log("\n{0}\nPhrase Words: {1}\nInstantiating new System.Func<LASI.Algorithm.UnknownPhrase> to compensate",
                    e.Message,
                    lazyWords.Format(lazy => lazy.Value.ToString()));
                return () => new UnknownPhrase(lazyWords.Select(lazy => lazy.Value));
            }
            catch (EmptyPhraseTagException e)
            {
                Logger.Log("\n{0}\nPhrase Words: {1}\nInstantiating new System.Func<LASI.Algorithm.UnknownPhrase> to compensate",
                    e.Message, lazyWords.Format(lazy => lazy.Value.ToString()));
                return () => new UnknownPhrase(lazyWords.Select(we => we.Value));
            }
        }
 
        /// <summary>
        /// Parses a string of text containing tagged words e.g. "LASI/NNP can/MD sniff-out/VBP
        /// the/DT problem/NN" into a collection of Part of Speech subtyped LASI.Algorithm.Word
        /// instances which represent them.
        /// </summary>
        /// <param name="text">
        /// A string containing tagged words from which to instantiate LASI.Algorithm.Word instances.
        /// </param>
        /// <returns>
        /// The collection of Part of Speech subtyped LASI.Algorithm.Word instances each
        /// corresponding to a tagged word element.
        /// </returns>
        protected virtual List<Word> CreateWords(string text)
        {
            var parsedWords = new List<Word>();
            var wordExtractor = new TaggedWordExtractor();

            var factory = new WordFactory(wordTagset);
            foreach (var taggedToken in GetTaggedWordStrings(text))
            {
                TaggedText? textTagPair = wordExtractor.Extract(taggedToken);
                if (textTagPair.HasValue)
                {
                    var pair = textTagPair.Value;
                    try
                    {
                        parsedWords.Add(factory.Create(pair));
                    }
                    catch (EmptyWordTagException x)
                    {
                        Logger.Log($"\n{x.Message}\nText: {pair.Text}\nInstantiating new LASI.Algorithm.UnknownWord to compensate\nAttempting to parse data: {taggedToken}");
                        parsedWords.Add(new UnknownWord(pair.Text));
                    }
                    catch (UnknownWordTagException x)
                    {
                        Logger.Log($"\n{x.Message}\nText: {pair.Text}\nInstantiating new LASI.Algorithm.UnknownWord to compensate\nAttempting to parse data: {taggedToken}");
                        parsedWords.Add(new UnknownWord(pair.Text));
                    }
                    catch (EmptyOrWhiteSpaceStringTaggedAsWordException x)
                    {
                        Logger.Log("\n" + x.Message + "\nDiscarding");
                    }
                }
            }
            return parsedWords;
        }

        /// <summary>
        /// Parses a string of text containing tagged words, e.g. "LASI/NNP can/MD sniff-out/VBP
        /// the/DT problem/NN", and returns of the collection containing, for each word, the
        /// function which will create the Part of Speech subtyped word instance representing that word.
        /// </summary>
        /// <param name="text">A string containing tagged words.</param>
        /// <returns>
        /// The List of constructor function instances which, when invoked, create the instances
        /// LASI.Algorithm.Word which represent each word in the source
        /// </returns>
        protected virtual List<Lazy<Word>> CreateWordExpressions(string text)
        {
            var wordExpressions = new List<Lazy<Word>>();
            var elements = GetTaggedWordStrings(text);
            var posExtractor = new TaggedWordExtractor();
            var wordFactory = new WordFactory(wordTagset);
            foreach (var element in elements)
            {
                TaggedText? textTagPair = posExtractor.Extract(element);
                if (textTagPair.HasValue)
                {
                    var pair = textTagPair.Value;
                    try
                    {
                        wordExpressions.Add(new Lazy<Word>(() => wordFactory.Create(pair)));
                    }
                    catch (UnknownWordTagException e)
                    {
                        Logger.Log("\n{0}\nText: {1}\nInstantiating new System.Lazy<LASI.Algorithm.UnknownWord> to compensate\nAttempting to parse data: {2}", e.Message, pair.Text, element);
                        wordExpressions.Add(new Lazy<Word>(() => new UnknownWord(pair.Text)));
                    }
                }
            }
            return wordExpressions;
        }

        private static IEnumerable<string> SplitIntoSentences(string paragraph, out bool hasBulletOrHeading)
        {
            hasBulletOrHeading = paragraph.Contains("<enumeration>");
            return paragraph
                .SplitRemoveEmpty("<sentence>", "</sentence>")
                .Select(sentence => sentence.Replace("<enumeration>", "").Replace("</enumeration>", ""));
        }

        private static IEnumerable<string> GetTaggedWordStrings(string text)
        {
            return text.SplitRemoveEmpty(' ', '\r', '\n', '\t');
        }

        private Paragraph BuildParagraph(string paragraph)
        {
            var parsedSentences = new List<Sentence>();
            bool hasBulletOrHeading;
            var sentences = SplitIntoSentences(paragraph, out hasBulletOrHeading).Select(sentence => sentence.Trim());
            foreach (var sentence in sentences/*.Where(sentence => !sentence.IsNullOrWhiteSpace())*/)
            {
                var parsedSentence = BuildSentence(sentence);
                parsedSentences.Add(parsedSentence);
            }
            return new Paragraph(parsedSentences, hasBulletOrHeading ? ParagraphKind.Enumeration : ParagraphKind.Default);
        }

        private Sentence BuildSentence(string sentence)
        {
            var skipToNextElement = (Func<string, char?>)(chunk => chunk.Cast<char?>().SkipWhile(c => c != ' ' && c != '/').FirstOrDefault());


            var accumulatedClauses = new List<Clause>();
            var accumulatedPhrases = new List<Phrase>();
            var chunks = from chunk in sentence.SplitRemoveEmpty('[', ']')//where !chunk.IsNullOrWhiteSpace()
                         where chunk.Contains('/')
                         select chunk.Trim();
            SentenceEnding sentenceEnding = null;
            foreach (var chunk in chunks)
            {
                char? token = skipToNextElement(chunk);
                if (token == ' ')
                {
                    var phraseTag = chunk.Substring(0, chunk.IndexOf(' '));
                    var phraseText = chunk.Substring(chunk.IndexOf(' '));
                    var currentPhrase = CreatePhrase(phraseTag, phraseText);

                    if (currentPhrase.Words.Any(word => !word.Text.IsNullOrWhiteSpace()))
                    {
                        accumulatedPhrases.Add(currentPhrase);
                    }
                    if (currentPhrase is SubordinateClauseBeginPhrase)
                    {
                        var parsedClause = new Clause(accumulatedPhrases.Take(accumulatedPhrases.Count - 1)); // create a new clause comprised of the accumulated phrases
                        accumulatedClauses.Add(parsedClause);
                        accumulatedPhrases = new List<Phrase> { currentPhrase }; // Reset the phrase accumulation list initializing it with the subordinate clause begin phrase.
                    }
                }
                else if (token == '/')
                {
                    var words = CreateWords(chunk);

                    var containsAnyQuoteMarks = words.Any(w => w is SingleQuote || w is DoubleQuote);

                    if (containsAnyQuoteMarks)
                    {
                        accumulatedPhrases.Add(new SymbolPhrase(words));
                        var parsedClause = new Clause(accumulatedPhrases.Take(accumulatedPhrases.Count));
                        accumulatedClauses.Add(parsedClause);
                        accumulatedPhrases = new List<Phrase>();
                    }
                    else
                    {
                        var wordsIsAPunctuatorFollowedByAConjunction = words.Count == 2 && words[0] is Punctuator && words[1] is Conjunction;

                        if (words.All(word => word is Conjunction) || wordsIsAPunctuatorFollowedByAConjunction)
                        {
                            accumulatedPhrases.Add(new ConjunctionPhrase(words));
                        }
                        else if (words.Count == 1 && words[0] is SentenceEnding)
                        {
                            sentenceEnding = words[0] as SentenceEnding;
                            accumulatedClauses.Add(new Clause(accumulatedPhrases.Take(accumulatedPhrases.Count)));
                            accumulatedPhrases = new List<Phrase>();
                        }
                        else
                        {
                            var allWordsArePunctuatorsOrConjunctions = words.All(w => w is Punctuator) || words.All(w => w is Punctuator || w is Conjunction);
                            if (allWordsArePunctuatorsOrConjunctions)
                            {
                                accumulatedPhrases.Add(new SymbolPhrase(words));
                            }
                            else
                            {
                                accumulatedPhrases.Add(new UnknownPhrase(words));
                            }
                        }
                    }
                }
            }
            var parsedSentence = new Sentence(accumulatedClauses, sentenceEnding);
            return parsedSentence;
        }

        private Phrase CreatePhrase(string phraseTag, string phraseText)
        {
            var words = CreateWords(phraseText);
            try
            {
                return phraseTagset[phraseTag](words);
            }
            catch (UnknownPhraseTagException e)
            {
                Logger.Log("\n{0}\nPhrase Words: {1}\nInstantiating new LASI.Algorithm.UnknownPhrase to compensate", e.Message, words.Format());
                return new UnknownPhrase(words);
            }
            catch (EmptyPhraseTagException e)
            {
                Logger.Log("\n{0}\nPhrase Words: {1}\nInstantiating new LASI.Algorithm.UnknownPhrase to compensate", e.Message, words.Format());
                return new UnknownPhrase(words);
            }
        }

        #endregion Methods

        #region Fields

        private static readonly WordTagsetMap wordTagset = new SharpNLPWordTagsetMap();

        private static readonly PhraseTagsetMap phraseTagset = new SharpNLPPhraseTagsetMap();

        #endregion Fields
    }
}