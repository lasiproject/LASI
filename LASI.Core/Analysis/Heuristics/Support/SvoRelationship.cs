using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core.Heuristics;

namespace LASI.Core.Analysis.Heuristics.Support
{
    /// <summary>
    /// Sometimes an anonymous type simple will not do. So this little class is defined to
    /// store temporary query data from transposed tables. god it is late. I can't document properly.
    /// </summary>
    public sealed class SvoRelationship : IEquatable<SvoRelationship>
    {
        /// <summary>
        /// Initializes a new instance of the SvoRelationship class.
        /// </summary>
        /// <param name="subject"> The subject component of the relationship.</param>
        /// <param name="verbal">  The verbal component of the relationship.</param>
        /// <param name="direct">  The direct object component of the relationship.</param>
        /// <param name="indirect">The indirect object component of the relationship.</param>
        public SvoRelationship(IEntity subject, IVerbal verbal, IEntity direct, IEntity indirect)
        {
            Verbal = verbal;
            Subject = subject;
            Direct = direct;
            Indirect = indirect;
        }

        /// <summary>
        /// Determines if two SvoRelationship instances are considered unequal.
        /// </summary>
        /// <param name="left"> The first SvoRelationship instance.</param>
        /// <param name="right">The second SvoRelationship instance.</param>
        /// <returns><c>true</c> if the SvoRelationship instances are considered unequal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(SvoRelationship left, SvoRelationship right) => !(left == right);

        /// <summary>
        /// Determines if two SvoRelationship instances are considered equal.
        /// </summary>
        /// <param name="left"> The first SvoRelationship instance.</param>
        /// <param name="right">The second SvoRelationship instance.</param>
        /// <returns><c>true</c> if the SvoRelationship instances are considered equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(SvoRelationship left, SvoRelationship right)
        {
            if (left is null)
            {
                return right is null;
            }
            if (right is null)
            {
                return left is null;
            }
            return left.Verbal.IsSimilarTo(right.Verbal) && (ReferenceEquals(left.Subject, right.Subject) ||
                left.Subject.IsAliasFor(right.Subject) || left.Subject.IsSimilarTo(right.Subject)) &&
                (ReferenceEquals(left.Direct, right.Direct) || left.Direct.IsAliasFor(right.Direct) ||
                left.Direct.IsSimilarTo(right.Direct)) && (ReferenceEquals(left.Indirect, right.Indirect) ||
                left.Indirect.IsAliasFor(right.Indirect) || left.Indirect.IsSimilarTo(right.Indirect));
        }

        /// <summary>
        /// Determines if the current SvoRelationship instance is equal to another SvoRelationship instance.
        /// </summary>
        /// <param name="other">The SvoRelationship to compare to.</param>
        /// <returns><c>true</c> if the current Relationship is equal to the supplied SvoRelationship.</returns>
        public bool Equals(SvoRelationship other) => other != null && this == other;

        /// <summary>
        /// Determines if the current SvoRelationship instance is equal to the specified System.Object.
        /// </summary>
        /// <param name="obj">The System.Object to compare to.</param>
        /// <returns><c>true</c> if the current SvoRelationship is equal to the specified System.Object.</returns>
        public override bool Equals(object obj) => this == obj as SvoRelationship;

        /// <summary>
        /// Gets a hash code for the current Relationship instance.
        /// </summary>
        /// <returns>A hash code of the current Relationship instance.</returns>
        public override int GetHashCode() => Elements.Count();

        /// <summary>
        /// Returns a string representation of the Relationship.
        /// </summary>
        /// <returns>A string representation of the Relationship.</returns>
        public override string ToString() => Subject.Text + Verbal.Text + Direct?.Text + Indirect?.Text;

        /// <summary>
        /// Gets or sets the Direct Object component of the SvoRelationship.
        /// </summary>
        public IEntity Direct { get; }

        /// <summary>
        /// Gets all of the Lexical elements participating in SvoRelationship.
        /// </summary>
        public IEnumerable<ILexical> Elements
        {
            get
            {
                yield return Subject;
                yield return Verbal;
                yield return Direct;
                yield return Indirect;
                yield return Prepositional;
            }
        }

        /// <summary>
        /// Gets or sets the Indirect Object component of the SvoRelationship.
        /// </summary>
        public IEntity Indirect { get; }

        /// <summary>
        /// Gets or sets the Prepositional component of the SvoRelationship.
        /// </summary>
        public ILexical Prepositional => Verbal.ObjectOfThePreposition;

        /// <summary>
        /// Gets or sets the Subject component of the SvoRelationship.
        /// </summary>
        public IEntity Subject { get; }

        /// <summary>
        /// Gets or sets the Verbal component of the SvoRelationship.
        /// </summary>
        public IVerbal Verbal { get; }

        /// <summary>
        /// The weight of the Relationship.
        /// </summary>
        public double Weight => Elements.Sum(e => e?.Weight) ?? 0;

        public void Deconstruct(out IEntity subject, out IVerbal verbal, out IEntity direct, out IEntity indirect, out ILexical prepositional) =>
            (subject, verbal, direct, indirect, prepositional) = (Subject, Verbal, Direct, Indirect, Prepositional);
    }
}
