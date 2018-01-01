using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// <para> Represents a Phrase which does not correspond to a known category. </para>
    /// <para> This may be the result of a Tagging error or a Tag-Parsing error. </para>
    /// </summary>
    public class UnknownPhrase : Phrase
    {
        /// <summary>
        /// Initializes a new instance of the UnknownPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the UnknownPhrase.</param>
        public UnknownPhrase(IEnumerable<Word> composedWords)
            : base(composedWords)
        {
        }
        /// <summary>
        /// Initializes a new instance of the UnknownPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the UnknownPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the UnknownPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of UnknownPhrases. 
        /// Thus, its purpose is to simplify test code.</remarks>
        public UnknownPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }
    }
}
