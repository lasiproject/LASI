namespace LASI.Core
{
    public interface ISentenceUnit : ILexical
    {
        /// <summary>
        /// Gets the <see cref="Sentence"/> that the <see cref="ISentenceUnit"/> is a unit of.
        /// </summary>
        Sentence Sentence { get; }
    }
}