using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class TransPresentPrtcplVPhrase : TransitiveVerbPhrase, IActionSubject, IActionObject
    {
        public TransPresentPrtcplVPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }

        public ITransitiveAction DirectObjectOf {
            get;
            set;
        }

        public ITransitiveAction IndirectObjectOf {
            get;
            set;
        }

        public IAction SubjectOf {
            get;
            set;
        }
    }
}
