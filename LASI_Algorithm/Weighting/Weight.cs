using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Weighting
{
    public class Weight
    {
        #region Constructors

        public Weight(double rawWeight, double multiplier) {
        }

        #endregion


        #region Properties

        public double RawWeight {
            get;
            protected set;
        }
        public double Multiplier {
            get;
            protected set;
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
