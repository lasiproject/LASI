namespace LASI.Interop
{
    /// <summary>
    /// Represents a positive quantity in MegaBytes.
    /// </summary>
    public struct MB : System.IEquatable<MB>, System.IComparable<MB>
    {
        #region constructors
        /// <summary>
        /// Initializes a new instance of the MB structure with the specified value.
        /// </summary>
        /// <param name="quantity">The quantity of MegaBytes the MB will represent.</param>
        public MB(uint quantity) : this()
        {
            this.quantity = quantity;
        }
        #endregion constructors

        #region public methods
        /// <summary>
        /// Returns a value that indicates whether the specified object is equal to the current MB.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current MB; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) => obj is MB && this == (MB)obj;
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => quantity.GetHashCode();
        /// <summary>
        /// Returns a string representation of the MB.
        /// </summary>
        /// <returns>A string representation of the MB.</returns>
        public override string ToString() => quantity + "MB";
        /// <summary>
        /// Returns a value that indicates whether the specified object is equal to the current MB.
        /// </summary>
        /// <param name="other">The object to compare with.</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current MB; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(MB other) => this == other;
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
        public int CompareTo(MB other) => quantity.CompareTo(other.quantity);
        #endregion public methods

        #region properties
        /// <summary>
        /// Stores the quantity of MegaBytes the MB represents.
        /// </summary>
        private uint quantity;
        #endregion properties

        #region conversion operators
        /// <summary>
        /// Creates a new MB instance representing the value of the uint as a megabytes.
        /// </summary>
        /// <param name="quantity">The value to convert to an MB.</param>
        /// <returns>A new MB instance representing the value of the uint as a megabytes.</returns>
        public static explicit operator MB(uint quantity) => new MB(quantity);
        /// <summary>
        /// Converts the specified MB into a uint value corresponding to the quantity of MegaBytes
        /// it represents.
        /// </summary>
        /// <param name="MB">The value to convert to an MB.</param>
        /// <returns>A new MB instance representing the value of the uint as a megabytes.</returns>
        public static explicit operator uint (MB MB) => MB.quantity;
        #endregion conversion operators

        #region operator + overloads
        /// <summary>
        /// Returns an MB representing the sum of the quantities of the provided MBs.
        /// </summary>
        /// <param name="left">The first MB.</param>
        /// <param name="right">The second MB</param>
        /// <returns>An MB representing the sum of the quantities of the provided MBs.</returns>
        public static MB operator +(MB left, MB right) => new MB(left.quantity + right.quantity);
        /// <summary>
        /// Returns an MB representing the sum of the provided MB and the provided uint.
        /// </summary>
        /// <param name="left">The MB.</param>
        /// <param name="right">The uint.</param>
        /// <returns>An MB representing the sum of the provided MB and the provided uint.</returns>
        public static MB operator +(MB left, uint right) => new MB(left.quantity + right);
        /// <summary>
        /// Returns an MB representing the sum of the provided uint and the provided MB.
        /// </summary>
        /// <param name="left">The uint.</param>
        /// <param name="right">The MB.</param>
        /// <returns>An MB representing the sum of the provided uint and the provided MB.</returns>
        public static MB operator +(uint left, MB right) => new MB(left + right.quantity);
        #endregion operator + overloads

        #region operator - overloads
        /// <summary>
        /// Returns an MB representing the difference of the quantities of the provided MBs.
        /// </summary>
        /// <param name="left">The first MB.</param>
        /// <param name="right">The second MB</param>
        /// <returns>An MB representing the difference of the quantities of the provided MBs.</returns>
        public static MB operator -(MB left, MB right) => new MB(left.quantity - right.quantity);
        /// <summary>
        /// Returns an MB representing the difference of the provided MB and the provided uint.
        /// </summary>
        /// <param name="left">The MB.</param>
        /// <param name="right">The uint.</param>
        /// <returns>
        /// An MB representing the difference of the provided MB and the provided uint.
        /// </returns>
        public static MB operator -(MB left, uint right) => new MB(left.quantity - right);
        /// <summary>
        /// Returns an MB representing the difference of the provided uint and the provided MB.
        /// </summary>
        /// <param name="left">The uint.</param>
        /// <param name="right">The MB.</param>
        /// <returns>
        /// An MB representing the difference of the provided uint and the provided MB.
        /// </returns>
        public static MB operator -(uint left, MB right) => new MB(left - right.quantity);
        #endregion operator - overloads

        #region operator * overloads
        /// <summary>
        /// Returns an MB representing the product of the product of the provided MBs.
        /// </summary>
        /// <param name="left">The first MB.</param>
        /// <param name="right">The second MB</param>
        /// <returns>An MB representing the product of the quantities of the provided MBs.</returns>
        public static MB operator *(MB left, MB right) => new MB(left.quantity * right.quantity);
        /// <summary>
        /// Returns an MB representing the product of the provided MB and the provided uint.
        /// </summary>
        /// <param name="left">The MB.</param>
        /// <param name="right">The uint.</param>
        /// <returns>
        /// An MB representing the product of the provided MB and the provided uint.
        /// </returns>
        public static MB operator *(MB left, uint right) => new MB(left.quantity * right);
        /// <summary>
        /// Returns an MB representing the product of the provided uint and the provided MB.
        /// </summary>
        /// <param name="left">The uint.</param>
        /// <param name="right">The MB.</param>
        /// <returns>
        /// An MB representing the product of the provided uint and the provided MB.
        /// </returns>
        public static MB operator *(uint left, MB right) => new MB(left * right.quantity);
        #endregion operator * overloads

        #region operator / overloads
        /// <summary>
        /// Returns an MB representing the quotient of the quotient of the the provided MBs.
        /// </summary>
        /// <param name="left">The first MB.</param>
        /// <param name="right">The second MB</param>
        /// <returns>An MB representing the quotient of the quantities of the the provided MBs.</returns>
        public static MB operator /(MB left, MB right) => new MB(left.quantity / right.quantity);
        /// <summary>
        /// Returns an MB representing the quotient of the  provided MB and the provided uint.
        /// </summary>
        /// <param name="left">The MB.</param>
        /// <param name="right">The uint.</param>
        /// <returns>
        /// An MB representing the quotient of the provided MB and the provided uint.
        /// </returns>
        public static MB operator /(MB left, uint right) => new MB(left.quantity / right);
        /// <summary>
        /// Returns an MB representing the quotient of the provided uint and the provided MB.
        /// </summary>
        /// <param name="left">The uint.</param>
        /// <param name="right">The MB.</param>
        /// <returns>
        /// An MB representing the quotient of the provided uint and the provided MB.
        /// </returns>
        public static MB operator /(uint left, MB right) => new MB(left / right.quantity);

        #endregion operator / overloads

        #region relational operator overloads

        /// <summary>
        /// Returns a value that indicates whether a specified MB is less than another specified MB.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns><c>true</c> if the left MB is less than the right MB.</returns>
        public static bool operator <(MB left, MB right) => left.quantity < right.quantity;
        /// <summary>
        /// Returns a value that indicates whether a specified MB is greater than another specified MB.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns><c>true</c> if the left MB is greater than the right MB.</returns>
        public static bool operator >(MB left, MB right) => left.quantity > right.quantity;
        /// <summary>
        /// Returns a value that indicates whether a specified MB is less than a specified uint.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The uint to compare.</param>
        /// <returns><c>true</c> if the MB on the left is less than the unit on the right.</returns>
        public static bool operator <(MB left, uint right) => left.quantity < right;
        /// <summary>
        /// Returns a value that indicates whether a specified uint is greater than a specified MB.
        /// </summary>
        /// <param name="left">The uint to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns><c>true</c> if the uint on the left is greater than the MB on the right.</returns>
        public static bool operator >(uint left, MB right) => left > right.quantity;
        /// <summary>
        /// Returns a value that indicates whether a specified uint is less than a specified MB.
        /// </summary>
        /// <param name="left">The uint to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns><c>true</c> if the uint on the left is less than the MB on the right.</returns>
        public static bool operator <(uint left, MB right) => left < right.quantity;
        /// <summary>
        /// Returns a value that indicates whether a specified MB is greater than a specified uint.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The uint to compare.</param>
        /// <returns><c>true</c> if the MB on the left is greater than the unit on the right.</returns>
        public static bool operator >(MB left, uint right) => left.quantity > right;
        /// <summary>
        /// Returns a value that indicates whether a specified MB is less than or equal to another
        /// specified MB.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns>
        /// <c>true</c> if the MB on the left is less than or equal to the MB on the right.
        /// </returns>
        public static bool operator <=(MB left, MB right) => left.quantity <= right.quantity;
        /// <summary>
        /// Returns a value that indicates whether a specified MB is greater than or equal to
        /// another specified MB.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns>
        /// <c>true</c> if the MB on the left is greater than or equal to the MB on the right.
        /// </returns>
        public static bool operator >=(MB left, MB right) => left.quantity >= right.quantity;
        /// <summary>
        /// Returns a value that indicates whether a specified MB is less than or equal to a
        /// specified uint.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The uint to compare.</param>
        /// <returns>
        /// <c>true</c> if the MB on the left is less than or equal to the unit on the right.
        /// </returns>
        public static bool operator <=(MB left, uint right) => left.quantity <= right;

        /// <summary>
        /// Returns a value that indicates whether a specified uint is greater than or equal to a
        /// specified MB.
        /// </summary>
        /// <param name="left">The uint to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns>
        /// <c>true</c> if the uint on the left is greater than or equal to the MB on the right.
        /// </returns>
        public static bool operator >=(uint left, MB right) => left >= right.quantity;

        /// <summary>
        /// Returns a value that indicates whether a specified uint is less than or equal to a
        /// specified MB.
        /// </summary>
        /// <param name="left">The uint to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns>
        /// <c>true</c> if the uint on the left is less than or equal to the MB on the right.
        /// </returns>
        public static bool operator <=(uint left, MB right) => left <= right.quantity;
        /// <summary>
        /// Returns a value that indicates whether a specified MB is greater than or equal to a
        /// specified uint.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The uint to compare.</param>
        /// <returns>
        /// <c>true</c> if the MB on the left is greater than or equal to the uint on the right.
        /// </returns>
        public static bool operator >=(MB left, uint right) => left.quantity >= right;
        /// <summary>
        /// Returns a value that indicates whether two specified MB structures are equal.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns><c>true</c> if the specified Weight instances are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(MB left, MB right) => left.quantity == right.quantity;
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
        /// Returns a value that indicates whether the MB on the left is equal to the uint on the right.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The uint to compare.</param>
        /// <returns><c>true</c> if the MB on the left is equal to the uint on the right.</returns>
        public static bool operator ==(MB left, uint right) => left.quantity == right;
        /// <summary>
        /// Returns a value that indicates whether the MB on the left is not equal to the uint on
        /// the right.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The uint to compare.</param>
        /// <returns><c>true</c> if the MB on the left is not equal to the uint on the right.</returns>
        public static bool operator !=(MB left, uint right) => !(left == right);
        /// <summary>
        /// Returns a value that indicates whether the uint on the left is equal to the MB on the right.
        /// </summary>
        /// <param name="left">The uint to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns><c>true</c> if the uint on the left is equal to the MB on the right.</returns>
        public static bool operator ==(uint left, MB right) => left == right.quantity;
        /// <summary>
        /// Returns a value that indicates whether the uint on the left is not equal to the MB on
        /// the right.
        /// </summary>
        /// <param name="left">The uint to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns><c>true</c> if the uint on the left is not equal to the MB on the right.</returns>
        public static bool operator !=(uint left, MB right) => !(left == right);
        #endregion relational operator overloads
    }
}