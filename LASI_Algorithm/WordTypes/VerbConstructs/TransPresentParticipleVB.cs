using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.DataRepresentation
{
    public class TransPresentPrtcplVB : TransitiveVerb, IActionObject, IActionSubject
    {
        public TransPresentPrtcplVB(string text)
            : base(text) {
        }

        public IIntransitiveAction SubjectOf {
            get;
            set;
        }

        public ITransitiveAction DirectObjectOf {
            get;
            set;
        }

        public ITransitiveAction IndirectObjectOf {
            get;
            set;
        }
    }
}
