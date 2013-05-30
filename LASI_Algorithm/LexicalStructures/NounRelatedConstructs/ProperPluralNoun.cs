using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{

    /// <summary>
    /// Represents a Proper Plural Noun.
    /// </summary>
    public class ProperPluralNoun : ProperNoun, IQuantifiable
    {
        /// <summary>
        /// Initializes a new instance of the ProperPluralNoun class.
        /// </summary>
        /// <param name="text">The literal text content of the ProperPluralNoun.</param>
        public ProperPluralNoun(string text)
            : base(text) { 
        }

        /// <summary>
        /// Gets or sets the Qunatifier which specifies the number of units of the ProperNoun which are referred to in this occurance.
        /// e.g. "[18] Pinkos"
        /// </summary>
        public virtual Quantifier Quantifier {
            get;
            set;
        }



    }
}
