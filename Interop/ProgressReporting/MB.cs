namespace LASI.Interop
{
    /// <summary>
    /// Represents a positive quantity in MegaBytes.
    /// </summary>
    public struct MB
    {
        #region constructors
        /// <summary>
        /// Initializes a new instance of the MB structure with the specified value.
        /// </summary>
        /// <param name="quantity">The quantity of MegaBytes the MB will represent.</param>
        public MB(uint quantity) : this() { Quantity = quantity; }
        #endregion

        #region public methods
        /// <summary>
        /// Returns a value that indicates whether the specified object is equal to the current MB.
        /// </summary>
        /// <param name="obj">The object to compare with.</param> 
        /// <returns>True if the specified object is equal to the current MB; otherwise, false.</returns> 
        public override bool Equals(object obj) { return obj is MB && this == (MB)obj; }
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() { return Quantity.GetHashCode(); }
        /// <summary>
        /// Returns a string representation of the MB.
        /// </summary>
        /// <returns>A string representation of the MB.</returns>
        public override string ToString() { return string.Format("{0}MB", Quantity); }
        #endregion

        #region properties
        /// <summary>
        /// Stores the quantity of MegaBytes the MB represents.
        /// </summary>
        private uint Quantity;
        #endregion

        #region conversion operators
        /// <summary>
        /// Creates a new MB instance representing the value of the uint as a megabytes.
        /// </summary>
        /// <param name="quantity">The value to convert to an MB.</param>
        /// <returns>A new MB instance representing the value of the uint as a megabytes.</returns>
        public static explicit operator MB(uint quantity) { return new MB(quantity); }
        /// <summary>
        /// Converts the specified MB into a uint value correpsonding to the quantity of MegaBytes it represents.
        /// </summary>
        /// <param name="MB">The value to convert to an MB.</param>
        /// <returns>A new MB instance representing the value of the uint as a megabytes.</returns>
        public static explicit operator uint(MB MB) { return MB.Quantity; }
        #endregion

        #region operator + overloads
        /// <summary>
        /// Returns an MB representing the sum of the quantities of the provided MBs.
        /// </summary>
        /// <param name="left">The first MB.</param>
        /// <param name="right">The second MB </param>
        /// <returns>An MB representing the sum of the quantities of the provided MBs.</returns>
        public static MB operator +(MB left, MB right) { return new MB(left.Quantity + right.Quantity); }
        /// <summary>
        /// Returns an MB representing the sum of the the provided MB and the provided uint.
        /// </summary>
        /// <param name="left">The MB.</param>
        /// <param name="right">The uint.</param>
        /// <returns>An MB representing the sum of the the provided MB and the provided uint.</returns>
        public static MB operator +(MB left, uint right) { return new MB(left.Quantity + right); }
        /// <summary>
        /// Returns an MB representing the sum of the the provided uint and the provided MB.
        /// </summary>
        /// <param name="left">The uint.</param>
        /// <param name="right">The MB.</param>
        /// <returns>An MB representing the sum of the the provided uint and the provided MB.</returns>
        public static MB operator +(uint left, MB right) { return new MB(left + right.Quantity); }
        #endregion

        #region operator - overloads
        /// <summary>
        /// Returns an MB representing the difference of the quantities of the provided MBs.
        /// </summary>
        /// <param name="left">The first MB.</param>
        /// <param name="right">The second MB </param>
        /// <returns>An MB representing the difference of the quantities of the provided MBs.</returns>
        public static MB operator -(MB left, MB right) { return new MB(left.Quantity - right.Quantity); }
        /// <summary>
        /// Returns an MB representing the difference of the the provided MB and the provided uint.
        /// </summary>
        /// <param name="left">The MB.</param>
        /// <param name="right">The uint.</param>
        /// <returns>An MB representing the difference of the the provided MB and the provided uint.</returns>
        public static MB operator -(MB left, uint right) { return new MB(left.Quantity - right); }
        /// <summary>
        /// Returns an MB representing the difference of the the provided uint and the provided MB.
        /// </summary>
        /// <param name="left">The uint.</param>
        /// <param name="right">The MB.</param>
        /// <returns>An MB representing the difference of the the provided uint and the provided MB.</returns>
        public static MB operator -(uint left, MB right) { return new MB(left - right.Quantity); }
        #endregion

        #region operator * overloads
        /// <summary>
        /// Returns an MB representing the product of the product of the the provided MBs.
        /// </summary>
        /// <param name="left">The first MB.</param>
        /// <param name="right">The second MB </param>
        /// <returns>An MB representing the product of the quantities of the the provided MBs.</returns>
        public static MB operator *(MB left, MB right) { return new MB(left.Quantity * right.Quantity); }
        /// <summary>
        /// Returns an MB representing the product of the the provided MB and the provided uint.
        /// </summary>
        /// <param name="left">The MB.</param>
        /// <param name="right">The uint.</param>
        /// <returns>An MB representing the product of the the provided MB and the provided uint.</returns>                                                                                    
        public static MB operator *(MB left, uint right) { return new MB(left.Quantity * right); }
        /// <summary>
        /// Returns an MB representing the product of the the provided uint and the provided MB.
        /// </summary>
        /// <param name="left">The uint.</param>
        /// <param name="right">The MB.</param>
        /// <returns>An MB representing the product of the the provided uint and the provided MB.</returns>
        public static MB operator *(uint left, MB right) { return new MB(left * right.Quantity); }
        #endregion

        #region operator / overloads
        /// <summary>
        /// Returns an MB representing the quotient of the quotient of the the provided MBs.
        /// </summary>
        /// <param name="left">The first MB.</param>
        /// <param name="right">The second MB </param>
        /// <returns>An MB representing the quotient of the quantities of the the provided MBs.</returns>
        public static MB operator /(MB left, MB right) { return new MB(left.Quantity / right.Quantity); }
        /// <summary>
        /// Returns an MB representing the quotient of the the provided MB and the provided uint.
        /// </summary>
        /// <param name="left">The MB.</param>
        /// <param name="right">The uint.</param>
        /// <returns>An MB representing the quotient of the the provided MB and the provided uint.</returns>
        public static MB operator /(MB left, uint right) { return new MB(left.Quantity / right); }
        /// <summary>
        /// Returns an MB representing the quotient of the the provided uint and the provided MB.
        /// </summary>
        /// <param name="left">The uint.</param>
        /// <param name="right">The MB.</param>
        /// <returns>An MB representing the quotient of the the provided uint and the provided MB.</returns>
        public static MB operator /(uint left, MB right) { return new MB(left / right.Quantity); }

        #endregion

        #region relational operator overloads

        /// <summary>
        /// Returns a value that indicates whether a specified MB is less than another specified MB.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns>True if the left MB is less than the right MB.</returns>
        public static bool operator <(MB left, MB right) { return left.Quantity < right.Quantity; }
        /// <summary>
        /// Returns a value that indicates whether a specified MB is greater than another specified MB.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns>True if the left MB is greater than the right MB.</returns>
        public static bool operator >(MB left, MB right) { return left.Quantity > right.Quantity; }
        /// <summary>
        /// Returns a value that indicates whether a specified MB is less than a specified uint.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The uint to compare.</param>
        /// <returns>True if the MB on the left is less than the unit on the right.</returns>
        public static bool operator <(MB left, uint right) { return left.Quantity < right; }
        /// <summary>
        /// Returns a value that indicates whether a specified uint is greater than a specified MB.
        /// </summary>
        /// <param name="left">The uint to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns>True if the uint on the left is greater than the MB on the right.</returns>
        public static bool operator >(uint left, MB right) { return left > right.Quantity; }
        /// <summary>
        /// Returns a value that indicates whether a specified uint is less than a specified MB.
        /// </summary>
        /// <param name="left">The uint to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns>True if the uint on the left is less than the MB on the right.</returns>
        public static bool operator <(uint left, MB right) { return left < right.Quantity; }
        /// <summary>
        /// Returns a value that indicates whether a specified MB is greater than a specified uint.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The uint to compare.</param>
        /// <returns>True if the MB on the left is greater than the unit on the right.</returns>
        public static bool operator >(MB left, uint right) { return left.Quantity > right; }
        /// <summary>
        /// Returns a value that indicates whether a specified MB is less than or equal to another specified MB.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns>True if the MB on the left is less than or equal to the MB on the right.</returns>
        public static bool operator <=(MB left, MB right) { return left.Quantity <= right.Quantity; }
        /// <summary>
        /// Returns a value that indicates whether a specified MB is greater than or equal to another specified MB.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns>True if the MB on the left is greater than or equal to the MB on the right.</returns>
        public static bool operator >=(MB left, MB right) { return left.Quantity >= right.Quantity; }
        /// <summary>
        /// Returns a value that indicates whether a specified MB is less than or equal to a specified uint.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The uint to compare.</param>
        /// <returns>True if the MB on the left is less than or equal to the unit on the right.</returns>
        public static bool operator <=(MB left, uint right) { return left.Quantity <= right; }


        ///         
        /// <summary>
        /// Returns a value that indicates whether a specified uint is greater than or equal to a specified MB.
        /// </summary>
        /// <param name="left">The uint to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns>True if the uint on the left is greater than or equal to the MB on the right.</returns>
        public static bool operator >=(uint left, MB right) { return left >= right.Quantity; }


        /// <summary>
        /// Returns a value that indicates whether a specified uint is less than or equal to a specified MB.
        /// </summary>
        /// <param name="left">The uint to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns>True if the uint on the left is less than or equal to the MB on the right.</returns>
        public static bool operator <=(uint left, MB right) { return left <= right.Quantity; }
        /// <summary>
        /// Returns a value that indicates whether a specified MB is greater than or equal to a specified uint.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The uint to compare.</param>
        /// <returns>True if the MB on the left is greater than or equal to the uint on the right.</returns>
        public static bool operator >=(MB left, uint right) { return left.Quantity >= right; }
        /// <summary>
        /// Returns a value that indicates whether two specified MB structures are equal.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns>True if the specified Weight instances are equal; otherwise, false.</returns>
        public static bool operator ==(MB left, MB right) { return left.Quantity == right.Quantity; }
        /// <summary>
        /// Returns a value that indicates whether two specified MB structures are not equal.
        /// </summary>
        /// <param name="left">The first MB to compare.</param>
        /// <param name="right">The second MB to compare.</param>
        /// <returns>True if the specified Weight instances are not equal; otherwise, false.</returns>
        public static bool operator !=(MB left, MB right) { return !(left == right); }
        /// <summary>
        /// Returns a value that indicates whether the MB on the left is equal to the uint on the right.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The uint to compare.</param>
        /// <returns>True if the MB on the left is equal to the uint on the right.</returns>
        public static bool operator ==(MB left, uint right) { return left.Quantity == right; }
        /// <summary>
        /// Returns a value that indicates whether the MB on the left is not equal to the uint on the right.
        /// </summary>
        /// <param name="left">The MB to compare.</param>
        /// <param name="right">The uint to compare.</param>
        /// <returns>True if the MB on the left is not equal to the uint on the right.</returns>
        public static bool operator !=(MB left, uint right) { return !(left == right); }
        /// <summary>
        /// Returns a value that indicates whether the uint on the left is equal to the MB on the right.
        /// </summary>
        /// <param name="left">The uint to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns>True if the uint on the left is equal to the MB on the right.</returns>
        public static bool operator ==(uint left, MB right) { return left == right.Quantity; }
        /// <summary>
        /// Returns a value that indicates whether the uint on the left is not equal to the MB on the right.
        /// </summary>
        /// <param name="left">The uint to compare.</param>
        /// <param name="right">The MB to compare.</param>
        /// <returns>True if the uint on the left is not equal to the MB on the right.</returns>
        public static bool operator !=(uint left, MB right) { return !(left == right); }
        #endregion
    }
}