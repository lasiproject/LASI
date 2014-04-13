using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// <para> Represents a lexically Symbolic element at the phrase level. It will generally contain predominantly word level Symbol instances. </para> 
    /// <para> For example, in the expression "x + y = (2 / 5) - xy" several of the token would be represented by Symbol instances including: "+", "=", "/", and "-". </para>
    /// </summary>
    public class SymbolPhrase : Phrase
    {
        /// <summary>
        /// Initializes a new instance of the SymbolPhrase class.
        /// </summary>
        /// <param name="words">The words which compose to form the SymbolPhrase.</param>
        public SymbolPhrase(IEnumerable<Word> words)
            : base(words) {
            LastPunctutionWord = words.Last(p => p is Punctuator) as Punctuator;
        }
        /// <summary>
        /// Initializes a new instance of the SymbolPhrase class.
        /// </summary>
        /// <param name="words">The words which compose to form the SymbolPhrase.</param>
        public SymbolPhrase(params Word[] words) : this(words.AsEnumerable()) { }

        /// <summary>
        /// Gets the last punctuation Word in the SymbolPhrase.
        /// </summary>
        public Punctuator LastPunctutionWord {
            get;
            protected set;
        }
    }
}
