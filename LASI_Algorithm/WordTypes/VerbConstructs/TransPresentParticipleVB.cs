using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class TransPresentPrtcplVerb : TransitiveVerb, IActionObject, IActionSubject
    {
        /// <summary>
        /// Initializes a new instance of the TransitivePresentPrtcplVerb class.
        /// </summary>
        /// <param name="text">The literal text content of the TransitivePresentPrtcplVerb.</param>
        public TransPresentPrtcplVerb(string text)
            : base(text) {
        }
        /// <summary>
        /// Gets or sets the Action, generally a Verb or VerbPhrase, whose subject is the TransitivePresentPrtcplVerb.
        /// </summary>
        public IAction SubjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the TransitiveAction, generally a TransitiveVerb or TransitiveVerbPhrase, whose direct object is the TransitivePresentPrtcplVerb.
        /// </summary>
        public ITransitiveAction DirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// The TransitiveAction, generally a TransitiveVerb or TransitiveVerbPhrase, whose indirect direct object is the TransitivePresentPrtcplVerb.
        /// </summary>
        public ITransitiveAction IndirectObjectOf {
            get;
            set;
        }
    }
}
