
namespace LASI.Algorithm.FundamentalSyntacticInterfaces
{
    /// <summary>
    /// Defines the role reqirements for Transitive elements, generally Verbs or VerbPhrases, which can be bound to one or more Direct objects.
    /// Along with the rhs interfaces in the Syntactic Interfaces Library, the IDirectObjectTaker interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface IDirectObjectTaker
    {
        void BindDirectObject(IEntity directObject);
        System.Collections.Generic.IEnumerable<IEntity> DirectObjects {
            get;
        }

    }

}
