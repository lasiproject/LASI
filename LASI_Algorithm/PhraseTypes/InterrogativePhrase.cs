using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a phrase signalling the beginning of an interrogative clause.
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

     
        public override void DetermineHeadWord() {
            throw new NotImplementedException();
        }

        public override System.Xml.Linq.XElement Serialize() {
            throw new NotImplementedException();
        }
    }
}
