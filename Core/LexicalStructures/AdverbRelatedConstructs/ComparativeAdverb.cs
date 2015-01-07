using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// <para> Represents an adverb which specifies a verb or adjective in a context presumably relative to some occurrence of the verb or adjective.</para>
    /// <para>For Verbs - </para>
    /// <para>If modifying an intransitive verb it will usually lexically follow the the verb: e.g. "Jane plays BETTER than John.</para>
    /// <para>If modifying a transitive verb it will usually lexically follow the verb object: e.g. "Jane plays poker "BETTER" than John.</para>
    /// <para>For Adjectives - </para>
    /// <para>The adverb will usually lexically precede the adjective it modifies: e.g. John's wardrobe is MORE colorful than Jane's.</para>
    /// </summary>
    public class ComparativeAdverb : Adverb
    {
        /// <summary>
        /// Initializes a new instance of the ComparativeAdverb class.
        /// </summary>
        /// <param name="text">The text content of the ComparativeAdverb.</param>
        public ComparativeAdverb(string text) : base(text) { }
    }
}
