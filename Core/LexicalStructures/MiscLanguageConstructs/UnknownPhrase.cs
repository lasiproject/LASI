using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Core
{
    /// <summary>
    /// <para> Represents a Phrase which does not correspond to a known category. </para>
    /// <para> This may be the result of a Tagging error or a Tag-Parsing error. </para>
    /// </summary>
    public class UnknownPhrase : Phrase
    {
        /// <summary>
        /// Initializes a new instance of the UndeterminedPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the UndeterminedPhrase.</param>
        public UnknownPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }





    }
}
