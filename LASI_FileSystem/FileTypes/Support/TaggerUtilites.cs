using LASI.Algorithm;
using SharpNLPTaggingModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    /// <summary>
    /// Provides dynamic, non file driven, access to the functionality of the POS Tagger and TaggedFileParser.
    /// </summary>
    public static class TaggerUtil
    {
        /// <summary>
        /// Parses an untagged string or string[] and constructs a new Document instance from their content
        /// </summary>
        /// <param name="strs">The untagged, raw string data to parse.</param>
        /// <returns>The runtime representation of the string(s) provided as a fully fledged LASI Document instance.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public static Document UntaggedToDoc(params string[] strs) {
            var tagged = TagString(strs);
            return TaggedToDoc(tagged);
        }

        /// <summary>
        /// Parses a pre-tagged string or string[] and constructs a new Document instance from their content
        /// </summary>
        /// <param name="tagged">The pre-tagged, raw string data to parse.</param>
        /// <returns>The runtime representation of the string(s) provided as a fully fledged LASI Document instance.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public static Document TaggedToDoc(params string[] tagged) {
            var documentContent = String.Join(" ", tagged);
            var taggedParser = new TaggedFileParser(documentContent);
            return taggedParser.LoadDocument();
        }

        /// <summary>
        /// Parses an untagged string or string[] with the SharpNLP tagger and returns a single string containing the result.
        /// </summary>
        /// <param name="strs">The untagged, raw string data to parse.</param>
        /// <returns>The tagged input string as it would appear in a tagged file.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public static string TagString(params string[] strs) {
            var documentContent = String.Join(" ", strs);
            var simpleTagger = new QuickTagger(TaggerOption);
            var tagged = simpleTagger.TagString(documentContent);
            return tagged;
        }
        /// <summary>
        /// Gets or sets the default mode the tagger will operate under. The default value is set to TagAndAggregate
        /// </summary>
        public static TaggingOption TaggerOption {
            get;
            set;
        }

        static TaggerUtil() {
            TaggerOption = TaggingOption.TagAndAggregate;
        }


    }
}
