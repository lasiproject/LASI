using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.LexicalLookup
{
    public static partial class Lookup
    {
        /// <summary>
        /// Encapsulates multiple pieces of information gathered during a similarity comparison into a light weight type.
        /// The structure cannot be created from outside of the Lookup class and is used to convey internal results.
        /// No special syntax is or code changes are required to manipulate this type. It will implicitely convert to bool
        /// So all code with forms such as: 
        /// <code>if ( a.IsSimilarTo(b) ) { ... }</code>
        /// need not and should not be changed. 
        /// However, If the numeric ratio used to determine similarity is needed and applicable, the type will implcitely convert
        /// to a double. This removes the need for public code such as: 
        /// <code>if ( Lookup.GetSimiliarityRatio(a, b) > 0.7 ) { ... }</code>
        /// Instead one may simple write the same logic as: 
        /// <code>if ( a.IsSimilarTo(b) > 0.7 ) { ... }</code>
        /// </summary>
        public struct SimilarityResult : IEquatable<SimilarityResult>, IComparable<SimilarityResult>
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the SimilarityResult structure from the provided values.
            /// </summary>
            /// <param name="similar">Indicates the result the true of false result of an IsSimilarTo test.</param>
            /// <param name="similarityRatio">Represents the similarity ratio between the tested elements, if applicable.</param>
            internal SimilarityResult(bool similar, double similarityRatio)
                : this() {
                booleanResult = similar;
                rationalResult = similarityRatio;
            }
            /// <summary>
            /// intializes a new instance of the SimilarityResult structure from the provided bool value.
            /// </summary>
            /// <param name="similar">Indicates the result the true of false result of an IsSimilarTo test.</param>
            /// <remarks>Use this constructor when the ratio itself is not specified or not provided.
            /// In such cases, the RatioResult property will be automatically set to 1 or 0 based on the truthfullness of the provided similar argument.
            /// </remarks>
            internal SimilarityResult(bool similar) : this(similar, similar ? 1 : 0) { }

            #endregion

            #region Methods
            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            ///  </summary>
            /// <param name="other">An object to compare with this object.</param>
            /// <returns>true if the current object is equal to the other parameter, false otherwise.</returns>
            public bool Equals(SimilarityResult other) {
                return this == other;
            }
            /// <summary>
            /// Returns a value that indicates whether the specified object is equal to the current SimResult.
            /// </summary>
            /// <param name="obj">The object to compare with.</param> 
            /// <returns>True if the specified object is equal to the current SimResult, false otherwise.</returns> 
            public override bool Equals(object obj) {
                return obj != null && obj is SimilarityResult && this == (SimilarityResult)obj;
            }
            /// <summary>
            /// Compares the current object with another object of the same type.
            /// </summary>
            /// <param name="other">An object to compare with this object.</param>
            /// <returns>
            /// A value that indicates the relative order of the objects being compared.
            /// The return value has the following meanings: Value Meaning Less than zero
            /// This object is less than the other parameter.Zero This object is equal to
            /// other. Greater than zero This object is greater than other.
            /// </returns>
            public int CompareTo(SimilarityResult other) {
                return this.rationalResult.CompareTo(other.rationalResult);
            }
            /// <summary>
            /// Returns the hash code for this instance.
            /// </summary>
            /// <returns>A 32-bit signed integer hash code.</returns>
            public override int GetHashCode() {
                return rationalResult.GetHashCode() ^ booleanResult.GetHashCode();
            }
            #endregion

            #region Fields

            private bool booleanResult;
            private double rationalResult;

            #endregion

            #region Operators

            #region Implcit Conversion Operators
            // These allow the type to implcitely convert to the desired result type for the condition. 
            // Thus, refactoring the IsSimilarTo implementations preserves and enhances existing code
            // without the need to rewrite any conditionals or call expensive methods multiple times to get numeric vs boolean results

            /// <summary>
            /// Converts the SimResult into its boolean representation. The resulting boolean has the same value as the conversion target's booleanResult Property.
            /// </summary>
            /// <param name="sr">The SimResult to convert.</param>
            /// <returns>A boolean with the same value as the conversion target's booleanResult Property.</returns>
            public static implicit operator bool(SimilarityResult sr) { return sr.booleanResult; }
            /// <summary>
            /// Converts the SimResult into its double representation. The resulting boolean has the same value as the conversion target's RatioResult Property.
            /// </summary>
            /// <param name="sr">The SimResult to convert.</param>
            /// <returns>A double with the same value as the conversion target's RatioResult Property.</returns>
            public static implicit operator double(SimilarityResult sr) { return sr.rationalResult; }

            #endregion

            #region Comparison Operators
            /// <summary>
            /// Returns a value that indicates whether the SimResult on the left is equal to the SimResult on the right.
            /// Although it seems unlikely that two instances of SimResult will be compared directly for equality. 
            /// The == and != operators or defined to ensure type coersion does not result from the implicit conversions which make the class convenient.
            /// Equality is defined strictly such that both RatioResult properties must match exactly in addition to both booleanResult properties.
            /// Keep this in mind if, for some reason, it is ever necessary to write code such as: 
            /// <code>if ( a.IsSimilarTo(b) == b.IsSimilarTo(a) ) { ... } </code>
            /// as the lexical lookup class itself currently makes no guarantees about reflexive equality over phrase-wise comparisons.
            /// </summary>
            /// <param name="left">The SimRult on the left hand side.</param>
            /// <param name="right">The SimRult on the right hand side.</param>
            /// <returns>True if the SimResult on the left is equal to the SimResult on the right.</returns>
            public static bool operator ==(SimilarityResult left, SimilarityResult right) {
                return left.rationalResult == right.rationalResult && left.booleanResult == right.booleanResult;
            }
            /// <summary>
            /// Returns a value that indicates whether the SimResult on the left is not equal to the SimResult on the right.
            /// Although it seems unlikely that two instances of SimResult will be compared directly for equality. 
            /// The == and != operators or defined to ensure type coersion does not result from the implicit conversions which make the class convenient.
            /// Equality is defined strictly such that both RatioResult properties must match exactly in addition to both booleanResult properties.
            /// Keep this in mind if, for some reason, it is ever necessary to write code such as:
            /// <code>if ( a.IsSimilarTo(b) == b.IsSimilarTo(a) ) { ... }</code> 
            /// as the lexical lookup class itself currently makes no guarantees about reflexive equality over phrase-wise comparisons.
            /// </summary>
            /// <param name="left">The SimRult on the left hand side.</param>
            /// <param name="right">The SimRult on the right hand side.</param>
            /// <returns>False if the SimResult on the left is equal to the SimResult on the right.</returns>
            public static bool operator !=(SimilarityResult left, SimilarityResult right) {
                return !(left == right);
            }
            #endregion

            #endregion

            #region Static Properties
            internal static readonly SimilarityResult Similar = new SimilarityResult(true, 1);
            internal static readonly SimilarityResult Dissimilar = new SimilarityResult(false, 0);
            #endregion
        }
    }
}
