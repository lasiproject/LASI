namespace LASI.Core.LexicalStructures
{
    /// <summary>
    /// Defines a logical lexical unit, a single structure such as a <see cref="Word"/>, <see cref="Phrase"/>, or <see cref="Clause"/>. 
    /// </summary>
    public interface IUnitLexical : ILexical, IPrepositionLinkable
    {
    }
}
