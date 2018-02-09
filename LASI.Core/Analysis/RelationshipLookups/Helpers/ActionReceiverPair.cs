using System;

namespace LASI.Core.Heuristics.Relationships
{
    /// <summary>
    /// Stores the relationship between a Verbal construct used transitively (having at least one
    /// direct or indirect object) and an Entity construct which receives the effect of said Verbal
    /// construct (i.e. its object).
    /// </summary>
    /// <typeparam name="TVerbal">
    /// The Type of the Verbal construct in the relationship. The stated or inferred Type must
    /// implement the IVerbal interface.
    /// </typeparam>
    /// <typeparam name="TEntity">
    /// The Type of the Entity construct in the relationship. The stated or inferred Type must
    /// implement the IEntity interface.
    /// </typeparam>
    /// <remarks>
    /// Any instance of the ActionReceiverPair struct is immutable unless passed as a 'ref' or 'out'
    /// argument to a function.
    /// </remarks>
    public struct ActionReceiverPair<TVerbal, TEntity> : IEquatable<ActionReceiverPair<TVerbal, TEntity>>, IActionReceiverPair<TVerbal, TEntity> where TVerbal : IVerbal
        where TEntity : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the ActionReceiverPair structure based on the provided
        /// action and receiver.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="receiver">The receiver of the action.</param>
        public ActionReceiverPair(TVerbal action, TEntity receiver)
        {
            Action = action;
            Receiver = receiver;
        }

        /// <summary>
        /// Determines if two ActionReceiverPair&lt;TVerbal, TEntity&gt; instances are considered unequal.
        /// </summary>
        /// <param name="left">The first ActionReceiverPair&lt;TVerbal, TEntity&gt; instance.</param>
        /// <param name="right">The second ActionReceiverPair&lt;TVerbal, TEntity&gt; instance.</param>
        /// <returns>
        /// <c>true</c> if the ActionReceiverPair&lt;TVerbal, TEntity&gt; instances are considered
        /// unequal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(ActionReceiverPair<TVerbal, TEntity> left, ActionReceiverPair<TVerbal, TEntity> right) => !(left == right);

        /// <summary>
        /// Determines if two ActionReceiverPair&lt;TVerbal, TEntity&gt; instances are considered equal.
        /// </summary>
        /// <param name="left">The first ActionReceiverPair&lt;TVerbal, TEntity&gt; instance.</param>
        /// <param name="right">The second ActionReceiverPair&lt;TVerbal, TEntity&gt; instance.</param>
        /// <returns>
        /// <c>true</c> if the ActionReceiverPair&lt;TVerbal, TEntity&gt; instances are considered
        /// equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(ActionReceiverPair<TVerbal, TEntity> left, ActionReceiverPair<TVerbal, TEntity> right) => left.Equals(right);

        /// <summary>
        /// Determines if the ActionReceiverPair&lt;TVerbal, TEntity&gt; is equal to the given object.
        /// </summary>
        /// <param name="obj">The object to compare for equality.</param>
        /// <returns>
        /// <c>true</c> if the ActionReceiverPair&lt;TVerbal, TEntity&gt; is equal to the given
        /// object, otherwise false.
        /// </returns>
        public override bool Equals(object obj) => obj is ActionReceiverPair<TVerbal, TEntity> o && Equals(o);

        /// <summary>
        /// Indicates whether the current <see cref="ActionReceiverPair{TEntity,TVerbal}"/> is equal
        /// to another instance of the same type.
        /// </summary>
        /// <param name="other">The instance to test for equality.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="ActionReceiverPair{TEntity,TVerbal}"/> are equal;
        /// otherwise <c>false</c>
        /// </returns>
        public bool Equals(ActionReceiverPair<TVerbal, TEntity> other) => Action.Equals(other.Action) && Receiver.Equals(other.Receiver);

        /// <summary>
        /// Gets a hash code for the current ActionReceiverPair instance.
        /// </summary>
        /// <returns>A hash code of the current ActionReceiverPair instance.</returns>
        public override int GetHashCode() => Action.GetHashCode() ^ Receiver.GetHashCode();

        /// <summary>
        /// The Action.
        /// </summary>
        public TVerbal Action { get; }

        /// <summary>
        /// The Receiver of the Action.
        /// </summary>
        public TEntity Receiver { get; }
    }
}