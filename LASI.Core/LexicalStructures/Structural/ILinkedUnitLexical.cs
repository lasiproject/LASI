namespace LASI.Core.LexicalStructures
{
    /// <summary>
    /// Defines a <see cref="IUnitLexical"/> which is linked to the first and or next <see cref="IUnitLexical" /> of the same kind.
    /// </summary>
    /// <see cref="LASI.Core.Phrase"/>
    interface ILinkedUnitLexical<out TLexical> : IUnitLexical
    {
        TLexical Previous { get; }
        TLexical Next { get; }
    }
}
