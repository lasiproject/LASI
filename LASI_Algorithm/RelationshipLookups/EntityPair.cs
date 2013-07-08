using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.RelationshipLookups
{
    /// <summary>
    /// Stores the relationship between two Entity constructs which are related together by the first performing an action received by the second.
    /// </summary>
    /// <typeparam name="TPerformer">The Type of the Performer Entity construct in the relationship. The stated or inferred Type must implement the IEntity interface.</typeparam>
    /// <typeparam name="TReceiver">The Type of the Receiver Entity construct in the relationship. The stated or inferred Type must implement the IEntity interface.</typeparam>
    /// <remarks>Any instance of the PerformerReceiverPair struct is immutable unless passed as a 'ref' or 'out' argument to a function.</remarks>
    public struct PerformerReceiverPair<TPerformer, TReceiver>
        where TPerformer : IEntity
        where TReceiver : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the ActionReceiverPair structure from the provided action performer and action receiver.
        /// </summary>
        /// <param name="performer">The performer of the action.</param>
        /// <param name="receiver">The receiver of the action.</param>
        public PerformerReceiverPair(TPerformer performer, TReceiver receiver)
            : this() {
            Performer = performer;
            Receiver = receiver;
        }
        /// <summary>
        /// Gets the Performer.
        /// </summary>
        public TPerformer Performer {
            get;
            private set;
        }
        /// <summary>
        /// Gets the Receiver.
        /// </summary>
        public TReceiver Receiver {
            get;
            private set;
        }
    }
}
