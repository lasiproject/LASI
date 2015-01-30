using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// A rought representation of a listed (bulleted or numbered) lexical element.
    /// </summary>
    public class RoughListPhrase : Phrase
    {
        /// <summary>
        /// Initializes a new instance of the RoughListPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the RoughListPhrase.</param>
        public RoughListPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
        /// <summary>
        /// Initializes a new instance of the RoughListPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the RoughListPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the RoughListPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of RoughListPhrase. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public RoughListPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }

    }
}
