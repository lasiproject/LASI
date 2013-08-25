using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Predpeterminer, a relative or abstract quantifier, such as "both" or "all"
    /// </summary>
    public class PreDeterminer : Quantifier
    {
        /// <summary>
        /// Initializes a new instance of the PreDeterminer class.
        /// </summary>
        /// <param name="text">The text content of the PreDeterminer.</param>
        public PreDeterminer(string text)
            : base(text) {
        }


    }
}
