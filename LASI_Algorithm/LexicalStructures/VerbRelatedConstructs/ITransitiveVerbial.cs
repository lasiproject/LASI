using LASI.Algorithm.FundamentalSyntacticInterfaces;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Transitive elements, generally Verbs or VerbPhrases,
    /// which can be bound to one or more Direct objects and to one or more Indirect objects .
    /// Along with the rhs interfaces in the Syntactic Interfaces Library,
    /// the ITransitiveVerbial interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface ITransitiveVerbial : IVerbial, IDirectObjectTaker, IInderectObjectTaker
    {
        VerbialArity Arity {
            get;
        }
    }
}

namespace LASI.Algorithm
{
    public enum VerbialArity
    {
        Undetermined,
        Nullary,
        Unary,
        Binary,
        Ternary,
        Quarternary
    }
}
