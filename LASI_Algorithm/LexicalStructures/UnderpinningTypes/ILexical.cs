using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the broad role requiements for the weightable, countable textual elements, of a written work.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the ILexical interface provides for generalization and abstraction over many otherwise disparate element types and type heirarchies.
    /// </summary>
    public interface ILexical
    {
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Left of the ILexical.
        /// </summary>
        IPrepositional PrepositionOnLeft {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Right of the ILexical.
        /// </summary>
        IPrepositional PrepositionOnRight {
            get;
            set;
        }

        /// <summary>
        /// Gets the key text of the ILexical.
        /// </summary>
        string Text {
            get;
        }
        /// <summary>
        /// Gets the System.Type of the ILexical.
        /// </summary>
        Type Type {
            get;
        }

        /// <summary>
        /// Gets or sets the numeric Weight of the ILexical construct within its document.
        /// </summary>
        double Weight {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the numeric Weight of the ILexical construct over the context of all extant documents.
        /// </summary>
        double MetaWeight {
            get;
            set;
        }
    }
}
