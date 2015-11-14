
using LASI.Utilities;
using System;
using System.Linq;
using System.Collections.Generic;
using Shared.Test.NFluentExtensions;
using Xunit;
using NFluent;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for WeightTest and is intended
    ///to contain all WeightTest Unit Tests
    /// </summary>
    public class WeightTest
    {
        /// <summary>
        ///A test for Weight Constructor
        /// </summary>
        [Fact]
        public void WeightConstructorTest()
        {
            double rawWeight = 65;
            double multiplier = 1.5;
            Weight target = new Weight(rawWeight, multiplier);
            Check.That(target.Raw).IsEqualTo(rawWeight);
            Check.That(target.Multiplier).IsEqualTo(multiplier);
        }

        /// <summary>
        ///A test for Equals
        /// </summary>
        [Fact]
        public void EqualsTest()
        {
            Weight target = new Weight(1, 1);
            object obj = new Weight(1, 1);
            bool expected = true;
            bool actual;
            actual = target.Equals(obj);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for GetHashCode
        /// </summary>
        [Fact]
        public void GetHashCodeTest()
        {
            Weight target = new Weight();
            int expected = ((object)target).GetHashCode();
            int actual;
            actual = target.GetHashCode();
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for op_Addition
        /// </summary>
        [Fact]
        public void op_AdditionTest()
        {
            Weight A = new Weight(43, 2);
            Weight B = new Weight(35, 1.5);
            double expected = 43 * 2 + 35 * 1.5;
            double actual;
            actual = (A + B);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for op_Division
        /// </summary>
        [Fact]
        public void op_DivisionTest()
        {
            Weight A = new Weight(15d, 0.75d);
            Weight B = new Weight(18d, 0.87d);
            double expected = (15 * 0.75) / (18 * 0.87);
            double actual;
            actual = (A / B);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for op_Equality
        /// </summary>
        [Fact]
        public void op_EqualityTest()
        {
            Weight A = new Weight(10, 0.5);
            Weight B = new Weight(20, 0.25);
            bool expected = true;
            bool actual;
            actual = (A == B);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for op_GreaterThan
        /// </summary>
        [Fact]
        public void op_GreaterThanTest()
        {
            Weight A = new Weight(10, 0.78);
            Weight B = new Weight(15, 0.5);
            bool expected = true;
            bool actual;
            actual = (A > B);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for op_Inequality
        /// </summary>
        [Fact]
        public void op_InequalityTest()
        {
            Weight A = new Weight(86, 1);
            Weight B = new Weight(95, 17.5);
            bool expected = true;
            bool actual;
            actual = (A != B);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for op_LessThan
        /// </summary>
        [Fact]
        public void op_LessThanTest()
        {
            Weight A = new Weight(15.6, 1.5);
            Weight B = new Weight(2.99, 1);
            bool expected = false;
            bool actual;
            actual = (A < B);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for op_Multiply
        /// </summary>
        [Fact]
        public void op_MultiplyTest()
        {
            Weight A = new Weight(9876.4, 15.65);
            Weight B = new Weight(752, 0.005);
            double expected = 9876.4 * 15.65 * 752 * 0.005;
            double actual;
            actual = (A * B);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for op_Subtraction
        /// </summary>
        [Fact]
        public void op_SubtractionTest()
        {
            Weight A = new Weight(10, 1.89);
            Weight B = new Weight(77.24, 2);
            double expected = 10 * 1.89 - 77.24 * 2;
            double actual;
            actual = (A - B);
            Check.That(actual).IsEqualTo(expected);
        }
    }
}
