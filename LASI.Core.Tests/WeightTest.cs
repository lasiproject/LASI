
using LASI.Utilities;
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
            var multiplier = 1.5;
            var target = new Weight(rawWeight, multiplier);
            Check.That(target.Raw).IsEqualTo(rawWeight);
            Check.That(target.Multiplier).IsEqualTo(multiplier);
        }

        /// <summary>
        ///A test for Equals
        /// </summary>
        [Fact]
        public void EqualsTest()
        {
            var target = new Weight(1, 1);
            object obj = new Weight(1, 1);
            var expected = true;
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
            var target = new Weight();
            var expected = ((object)target).GetHashCode();
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
            var A = new Weight(43, 2);
            var B = new Weight(35, 1.5);
            var expected = 43 * 2 + 35 * 1.5;
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
            var A = new Weight(15d, 0.75d);
            var B = new Weight(18d, 0.87d);
            var expected = (15 * 0.75) / (18 * 0.87);
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
            var A = new Weight(10, 0.5);
            var B = new Weight(20, 0.25);
            var expected = true;
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
            var A = new Weight(10, 0.78);
            var B = new Weight(15, 0.5);
            var expected = true;
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
            var A = new Weight(86, 1);
            var B = new Weight(95, 17.5);
            var expected = true;
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
            var A = new Weight(15.6, 1.5);
            var B = new Weight(2.99, 1);
            var expected = false;
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
            var A = new Weight(9876.4, 15.65);
            var B = new Weight(752, 0.005);
            var expected = 9876.4 * 15.65 * 752 * 0.005;
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
            var A = new Weight(10, 1.89);
            var B = new Weight(77.24, 2);
            var expected = 10 * 1.89 - 77.24 * 2;
            double actual;
            actual = (A - B);
            Check.That(actual).IsEqualTo(expected);
        }
    }
}
