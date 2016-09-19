using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for constructs; generally Nouns, NounPhrases or Pronouns; which are "possessable" by other Entities. </para> 
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IPossessable interface provides for cross-axial generalization over lexical types. </para>
    /// </summary>
    public interface IPossessable : ILexical
    {
        /// <summary>
        /// Gets or sets the Entity which has been inferred as the "owner" of the IPossessable.
        /// </summary>
        Option<IPossesser> Possesser { get; set; }
    }
}
