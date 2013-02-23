using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem.TaggerEncapsulation
{
    public class PhraseExtractor
    {
        public TaggedPhraseObject ExtractAndConsume(ref string phraseString) {

            var tagStart = phraseString.IndexOf('(');
            var tagEnd = phraseString.IndexOf(" (");
            if (tagEnd == -1||tagEnd<tagStart) {

                var result = new TaggedPhraseObject {
                    Tag = "X", Text = phraseString
                };
                phraseString = "";
                return result;
            }
            var tagLength = tagEnd - tagStart;
            var tag = phraseString.Substring(tagStart + 1, tagLength - 1);
            var innerTextEnd = phraseString.IndexOf("))");
            var innerTextLen = innerTextEnd - tagEnd + 1;
            var innerText = phraseString.Substring(tagEnd + 1, innerTextLen);
            if (innerText.Count(c => c == '(') > 0) {
                phraseString = phraseString.Substring(tagLength+innerText.Length).Trim();
                return new TaggedPhraseObject {
                    Tag = tag, Text = innerText
                };
            } else {
                phraseString = phraseString.Substring(innerTextEnd + 1).Trim();
                return new TaggedPhraseObject {
                    Tag = "X", Text = String.Format("({0} {1})", tag, innerText)
                };
            }
        }
    }
}
