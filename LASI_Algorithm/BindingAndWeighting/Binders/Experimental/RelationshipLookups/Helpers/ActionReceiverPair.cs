using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core.ComparativeHeuristics
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
        public override bool Equals(object obj) {
            return obj is ActionReceiverPair<TVerbal, TEntity> && Action.Equals(((ActionReceiverPair<TVerbal, TEntity>)obj).Action) && Receiver.Equals(((ActionReceiverPair<TVerbal, TEntity>)obj).Receiver);
        }
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
        public static bool operator ==(ActionReceiverPair<TVerbal, TEntity> left, ActionReceiverPair<TVerbal, TEntity> right) { return left.Equals(right); }
        public static bool operator !=(ActionReceiverPair<TVerbal, TEntity> left, ActionReceiverPair<TVerbal, TEntity> right) { return !(left == right); }
    }
}
