using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a generic, proper, singular noun.
    /// </summary>
    public class CommonPluralNoun : CommonNoun, IQuantifiable
    {
        /// <summary>
        /// Initializes a new instances of the GenericPluralNoun class.
        /// </summary>
        /// <param name="text">The key text content of the GenericPluralNoun</param>
        public CommonPluralNoun(string text)
            : base(text) {
        }

        /// <summary>
        /// Gets or sets the Qunatifier which specifies the number of units of the GenericPluralNoun which are referred to in this occurance.
        /// e.g. "[five] miscreants"
        /// </summary>
        public override IQuantifier QuantifiedBy {
            get;
            set;
        }
    }
}
