using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Phrase Which does not correspond to a known catregory.
    /// This may be the result of a Tagging error or a Tag-Parsing error.
    /// </summary>
    public class UndeterminedPhrase : Phrase
    {
        public UndeterminedPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }



        public override void DetermineHeadWord() {
            throw new NotImplementedException();
        }
        #region Properties
        public override Word HeadWord {
            get {
                throw new NotImplementedException();
            }
            protected set {
                throw new NotImplementedException();
            }
        }
        #endregion

    }
}
