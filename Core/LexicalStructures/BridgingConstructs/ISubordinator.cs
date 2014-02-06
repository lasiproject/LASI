using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{

    /// <summary>
    /// <para> Represents a Lexical construct which has the effect of subordinating the elements which follow it. This behavior has many concrete forms. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the ISubordinator interface provides for generalization and abstraction over many otherwise disparate element types and Type heirarchies. </para>
    /// </summary>
    /// <see cref="LASI.Core.SubordinateClauseBeginPhrase"/>
    public interface ISubordinator : ILexical
    {
        ILexical Subordinates { get; set; }
    }
}
