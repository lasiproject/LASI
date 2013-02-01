using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a transative verb in its base tense, a verb which can take a direct and an optional indirect object.
    /// </summary>
    public class TransitiveVerb : Verb, ITransitiveAction
    {
        /// <summary>
        /// Initializes a new instance of the TransitiveVerb class.
        /// </summary>
        /// <param name="text">The literal text content of the verb.</param>
        public TransitiveVerb(string text)
            : base(text, VerbTense.Base) {
        }

        /// <summary>
        /// Gets or sets the directobject of the TransitiveVerb.
        /// </summary>
        public virtual IEntity DirectObject {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the indirect object of the TransitiveVerb.
        /// </summary>
        public virtual IEntity IndirectObject {
            get;
            set;
        }
    }
}
