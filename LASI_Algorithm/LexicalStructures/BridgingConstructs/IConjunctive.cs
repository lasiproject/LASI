using System;
namespace LASI.Algorithm.SyntacticInterfaces
{
    /// <summary>
    /// Defines the role reqirements of Conjunctive constructs which link two Clauses, Phrases, or Words together.
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IConjunctive interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface IConjunctive : ILexical
    {
        /// <summary>
        /// Gets or sets the ILexical element on the right of the conjunctive.
        /// </summary>
        ILexical OnRight {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ILexical element on the left of the conjunctive.
        /// </summary>
        ILexical OnLeft {
            get;
            set;
        }
    }
}
