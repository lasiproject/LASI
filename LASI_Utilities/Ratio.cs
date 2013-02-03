using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{
    struct Fraction
    {
        public int A {
            get;
            private set;
        }
        public int B {
            get;
            private set;
        }
        public static bool operator ==(Fraction lhs, Fraction rhs) {
            return lhs.A == rhs.A && lhs.B == rhs.B;
        }
        public static bool operator !=(Fraction lhs, Fraction rhs) {
            return !(lhs == rhs);
        }
    }
}
