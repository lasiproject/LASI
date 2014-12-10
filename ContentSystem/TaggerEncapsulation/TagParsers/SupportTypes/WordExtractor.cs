using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Content
{
    /// <summary>
    /// Extracts tagged words from a string.
    /// </summary>
    internal class TaggedWordExtractor
    {
        /// <summary>
        /// Extracts the text and tag pair from the given string.
        /// </summary>
        /// <param name="data">The string to extract from.</param>
        /// <returns>A TextTagPair? containing the information or null if the element is null, whitespace, the empty string, contains no '/' characters , or contains only single characer ']'.</returns>
        /// <exception cref="UntaggedWordException">Thrown when a text element is present in the string without a tag.</exception>
        public TaggedText? Extract(string data) {
            //If there are no forward-slashes, the string contains no word level tags.
            //Although there may be more slashes than word-level-tags, there there are at least as many forward-slashes as word-level-tags
            int tagBegin = data.LastIndexOf('/');
            return !data.IsNullOrWhiteSpace() && data.Trim() != "]" && data.Count(c => c == '/') != 0
                ? new TaggedText(
                        text:
                        data.Substring(0, tagBegin),
                        tag:
                        data.Substring(tagBegin + 1))
                : default(TaggedText?);
        }

    }
}
