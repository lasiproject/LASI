using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core.Heuristics
{
    /// <summary>
    /// Stores the relationship between a Verbal construct used transitively (having at least one direct or indirect object) 
    /// and an Entity construct which receives the effect of said Verbal construct (i.e. its object).
    /// </summary>
    /// <typeparam name="TVerbal">The Type of the Verbal construct in the relationship. The stated or inferred Type must implement the IVerbal interface.</typeparam>
    /// <typeparam name="TEntity">The Type of the Entity construct in the relationship. The stated or inferred Type must implement the IEntity interface.</typeparam>
    /// <remarks>Any instance of the ActionReceiverPair struct is immutable unless passed as a 'ref' or 'out' argument to a function.</remarks>
    public struct ActionReceiverPair<TVerbal, TEntity>
        where TVerbal : IVerbal
        where TEntity : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the ActionReceiverPair structure based on the provided action and receiver.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="receiver">The receiver of the action.</param>
        public ActionReceiverPair(TVerbal action, TEntity receiver)
            : this() {
            Action = action;
            Receiver = receiver;
        }
        /// <summary>
        /// Determines if the ActionReceiverPair&lt;TVerbal, TEntity&gt; is equal to the given object.
        /// </summary>
        /// <param name="obj">The object to compare for equality.</param>
        /// <returns>True if the ActionReceiverPair&lt;TVerbal, TEntity&gt; is equal to the given object, otherwise false.</returns>
        public override bool Equals(object obj) {
            return obj is ActionReceiverPair<TVerbal, TEntity> && Action.Equals(((ActionReceiverPair<TVerbal, TEntity>)obj).Action) && Receiver.Equals(((ActionReceiverPair<TVerbal, TEntity>)obj).Receiver);
        }

        /// <summary>
        /// Gets a hash code for the current ActionReceiverPair instance.
        /// </summary>
        /// <returns>A hash code of the current ActionReceiverPair instance.</returns>
        public override int GetHashCode() {
            return Action.GetHashCode() ^ Receiver.GetHashCode();
        }
        /// <summary>
        /// Gets the Action.
        /// </summary>
        public TVerbal Action {
            get;
            private set;
        }
        /// <summary>
        /// Gets the Receiver of the Action.
        /// </summary>
        public TEntity Receiver {
            get;
            private set;
        }
        /// <summary>
        /// Determines if two ActionReceiverPair&lt;TVerbal, TEntity&gt; instances are considered equal.
        /// </summary>
        /// <param name="left">The first ActionReceiverPair&lt;TVerbal, TEntity&gt; instance.</param>
        /// <param name="right">The second ActionReceiverPair&lt;TVerbal, TEntity&gt; instance.</param>
        /// <returns>True if the ActionReceiverPair&lt;TVerbal, TEntity&gt; instances are considered equal, false otherwise.</returns>
        public static bool operator ==(ActionReceiverPair<TVerbal, TEntity> left, ActionReceiverPair<TVerbal, TEntity> right) {
            return left.Equals(right);
        }
        /// <summary>
        /// Determines if two ActionReceiverPair&lt;TVerbal, TEntity&gt; instances are considered unequal.
        /// </summary>
        /// <param name="left">The first ActionReceiverPair&lt;TVerbal, TEntity&gt; instance.</param>
        /// <param name="right">The second ActionReceiverPair&lt;TVerbal, TEntity&gt; instance.</param>
        /// <returns>True if the ActionReceiverPair&lt;TVerbal, TEntity&gt; instances are considered unequal, false otherwise.</returns>
        public static bool operator !=(ActionReceiverPair<TVerbal, TEntity> left, ActionReceiverPair<TVerbal, TEntity> right) {
            return !(left == right);
        }
    }
}
