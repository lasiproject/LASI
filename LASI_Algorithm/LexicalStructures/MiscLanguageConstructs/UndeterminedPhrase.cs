using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents entity Phrase Which does not correspond to entity known catregory.
    /// This may be the result of entity Tagging error or entity Tag-Parsing error.
    /// </summary>
    public class UndeterminedPhrase : Phrase
    {
        /// <summary>
        /// Initializes entity new instance of the UndeterminedPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the UndeterminedPhrase.</param>
        public UndeterminedPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }





    }
}
