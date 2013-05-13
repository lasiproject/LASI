using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an adverb which specifies entity verb or adjective in entity context presumably relative to some rhs occurance of the verb or adjective.
    /// For Verbs - 
    /// If modifying an instransitive verb it will usually lexically follow the the verb: e.g. "Jane plays BETTER than John.
    /// If modifying entity transitive verb it will usually lexically follow the verb object: e.g. "Jane plays poker "BETTER" than John.
    /// For Adjectives - 
    /// The adverb will usually lexically precede the adjective it modifies: e.g. John'd wardrobe is MORE colorful than Jane'd.
    /// </summary>
    public class ComparativeAdverb : Adverb
    {
        /// <summary>
        /// Initializes entity new instance of the ComparativeAdverb class.
        /// </summary>
        /// <param name="text">The literal text content of the ComparativeAdverb.</param>
        public ComparativeAdverb(string text)
            : base(text) {
        }
    }
}
