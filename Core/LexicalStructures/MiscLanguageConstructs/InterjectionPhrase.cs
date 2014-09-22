using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// Represents an interjection phrase such as "by jove".
    /// </summary>
    public class InterjectionPhrase : Phrase
    {

        /// <summary>
        /// Initializes a new instance of the InterjectionPhrase class.
        /// </summary>
        /// <param name="composed">The words which compose to form the InterjectionPhrase.</param>
        public InterjectionPhrase(IEnumerable<Word> composed)
            : base(composed) {
        }
        /// <summary>
        /// Initializes a new instance of the InterjectionPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the InterjectionPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the InterjectionPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of Phrases. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public InterjectionPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }


    }
}
