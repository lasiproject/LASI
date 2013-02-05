using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Linq;
namespace SharpNLPTaggingModule
{

    public class SharpNLPTagger
    {
        /// <summary>
        /// Based on the example UI code which came with sharp NLP, just added a few way to pass it a file and get a file back and the TaggingOption enum
        /// </summary>
        private string mModelPath;

        private OpenNLP.Tools.SentenceDetect.MaximumEntropySentenceDetector mSentenceDetector;
        private OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer mTokenizer;
        private OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger mPosTagger;
        private OpenNLP.Tools.Chunker.EnglishTreebankChunker mChunker;
        private OpenNLP.Tools.Parser.EnglishTreebankParser mParser;
        private OpenNLP.Tools.NameFind.EnglishNameFinder mNameFinder;
        private OpenNLP.Tools.Lang.English.TreebankLinker mCoreferenceFinder;


        public SharpNLPTagger(TaggingOption taggingMode, string sourcePath, string destinationPath = null) {
            //  mModelPath = ConfigurationManager.AppSettings["MaximumEntropyModelDirectory"];

            mModelPath = @"..\\..\\..\\ThirdPartyComponents\TaggingPackage\Resources\OpenNLP\OpenNLP\Models\";
            TaggingMode = taggingMode;
            InputFilePath = sourcePath;
            OutputFilePath = destinationPath != null ? destinationPath :
                new FileInfo(sourcePath).DirectoryName + @"\" + new FileInfo(sourcePath.Substring(0, sourcePath.LastIndexOf('.'))).Name + @".tagged";

            SourceText = LoadSourceText();//

        }
        public LASI.FileSystem.TaggedFile ProcessFile() {
            switch (TaggingMode) {
                case TaggingOption.TagIndividual:
                    POSTag();
                    break;
                case TaggingOption.TagAndAggregate:
                    Chunk();
                    break;
                default:
                    POSTag();
                    break;
            }
            return new LASI.FileSystem.TaggedFile(OutputFilePath);
        }

        private string LoadSourceText() {
            using (var reader = new StreamReader(new FileStream(InputFilePath, FileMode.Open, FileAccess.Read, FileShare.None, 1024, FileOptions.SequentialScan),Encoding.UTF8)) {
                return String.Join(" ", reader.ReadToEnd().Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(s => s.Trim()));
            }
        }



        /// <summary>
        /// The main entry point for the application.
        /// </summary>


        #region Button click events

        private void SplitIntoSentences() {
            string[] sentences = SplitSentences(SourceText);

            var result = String.Join("\r\n\r\n", sentences);
            WriteToFile(result);
        }

        private void WriteToFile(params string[] txtOut) {
            using (var writer = new StreamWriter(new FileStream( OutputFilePath,FileMode.Create),Encoding.UTF8,200)) {
                foreach (var line in txtOut) {
                    writer.Write(line);
                }
            }
        }

        private void Tokenize() {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceText);

            foreach (string sentence in sentences) {
                string[] tokens = TokenizeSentence(sentence);
                output.Append(string.Join(" | ", tokens)).Append("\r\n\r\n");
            }

            var result = output.ToString();

        }

        private void POSTag() {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceText);

            foreach (string sentence in sentences) {
                string[] tokens = TokenizeSentence(sentence);
                string[] tags = PosTagTokens(tokens);

                for (int currentTag = 0; currentTag < tags.Length; currentTag++) {
                    output.Append(tokens[currentTag]).Append("/").Append(tags[currentTag]).Append(" ");
                }

                output.Append("\r\n\r\n");
            }

            var result = output.ToString();
            WriteToFile(result);
        }

        private void Chunk() {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceText);

            foreach (string sentence in sentences) {
                string[] tokens = TokenizeSentence(sentence);
                string[] tags = PosTagTokens(tokens);

                output.Append(ChunkSentence(tokens, tags)).Append("\r\n");
            }

            var result = output.ToString();
            WriteToFile(result);
        }

        private void Parse() {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceText);

            foreach (string sentence in sentences) {
                output.Append(ParseSentence(sentence).Show()).Append("\r\n\r\n");
            }

            var result = output.ToString();
            WriteToFile(result);
        }

        private void NameFind() {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceText);

            foreach (string sentence in sentences) {
                output.Append(FindNames(sentence)).Append("\r\n");
            }

            var result = output.ToString();
            WriteToFile(result);
        }

        #endregion

        #region NLP methods

        private string[] SplitSentences(string paragraph) {
            if (mSentenceDetector == null) {
                mSentenceDetector = new OpenNLP.Tools.SentenceDetect.EnglishMaximumEntropySentenceDetector(mModelPath + "EnglishSD.nbin");
            }

            return mSentenceDetector.SentenceDetect(paragraph);
        }

        private string[] TokenizeSentence(string sentence) {
            if (mTokenizer == null) {
                mTokenizer = new OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer(mModelPath + "EnglishTok.nbin");
            }

            return mTokenizer.Tokenize(sentence);
        }

        private string[] PosTagTokens(string[] tokens) {
            if (mPosTagger == null) {
                mPosTagger = new OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger(mModelPath + "EnglishPOS.nbin", mModelPath + @"\Parser\tagdict");
            }

            return mPosTagger.Tag(tokens);
        }

        private string ChunkSentence(string[] tokens, string[] tags) {
            if (mChunker == null) {
                mChunker = new OpenNLP.Tools.Chunker.EnglishTreebankChunker(mModelPath + "EnglishChunk.nbin");
            }

            return mChunker.GetChunks(tokens, tags);
        }

        private OpenNLP.Tools.Parser.Parse ParseSentence(string sentence) {
            if (mParser == null) {
                mParser = new OpenNLP.Tools.Parser.EnglishTreebankParser(mModelPath, true, false);
            }

            return mParser.DoParse(sentence);
        }

        private string FindNames(string sentence) {
            if (mNameFinder == null) {
                mNameFinder = new OpenNLP.Tools.NameFind.EnglishNameFinder(mModelPath + "namefind\\");
            }

            string[] models = new string[] { "date", "location", "money", "organization", "percentage", "person", "time" };
            return mNameFinder.GetNames(models, sentence);
        }

        private string FindNames(OpenNLP.Tools.Parser.Parse sentenceParse) {
            if (mNameFinder == null) {
                mNameFinder = new OpenNLP.Tools.NameFind.EnglishNameFinder(mModelPath + "namefind\\");
            }

            string[] models = new string[] { "date", "location", "money", "organization", "percentage", "person", "time" };
            return mNameFinder.GetNames(models, sentenceParse);
        }

        private string IdentifyCoreferents(string[] sentences) {
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

        #endregion

        private void Gender(object sender, EventArgs e) {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceText);

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

            var result = output.ToString();
            WriteToFile(result);
        }

        private void Similarity() {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(SourceText);

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

            var result = output.ToString();
            WriteToFile(result);
        }

        private void Coreference(object sender, EventArgs e) {
            string[] sentences = SplitSentences(SourceText);

            var result = IdentifyCoreferents(sentences);
            WriteToFile(result);
        }

        #region Properties

        public string OutputFilePath {
            get;
            private set;
        }

        public string InputFilePath {
            get;
            private set;
        }

        public string SourceText {
            get;
            private set;
        }

        public TaggingOption TaggingMode {
            get;
            private set;
        }


        #endregion
    }


}

