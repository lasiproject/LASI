namespace LASI.Core
{
    public interface ITypePattern<TLexical> where TLexical : class, ILexical
    {
        PatternMatching.Match<TLexical> Match { get; set; }
    }
}