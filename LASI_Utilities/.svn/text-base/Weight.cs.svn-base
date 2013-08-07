using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{
    /// <summary>
    /// A numberical data type associating a double value with contextual multiplier.
    /// </summary>
    public struct Weight : IComparable, IComparable<Weight>, IEquatable<Weight>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Weight strucutre with the provided raw weight and multiplier.
        /// </summary>
        /// <param name="rawWeight">The raw weight.</param>
        /// <param name="multiplier">The scaling multiplier.</param>
        public Weight(double rawWeight, double multiplier)
            : this() {
            RawWeight = rawWeight;
            Multiplier = multiplier;
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
        public double ScaledWeight { get { return RawWeight * Multiplier; } }

        #endregion

        #region Comparison Operators


        /// <summary>
        /// Returns a value that indicates whether two specified Weights are equal.
        /// </summary>
        /// <param name="left">The first Weight to compare.</param>
        /// <param name="right">The second Weight to compare.</param>
        /// <returns>True if the specified Weight instances are equal, false otherwise.</returns>
        public static bool operator ==(Weight left, Weight right) {
            return left.RawWeight * left.Multiplier == right.RawWeight * right.Multiplier;
        }
        /// <summary>
        /// Returns a value that indicates whether two specified Weights are not equal.
        /// </summary>
        /// <param name="left">The first Weight to compare.</param>
        /// <param name="right">The second Weight to compare.</param>
        /// <returns>True if the specified Weight instances are not equal, false otherwise.</returns>
        public static bool operator !=(Weight left, Weight right) {
            return !(left == right);
        }
        /// <summary>
        /// Returns a value that indicates whether a specified Weight is greater than another specified Weight.
        /// </summary>
        /// <param name="left">The first Weight to compare.</param>
        /// <param name="right">The second Weight to compare.</param>
        /// <returns>True if the left Weight is greater than the right Weight.</returns>
        public static bool operator >(Weight left, Weight right) {
            return left.RawWeight * left.Multiplier > right.RawWeight * right.Multiplier;
        }
        /// <summary>
        /// Returns a value that indicates whether a specified Weight is greater than or equal to another specified Weight.
        /// </summary>
        /// <param name="left">The first Weight to compare.</param>
        /// <param name="right">The second Weight to compare.</param>
        /// <returns>True if the left Weight is greater than or equal to the right Weight.</returns>
        public static bool operator >=(Weight left, Weight right) {
            return left.RawWeight * left.Multiplier > right.RawWeight * right.Multiplier;
        }
        /// <summary>
        /// Returns a value that indicates whether a specified Weight is less than another specified Weight.
        /// </summary>
        /// <param name="left">The first Weight to compare.</param>
        /// <param name="right">The second Weight to compare.</param>
        /// <returns>True if the left Weight is less than the right Weight.</returns>
        public static bool operator <(Weight left, Weight right) {
            return left.RawWeight * left.Multiplier < right.RawWeight * right.Multiplier;
        }
        /// <summary>
        /// Returns a value that indicates whether a specified Weight is less than or equal to another specified Weight.
        /// </summary>
        /// <param name="left">The first Weight to compare.</param>
        /// <param name="right">The second Weight to compare.</param>
        /// <returns>True if the left Weight is less than or equal to the right Weight.</returns>
        public static bool operator <=(Weight left, Weight right) {
            return left.RawWeight * left.Multiplier < right.RawWeight * right.Multiplier;
        }
        #endregion


        #region Numeric Operators

        /// <summary>
        /// Calculates sum of the weight values of two Weights and returns it as a double.
        /// </summary>
        /// <param name="left">The first Weight.</param>
        /// <param name="right">The second Weight</param>
        /// <returns>The sum of the scaled values of two Weights as a double.</returns>
        public static double operator +(Weight left, Weight right) {
            return left.RawWeight * left.Multiplier + right.RawWeight * right.Multiplier;
        }
        /// <summary>
        /// Calculates difference of the weight values of two Weights and returns it as a double.
        /// </summary>
        /// <param name="left">The first Weight.</param>
        /// <param name="right">The second Weight</param>
        /// <returns>The difference of the scaled values of two Weights as a double.</returns>
        public static double operator -(Weight left, Weight right) {
            return left.RawWeight * left.Multiplier - right.RawWeight * right.Multiplier;
        }

        /// <summary>
        /// Calculates product of the weight values of two Weights and returns it as a double.
        /// </summary>
        /// <param name="left">The first Weight.</param>
        /// <param name="right">The second Weight</param>
        /// <returns>The product of the scaled values of two Weights as a double.</returns>
        public static double operator *(Weight left, Weight right) {
            return (left.RawWeight * left.Multiplier) * (right.RawWeight * right.Multiplier);
        }

        /// <summary>
        /// Calculates quotient of the weight values of two Weights and returns it as a double.
        /// </summary>
        /// <param name="left">The first Weight.</param>
        /// <param name="right">The second Weight</param>
        /// <returns>The quotient of the scaled values of two Weights as a double.</returns>
        public static double operator /(Weight left, Weight right) {
            return (left.RawWeight * left.Multiplier) / (right.RawWeight * right.Multiplier);
        }
        #endregion

        #region Numeric Conversions

        /// <summary>
        /// Converts the specified Weight to a Double value corresponding to its scaled weight.
        /// </summary>
        /// <param name="weight">The Weight to convert.</param>
        /// <returns>A Double representation of the specified Weight.</returns>
        public static implicit operator double(Weight weight) { return weight.ScaledWeight; }
        /// <summary>
        /// Converts the specified Weight to a Decimal value corresponding to its scaled weight.
        /// </summary>
        /// <param name="weight">The Weight to convert.</param>
        /// <returns>A Decimal representation of the specified Weight.</returns>
        public static implicit operator decimal(Weight weight) { return (decimal)weight.ScaledWeight; }
        /// <summary>
        /// Converts the specified Weight to an Int64 value corresponding to the integral portion of its scaled weight.
        /// </summary>
        /// <param name="weight">The Weight to convert.</param>
        /// <returns>An Int64 representation of the integral portion of the specified Weight.</returns>
        public static explicit operator long(Weight weight) { return (long)weight.ScaledWeight; }
        /// <summary>
        /// Converts the specified Weight to a UInt64 value corresponding to the integral portion of its scaled weight.
        /// </summary>
        /// <param name="weight">The Weight to convert.</param>
        /// <returns>An UInt64 representation of the integral portion of the specified Weight.</returns>
        public static explicit operator ulong(Weight weight) { return (ulong)weight.ScaledWeight; }
        /// <summary>
        /// Converts the specified Weight to an BigInteger value corresponding to the integral portion of its scaled weight.
        /// </summary>
        /// <param name="weight">The Weight to convert.</param>
        /// <returns>An BigInteger representation of the integral portion of the specified Weight.</returns>
        public static explicit operator BigInteger(Weight weight) { return (BigInteger)weight.ScaledWeight; }
        /// <summary>
        /// Creates a new Weight instance with its raw weight as the value of the long and its multiplier as 1.
        /// </summary>
        /// <param name="value">The long to convert.</param>
        /// <returns>A new Weight instance with its raw weight as the value of the long and its multiplier as 1.</returns>
        public static explicit operator Weight(long value) { return new Weight(value, 1); }
        /// <summary>
        /// Creates a new Weight instance with its raw weight as the value of the ulong and its multiplier as 1.
        /// </summary>
        /// <param name="value">The ulong to convert.</param>
        /// <returns>A new Weight instance with its raw weight as the value of the ulong and its multiplier as 1.</returns>
        public static explicit operator Weight(ulong value) { return new Weight(value, 1); }
        /// <summary>
        /// Creates a new Weight instance with its raw weight as the possible truncated value of the decimal and its multiplier as 1.
        /// </summary>
        /// <param name="value">The decimal to convert.</param>
        /// <returns>A new Weight instance with its raw weight as the possible truncated value of the decimal and its multiplier as 1.</returns>
        public static explicit operator Weight(decimal value) { return new Weight((double)value, 1); }
        /// <summary>
        /// Creates a new Weight instance with its raw weight as the possible truncated value of the BigInteger and its multiplier as 1.
        /// </summary>
        /// <param name="value">The BigInteger to convert.</param>
        /// <returns>A new Weight instance with its raw weight as the possible truncated value of the BigInteger and its multiplier as 1.</returns>
        public static explicit operator Weight(BigInteger value) { return new Weight((double)value, 1); }
        /// <summary>
        /// Implicitely creates a new Weight instance with its raw weight as the value of the int and its multiplier as 1.
        /// </summary>
        /// <param name="value">The int to convert.</param>
        /// <returns>A new Weight instance with its raw weight as the value of the int and its multiplier as 1.</returns>
        public static implicit operator Weight(int value) { return new Weight(value, 1); }
        /// <summary>
        /// Implicitely creates a new Weight instance with its raw weight as the value of the uint and its multiplier as 1.
        /// </summary>
        /// <param name="value">The uint to convert.</param>
        /// <returns>A new Weight instance with its raw weight as the value of the uint and its multiplier as 1.</returns>
        public static implicit operator Weight(uint value) { return new Weight(value, 1); }
        /// <summary>
        /// Implicitely creates a new Weight instance with its raw weight as the value of the float and its multiplier as 1.
        /// </summary>
        /// <param name="value">The float to convert.</param>
        /// <returns>A new Weight instance with its raw weight as the value of the float and its multiplier as 1.</returns>
        public static implicit operator Weight(float value) { return new Weight(value, 1); }
        /// <summary>
        /// Implicitely creates a new Weight instance with its raw weight as the value of the double and its multiplier as 1.
        /// </summary>
        /// <param name="value">The double to convert.</param>
        /// <returns>A new Weight instance with its raw weight as the value of the double and its multiplier as 1.</returns>
        public static implicit operator Weight(double value) { return new Weight(value, 1); }

        #endregion

        #region Methods
        /// <summary>
        /// Returns a string representation of the Weight.
        /// </summary>
        /// <returns>A string representation of the Weight.</returns>
        public override string ToString() { return string.Format("{0} = raw: {1} multiplier: {2} scaled: {3}", base.ToString(), RawWeight, Multiplier, ScaledWeight); }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() {
            return RawWeight.GetHashCode() ^ Multiplier.GetHashCode();
        }
        /// <summary>
        /// Returns a value that indicates whether the specified object is equal to the current Weight.
        /// </summary>
        /// <param name="obj">The object to compare with.</param> 
        /// <returns>True if the specified object is equal to the current Weight, false otherwise.</returns>
        public override bool Equals(object obj) {
            return obj is Weight && this == (Weight)obj;
        }
        /// <summary>
        /// Returns a value that indicates whether the specified Weight is equal to the current Weight.
        /// </summary>
        /// <param name="other">The Weight to compare with.</param> 
        /// <returns>True if the specified Weight is equal to the current Weight, false otherwise.</returns>
        public bool Equals(Weight other) {
            return this == other;
        }
        /// <summary>
        /// Returns a value that indicates whether the specified Object is equal to the current Weight.
        /// </summary>
        /// <param name="obj">The Object to compare with.</param> 
        /// <returns>True if the specified Object is equal to the current Weight, false otherwise.</returns>
        public int CompareTo(object obj) {
            return obj == null ? 1 : obj is Weight ? (int)(this - (Weight)obj) : 1;
        }
        /// <summary>
        /// Compares the current Weight with another Weight.
        /// </summary>
        /// <param name="other">A Weight to compare with this Weight.</param>
        /// <returns>
        /// A value that indicates the relative order of the Weights being compared.
        /// The return value has the following meanings: Value Meaning Less than zero This Weight is less than the other parameter.
        /// Zero This Weight is equal to other. 
        /// Greater than zero This Weight is greater than other.
        /// </returns>
        public int CompareTo(Weight other) {
            return (int)(this - other);
        }
        #endregion

    }
}
