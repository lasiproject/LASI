using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.ComparativeHeuristics;

namespace LASI.Core
{

    /// <summary>
    /// Sometimes an anonymous type simple will not do. So this little class is defined to 
    /// store temporary query data from transposed tables. god it is late. I can't document properly.
    /// </summary>
    public class Relationship : IEquatable<Relationship>
    {
        private IVerbal verbal;
        private IAggregateEntity subject;
        private IAggregateEntity direct;
        private IAggregateEntity indirect;
        private ILexical prepositional;
        private HashSet<ILexical> elements = new HashSet<ILexical>();

        /// <summary>
        /// Gets or sets the Subject component of the Relationship.
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
        /// Gets or sets the Verbal component of the Relationship.
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
        /// Gets or sets the Direct Object component of the Relationship.
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
        /// Gets or sets the Indirect Object component of the Relationship.
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
        /// Gets or sets the Prepositional component of the Relationship.
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
        /// Gets all of the Lexical elements participating in Relationship.
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
        /// Determines if the current Relationship instance is equal to another Relationship instance.
        /// </summary>
        /// <param name="other">The Relationship to compare to.</param>
        /// <returns>True if the current Relationship is equal to the supplied Relationship.</returns>
        public bool Equals(Relationship other) { return this == other; }
        /// <summary>
        /// Determines if the current Relationship instance is equal to the specified System.Object.
        /// </summary>
        /// <param name="obj">The System.Object to compare to.</param>
        /// <returns>True if the current Relationship is equal to the specified System.Object.</returns>
        public override bool Equals(object obj) { return this == obj as Relationship; }
        /// <summary>
        /// Gets a hash code for the current Relationship instance.
        /// </summary>
        /// <returns>A hash code of the current Relationship instance.</returns>
        public override int GetHashCode() { return elements.Count; }
        /// <summary>
        /// Determines if two Relationship instances are considered equal.
        /// </summary>
        /// <param name="left">The first Relationship instance.</param>
        /// <param name="right">The second Relationship instance.</param>
        /// <returns>True if the Relationship instances are considered equal, false otherwise.</returns>
        public static bool operator ==(Relationship left, Relationship right) {

            if ((left as object != null || right as object == null) || (left as object == null || right as object != null))
                return false;
            else if (left as object == null && right as object == null)
                return true;
            else {
                bool result = left.Verbal.Text == right.Verbal.Text || left.Verbal.IsSimilarTo(right.Verbal);
                result &= left.Subject.IsAliasFor(right.Subject) || left.Subject.IsSimilarTo(right.Subject);
                if (left.Direct != null && right.Direct != null) {
                    result &= left.Direct.IsAliasFor(right.Direct) || left.Direct.IsSimilarTo(right.Direct);
                } else if (left.Direct == null || right.Direct == null)
                    return false;
                if (left.Indirect != null && right.Indirect != null) {
                    result &= left.Indirect.IsAliasFor(right.Indirect) || left.Indirect.IsSimilarTo(right.Indirect);
                } else if (left.Indirect == null || right.Indirect == null)
                    return false;
                return result;
            }
        }
        /// <summary>
        /// Determines if two Relationship instances are considered unequal.
        /// </summary>
        /// <param name="left">The first Relationship instance.</param>
        /// <param name="right">The second Relationship instance.</param>
        /// <returns>True if the Relationship instances are considered unequal, false otherwise.</returns>
        public static bool operator !=(Relationship left, Relationship right) {
            return !(left == right);
        }
    }

}
