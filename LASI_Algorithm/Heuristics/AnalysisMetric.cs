using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm.Heuristics
{
    /// <summary>
    /// A lightweight struct which stores references to collections of highly weighted Heuristic results
    /// <see cref="Heuristic"/>
    /// <seealso cref="PronounAwareEntityHeuristic"/>
    /// </summary>
    public struct Metric
    {
        /// <summary>
        /// An enumerable collection of the most siginificant, with respect to the methodology of the Heuristic providing it, Entities and their frequency of appearance in a document
        /// </summary>
        public IEnumerable<CountedEntityResult> MostSignificantEntities {
            get;
            set;
        }
        /// <summary>
        /// An enumerable collection of the most siginificant, with respect to the methodology of the Heuristic providing it, Actions/Behaviors and their frequency of occurance in a document
        /// </summary>
        public IEnumerable<CountedActionResult> MostSignificantActions {
            get;
            set;
        }
    }

    /// <summary>
    /// A lightweight struct which encapsulates an Entity and the number of times it appears within a document, as determined by some Heuristic.
    /// </summary>
    public struct CountedEntityResult
    {
        /// <summary>
        /// The frequency of the Entity within a document as determined by some Heuristic
        /// </summary>
        public int Count {
            get;
            set;
        }
        /// <summary>
        /// The counted Entity
        /// </summary>
        public IEntity Entity {
            get;
            set;
        }
    }

    /// <summary>
    /// A lightweight struct which encapsulates an Action/Behavior and the number of times it occurs within a document, as determined by some Heuristic.
    /// </summary>
    public struct CountedActionResult
    {
        /// <summary>
        /// The frequency of the Action within a document as determined by some Heuristic
        /// </summary>
        public int Count {
            get;
            set;
        }
        /// <summary>
        /// The counted Action
        /// </summary>
        public IAction Action {
            get;
            set;
        }
    }
}
