using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core.Heuristics
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
        /// Determines if the current Relationship instance is equal to the specified System.Object.
        /// </summary>
        /// <param name="obj">The Object to compare to.</param>
        /// <returns>True if the current Relationship is equal to the specified System.Object.</returns>
        public override bool Equals(object obj) {
            return obj is PerformerReceiverPair<TPerformer, TReceiver> &&
                Performer.Equals((PerformerReceiverPair<TPerformer, TReceiver>)obj) &&
                Receiver.Equals((PerformerReceiverPair<TPerformer, TReceiver>)obj);
        }
        /// <summary>
        /// Gets a hash code for the current PerformerReceiverPair&lt;TPerformer, TReceiver&gt; instance.
        /// </summary>
        /// <returns>A hash code for the current PerformerReceiverPair&lt;TPerformer, TReceiver&gt; instance.</returns>
        public override int GetHashCode() {
            return Performer.GetHashCode() ^ Receiver.GetHashCode();
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
        /// <summary>
        /// Determines if two PerformerReceiverPair&lt;TPerformer, TReceiver&gt; instances are considered equal.
        /// </summary>
        /// <param name="left">The first PerformerReceiverPair&lt;TPerformer, TReceiver&gt;.</param>
        /// <param name="right">The second PerformerReceiverPair&lt;TPerformer, TReceiver&gt;.</param>
        /// <returns>True if the structures are considered equal; otherwise, false.</returns>
        public static bool operator ==(PerformerReceiverPair<TPerformer, TReceiver> left, PerformerReceiverPair<TPerformer, TReceiver> right) { return left.Equals(right); }
        /// <summary>
        /// Determines if two PerformerReceiverPair&lt;TPerformer, TReceiver&gt; instances are considered unequal.
        /// </summary>
        /// <param name="left">The first PerformerReceiverPair&lt;TPerformer, TReceiver&gt;.</param>
        /// <param name="right">The second PerformerReceiverPair&lt;TPerformer, TReceiver&gt;.</param>
        /// <returns>True if the structures are considered unequal; otherwise, false.</returns>
        public static bool operator !=(PerformerReceiverPair<TPerformer, TReceiver> left, PerformerReceiverPair<TPerformer, TReceiver> right) { return !(left == right); }
    }
}
