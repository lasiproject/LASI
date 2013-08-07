using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role requirements for "weakly possessive" lexical elements; generally PossiveEnding or PossessivePronoun objects. Weak possessive means that, while they 
    /// indicate possession semantics, they are not capable of being independent (e.g. first class owners of what they possess. For example, PossessiveEndings fall into this
    /// category because they indicate a possessive relationship beween the entity(ies) which procede(s) them and the entity(ies) which follow(s) them. 
    /// Simularly, PossessivePronouns fall into this category because they indicate possession between some posessor and some possession but do not in themselves have a first class existence. 
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IPossesser interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface IWeakPossessor : IPossesser
    {
        /// <summary>
        /// Gets or sets the possessing IEntity construct which possesses through the IWeakPossessor construct.
        /// </summary>
        IEntity PossessesFor {
            get;
            set;
        }
    }
}
