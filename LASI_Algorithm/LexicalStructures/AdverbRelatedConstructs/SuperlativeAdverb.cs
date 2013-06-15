using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an adverb which 'absolutely' specifies a adverb or adjective relative to all comparable occurances of that adverb or adjective.
    /// For Verbs - 
    /// If modifying an instransitive adverb it will usually lexically follow the the adverb: e.g. "Jane performed BEST.
    /// If modifying a transitive adverb it will usually lexically follow the adverb object: e.g. "Jane played poker BEST.
    /// For Adjectives - 
    /// The adverb will usually lexically precede the adjective it modifies: e.g. John's wardrobe is MOST colorful than Jane's.
    /// </summary>
    public class SuperlativeAdverb : Adverb
    {
        /// <summary>
        /// Initializes a new instance of the SuperlativeAdverb class.
        /// </summary>
        /// <param name="text">The key text content of the SuperlativeAdverb.</param>
        public SuperlativeAdverb(string text)
            : base(text) {

        }
    }
}
