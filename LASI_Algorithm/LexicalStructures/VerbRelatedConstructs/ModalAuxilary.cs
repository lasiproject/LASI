using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents entity Modality modifier which modifies entity modality-modifiable construct such as entity Verb or VerbPhrase.
    /// Examples of ModalAuxilary words are "can", and "might"
    /// </summary>
    public class ModalAuxilary : Word
    { 
        #region Constructors
        /// <summary>
        /// Initializes entity new instance of the ModalAuxilary class
        /// </summary>
        /// <param name="text">The literal text content of the verb.</param>
        public ModalAuxilary(string text)
            : base(text) {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the modality-modifiable construct such as entity Verb or VerbPhrase, which this ModalAuxilary Modifies
        /// </summary>
        public virtual IModalityModifiable Modifies {
            get;
            set;
        }

        #endregion

    }
}
