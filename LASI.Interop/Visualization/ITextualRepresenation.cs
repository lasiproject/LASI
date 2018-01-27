namespace LASI.Interop.Visualization
{
    /// <summary>
    /// Defines the linkage between a <typeparamref name="TLexical"/> and the <typeparamref name="TRrepresentation"/> providing its visual representation.
    /// </summary>
    /// <typeparam name="TLexical"></typeparam>
    /// <typeparam name="TRrepresentation"></typeparam>
    public interface ITextualRepresenation<out TLexical, out TRrepresentation> where TLexical : Core.ILexical
    {
        /// <summary>
        /// The <typeparamref name="TLexical"/> respresented.
        /// </summary>
        TLexical Represented { get; }
        /// <summary>
        /// The <typeparamref name="TRrepresentation"/> serving as the visual representation.
        /// </summary>
        TRrepresentation Representation { get; }
    }
}
