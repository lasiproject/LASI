using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the broad role requirements for the weightable, countable textual elements, of a written work. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the ILexical interface provides for generalization and abstraction over many otherwise disparate element types and type hierarchies. 
    /// </para>
    /// </summary>
    public interface ILexical
    {
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Left of the Lexical element.
        /// </summary>
        IPrepositional PrepositionOnLeft {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Right of the Lexical element.
        /// </summary>
        IPrepositional PrepositionOnRight {
            get;
            set;
        }

        /// <summary>
        /// Gets the textual content of the Lexical element.
        /// </summary>
        string Text {
            get;
        }

        /// <summary>
        /// Gets or sets the numeric Weight of the Lexical element construct within its document.
        /// </summary>
        double Weight {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the numeric Weight of the Lexical element construct over the context of some subset of project extant documents.
        /// </summary>
        double MetaWeight {
            get;
            set;
        }
    }
}
