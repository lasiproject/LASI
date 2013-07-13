using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
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
            : base(composedWords) {
        }


    }
}
