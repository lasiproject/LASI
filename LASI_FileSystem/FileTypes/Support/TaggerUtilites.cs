using LASI.Algorithm;
using SharpNLPTaggingModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    public static class PosUtil
    {
        /// <summary>
        /// Interprets the untagged English string or strings provided and constructs a new Document instance from their content
        /// </summary>
        /// <param name="strs">The untagged, raw sentence or sentences to parse through the SharpNLP Tagger TaggedFileParser.</param>
        /// <returns>The runtime representation of the string(s) provided as a fully fledged LASI Document instance.</returns>
        /// <remarks>No files are created when calling this function</remarks>
        public static Document MakeLASIDoc(params string[] strs) {
            var tagged = QuickTag(strs);
            return DocFromTagged(tagged);
        }

        private static Document DocFromTagged(params string[] tagged) {
            var documentContent = String.Join(" ", tagged);
            var taggedParser = new TaggedFileParser(documentContent);
            return taggedParser.LoadDocument();
        }

        private static string QuickTag(string[] strs) {
            var documentContent = String.Join(" ", strs);
            var simpleTagger = new QuickTagger(TaggingOption.TagAndAggregate);
            var tagged = simpleTagger.TagString(documentContent);
            return tagged;
        }

    }
}
