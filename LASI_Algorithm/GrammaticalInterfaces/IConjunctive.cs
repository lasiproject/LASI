using System;
namespace LASI.DataRepresentation
{
    /// <summary>
    /// Defines the roles of Conjunctive constructs which link two Clauses together.
    /// </summary>
    interface IConjunctive
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
