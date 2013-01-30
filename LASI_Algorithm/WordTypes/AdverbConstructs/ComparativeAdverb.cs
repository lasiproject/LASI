using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.DataRepresentation
{
    /// <summary>
    /// Represents an adverb which specifies a verb or adjective in a context presumably relative to some other occurance of the verb or adjective.
    /// For Verbs - 
    /// If modifying an instransitive verb it will usually lexically follow the the verb: E.g. "Jane plays BETTER than John.
    /// If modifying a transitive verb it will usually lexically follow the verb object: E.g. "Jane plays poker "BETTER" than John.
    /// For Adjectives - 
    /// The adverb will usually lexically precede the adjective it modifies: E.g. John's wardrobe is MORE colorful than Jane's.
    /// </summary>
    public class ComparativeAdverb : Adverb, IAdverbial
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
