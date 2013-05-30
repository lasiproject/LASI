using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an adverb which specifies a verb or adjective in a context presumably relative to some occurance of the verb or adjective.
    /// For Verbs - 
    /// If modifying an instransitive verb it will usually lexically follow the the verb: e.g. "Jane plays BETTER than John.
    /// If modifying a transitive verb it will usually lexically follow the verb object: e.g. "Jane plays poker "BETTER" than John.
    /// For Adjectives - 
    /// The adverb will usually lexically precede the adjective it modifies: e.g. John's wardrobe is MORE colorful than Jane's.
    /// </summary>
    public class ComparativeAdverb : Adverb
    {
        /// <summary>
        /// Initializes a new instance of the ComparativeAdverb class.
        /// </summary>
        /// <param name="text">The literal text content of the ComparativeAdverb.</param>
        public ComparativeAdverb(string text)
            : base(text) {
        }
    }
}
