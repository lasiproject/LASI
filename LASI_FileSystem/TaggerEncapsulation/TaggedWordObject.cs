using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    /// <summary>
    /// Holds a pair of strings representing a piece of natural language text and its NLP w tag.
    /// Note, use with the elegant object initializer sytnax when creating an instance.
    /// eg. var myTTPair = new TextTagPair{ Text = "collie", Tag = "NN" }; 
    /// as opposed to
    /// var myTTPair = new TextTagPair();
    /// myTTPair.Text= "collie";
    /// myTTPair.Tag= "NN";
    /// because it is easier to read and reduces errors.
    /// </summary>
    public struct TaggedWordObject
    {
        /// <summary>
        /// The english text of a tagged w.
        /// </summary>
        public string Text {
            get;
            set;
        }
        /// <summary>
        /// The text of the pos tag associated with the w.
        /// </summary>
        public string Tag {
            get;
            set;
        }
    }
    /// <summary>
    /// Holds a pair of strings representing the internal text of a entity and its NLP entity tag.
    /// </summary>
    public struct TaggedPhraseObject
    {
        /// <summary>
        /// The inner text content of the entity, presumably consisting of one or more words.
        /// </summary>
        public string Text {
            get;
            set;
        }
        /// <summary>
        /// The text of pos tag associated with the entity.
        /// </summary>
        public string Tag {
            get;
            set;
        }
    }
    public struct TaggedClauseObject
    {
        /// <summary>
        /// The inner text content of the clause, presumably consisting of one or more phrases.
        /// </summary>
        public string Text {
            get;
            set;
        }
        /// <summary>
        /// The text of pos tag associated with the clause.
        /// </summary>
        public string Tag {
            get;
            set;
        }
    }
}
