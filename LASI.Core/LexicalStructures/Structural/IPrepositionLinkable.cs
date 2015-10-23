namespace LASI.Core
{
    public interface IPrepositionLinkable : ILexical
    {
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Left of the Lexical element.
        /// </summary>
        IPrepositional PrepositionOnLeft { get; set; }

        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Right of the Lexical element.
        /// </summary>
        IPrepositional PrepositionOnRight { get; set; }

    }
}