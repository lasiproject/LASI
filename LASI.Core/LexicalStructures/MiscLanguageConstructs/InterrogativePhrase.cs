using System.Collections.Generic;
using System.Linq;

namespace LASI.Core
{
    /// <summary>
    /// Represents a phrase which signals the beginning of an interrogative clause.
    /// </summary>
    public class InterrogativePhrase : Phrase
    {
        /// <summary>
        /// Initializes a new instance of the InterrogativePhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the InterrogativePhrase.</param>
        public InterrogativePhrase(IEnumerable<Word> composedWords)
            : base(composedWords)
        {
        }
        /// <summary>
        /// Initializes a new instance of the InterrogativePhrase class.
        /// </summary>
        /// <param name="first">The first Word of the InterrogativePhrase.</param>
        /// <param name="rest">The rest of the Words comprise the InterrogativePhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of InterrogativePhrases. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public InterrogativePhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }
    }
}
