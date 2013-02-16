using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Modality modifier which modifies a modality-modifiable construct such as a Verb or VerbPhrase.
    /// Examples of Modal words are "can", and "might"
    /// </summary>
    public class Modal : Word
    {
        /// <summary>
        /// Initializes a new instance of the Modal class
        /// </summary>
        /// <param name="text">The literal text content of the word.</param>
        public Modal(string text)
            : base(text) {
        }
        /// <summary>
        /// Gets or sets the modality-modifiable construct such as a Verb or VerbPhrase, which this Modal Modifies
        /// </summary>
        public virtual IModalityModifiable Modifies {
            get;
            set;
        }
    }
}
