namespace LASI.Core
{
    /// <summary>
    /// <para>
    /// Defines the role requirements for Transitive elements, generally Verbs or VerbPhrases, which
    /// can be bound to one or more Direct Objects and one or more Indirect objects.
    /// </para>
    /// <para>
    /// Along with the other interfaces in the Syntactic Interfaces Library, the
    /// IInderectObjectTaker interface provides for generalization and abstraction over word and
    /// Phrase types.
    /// </para>
    /// </summary>
    public interface IObjectTaker : IInderectObjectTaker, IDirectObjectTaker
    {
        /// <summary>Gets the all of the Direct and Indirect objects of the IObjectTaker.</summary>
        System.Collections.Generic.IEnumerable<IEntity> DirectAndIndirectObjects
        {
            get;
        }
    }
}