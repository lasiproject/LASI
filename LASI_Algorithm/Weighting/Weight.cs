using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Weighting
{
    public struct Weight
    {
        #region Constructors

        public Weight(double rawWeight, double multiplier)
            : this() {
        }

        #endregion


        #region Properties

        public double RawWeight {
            get;
            private set;
        }
        public double Multiplier {
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
            return A.RawWeight * A.Multiplier + B.RawWeight * B.Multiplier;
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
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        #endregion
    }
}
