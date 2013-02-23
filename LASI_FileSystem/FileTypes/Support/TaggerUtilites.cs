using LASI.Algorithm;
using SharpNLPTaggingModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    public static class TaggerUtil
    {
        /// <summary>
        /// Parses an untagged string or string[] and constructs a new Document instance from their content
        /// </summary>
        /// <param name="strs">The untagged, raw string data to parse.</param>
        /// <returns>The runtime representation of the string(s) provided as a fully fledged LASI Document instance.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public static Document DocObjectFromRawString(params string[] strs) {
            var tagged = TaggedStringFromString(strs);
            return DocObjectFromTaggedString(tagged);
        }

        /// <summary>
        /// Parses a pre-tagged string or string[] and constructs a new Document instance from their content
        /// </summary>
        /// <param name="tagged">The pre-tagged, raw string data to parse.</param>
        /// <returns>The runtime representation of the string(s) provided as a fully fledged LASI Document instance.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public static Document DocObjectFromTaggedString(params string[] tagged) {
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
        public static string TaggedStringFromString(string[] strs) {
            var documentContent = String.Join(" ", strs);
            var simpleTagger = new QuickTagger(TaggingOption.TagAndAggregate);
            var tagged = simpleTagger.TagString(documentContent);
            return tagged;
        }

    }
}
