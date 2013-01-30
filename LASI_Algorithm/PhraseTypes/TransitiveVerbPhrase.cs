using System;
using System.Collections.Generic;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a transative verb phrase in its base tense, a VerbPhrase which can take a direct and an optional indirect object.
    /// </summary>
    public class TransitiveVerbPhrase : VerbPhrase, ITransitiveAction
    {
        /// <summary>
        /// Initializes a new instance of the TransitiveVerbPhrase class.
        /// </summary>
        /// <param name="componentWords">The words which compose to form the TransitiveVerbPhrase.</param>
        /// <param name="tense">The Tense of the TransitiveVerbPhrase.</param>
        public TransitiveVerbPhrase(IEnumerable<Word> componentWords)
            : base(componentWords) {
        }

        public virtual void BindToDirectObject(IActionObject verbObject) {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets or sets 
        /// </summary>
        public virtual IActionObject DirectObject {
            get;
            set;
        }

        public virtual void BindToIndirectObject(IActionObject verbObject) {
            throw new NotImplementedException();
        }

        public virtual IActionObject IndirectObject {
            get;
            set;
        }
    }
}

