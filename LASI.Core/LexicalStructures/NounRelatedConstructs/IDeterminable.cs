namespace LASI.Core
{
    /// <summary>
    /// Represents a lexical construct, usually a Noun or NounPhrase which can be qualified by a determiner. Along with the other interfaces in the Syntactic Interfaces Library, the Quantifiable interface provides for generalization and abstraction over many otherwise disparate element types and Type hierarchies.
    /// </summary>
    /// <see cref="LASI.Core.Determiner"/>
    public interface IDeterminable : ILexical
    {
        /// <summary>
        /// The Determiner which is bound to the IDterminable.
        /// </summary>
        Determiner Determiner
        {
            get;
        }
        /// <summary>
        /// Binds the provided Determiner as a qualifier to the IDeterminable.
        /// </summary>
        /// <param name="determiner">The determiner which will be bound to the IDeterminable.</param>
        void BindDeterminer(Determiner determiner);
    }
}
