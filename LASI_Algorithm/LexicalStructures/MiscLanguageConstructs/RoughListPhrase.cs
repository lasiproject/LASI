using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
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

    }
}
