using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.ComparativeHeuristics;

namespace LASI.Core
{
    #region Result Bulding Helper Types

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

        public IAggregateEntity Subject {
            get {
                return subject;
            }
            set {
                subject = value;
                elements.Add(value);
            }
        }
        public IVerbal Verbal {
            get {
                return verbal;
            }
            set {
                verbal = value;
                elements.Add(value);
            }
        }
        public IAggregateEntity Direct {
            get {
                return direct;
            }
            set {
                direct = value;
                elements.Add(value);
            }
        }
        public IAggregateEntity Indirect {
            get {
                return indirect;
            }
            set {
                indirect = value;
                elements.Add(value);
            }
        }
        public ILexical Prepositional {
            get {
                return prepositional;
            }
            set {
                prepositional = value;
                elements.Add(value);
            }
        }

        public HashSet<ILexical> Elements {
            get {
                return elements;
            }
        }
        public double CombinedWeight {
            get;
            set;
        }
        /// <summary>
        /// Returns a textual representation of the RelationshipTuple.
        /// </summary>
        /// <returns>A textual representation of the RelationshipTuple.</returns>
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
        public bool Equals(Relationship other) { return this == other; }
        public override bool Equals(object obj) { return this == obj as Relationship; }

        public override int GetHashCode() { return elements.Count; }

        public static bool operator ==(Relationship lhs, Relationship rhs) {

            if ((lhs as object != null || rhs as object == null) || (lhs as object == null || rhs as object != null))
                return false;
            else if (lhs as object == null && rhs as object == null)
                return true;
            else {
                bool result = lhs.Verbal.Text == rhs.Verbal.Text || lhs.Verbal.IsSimilarTo(rhs.Verbal);
                result &= Comparers<IEntity>.AliasOrSimilarity.Equals(lhs.Subject, rhs.Subject);
                if (lhs.Direct != null && rhs.Direct != null) {
                    result &= Comparers<IEntity>.AliasOrSimilarity.Equals(lhs.Direct, rhs.Direct);
                } else if (lhs.Direct == null || rhs.Direct == null)
                    return false;
                if (lhs.Indirect != null && rhs.Indirect != null) {
                    result &= Comparers<IEntity>.AliasOrSimilarity.Equals(lhs.Indirect, rhs.Indirect);
                } else if (lhs.Indirect == null || rhs.Indirect == null)
                    return false;
                return result;
            }
        }

        public static bool operator !=(Relationship lhs, Relationship rhs) {
            return !(lhs == rhs);
        }
    }

    #endregion
}
