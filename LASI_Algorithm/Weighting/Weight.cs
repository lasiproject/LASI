using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Weighting
{
    public struct Weight
    {
        public double RawWeight {
            get;
            set;
        }
        public double Multiplier {
            get;
            set;
        }


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

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
