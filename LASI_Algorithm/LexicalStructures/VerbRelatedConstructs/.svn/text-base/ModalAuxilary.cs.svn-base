
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Modality modifier which modifies a modality-modifiable construct such as a Verb or VerbPhrase.
    /// Examples of ModalAuxilary words are "can", and "might"
    /// </summary>
    public class ModalAuxilary : Word
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the ModalAuxilary class
        /// </summary>
        /// <param name="text">The text content of the ModalAuxilary.</param>
        public ModalAuxilary(string text)
            : base(text) {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the modality-modifiable construct such as a Verb or VerbPhrase, which this ModalAuxilary Modifies
        /// </summary>
        public virtual IModalityModifiable Modifies {
            get;
            set;
        }

        #endregion

    }
}
