namespace LASI.Interop.Visualization
{
    public interface ITextualRepresenation<out TLexical, out TRrepresentation> where TLexical : LASI.Core.ILexical
    {
        TLexical Represented { get; }
        TRrepresentation Representation { get; }
    }
}
