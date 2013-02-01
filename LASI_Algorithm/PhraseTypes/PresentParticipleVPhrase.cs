using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class PresentPrtcplVPhrase : VerbPhrase, IActionSubject, IActionObject
    {
        public PresentPrtcplVPhrase(IEnumerable<Word> composedWords)
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
