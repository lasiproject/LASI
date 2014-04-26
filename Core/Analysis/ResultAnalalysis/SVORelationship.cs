using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Heuristics;

namespace LASI.Core
{

    /// <summary>
    /// Sometimes an anonymous type simple will not do. So this little class is defined to 
    /// store temporary query data from transposed tables. god it is late. I can't document properly.
    /// </summary>
    public class SvoRelationship : IEquatable<SvoRelationship>
    {
        private IVerbal verbal;
        private IAggregateEntity subject;
        private IAggregateEntity direct;
        private IAggregateEntity indirect;
        private ILexical prepositional;
        private HashSet<ILexical> elements = new HashSet<ILexical>();

        /// <summary>
        /// Gets or sets the Subject component of the SvoRelationship.
        /// </summary>
        public IAggregateEntity Subject {
            get {
                return subject;
            }
            set {
                subject = value;
                elements.Add(value);
            }
        }

        /// <summary>
        /// Gets or sets the Verbal component of the SvoRelationship.
        /// </summary>
        public IVerbal Verbal {
            get {
                return verbal;
            }
            set {
                verbal = value;
                elements.Add(value);
            }
        }

        /// <summary>
        /// Gets or sets the Direct Object component of the SvoRelationship.
        /// </summary>
        public IAggregateEntity Direct {
            get {
                return direct;
            }
            set {
                direct = value;
                elements.Add(value);
            }
        }
        /// <summary>
        /// Gets or sets the Indirect Object component of the SvoRelationship.
        /// </summary>
        public IAggregateEntity Indirect {
            get {
                return indirect;
            }
            set {
                indirect = value;
                elements.Add(value);
            }
        }
        /// <summary>
        /// Gets or sets the Prepositional component of the SvoRelationship.
        /// </summary>
        public ILexical Prepositional {
            get {
                return prepositional;
            }
            set {
                prepositional = value;
                elements.Add(value);
            }
        }
        /// <summary>
        /// Gets all of the Lexical elements participating in SvoRelationship.
        /// </summary>
        public IEnumerable<ILexical> Elements {
            get {
                return elements;
            }
        }
        /// <summary>
        /// Gets the weight of the Relationship.
        /// </summary>
        public double CombinedWeight {
            get;
            set;
        }
        /// <summary>
        /// Returns a string representation of the Relationship.
        /// </summary>
        /// <returns>A string representation of the Relationship.</returns>
        public override string ToString() {
            var result = Subject.Text + Verbal.Text;
            if (Direct != null) {
                result += Direct.Text;
            }
            if (Indirect != null) {
                result += Indirect.Text;
            }
            return result;
        }
        /// <summary>   
        /// Determines if the current SvoRelationship instance is equal to another SvoRelationship instance.
        /// </summary>
        /// <param name="other">The SvoRelationship to compare to.</param>
        /// <returns>True if the current Relationship is equal to the supplied SvoRelationship.</returns>
        public bool Equals(SvoRelationship other) { return this == other; }
        /// <summary>
        /// Determines if the current SvoRelationship instance is equal to the specified System.Object.
        /// </summary>
        /// <param name="obj">The System.Object to compare to.</param>
        /// <returns>True if the current SvoRelationship is equal to the specified System.Object.</returns>
        public override bool Equals(object obj) { return this == obj as SvoRelationship; }
        /// <summary>
        /// Gets a hash code for the current Relationship instance.
        /// </summary>
        /// <returns>A hash code of the current Relationship instance.</returns>
        public override int GetHashCode() { return elements.Count; }
        /// <summary>
        /// Determines if two SvoRelationship instances are considered equal.
        /// </summary>
        /// <param name="left">The first SvoRelationship instance.</param>
        /// <param name="right">The second SvoRelationship instance.</param>
        /// <returns>True if the SvoRelationship instances are considered equal; otherwise, false.</returns>
        public static bool operator ==(SvoRelationship left, SvoRelationship right) {

            if ((object)left == null || (object)right == null) {
                return ReferenceEquals(left, right);
            }
            return left.Verbal.IsSimilarTo(right.Verbal) &&
                   (left.Subject.IsAliasFor(right.Subject) || left.Subject.IsSimilarTo(right.Subject)) &&
                   (left.Direct.IsAliasFor(right.Direct) || left.Direct.IsSimilarTo(right.Direct)) &&
                   (left.Indirect.IsAliasFor(right.Indirect) || left.Indirect.IsSimilarTo(right.Indirect));

        }
        /// <summary>
        /// Determines if two SvoRelationship instances are considered unequal.
        /// </summary>
        /// <param name="left">The first SvoRelationship instance.</param>
        /// <param name="right">The second SvoRelationship instance.</param>
        /// <returns>True if the SvoRelationship instances are considered unequal; otherwise, false.</returns>
        public static bool operator !=(SvoRelationship left, SvoRelationship right) {
            return !(left == right);
        }
    }

}
