using System;
namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the roles of Conjunctive constructs which link two Clauses or Phrases or Words together.
    /// </summary>
    public interface IConjunctive : ILexical
    {

        ILexical OnRight {
            get;
            set;
        }
        ILexical OnLeft {
            get;
            set;
        }
    }
}
