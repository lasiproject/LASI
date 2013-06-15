using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an adverb which specifies a adverb or adjective in a context presumably relative to some occurance of the adverb or adjective.
    /// For Verbs - 
    /// If modifying an instransitive adverb it will usually lexically follow the the adverb: e.g. "Jane plays BETTER than John.
    /// If modifying a transitive adverb it will usually lexically follow the adverb object: e.g. "Jane plays poker "BETTER" than John.
    /// For Adjectives - 
    /// The adverb will usually lexically precede the adjective it modifies: e.g. John's wardrobe is MORE colorful than Jane's.
    /// </summary>
    public class ComparativeAdverb : Adverb
    {
        /// <summary>
        /// Initializes a new instance of the ComparativeAdverb class.
        /// </summary>
        /// <param name="text">The key text content of the ComparativeAdverb.</param>
        public ComparativeAdverb(string text)
            : base(text) {
        }
    }
}
