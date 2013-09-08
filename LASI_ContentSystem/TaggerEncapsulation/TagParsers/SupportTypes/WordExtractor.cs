using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.Text;

namespace LASI.ContentSystem
{
    /// <summary>
    /// Extracts tagged words from a string.
    /// </summary>
    public class WordExtractor
    {
        /// <summary>
        /// Extracts the text and tag pair from the given string.
        /// </summary>
        /// <param name="data">The string to extract from.</param>
        /// <returns>A TextTagPair? containing the information or null if the element is null, whitespace, the empty string, contains no '/' characters , or contains only single characer ']'.</returns>
        /// <exception cref="UntaggedWordException">Thrown when a text element is present in the string without a tag.</exception>
        public TextTagPair? ExtractNextPos(string data) {
            //If there are no forward-slashes, the string contains no word level tags.
            //Although there may be more slashes than word-level-tags, there there are at least as many forward-slashes as word-level-tags
            int tagBegin = data.LastIndexOf('/');
            return data.IsNotWsOrNull() && data.Trim() != "]" && data.Count(c => c == '/') != 0
                ? new TextTagPair(
                        elementText:
                        data.Substring(0, tagBegin),
                        elementTag:
                        data.Substring(tagBegin + 1))
                : default(TextTagPair?);
        }

    }
}
