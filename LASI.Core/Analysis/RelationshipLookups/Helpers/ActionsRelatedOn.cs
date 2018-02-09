using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core.Heuristics.Relationships
{
    /// <summary>
    /// Provides a nullable sequence of IVerbal constructs. Instances of the structure will implicitly collapse to true or false as needed
    /// </summary>.
    public struct ActionsRelatedOn : IEquatable<ActionsRelatedOn>
    {
        /// <summary>
        /// Initializes a new instance of the ActionsRelatedOn structure.
        /// </summary>
        /// <param name="actionsRelatedOn"></param>
        internal ActionsRelatedOn(IEnumerable<IVerbal> actionsRelatedOn) => RelatedOn = actionsRelatedOn;

        /// <summary>
        /// Determines if the current ActionsRelatedOn instance is equal to another ActionsRelatedOn instance.
        /// </summary>
        /// <param name="other">The ActionsRelatedOn to compare to.</param>
        /// <returns><c>true</c> if the current ActionsRelatedOn is equal to the supplied ActionsRelatedOn.</returns>
        public bool Equals(ActionsRelatedOn other) => RelatedOn.SetEqual(other.RelatedOn);

        /// <summary>
        /// Determines if the current ActionsRelatedOn instance is equal to the specified System.Object.
        /// </summary>
        /// <param name="obj">The System.Object to compare to.</param>
        /// <returns> <c>true</c> if the current ActionsRelatedOn is equal to the specified System.Object.</returns>
        public override bool Equals(object obj) => obj is ActionsRelatedOn a && Equals(a);

        /// <summary>
        /// Computes the hash code for the ActionsRelatedOn instance.
        /// </summary>
        /// <returns>The hash code for the ActionsRelatedOn instance.</returns>
        public override int GetHashCode() => RelatedOn.Aggregate(0, (hash, e) => hash ^ e.GetHashCode());

        internal IEnumerable<IVerbal> RelatedOn { get; }
        /// <summary>
        /// Returns true if the given the ActionsRelatedOn? is not null, otherwise; false.
        /// </summary>
        /// <param name="actions">The ActionsRelatedOn? structure to test.</param>
        /// <returns> <c>true</c> if the given the ActionsRelatedOn? is not null; otherwise, <c>false</c>.</returns>
        public static bool operator true(ActionsRelatedOn? actions) => actions != null;

        /// <summary>
        /// Returns true only if given the ActionsRelatedOn? is null;
        /// </summary>
        /// <param name="actions">The ActionsRelatedOn? structure to test.</param>
        /// <returns><c>true</c> if the given ActionsRelatedOn? is null; otherwise, <c>false</c>.</returns>
        public static bool operator false(ActionsRelatedOn? actions) => actions == null;
        /// <summary>
        /// Determines if two ActionsRelatedOn instances are equal.
        /// </summary>
        /// <param name="left">The first ActionsRelatedOn instance.</param>
        /// <param name="right">The second ActionsRelatedOn instance.</param>
        /// <returns> <c>true</c> if the ActionsRelatedOn instances are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ActionsRelatedOn left, ActionsRelatedOn right) => left.Equals(right);
        /// <summary>
        /// Determines if two ActionsRelatedOn instances are unequal.
        /// </summary>
        /// <param name="left">The first ActionsRelatedOn instance.</param>
        /// <param name="right">The second ActionsRelatedOn instance.</param>
        /// <returns> <c>true</c> if the ActionsRelatedOn instances are unequal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ActionsRelatedOn left, ActionsRelatedOn right) => !(left == right);
    }
}
