namespace LASI.Core.Heuristics.WordNet
{
    /// <summary>
    /// Defines the broad lexical categories assigned to Adjectives in the WordNet system.
    /// </summary>
    public enum AdjectiveCategory : byte
    {
        /// <summary>
        /// all adjective clusters
        /// </summary>
        All = 0,
        /// <summary>
        /// relational adjectives (pertainyms)
        /// </summary>
        Pert = 1,
        /// <summary>
        /// participial adjectives
        /// </summary>
        PPL = 44,
    }
}