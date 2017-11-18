namespace LASI.Core
{
    public interface ISentenceUnit : ILexical
    {
        /// <summary>
        /// The <see cref="Sentence"/> that the <see cref="ISentenceUnit"/> is a unit of.
        /// </summary>
        Sentence Sentence { get; }
    }
}