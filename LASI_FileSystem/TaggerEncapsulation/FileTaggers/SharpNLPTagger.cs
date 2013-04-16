using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
namespace SharpNLPTaggingModule
{

    public class SharpNLPTagger
    {
        /// <summary>
        /// Based on the example UI code which came with sharp NLP, just added a few way to pass it a file and get a file back and the TaggingOption enum
        /// </summary>
        private string mModelPath;

        protected OpenNLP.Tools.SentenceDetect.MaximumEntropySentenceDetector mSentenceDetector;
        protected OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer mTokenizer;
        protected OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger mPosTagger;
        protected OpenNLP.Tools.Chunker.EnglishTreebankChunker mChunker;
        protected OpenNLP.Tools.Parser.EnglishTreebankParser mParser;
        protected OpenNLP.Tools.NameFind.EnglishNameFinder mNameFinder;
        protected OpenNLP.Tools.Lang.English.TreebankLinker mCoreferenceFinder;

        public SharpNLPTagger(TaggingOption taggingMode) {
            TaggingMode = taggingMode;
            mModelPath = ConfigurationManager.AppSettings["MaximumEntropyModelDirectory"];
            mNameFinder = new OpenNLP.Tools.NameFind.EnglishNameFinder(ConfigurationManager.AppSettings["WordnetSearchDirectory"]);

        }


        public SharpNLPTagger(TaggingOption taggingMode, string sourcePath, string destinationPath = null)
            : this(taggingMode) {



            InputFilePath = sourcePath;
            OutputFilePath = destinationPath != null ? destinationPath :
                new FileInfo(sourcePath).DirectoryName + @"\" + new FileInfo(sourcePath.Substring(0, sourcePath.LastIndexOf('.'))).Name + @".tagged";

            SourceTextParagraphs = PreProcessText(LoadSourceText());

        }

        protected IEnumerable<string> PreProcessText(string p) {
            foreach (var rr in textToNumeralMap) {
                p = p.Replace(rr.Key, rr.Value);
            }
            return from para in p.Split(new[] { "<paragraph>", "</paragraph>" }, StringSplitOptions.RemoveEmptyEntries)
                   select para.Trim();

        }
        public virtual LASI.FileSystem.FileTypes.TaggedFile ProcessFile() {
            WriteToFile(ParseViaTaggingMode());
            return new LASI.FileSystem.FileTypes.TaggedFile(OutputFilePath);

        }
        public virtual async System.Threading.Tasks.Task<LASI.FileSystem.FileTypes.TaggedFile> ProcessFileAsync() {
            return await System.Threading.Tasks.Task.Run(() => ProcessFile());


        }

        protected string LoadSourceText() {
            using (
                var reader = new StreamReader(
                new FileStream(
                    InputFilePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.None, 1024,
                    FileOptions.SequentialScan),
                    Encoding.UTF8
                    )) {
                return String.Join(" ", reader.ReadToEnd().Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(s => s.Trim()));
            }
        }
        protected string ParseViaTaggingMode() {
            switch (TaggingMode) {
                case TaggingOption.TagIndividual:
                    return POSTag();
                case TaggingOption.TagAndAggregate:
                    return Chunk();
                case TaggingOption.ExperimentalClauseNesting:
                    return Parse();
                case TaggingOption.GenderFind:

                    return Gender();
                case TaggingOption.NameFind:
                    return NameFind();
                default:
                    return POSTag();
            }
        }
        protected string SplitIntoSentences() {
            string[] sentences = SplitSentences(SourceTextParagraphs);

            var result = String.Join("\r\n\r\n", sentences);
            return result;
        }

        protected void WriteToFile(params string[] txtOut) {
            using (var writer = new StreamWriter(new FileStream(OutputFilePath, FileMode.Create), Encoding.Unicode, 200)) {
                foreach (var line in txtOut) {
                    writer.Write(line);
                }
            }
        }

        protected string Tokenize() {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceTextParagraphs);

            foreach (string sentence in sentences) {
                string[] tokens = TokenizeSentence(sentence);
                output.Append(string.Join(" | ", tokens)).Append("\r\n\r\n");
            }

            var result = output.ToString();
            return result;

        }

        protected string POSTag() {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceTextParagraphs);

            foreach (string sentence in sentences) {
                string[] tokens = TokenizeSentence(sentence);
                string[] tags = PosTagTokens(tokens);
                for (int currentTag = 0; currentTag < tags.Length; currentTag++) {
                    output.Append(tokens[currentTag]).Append("/").Append(tags[currentTag]).Append(" ");
                }

            }

            var result = output.ToString();
            return result;
        }

        protected string Chunk() {

            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceTextParagraphs);

            foreach (string sentence in sentences) {
                string[] tokens = TokenizeSentence(sentence);
                string[] tags = PosTagTokens(tokens);
                output.Append(String.Format("<sentence>{0}</sentence>", ChunkSentence(tokens, tags)));
            }

            var result = output.ToString();
            return result;
        }

        protected string Parse() {
            var sentenceID = 0;
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceTextParagraphs);

            foreach (string sentence in sentences) {

                output.Append(String.Format("<sentence id = \"{0}\">{1}</sentence>", sentenceID++, ParseSentence(sentence).Show())).Append("\r\n\r\n");
            }

            var result = output.ToString();
            return result;
        }

        protected string NameFind() {
            StringBuilder output = new StringBuilder();
             foreach (var para in SourceTextParagraphs){
            string[] sentences = SplitSentences(para);

            foreach (string sentence in sentences) {
                output.Append(FindNames(sentence)).Append("\r\n");
            }

            var result = output.ToString();
            return result;
        }

        protected string[] SplitSentences(string paragraph) {
            if (mSentenceDetector == null) {
                mSentenceDetector = new OpenNLP.Tools.SentenceDetect.EnglishMaximumEntropySentenceDetector(mModelPath + "EnglishSD.nbin");
            }

            return mSentenceDetector.SentenceDetect(paragraph);
        }

        protected string[] TokenizeSentence(string sentence) {
            if (mTokenizer == null) {
                mTokenizer = new OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer(mModelPath + "EnglishTok.nbin");
            }

            return mTokenizer.Tokenize(sentence);
        }

        protected string[] PosTagTokens(string[] tokens) {
            if (mPosTagger == null) {
                mPosTagger = new OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger(mModelPath + "EnglishPOS.nbin", mModelPath + @"\Parser\tagdict");
            }

            return mPosTagger.Tag(tokens);
        }

        protected string ChunkSentence(string[] tokens, string[] tags) {
            if (mChunker == null) {
                mChunker = new OpenNLP.Tools.Chunker.EnglishTreebankChunker(mModelPath + "EnglishChunk.nbin");
            }

            return mChunker.GetChunks(tokens, tags);
        }

        protected OpenNLP.Tools.Parser.Parse ParseSentence(string sentence) {
            if (mParser == null) {
                mParser = new OpenNLP.Tools.Parser.EnglishTreebankParser(mModelPath, true, false);
            }

            return mParser.DoParse(sentence);
        }

        protected string FindNames(string sentence) {
            if (mNameFinder == null) {
                mNameFinder = new OpenNLP.Tools.NameFind.EnglishNameFinder(mModelPath + "namefind\\");
            }

            string[] models = new string[] { "date", "location", "money", "organization", "percentage", "person", "time" };
            return mNameFinder.GetNames(models, sentence);
        }

        protected virtual string FindNames(OpenNLP.Tools.Parser.Parse sentenceParse) {
            if (mNameFinder == null) {
                mNameFinder = new OpenNLP.Tools.NameFind.EnglishNameFinder(mModelPath + "namefind\\");
            }

            string[] models = new string[] { "date", "location", "money", "organization", "percentage", "person", "time" };
            return mNameFinder.GetNames(models, sentenceParse);
        }

        protected string IdentifyCoreferents(string[] sentences) {
            if (mCoreferenceFinder == null) {
                mCoreferenceFinder = new OpenNLP.Tools.Lang.English.TreebankLinker(mModelPath + "coref");
            }

            System.Collections.Generic.List<OpenNLP.Tools.Parser.Parse> parsedSentences = new System.Collections.Generic.List<OpenNLP.Tools.Parser.Parse>();

            foreach (string sentence in sentences) {
                OpenNLP.Tools.Parser.Parse sentenceParse = ParseSentence(sentence);
                string findNames = FindNames(sentenceParse);
                parsedSentences.Add(sentenceParse);
            }
            return mCoreferenceFinder.GetCoreferenceParse(parsedSentences.ToArray());
        }

        protected string Gender() {
            StringBuilder output = new StringBuilder();
            foreach (var para in SourceTextParagraphs) {
                string[] sentences = SplitSentences(para);

                foreach (string sentence in sentences) {
                    string[] tokens = TokenizeSentence(sentence);
                    string[] tags = PosTagTokens(tokens);

                    string posTaggedSentence = string.Empty;

                    for (int currentTag = 0; currentTag < tags.Length; currentTag++) {
                        posTaggedSentence += tokens[currentTag] + @"/" + tags[currentTag] + " ";
                    }

                    output.Append(posTaggedSentence);
                    output.Append("\r\n");
                    output.Append(OpenNLP.Tools.Coreference.Similarity.GenderModel.GenderMain(mModelPath + "coref\\gen", posTaggedSentence));
                    output.Append("\r\n\r\n");
                }
                output.Insert(0, "<paragraph>");
                output.Append("</paragraph>");
            }

            return output.ToString();
        }

        protected string Similarity() {
            StringBuilder output = new StringBuilder();

            foreach (var para in SourceTextParagraphs) {

                string[] sentences = SplitSentences(para).ToArray();

                foreach (string sentence in sentences) {
                    string[] tokens = TokenizeSentence(sentence);
                    string[] tags = PosTagTokens(tokens);

                    string posTaggedSentence = string.Empty;

                    for (int currentTag = 0; currentTag < tags.Length; currentTag++) {
                        posTaggedSentence += tokens[currentTag] + @"/" + tags[currentTag] + " ";
                    }

                    output.Append(posTaggedSentence);
                    output.Append("\r\n");
                    output.Append(OpenNLP.Tools.Coreference.Similarity.SimilarityModel.SimilarityMain(mModelPath + "coref\\sim", posTaggedSentence));
                    output.Append("\r\n\r\n");
                }
                output.Insert(0, "<paragraph>");
                output.Append("</paragraph>");
            }

            return output.ToString();

        }

        //private string Coreference() {
        //    string[] sentences = SplitSentences(SourceTextParagraphs);

        //    var result = IdentifyCoreferents(sentences);
        //    return result;
        //}

        #region Properties

        public string OutputFilePath {
            get;
            private set;
        }

        public string InputFilePath {
            get;
            private set;
        }

        public string[] SourceTextParagraphs {
            get;
            protected set;
        }

        public TaggingOption TaggingMode {
            get;
            private set;
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


}

