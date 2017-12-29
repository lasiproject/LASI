namespace LASI.Core
{
    /// <summary>
    /// <para> Represents an adverb which 'absolutely' specifies a verb or adjective relative to all comparable occurrences of that verb or adjective. </para>
    /// <para> For Verbs - </para>
    /// <para> If modifying an intransitive verb it will usually lexically follow the the verb: e.g. "Jane performed BEST. </para>
    /// <para> If modifying a transitive verb it will usually lexically follow the verb object: e.g. "Jane played poker BEST. </para>
    /// <para> For Adjectives - </para>
    /// <para> The adverb will usually lexically precede the adjective it modifies: e.g. John's wardrobe is MOST colorful than Jane's. </para>
    /// </summary>
    public class SuperlativeAdverb : Adverb
    {
        /// <summary>
        /// Initializes a new instance of the SuperlativeAdverb class.
        /// </summary>
        /// <param name="text">The text content of the SuperlativeAdverb.</param>
        public SuperlativeAdverb(string text) : base(text) { }
    }
}
 