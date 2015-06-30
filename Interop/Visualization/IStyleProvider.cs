namespace LASI.Interop.Visualization
{
    /// <summary>
    /// Represents a syntactic mapping from Lexical element Types to stylization. 
    /// </summary>
    /// <typeparam name="TLexical">The type of Lexical elements to map from. This type parameter is contravariant. That is,
    /// you can use either the type you specified or any type that is less derived.</typeparam>
    /// <typeparam name="TStylingArtifact">The type of styling artifacts that are associated to mapped elements. 
    /// This type parameter is covariant. That is, you can use either the type you specified or any type that is more derived.
    /// </typeparam>
    /// <remarks>
    /// Motivation:
    /// This interface was defined to encourage a common pattern in which lexical elements would be styled for display by indexing into the map. 
    /// For example in the LASI WebApp an implementation of this interface associates CSS classes with syntactic elements.
    /// These implementations use the pattern matching facilities provided by the LASI infrastructure to provide a clean readable style mappings to various types.
    /// </remarks>
    public interface IStyleProvider<in TLexical, out TStylingArtifact> where TLexical : LASI.Core.ILexical
    {
        /// <summary>
        /// Gets a stylization value corresponding to the syntactic nature of the indexing lexical element. 
        /// Implementors should provide a default value as a fall back.
        /// </summary>
        /// <param name="element">The lexical element to map to a stylization value.</param>
        /// <returns>A stylization value corresponding to the syntactic nature of the indexing lexical element.</returns>
        TStylingArtifact this[TLexical element] { get; }
    }
}
