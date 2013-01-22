using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    /// <summary>
    /// Holds a pair of strings representing a piece of natural language text and its NLP tag.
    /// Note, use with the elegant object initializer sytnax when creating an instance.
    /// eg. var myTTPair = new TextTagPair{ Text = "collie", Tag = "NN" }; 
    /// as opposed to
    /// var myTTPair = new TextTagPair();
    /// myTTPair.Text= "collie";
    /// myTTPair.Tag= "NN";
    /// because it is easier to read and reduces errors.
    /// </summary>
    public struct TextTagPair
    {
        /// <summary>
        /// The english text of a tagged elemenet.
        /// </summary>
        public string Text {
            get;
            set;
        }
        /// <summary>
        /// The text of pos tag associated with the element.
        /// </summary>
        public string Tag {
            get;
            set;
        }
    }
}
