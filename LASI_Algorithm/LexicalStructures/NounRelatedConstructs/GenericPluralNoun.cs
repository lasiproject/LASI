using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a generic, proper, singular noun.
    /// </summary>
    public class GenericPluralNoun : GenericNoun, IQuantifiable
    {
        /// <summary>
        /// Initializes a new instances of the GenericPluralNoun class.
        /// </summary>
        /// <param name="text">The literal text content of the GenericPluralNoun</param>
        public GenericPluralNoun(string text)
            : base(text) {
        }



        /// <summary>
        /// Gets or sets a Qunatifier which specifies the number of units of the GenericPluralNoun which are referred to in this occurance.
        /// e.g. "[five] miscreants"
        /// </summary>
        public virtual Quantifier Quantifier {
            get;
            set;
        }
    }
}
