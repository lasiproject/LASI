using System;

namespace LASI.Interop.ProgressReporting
{
    /// <summary>
    /// Represents a positive quantity  MegaBytes.
    /// </summary>
    public struct MB : IEquatable<MB>, IComparable<MB>
    {
        /// <summary>
        /// Initializes a new instance of the MB structure with the specified value.
        /// </summary>
        /// <param name="value">The quantity of MegaBytes the MB will represent.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is negative.</exception>
        public MB(long value) => this.value = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(value)} may not be negative.");

        /// <summary>
        /// Compares the current MB with another MB.
        /// </summary>
        /// <param name="other">An MB structure to compare with the current instance.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and value. A value less
        /// than zero indicates that this instance is less than the other MB. A value of zero
        /// indicates that this instance is equal to the other MB. A value greater than zero
        /// indicates that this instance is greater than the other MB.
        /// </returns>
        public int CompareTo(MB other) => value.CompareTo(other.value);
        /// <summary>
        /// Returns a value that indicates whether the specified object is equal to the current MB.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current MB; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) => obj is MB mb && this == mb;
        /// <summary>
        /// Returns a value that indicates whether the specified object is equal to the current MB.
        /// </summary>
        /// <param name="other">The object to compare with.</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current MB; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(MB other) => this == other;
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => value.GetHashCode();
        /// <summary>
        /// Returns a string representation of the MB.
        /// </summary>
        /// <returns>A string representation of the MB.</returns>
        public override string ToString() => $"{value} {nameof(MB)}s";

        /// <summary>
        /// Converts the specified MB into a int value corresponding to the quantity of MegaBytes
        /// it represents.
        /// </summary>
        /// <param name="mb">The value to convert to an MB.</param>
        /// <returns>A new MB instance representing the value of the int as a megabytes.</returns>
        public static explicit operator long(MB mb) => mb.value;
        /// <summary>
        /// Creates a new MB instance representing the value of the int as a megabytes.
        /// </summary>
        /// <param name="quantity">The value to convert to an MB.</param>
        /// <returns>A new MB instance representing the value of the int as a megabytes.</returns>
        public static explicit operator MB(long quantity) => new MB(quantity);

        /// <summary>
        /// Stores the quantity of MegaBytes the MB represents.
        /// </summary>
        readonly long value;

        #region operator + overloads
        /// <summary>
        /// Returns an MB representing the sum of the quantities of the provided MBs.
        /// </summary>
        /// <param name="left">The first MB.</param>
        /// <param name="right">The second MB</param>
        /// <returns>An MB representing the sum of the quantities of the provided MBs.</returns>
        public static MB operator +(MB left, MB right) => new MB(left.value + right.value);

        /// <summary>
        /// Returns an MB representing the sum of the provided MB and the provided int.
        /// </summary>
        /// <param name="left">The MB.</param>
        /// <param name="right">The int.</param>
        /// <returns>An MB representing the sum of the provided MB and the provided int.</returns>
        public static MB operator +(MB left, int right) => new MB(left.value + right);

        /// <summary>
        /// Returns an MB representing the sum of the provided int and the provided MB.
        /// </summary>
        /// <param name="left">The int.</param>
        /// <param name="right">The MB.</param>
        /// <returns>An MB representing the sum of the provided int and the provided MB.</returns>
        public static MB operator +(int left, MB right) => new MB(left + right.value);
        #endregion operator + overloads

        #region operator - overloads
        /// <summary>
        /// Returns an MB representing the difference of the quantities of the provided MBs.
        /// </summary>
        /// <param name="left">The first MB.</param>
        /// <param name="right">The second MB</param>
        /// <returns>An MB representing the difference of the quantities of the provided MBs.</returns>
        public static MB operator -(MB left, MB right) => new MB(left.value - right.value);

        /// <summary>
        /// Returns an MB representing the difference of the provided MB and the provided int.
        /// </summary>
        /// <param name="left">The MB.</param>
        /// <param name="right">The int.</param>
        /// <returns>
        /// An MB representing the difference of the provided MB and the provided int.
        /// </returns>
        public static MB operator -(MB left, int right) => new MB(left.value - right);

        /// <summary>
        /// Returns an MB representing the difference of the provided int and the provided MB.
        /// </summary>
        /// <param name="left">The int.</param>
        /// <param name="right">The MB.</param>
        /// <returns>
        /// An MB representing the difference of the provided int and the provided MB.
        /// </returns>
        public static MB operator -(int left, MB right) => new MB(left - right.value);
        #endregion operator - overloads

        #region operator * overloads
        /// <summary>
        /// Returns an MB representing the product of the product of the provided MBs.
        /// </summary>
        /// <param name="left">The first MB.</param>
        /// <param name="right">The second MB</param>
        /// <returns>An MB representing the product of the quantities of the provided MBs.</returns>
        public static MB operator *(MB left, MB right) => new MB(left.value * right.value);

        /// <summary>
        /// Returns an MB representing the product of the provided MB and the provided int.
        /// </summary>
        /// <param name="left">The MB.</param>
        /// <param name="right">The int.</param>
        /// <returns>
        /// An MB representing the product of the provided MB and the provided int.
        /// </returns>
        public static MB operator *(MB left, int right) => new MB(left.value * right);

        /// <summary>
        /// Returns an MB representing the product of the provided int and the provided MB.
        /// </summary>
        /// <param name="left">The int.</param>
        /// <param name="right">The MB.</param>
        /// <returns>
        /// An MB representing the product of the provided int and the provided MB.
        /// </returns>
        public static MB operator *(int left, MB right) => new MB(left * right.value);
        #endregion operator * overloads

        #region operator / overloads
        /// <summary>
        /// Returns an MB representing the quotient of the quotient of the provided MBs.
        /// </summary>
        /// <param name="left">The first MB.</param>
        /// <param name="right">The second MB</param>
        /// <returns>An MB representing the quotient of the quantities of the provided MBs.</returns>
        public static MB operator /(MB left, MB right) => new MB(left.value / right.value);

        /// <summary>
        /// Returns an MB representing the quotient of the  provided MB and the provided int.
        /// </summary>
        /// <param name="left">The MB.</param>
        /// <param name="right">The int.</param>
        /// <returns>
        /// An MB representing the quotient of the provided MB and the provided int.
        /// </returns>
        public static MB operator /(MB left, int right) => new MB(left.value / right);

        /// <summary>
        /// Returns an MB representing the quotient of the provided int and the provided MB.
        /// </summary>
        /// <param name="left">The int.</param>
        /// <param name="right">The MB.</param>
        /// <returns>
        /// An MB representing the quotient of the provided int and the provided MB.
        /// </returns>
        public static MB operator /(int left, MB right) => new MB(left / right.value);
        #endregion operator / overloads

        #region relational operator overloads

        /// <summary>
        /// Returns a value that indicates whether a specified MB is less than another specified MB.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns><c>true</c> if the left MB is less than the right MB.</returns>
        public static bool operator <(MB left, MB right) => left.value < right.value;

        /// <summary>
        /// Returns a value that indicates whether a specified MB is greater than another specified MB.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns><c>true</c> if the left MB is greater than the right MB.</returns>
        public static bool operator >(MB left, MB right) => left.value > right.value;

        /// <summary>
        /// Returns a value that indicates whether a specified MB is less than a specified int.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The int to compare.</param>
        /// <returns><c>true</c> if the MB on the left is less than the unit on the right.</returns>
        public static bool operator <(MB left, int right) => left.value < right;

        /// <summary>
        /// Returns a value that indicates whether a specified int is greater than a specified MB.
        /// </summary>
        /// <param name="left">The int to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns><c>true</c> if the int on the left is greater than the MB on the right.</returns>
        public static bool operator >(int left, MB right) => left > right.value;

        /// <summary>
        /// Returns a value that indicates whether a specified int is less than a specified MB.
        /// </summary>
        /// <param name="left">The int to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns><c>true</c> if the int on the left is less than the MB on the right.</returns>
        public static bool operator <(int left, MB right) => left < right.value;

        /// <summary>
        /// Returns a value that indicates whether a specified MB is greater than a specified int.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The int to compare.</param>
        /// <returns><c>true</c> if the MB on the left is greater than the unit on the right.</returns>
        public static bool operator >(MB left, int right) => left.value > right;

        /// <summary>
        /// Returns a value that indicates whether a specified MB is less than or equal to another
        /// specified MB.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns>
        /// <c>true</c> if the MB on the left is less than or equal to the MB on the right.
        /// </returns>
        public static bool operator <=(MB left, MB right) => left.value <= right.value;

        /// <summary>
        /// Returns a value that indicates whether a specified MB is greater than or equal to
        /// another specified MB.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns>
        /// <c>true</c> if the MB on the left is greater than or equal to the MB on the right.
        /// </returns>
        public static bool operator >=(MB left, MB right) => left.value >= right.value;

        /// <summary>
        /// Returns a value that indicates whether a specified MB is less than or equal to a
        /// specified int.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The int to compare.</param>
        /// <returns>
        /// <c>true</c> if the MB on the left is less than or equal to the unit on the right.
        /// </returns>
        public static bool operator <=(MB left, int right) => left.value <= right;

        /// <summary>
        /// Returns a value that indicates whether a specified int is greater than or equal to a
        /// specified MB.
        /// </summary>
        /// <param name="left">The int to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns>
        /// <c>true</c> if the int on the left is greater than or equal to the MB on the right.
        /// </returns>
        public static bool operator >=(int left, MB right) => left >= right.value;

        /// <summary>
        /// Returns a value that indicates whether a specified int is less than or equal to a
        /// specified MB.
        /// </summary>
        /// <param name="left">The int to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns>
        /// <c>true</c> if the int on the left is less than or equal to the MB on the right.
        /// </returns>
        public static bool operator <=(int left, MB right) => left <= right.value;

        /// <summary>
        /// Returns a value that indicates whether a specified MB is greater than or equal to a
        /// specified int.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The int to compare.</param>
        /// <returns>
        /// <c>true</c> if the MB on the left is greater than or equal to the int on the right.
        /// </returns>
        public static bool operator >=(MB left, int right) => left.value >= right;

        /// <summary>
        /// Returns a value that indicates whether two specified MB structures are equal.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns><c>true</c> if the specified Weight instances are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(MB left, MB right) => left.value == right.value;

        /// <summary>
        /// Returns a value that indicates whether two specified MB structures are not equal.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns>
        /// <c>true</c> if the specified Weight instances are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(MB left, MB right) => !(left == right);

        /// <summary>
        /// Returns a value that indicates whether the MB on the left is equal to the int on the right.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The int to compare.</param>
        /// <returns><c>true</c> if the MB on the left is equal to the int on the right.</returns>
        public static bool operator ==(MB left, int right) => left.value == right;

        /// <summary>
        /// Returns a value that indicates whether the MB on the left is not equal to the int on
        /// the right.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The int to compare.</param>
        /// <returns><c>true</c> if the MB on the left is not equal to the int on the right.</returns>
        public static bool operator !=(MB left, int right) => !(left == right);

        /// <summary>
        /// Returns a value that indicates whether the int on the left is equal to the MB on the right.
        /// </summary>
        /// <param name="left">The int to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns><c>true</c> if the int on the left is equal to the MB on the right.</returns>
        public static bool operator ==(int left, MB right) => left == right.value;

        /// <summary>
        /// Returns a value that indicates whether the int on the left is not equal to the MB on
        /// the right.
        /// </summary>
        /// <param name="left">The int to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns><c>true</c> if the int on the left is not equal to the MB on the right.</returns>
        public static bool operator !=(int left, MB right) => !(left == right);
        #endregion relational operator overloads
    }
}