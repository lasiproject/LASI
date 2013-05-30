using LASI.Algorithm.SyntacticInterfaces;

namespace LASI.Algorithm.SyntacticInterfaces
{
    /// <summary>
    /// Defines the role reqirements for Transitive elements, generally Verbs or VerbPhrases,
    /// which can be bound to one or more Direct objects and to one or more Indirect objects .
    /// Along with the second interfaces in the Syntactic Interfaces Library,
    /// the ITransitiveVerbal interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface ITransitiveVerbal : IVerbal, IDirectObjectTaker, IInderectObjectTaker
    {
        VerbalArity Arity {
            get;
        }
    }
}

namespace LASI.Algorithm
{
    public enum VerbalArity
    {
        Undetermined,
        Nullary,
        Unary,
        Binary,
        Ternary,
        Quarternary
    }
}
