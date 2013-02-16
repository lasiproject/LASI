using System;
namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the roles of Conjunctive constructs which link two Clauses or Phrases together.
    /// </summary>
    interface IConjunctive : ILexical
    {
        Clause OnLeft {
            get;
            set;
        }
        Clause OnRight {
            get;
            set;
        }
    }
}
