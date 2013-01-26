using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class AdverbPhrase : Phrase, IAdverbial
    {
        public AdverbPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
        public virtual IModifiable Modiffied {
            get;
            set;
        }

        public override Word HeadWord {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
    }
}
