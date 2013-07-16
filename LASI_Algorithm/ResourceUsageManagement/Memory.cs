using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public static class Memory
    {
        public static void SetFromResourceUsageMode(ResourceMode mode) {
            MaxLookupCacheSize = mode == ResourceMode.High ? (MB)4096 : mode == ResourceMode.Normal ? (MB)3072 : (MB)1536;
        }
        public static void SetFromMB(MB maxMemUsage) { MaxLookupCacheSize = maxMemUsage; }
        public static MB MaxLookupCacheSize { get; private set; }
        public static MB MinLookupCacheSize { get; private set; }
        static Memory() {
            MaxLookupCacheSize = (MB)4096;
            MinLookupCacheSize = (MB)512;
        }
    }

    public struct MB
    {
        #region constructors
        public MB(uint quantity) : this() { Quantity = quantity; }
        #endregion

        #region public methods
        public override bool Equals(object obj) { return this == (MB)obj; }
        public override int GetHashCode() { return Quantity.GetHashCode(); }
        public override string ToString() { return string.Format("{0}MB", Quantity); }
        #endregion

        #region properties
        public uint Quantity { get; private set; }
        #endregion

        #region conversion operators
        public static explicit operator MB(uint quantity) { return new MB(quantity); }
        public static explicit operator uint(MB megabytes) { return megabytes.Quantity; }
        #endregion

        #region operator + overloads
        public static MB operator +(MB left, MB right) { var temp = new MB(left.Quantity + right.Quantity); return temp <= Memory.MaxLookupCacheSize ? temp : Memory.MaxLookupCacheSize; }
        public static MB operator +(MB left, uint right) { var temp = new MB(left.Quantity + right); return temp <= Memory.MaxLookupCacheSize ? temp : Memory.MaxLookupCacheSize; }
        public static MB operator +(uint left, MB right) { var temp = new MB(left + right.Quantity); return temp <= Memory.MaxLookupCacheSize ? temp : Memory.MaxLookupCacheSize; }
        #endregion

        #region operator - overloads
        public static MB operator -(MB left, MB right) { var temp = new MB(left.Quantity - right.Quantity); return temp >= Memory.MinLookupCacheSize ? temp : Memory.MinLookupCacheSize; }
        public static MB operator -(MB left, uint right) { var temp = new MB(left.Quantity - right); return temp >= Memory.MinLookupCacheSize ? temp : Memory.MinLookupCacheSize; }
        public static MB operator -(uint left, MB right) { var temp = new MB(left - right.Quantity); return temp >= Memory.MinLookupCacheSize ? temp : Memory.MinLookupCacheSize; }
        #endregion

        #region operator * overloads
        public static MB operator *(MB left, MB right) { var temp = new MB(left.Quantity * right.Quantity); return temp <= Memory.MaxLookupCacheSize ? temp : Memory.MaxLookupCacheSize; }
        public static MB operator *(MB left, uint right) { var temp = new MB(left.Quantity * right); return temp <= Memory.MaxLookupCacheSize ? temp : Memory.MaxLookupCacheSize; }
        public static MB operator *(uint left, MB right) { var temp = new MB(left * right.Quantity); return temp <= Memory.MaxLookupCacheSize ? temp : Memory.MaxLookupCacheSize; }
        #endregion

        #region operator / overloads
        public static MB operator /(MB left, MB right) { var temp = new MB(left.Quantity / right.Quantity); return temp >= Memory.MinLookupCacheSize ? temp : Memory.MinLookupCacheSize; }
        public static MB operator /(MB left, uint right) { var temp = new MB(left.Quantity / right); return temp >= Memory.MinLookupCacheSize ? temp : Memory.MinLookupCacheSize; }
        public static MB operator /(uint left, MB right) { var temp = new MB(left / right.Quantity); return temp >= Memory.MinLookupCacheSize ? temp : Memory.MinLookupCacheSize; }

        #endregion

        #region relational operator overloads

        public static bool operator <(MB left, MB right) { return left.Quantity < right.Quantity; }
        public static bool operator >(MB left, MB right) { return left.Quantity > right.Quantity; }
        public static bool operator <(MB left, uint right) { return left.Quantity < right; }
        public static bool operator >(uint left, MB right) { return left > right.Quantity; }
        public static bool operator <(uint left, MB right) { return left < right.Quantity; }
        public static bool operator >(MB left, uint right) { return left.Quantity > right; }

        public static bool operator <=(MB left, MB right) { return left.Quantity <= right.Quantity; }
        public static bool operator >=(MB left, MB right) { return left.Quantity >= right.Quantity; }
        public static bool operator <=(MB left, uint right) { return left.Quantity <= right; }
        public static bool operator >=(uint left, MB right) { return left >= right.Quantity; }
        public static bool operator <=(uint left, MB right) { return left <= right.Quantity; }
        public static bool operator >=(MB left, uint right) { return left.Quantity >= right; }

        public static bool operator ==(MB left, MB right) { return left.Quantity == right.Quantity; }
        public static bool operator !=(MB left, MB right) { return !(left == right); }
        public static bool operator ==(MB left, uint right) { return left.Quantity == right; }
        public static bool operator !=(MB left, uint right) { return !(left == right); }
        public static bool operator ==(uint left, MB right) { return left == right.Quantity; }
        public static bool operator !=(uint left, MB right) { return !(left == right); }
        #endregion
    }
}
