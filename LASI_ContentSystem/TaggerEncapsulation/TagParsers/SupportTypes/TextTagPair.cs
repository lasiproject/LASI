using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.ContentSystem
{
    /// <summary>
    /// Holds a pair of strings representing a piece of natural language text and its NLP word tag.
    /// Note, use with the elegant object initializer sytnax when creating an instance. 
    /// <Example>
    /// <code>var myTTPair = new TextTagPair{ Text = "collie", Tag = "NN" };</code> 
    /// as opposed to
    /// <code>
    /// var myTTPair = new TextTagPair();
    /// myTTPair.Text= "collie";
    /// myTTPair.Tag= "NN";
    /// </code>
    /// because it is easier to read and reduces errors.
    /// </Example>
    /// </summary>
    public struct TextTagPair
    {
        /// <summary>
        /// Initializes a new instance of the TextTagPair structure from the provided text string and pos tag string.
        /// </summary>
        /// <param name="elementText">The text content string of the element.</param>
        /// <param name="elementTag">The pos tag string of the element.</param>
        public TextTagPair(string elementText, string elementTag) : this() { Text = elementText; Tag = elementTag; }

        /// <summary>
        /// Gets the english text of a tagged word.
        /// </summary>
        public string Text {
            get;
            private set;
        }
        /// <summary>
        /// Gets the text of the pos tag associated with the word.
        /// </summary>
        public string Tag {
            get;
            private set;
        }
    }
}
