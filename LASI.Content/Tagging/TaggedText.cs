using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Content
{
    /// <summary>
    /// Holds a pair of strings representing a piece of natural language text and its NLP word tag.
    /// </summary>
    struct TaggedText
    {
        /// <summary>
        /// Initializes a new instance of the TaggedText structure from the provided text string and pos tag string.
        /// </summary>
        /// <param name="text">The text content string of the element.</param>
        /// <param name="tag">The pos tag string of the element. Any whitespace will be removed.</param>
        public TaggedText(string text, string tag) { Text = text; Tag = tag.Trim(); }

        /// <summary>
        /// The English text of a tagged word.
        /// </summary>
        public string Text { get; }
        /// <summary>
        /// The text of the pos tag associated with the piece of text.
        /// </summary>
        public string Tag { get; }
    }
}
