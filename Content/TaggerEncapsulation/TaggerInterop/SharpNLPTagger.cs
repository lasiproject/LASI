using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaggerInterop
{
    public class SharpNLPTagger
    {
        private string mModelPath;
        private string mNameFinderPath;

        private OpenNLP.Tools.SentenceDetect.MaximumEntropySentenceDetector mSentenceDetector;
        private OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer mTokenizer;
        private OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger mPosTagger;
        private OpenNLP.Tools.Chunker.EnglishTreebankChunker mChunker;
        private OpenNLP.Tools.Parser.EnglishTreebankParser mParser;
        private OpenNLP.Tools.NameFind.EnglishNameFinder mNameFinder;
        private OpenNLP.Tools.Lang.English.TreebankLinker mCoreferenceFinder;

        public static LASI.Utilities.Configuration.IConfig Settings { internal get; set; }
        /// <summary>
        /// Initializes a new instance of the SharpNLPTagger class with its behavior specified by the provied TaggerMode value.
        /// </summary>
        /// <param name="taggingMode">Specifies the mode under which the tagger will operate.</param>
        public SharpNLPTagger(TaggerMode taggingMode)
        {
            string resourcesDirectory;
            TaggingMode = taggingMode;
            if (Settings != null)
            {
                resourcesDirectory = Settings["ResourcesDirectory"];
                mModelPath = resourcesDirectory + Settings["MaximumEntropyModelDirectory"];
                mNameFinderPath = resourcesDirectory + Settings["WordnetSearchDirectory"];
            }
            else
            {
                resourcesDirectory = ConfigurationManager.AppSettings["ResourcesDirectory"];
                mModelPath = resourcesDirectory + ConfigurationManager.AppSettings["MaximumEntropyModelDirectory"];
                mNameFinderPath = resourcesDirectory + ConfigurationManager.AppSettings["WordnetSearchDirectory"];
            }

            mNameFinder = new OpenNLP.Tools.NameFind.EnglishNameFinder(mNameFinderPath);
        }

        /// <summary>
        /// Initializes a new instance of the SharpNLPTagger class its analysis behavior specified by the provied TaggerMode value.
        /// </summary>
        /// <param name="taggingMode">Specifies the mode under which the tagger will operate.</param>
        /// <param name="sourcePath">The path to a text file whose contents will be read and tagged.</param>
        /// <param name="destinationPath">The optional path specifying the location where the tagged file should be created. If not provided, the tagged file will be placed in the same directory as the source.</param>
        public SharpNLPTagger(TaggerMode taggingMode, string sourcePath, string destinationPath = null)
            : this(taggingMode)
        {



            inputFilePath = sourcePath;
            outputFilePath = destinationPath != null ? destinationPath :
                new FileInfo(sourcePath).DirectoryName + @"\" + new FileInfo(sourcePath.Substring(0, sourcePath.LastIndexOf('.'))).Name + @".tagged";

            SourceText = LoadSourceText();

        }
        /// <summary>
        /// Returns a new string with certain characters replaced by aliases to aid in the ease of parsing.
        /// </summary>
        /// <param name="p">The source string.</param>
        /// <returns>A new string with certain characters replaced by aliases to aid in the ease of parsing.</returns>
        protected string PreProcessText(string p)
        {
            foreach (var rr in textToNumeralMap)
            {
                p = p.Replace(rr.Key, rr.Value);

            }
            return p;
        }
        /// <summary>
        /// Processes the text given to the tagger based on the Tagger's current TaggerMode. Returns the path to the tagged file resulting from the process.
        /// </summary>
        ///// <returns>The TaggedFile resulting from the process.</returns>
        public virtual string ProcessFile()
        {
            WriteToFile(ParseViaTaggingMode());
            return outputFilePath;

        }
        /// <summary>
        /// Asynchronously processes the text given to the tagger based on the Tagger's current TaggerMode. Returns the path to the tagged file resulting from the process.
        /// </summary>
        /// <returns>rocesses the text given to the tagger based on the Tagger's current TaggerMode. Returns the path to the tagged file resulting from the process.</returns>
        public virtual async Task<string> ProcessFileAsync()
        {
            return await Task.Run(() => ProcessFile());


        }

        private string LoadSourceText()
        {
            using (
                var reader = new StreamReader(
                new FileStream(inputFilePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.None, 1024,
                    FileOptions.SequentialScan),
                    Encoding.UTF8))
            {
                return string.Join(" ",
                    reader.ReadToEnd()
                    .Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                );
            }
        }
        protected async System.Threading.Tasks.Task<string> ParseViaTaggingModeAsync(TaggerMode taggingMode)
        {
            return await ParseViaTaggingModeAsync(taggingMode);
        }
        protected async System.Threading.Tasks.Task<string> ParseViaTaggingModeAsync()
        {
            return await System.Threading.Tasks.Task.Run(() => ParseViaTaggingMode(TaggingMode));
        }
        protected string ParseViaTaggingMode()
        {
            return ParseViaTaggingMode(TaggingMode);
        }
        protected string ParseViaTaggingMode(TaggerMode taggingMode)
        {
            switch (TaggingMode)
            {
                case TaggerMode.TagIndividual:
                    return POSTag();
                case TaggerMode.TagAndAggregate:
                    return Chunk();
                case TaggerMode.FullyNestingParse:
                    return Parse();
                case TaggerMode.GenderFind:

                    return Gender();
                case TaggerMode.NameFind:
                    return NameFind();
                default:
                    return POSTag();
            }
        }
        private string SplitIntoSentences()
        {
            string[] sentences = SplitSentences(SourceText);

            var result = string.Join("\r\n\r\n", sentences);
            return result;
        }

        private void WriteToFile(params string[] txtOut)
        {
            using (var writer = new StreamWriter(new FileStream(outputFilePath, FileMode.Create), Encoding.Unicode, 200))
            {
                foreach (var line in txtOut)
                {
                    writer.Write(line);
                }
            }
        }

        private string Tokenize()
        {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceText);

            foreach (string sentence in sentences)
            {
                string[] tokens = TokenizeSentence(sentence);
                output.Append(string.Join(" | ", tokens)).Append("\r\n\r\n");
            }

            var result = output.ToString();
            return result;

        }

        private string POSTag()
        {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceText);

            foreach (string sentence in sentences)
            {
                string[] tokens = TokenizeSentence(sentence);
                string[] tags = PosTagTokens(tokens);
                for (int currentTag = 0; currentTag < tags.Length; currentTag++)
                {
                    output.Append(tokens[currentTag]).Append("/").Append(tags[currentTag]).Append(" ");
                }

            }

            var result = output.ToString();
            return result;
        }

        private string Chunk()
        {

            StringBuilder output = new StringBuilder();
            var paragraphs = SourceText.Split(new[] { "\r\n\r\n", "<paragraph>", "</paragraph>" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var paragraph in paragraphs.AsParallel().AsOrdered().Select(p => StripParentheticals(p)))
            {
                string[] sentences = SplitSentences(paragraph);

                foreach (var sentence in sentences.Where(s => !string.IsNullOrWhiteSpace(s)))
                {
                    string[] tokens = TokenizeSentence(sentence);
                    string[] tags = PosTagTokens(tokens);
                    output.Append(string.Format("<sentence>{0}</sentence>", ChunkSentence(tokens, tags)));
                }
                output.Insert(0, "<paragraph>").Append("</paragraph>");
            }
            var result = output.ToString();
            return result;
        }

        private string StripParentheticals(string paragraph)
        {
            for (int j = paragraph.IndexOf(')'), i = paragraph.IndexOf('('); i < j && i != -1 && j != -1; i = paragraph.IndexOf('('), j = paragraph.IndexOf(')'))
            {
                paragraph = paragraph.Substring(0, i) + paragraph.Substring(j + 1);
            }
            return paragraph;
        }
        private string Gender()
        {
            StringBuilder output = new StringBuilder();
            var paragraphs = from p in SourceText.Split(new[] { "<paragraph>", "</paragraph>" }, StringSplitOptions.RemoveEmptyEntries)
                             select p;
            foreach (var p in paragraphs)
            {
                string[] sentences = SplitSentences(p);

                foreach (string sentence in sentences)
                {
                    string[] tokens = TokenizeSentence(sentence);
                    string[] tags = PosTagTokens(tokens);

                    string posTaggedSentence = string.Empty;

                    for (int currentTag = 0; currentTag < tags.Length; currentTag++)
                    {
                        posTaggedSentence += tokens[currentTag] + @"/" + tags[currentTag] + " ";
                    }

                    output.Append(posTaggedSentence);
                    output.Append("\r\n");
                    output.Append(OpenNLP.Tools.Coreference.Similarity.GenderModel.GenderMain(mModelPath + "coref\\gen", posTaggedSentence));
                    output.Append("\r\n\r\n");
                }
            }
            var result = output.ToString();
            return result;
        }

        private string Parse()
        {
            var sentenceID = 0;
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceText);

            foreach (string sentence in sentences)
            {

                output.Append(string.Format("<sentence id = \"{0}\">{1}</sentence>", sentenceID++, ParseSentence(sentence).Show())).Append("\r\n\r\n");
            }

            var result = output.ToString();
            return result;
        }

        private string NameFind()
        {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceText);

            foreach (string sentence in sentences)
            {
                output.Append(FindNames(sentence)).Append("\r\n");
            }

            var result = output.ToString();
            return result;
        }

        private string[] SplitSentences(string paragraph)
        {
            if (mSentenceDetector == null)
            {
                mSentenceDetector = new OpenNLP.Tools.SentenceDetect.EnglishMaximumEntropySentenceDetector(mModelPath + "EnglishSD.nbin");
            }

            return mSentenceDetector.SentenceDetect(paragraph);
        }

        private string[] TokenizeSentence(string sentence)
        {
            if (mTokenizer == null)
            {
                mTokenizer = new OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer(mModelPath + "EnglishTok.nbin");
            }

            return mTokenizer.Tokenize(sentence);
        }

        private string[] PosTagTokens(string[] tokens)
        {
            if (mPosTagger == null)
            {
                mPosTagger = new OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger(mModelPath + "EnglishPOS.nbin", mModelPath + @"\Parser\tagdict");
            }

            return mPosTagger.Tag(tokens);
        }

        private string ChunkSentence(string[] tokens, string[] tags)
        {
            if (mChunker == null)
            {
                mChunker = new OpenNLP.Tools.Chunker.EnglishTreebankChunker(mModelPath + "EnglishChunk.nbin");
            }

            return mChunker.GetChunks(tokens, tags);
        }

        private OpenNLP.Tools.Parser.Parse ParseSentence(string sentence)
        {
            if (mParser == null)
            {
                mParser = new OpenNLP.Tools.Parser.EnglishTreebankParser(mModelPath, true, false);
            }

            return mParser.DoParse(sentence);
        }

        private string FindNames(string sentence)
        {
            if (mNameFinder == null)
            {
                mNameFinder = new OpenNLP.Tools.NameFind.EnglishNameFinder(mModelPath + "namefind\\");
            }

            string[] models = new string[] { "date", "location", "money", "organization", "percentage", "person", "time" };
            return mNameFinder.GetNames(models, sentence);
        }

        private string FindNames(OpenNLP.Tools.Parser.Parse sentenceParse)
        {
            if (mNameFinder == null)
            {
                mNameFinder = new OpenNLP.Tools.NameFind.EnglishNameFinder(mModelPath + "namefind\\");
            }

            string[] models = new string[] { "date", "location", "money", "organization", "percentage", "person", "time" };
            return mNameFinder.GetNames(models, sentenceParse);
        }

        private string IdentifyCoreferents(string[] sentences)
        {
            if (mCoreferenceFinder == null)
            {
                mCoreferenceFinder = new OpenNLP.Tools.Lang.English.TreebankLinker(mModelPath + "coref");
            }

            System.Collections.Generic.List<OpenNLP.Tools.Parser.Parse> parsedSentences = new System.Collections.Generic.List<OpenNLP.Tools.Parser.Parse>();

            foreach (string sentence in sentences)
            {
                OpenNLP.Tools.Parser.Parse sentenceParse = ParseSentence(sentence);
                string findNames = FindNames(sentenceParse);
                parsedSentences.Add(sentenceParse);
            }
            return mCoreferenceFinder.GetCoreferenceParse(parsedSentences.ToArray());
        }


        private string Similarity()
        {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceText);

            foreach (string sentence in sentences)
            {
                string[] tokens = TokenizeSentence(sentence);
                string[] tags = PosTagTokens(tokens);

                string posTaggedSentence = string.Empty;

                for (int currentTag = 0; currentTag < tags.Length; currentTag++)
                {
                    posTaggedSentence += tokens[currentTag] + @"/" + tags[currentTag] + " ";
                }

                output.Append(posTaggedSentence);
                output.Append("\r\n");
                output.Append(OpenNLP.Tools.Coreference.Similarity.SimilarityModel.SimilarityMain(mModelPath + "coref\\sim", posTaggedSentence));
                output.Append("\r\n\r\n");
            }

            var result = output.ToString();
            return result;
        }

        private string Coreference()
        {
            string[] sentences = SplitSentences(SourceText);

            var result = IdentifyCoreferents(sentences);
            return result;
        }

        #region Properties

        private string outputFilePath;

        private string inputFilePath;

        /// <summary>
        /// Gets or sets the text which the SharpNLPTagger will tag when the ProcessFile or ProcessFileAsync methods are invoked.
        /// </summary>
        protected string SourceText
        {
            get;
            set;
        }
        /// <summary>
        /// Gets the TaggerMode of the SharpNLPTagger. 
        /// </summary>
        public TaggerMode TaggingMode
        {
            get;
            protected set;
        }
        #endregion

        #region Fields
        private static readonly Dictionary<string, string> textToNumeralMap = new Dictionary<string, string> {
        {  " two " , " 2 " },
        {  " three " , " 3 " },
        {  " four " , " 4 " },
        {  " five " , " 5 " },
        {  " six " , " 6 " },
        {  " seven " , " 7 " },
        {  " eight " , " 8 " },
        {  " nine " , " 9 " },
        {  " ten " , " 10 " },
        {  " eleven " , " 11 " },
        {  " twelve " , " 12 " },
        {  " thirteen " , " 13 " },
        {  " fourteen " , " 14 " },
        {  " fifteen " , " 15 " },
        {  " sixteen " , " 16 " },
        {  " seventeen " , " 17 " },
        {  " eighteen " , " 18 " },
        {  " nineteen " , " 19 " },
        {  " twenty " , " 20 " },
        {  " twenty-one " , " 21 " },
        {  " twenty-two " , " 22 " },
        {  " twenty-three " , " 23 " },
        {  " twenty-four " , " 24 " },
        {  " twenty-five " , " 25 " },
        {  " twenty-six " , " 26 " },
        {  " twenty-seven " , " 27 " },
        {  " twenty-eight " , " 28 " },
        {  " twenty-nine " , " 29 " },
        {  " thirty " , " 30 " },
        {  " thirty-one " , " 31 " },
        {  " thirty-two " , " 32 " },
        {  " thirty-three " , " 33 " },
        {  " thirty-four " , " 34 " },
        {  " thirty-five " , " 35 " },
        {  " thirty-six " , " 36 " },
        {  " thirty-seven " , " 37 " },
        {  " thirty-eight " , " 38 " },
        {  " thirty-nine " , " 39 " },
        {  " forty " , " 40 " },
        {  " forty-one " , " 41 " },
        {  " forty-two " , " 42 " },
        {  " forty-three " , " 43 " },
        {  " forty-four " , " 44 " },
        {  " forty-five " , " 45 " },
        {  " forty-six " , " 46 " },
        {  " forty-seven " , " 47 " },
        {  " forty-eight " , " 48 " },
        {  " forty-nine " , " 49 " },
        {  " fifty " , " 50 " },
        {  " fifty-one " , " 51 " },
        {  " fifty-two " , " 52 " },
        {  " fifty-three " , " 53 " },
        {  " fifty-four " , " 54 " },
        {  " fifty-five " , " 55 " },
        {  " fifty-six " , " 56 " },
        {  " fifty-seven " , " 57 " },
        {  " fifty-eight " , " 58 " },
        {  " fifty-nine " , " 59 " },
        {  " sixty " , " 60 " },
        {  " sixty-one " , " 61 " },
        {  " sixty-two " , " 62 " },
        {  " sixty-three " , " 63 " },
        {  " sixty-four " , " 64 " },
        {  " sixty-five " , " 65 " },
        {  " sixty-six " , " 66 " },
        {  " sixty-seven " , " 67 " },
        {  " sixty-eight " , " 68 " },
        {  " sixty-nine " , " 69 " },
        {  " seventy " , " 70 " },
        {  " seventy-one " , " 71 " },
        {  " seventy-two " , " 72 " },
        {  " seventy-three " , " 73 " },
        {  " seventy-four " , " 74 " },
        {  " seventy-five " , " 75 " },
        {  " seventy-six " , " 76 " },
        {  " seventy-seven " , " 77 " },
        {  " seventy-eight " , " 78 " },
        {  " seventy-nine " , " 79 " },
        {  " eighty " , " 80 " },
        {  " eighty-one " , " 81 " },
        {  " eighty-two " , " 82 " },
        {  " eighty-three " , " 83 " },
        {  " eighty-four " , " 84 " },
        {  " eighty-five " , " 85 " },
        {  " eighty-six " , " 86 " },
        {  " eighty-seven " , " 87 " },
        {  " eighty-eight " , " 88 " },
        {  " eighty-nine " , " 89 " },
        {  " ninety " , " 90 " },
        {  " ninety-one " , " 91 " },
        {  " ninety-two " , " 92 " },
        {  " ninety-three " , " 93 " },
        {  " ninety-four " , " 94 " },
        {  " ninety-five " , " 95 " },
        {  " ninety-six " , " 96 " },
        {  " ninety-seven " , " 97 " },
        {  " ninety-eight " , " 98 " },
        {  " ninety-nine ", " 99 " },};
        #endregion
    }




    sealed class QuickTagger : SharpNLPTagger
    {
        public QuickTagger(TaggerMode option)
            : base(option)
        {

        }

        public string TagTextSource(string source)
        {
            SourceText = base.PreProcessText(source);
            return base.ParseViaTaggingMode();

        }
        public async Task<string> TagTextSourceAsync(string source)
        {
            SourceText = base.PreProcessText(source);
            return await base.ParseViaTaggingModeAsync();

        }

    }
    /// <summary>
    /// Used to specify the behavior tagging options of an instance of the SharpNLPtagger class.
    /// </summary>
    public enum TaggerMode
    {
        /// <summary>
        /// Assign Part of Speech Tags to each input token.
        /// </summary>
        TagIndividual,
        /// <summary>
        /// Assigns Part Of Speech Tags to words (with the form "word/tag") and simple phrases( with the form [ tag word1/t1 word2/t2... ])(chunks)
        /// </summary>
        TagAndAggregate,
        /// <summary>
        /// Parses and nests arbitarily
        /// </summary>
        FullyNestingParse,
        /// <summary>
        /// Embeds gender liklihood information with nouns
        /// </summary>
        GenderFind,
        /// <summary>
        /// Embeds enity recognition with nouns for broad categories such as location, organization, etc.
        /// </summary>
        NameFind,
    }

}