using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for "weakly possessive" lexical elements; generally PossiveEnding or PossessivePronoun objects. Weak possessive means that, while they </para> 
    /// <para> indicate possession semantics, they are not capable of being independent (e.g. first class owners of what they possess. For example, PossessiveEndings fall into this </para>
    /// <para> category because they indicate a possessive relationship between the entity(ies) which proceed(s) them and the entity(ies) which follow(s) them.  </para>
    /// <para> Similarly, PossessivePronouns fall into this category because they indicate possession between some possessor and some possession but do not in themselves have a first class existence. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IPossesser interface provides for generalization and abstraction over word and Phrase types. </para>
    /// </summary>
    public interface IWeakPossessor : IPossesser
    {
        /// <summary>
        /// Gets or sets the possessing Entity which possesses through the Weak Possessor.
        /// </summary>
        IPossesser ProxyFor { get; set; }
    }
}
