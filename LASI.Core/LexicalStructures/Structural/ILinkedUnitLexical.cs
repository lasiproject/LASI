namespace LASI.Core.LexicalStructures
{
    interface ILinkedUnitLexical<out TLexical> : IUnitLexical
    {
        TLexical Previous { get; }
        TLexical Next { get; }
    }
}
