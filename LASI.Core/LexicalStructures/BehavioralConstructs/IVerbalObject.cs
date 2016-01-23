
namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for Action Objects, generally the objects of Verbs or VerbPhrases. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IActionObject interface provides for generalization and abstraction over word and Phrase types. </para>
    /// </summary>
    public interface IVerbalObject
    {
        /// <summary>
        /// Binds the <see cref="IVerbalObject"/> as a direct object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        void BindAsDirectObjectOf(IVerbal verbal);

        /// <summary>
        /// Binds the <see cref="IVerbalObject"/> as an indirect object of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        void BindAsIndirectObjectOf(IVerbal verbal);

        /// <summary>
        /// Gets the <see cref="IVerbal"/> construct the <see cref="IVerbalObject"/> is the direct object of.
        /// </summary>

        IVerbal DirectObjectOf { get; }
        /// <summary>
        /// Gets the <see cref="IVerbal"/> construct the <see cref="IVerbalObject"/> is the indirect object of.
        /// </summary>
        IVerbal IndirectObjectOf { get; }
    }
}
