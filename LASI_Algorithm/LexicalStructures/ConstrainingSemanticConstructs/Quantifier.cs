using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a quantifier verb which specifies the quanitity of an IQuantifiable construct.
    /// </summary>
    public class Quantifier : Word, IQuantifier
    {
        /// <summary>
        /// Represents a quantifier which specifies the value, count, or degree, of some IQuantifiabe such as a GenericSingularNoun
        /// </summary>
        /// <param name="text">the literal text content of the quantifer.</param> 
        public Quantifier(string text)
            : base(text) {
        }

        #region Methods
        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        #endregion


        #region Properties
        /// <summary>
        /// Gets or sets the 
        /// </summary>
        public virtual IQuantifiable Quantifies {
            get;
            set;
        }

        #endregion



        #region Operators

        public static bool operator ==(Quantifier lhs, Quantifier rhs) {
            return lhs == null ? rhs == null ? true : false : lhs.Text == rhs.Text;
        }
        public static bool operator !=(Quantifier lhs, Quantifier rhs) {

            return !(lhs == rhs);
        }

        #endregion


    }
}
