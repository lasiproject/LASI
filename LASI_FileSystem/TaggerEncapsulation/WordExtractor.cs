using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    public class WordExtractor
    {
        /// <summary>
        /// Extracts a text and tag pair from the given string.
        /// </summary>
        /// <param name="line">The string to extract from.</param>
        /// <returns>A TextTagPair containing the information or null if the element is null, whitespace, or an empty string.</returns>
        /// <exception cref="UntaggedElementException">Thrown when a text element is present in the string without a tag.</exception>
        public TaggedWordObject? ExtractNextPos(string data) {
            if (String.IsNullOrEmpty(data) || String.IsNullOrWhiteSpace(data) || data.Trim() == "]") {
                return null;
            }
            //If there are no forward-slashes, the string contains no word level tags.
            //Although there may be more slashes than word-level-tags, there there are at least as many forward-slashes as word-level-tags
            if (data.Count(c => c == '/') == 0) {

                //throw new UntaggedElementException(String.Format(
                //   "The given text section, \"{0}\", contains no POS tags",
                //   line));
                return null;
            }

            int tagBegin = data.LastIndexOf('/');
            var text = data.Substring(0, tagBegin);
            var tag = data.Substring(tagBegin + 1);

            return new TaggedWordObject {
                Text = text,
                Tag = tag
            };
        }

    }
}
