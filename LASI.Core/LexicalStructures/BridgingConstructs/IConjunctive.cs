namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements of Conjunctive constructs which link two Clauses, Phrases, or Words together. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IConjunctive interface provides for cross-axial generalization over lexical types. </para>
    /// </summary>
    public interface IConjunctive : ILexical
    {
        /// <summary>
        /// Gets or sets the ILexical element on the Right of the conjunctive.
        /// </summary>
        ILexical JoinedRight
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ILexical element on the Left of the conjunctive.
        /// </summary>
        ILexical JoinedLeft
        {
            get;
            set;
        }
    }
}
