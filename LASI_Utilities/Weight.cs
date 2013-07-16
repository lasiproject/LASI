using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{
    public struct Weight
    {
        #region Constructors

        public Weight(double rawWeight, double multiplier)
            : this() {
            RawWeight = rawWeight;
            Multiplier = multiplier;
            ScaledWeight = multiplier * rawWeight;
        }

        #endregion


        #region Properties
        /// <summary>
        /// Gets the Raw Weight.
        /// </summary>
        public double RawWeight {
            get;
            private set;
        }
        /// <summary>
        /// Gets the Multiplier.
        /// </summary>
        public double Multiplier {
            get;
            private set;
        }
        /// <summary>
        /// Gets the scaled weight computed as the product of the Multiplier and the RawWeight of the Weight.
        /// </summary>
        public double ScaledWeight {
            get;
            private set;
        }

        #endregion

        #region Operators

        public static bool operator ==(Weight A, Weight B) {
            return A.RawWeight * A.Multiplier == B.RawWeight * B.Multiplier;
        }
        public static bool operator !=(Weight A, Weight B) {
            return !(A == B);
        }
        public static bool operator >(Weight A, Weight B) {
            return A.RawWeight * A.Multiplier > B.RawWeight * B.Multiplier;
        }
        public static bool operator <(Weight A, Weight B) {
            return A.RawWeight * A.Multiplier < B.RawWeight * B.Multiplier;
        }

        public static double operator +(Weight A, Weight B) {
            return A.RawWeight * A.Multiplier + B.RawWeight * B.Multiplier;
        }
        public static double operator -(Weight A, Weight B) {
            return A.RawWeight * A.Multiplier - B.RawWeight * B.Multiplier;
        }
        public static double operator *(Weight A, Weight B) {
            return A.RawWeight * A.Multiplier * B.RawWeight * B.Multiplier;
        }
        public static double operator /(Weight A, Weight B) {
            return (A.RawWeight * A.Multiplier) / (B.RawWeight * B.Multiplier);
        }

        #endregion

        #region Methods

        public override bool Equals(object obj) {
            return this == ( Weight )obj;
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        #endregion
    }
}
