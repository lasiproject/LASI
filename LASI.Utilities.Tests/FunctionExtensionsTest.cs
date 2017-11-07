using System;
using System.Numerics;
namespace LASI.Utilities.Tests
{
    using NFluent;
    using Xunit;
    using static FunctionExtensions;
    using static System.Threading.Thread;

    public class FunctionExtensionsTest
    {
        [Fact]
        public void ComposeTest()
        {
            Func<double, double> f = x => x * x;
            Func<double, double> g = x => x - 1;
            var target = f.Compose(g);
            var value = 5D;
            var temp = g(value);
            var expected = f(temp);
            var actual = target(value);
            Check.That(actual).IsEqualTo(expected);
        }

        [Fact]
        public void ComposeCallsGBeforeFTest()
        {
            var fCalled = false;
            var gCalled = false;
            Func<double, double> f = x =>
            {
                Check.That(gCalled).IsTrue();
                fCalled = true;
                return x * x;
            };
            Func<double, double> g = x =>
            {
                Check.That(fCalled).IsFalse();
                gCalled = true;
                return x - 1;
            };
            var target = f.Compose(g)(5);

            Check.That(fCalled).IsTrue();
            Check.That(gCalled).IsTrue();
        }

        [Fact]
        public void ComposePropagatesExceptionsThrownByGAndDoesNotCallFTest()
        {
            var fCalled = false;
            var gCalled = false;
            Func<double, double> f;
            Func<double, double> g;
            var failure = $"{nameof(g)} threw";
            f = x =>
            {
                fCalled = true;
                return 5;
            };
            g = x =>
            {
                gCalled = true;
                throw new Exception(failure);
            };
            var target = f.Compose(g);
            try
            {
                target(5);
            }
#pragma warning disable RCS1075 // Avoid empty catch clause that catches System.Exception.
            catch (Exception e) when (e.Message == failure)
#pragma warning restore RCS1075 // Avoid empty catch clause that catches System.Exception.
            {
            }
            Check.That(fCalled).IsFalse();
            Check.That(gCalled).IsTrue();
        }

        [Fact]
        public void ComposeTest4()
        {
            ComposeTest1Helper<int, float, decimal>();
            ComposeTest1Helper<int?, float?, decimal?>();
            ComposeTest1Helper<object, string, StringComparer>();
        }
        [Fact]
        public void ComposeTest5()
        {
            ComposeTest1Helper<object, object, object>();
            ComposeTest1Helper<string, string, object>();
            ComposeTest1Helper<string, string, object>();

            ComposeTest1Helper<object, object, int>();
            ComposeTest1Helper<string, int, object>();
            ComposeTest1Helper<int, string, object>();
            ComposeTest1Helper<byte, int, decimal>();
            ComposeTest1Helper<Complex, int, int>();
            ComposeTest1Helper<BigInteger, int, int>();
            ComposeTest1Helper<int?, int?, int?>();
            ComposeTest1Helper<Complex?, Complex?, Complex?>();
            ComposeTest1Helper<BigInteger?, BigInteger?, BigInteger?>();
        }

        /// <summary>
        ///A test for Compose
        /// </summary>
        void ComposeTest1Helper<R, U, T>()
        {
            Func<R, T> f = r => default;
            Func<U, R> g = u => default;
            Func<U, T> expected = u => default;
            Func<U, T> actual;
            actual = FunctionExtensions.Compose(f, g);
            Check.That(expected(default)).IsEqualTo(default);
        }

        [Fact]
        public void CurryOfTwoArgumentFunctionTest()
        {
            Func<int, int, string> uncurried = (x, y) => $"{x} * {y} = {x * y}";
            var curried = uncurried.Curry();
            var arg1 = 4;
            var arg2 = 5;
            var expected = $"{arg1} * {arg2} = {arg1 * arg2}";
            var uncurriedResult = uncurried(arg1, arg2);
            Check.That(expected).IsEqualTo(uncurriedResult);
            var curriedResult = curried(arg1)(arg2);
            Check.That(curriedResult).IsEqualTo(uncurriedResult);
        }

        [Fact]
        public void CurryOfThreeArgumentFunctionTest()
        {
            Func<int, int, int, string> uncurried = (x, y, z) => $"{x} * {y} * {z} = {x * y * z}";
            var arg1 = 4;
            var arg2 = 5;
            var arg3 = 6;
            var expected = $"{arg1} * {arg2} * {arg3} = {arg1 * arg2 * arg3}";
            var uncurriedResult = uncurried(arg1, arg2, arg3);
            Check.That(expected).IsEqualTo(uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3);
            Check.That(curriedResult).IsEqualTo(uncurriedResult);
        }

        [Fact]
        public void CurryOfFourArgumentFunctionTest()
        {
            Func<int, int, int, int, string> uncurried = (x, y, z, a) => $"({x} * {y} * {z}) / {a} = {(x * y * z) / a}";
            var arg1 = 4;
            var arg2 = 5;
            var arg3 = 6;
            var arg4 = 3;
            var expected = $"({arg1} * {arg2} * {arg3}) / {arg4} = {(arg1 * arg2 * arg3) / arg4}";
            var uncurriedResult = uncurried(arg1, arg2, arg3, arg4);
            Check.That(expected).IsEqualTo(uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3)(arg4);
            Check.That(curriedResult).IsEqualTo(uncurriedResult);
        }

        [Fact]
        public void CurryOfFiveArgumentFunctionTest()
        {
            Func<int, int, int, int, int, string> uncurried = (x, y, z, a, b) => $"({x} * {y} * {z}) / {a} - {b} = {(x * y * z) / a - b}";
            var arg1 = 4;
            var arg2 = 5;
            var arg3 = 6;
            var arg4 = 3;
            var arg5 = 2;
            var expected = $"({arg1} * {arg2} * {arg3}) / {arg4} - {arg5} = {(arg1 * arg2 * arg3) / arg4 - arg5}";
            var uncurriedResult = uncurried(arg1, arg2, arg3, arg4, arg5);
            Check.That(expected).IsEqualTo(uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3)(arg4)(arg5);
            Check.That(curriedResult).IsEqualTo(uncurriedResult);
        }

        [Fact]
        public void CurryOfSixArgumentFunctionTest()
        {
            Func<int, int, int, int, int, int, string> uncurried = (x, y, z, a, b, c) => $"({x} * {y} * {z}) / ({a} - {b} + {c}) = {(x * y * z) / (a - b + c)}";
            var arg1 = 4;
            var arg2 = 5;
            var arg3 = 6;
            var arg4 = 3;
            var arg5 = 2;
            var arg6 = 2;
            var expected = $"({arg1} * {arg2} * {arg3}) / ({arg4} - {arg5} + {arg6}) = {(arg1 * arg2 * arg3) / (arg4 - arg5 + arg6)}";
            var uncurriedResult = uncurried(arg1, arg2, arg3, arg4, arg5, arg6);
            Check.That(expected).IsEqualTo(uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3)(arg4)(arg5)(arg6);
            Check.That(curriedResult).IsEqualTo(uncurriedResult);
        }

        [Fact]
        public void CurryOfSevenArgumentFunctionTest()
        {
            Func<int, int, int, int, int, int, int, string> uncurried = (x, y, z, a, b, c, d) => $"(({d})({x} + {y} + {z}) / ({a} - {b} + {c}) = {d * (x + y + z) / (a - b + c)}";
            var arg1 = 4;
            var arg2 = 5;
            var arg3 = 6;
            var arg4 = 3;
            var arg5 = 2;
            var arg6 = 2;
            var arg7 = 17;
            var expected = $"(({arg7})({arg1} + {arg2} + {arg3}) / ({arg4} - {arg5} + {arg6}) = {arg7 * (arg1 + arg2 + arg3) / (arg4 - arg5 + arg6)}";
            var uncurriedResult = uncurried(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            Check.That(expected).IsEqualTo(uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3)(arg4)(arg5)(arg6)(arg7);
            Check.That(curriedResult).IsEqualTo(uncurriedResult);
        }

        [Fact]
        public void CurryOfEightArgumentFunctionTest()
        {
            Func<int, int, int, int, int, int, int, int, string> uncurried = (x, y, z, a, b, c, d, e) => $"(({d} - {e})({x} + {y} + {z}) / ({a} - {b} + {c}) = {(d - e) * (x + y + z) / (a - b + c)}";
            var arg1 = 4;
            var arg2 = 5;
            var arg3 = 6;
            var arg4 = 3;
            var arg5 = 2;
            var arg6 = 2;
            var arg7 = 17;
            var arg8 = 18;
            var expected = $"(({arg7} - {arg8})({arg1} + {arg2} + {arg3}) / ({arg4} - {arg5} + {arg6}) = {(arg7 - arg8) * (arg1 + arg2 + arg3) / (arg4 - arg5 + arg6)}";
            var uncurriedResult = uncurried(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            Check.That(expected).IsEqualTo(uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3)(arg4)(arg5)(arg6)(arg7)(arg8);
            Check.That(curriedResult).IsEqualTo(uncurriedResult);
        }

        [Fact]
        public void CurryOfNineArgumentFunctionTest()
        {
            Func<int, int, int, int, int, int, int, int, int, string> uncurried = (x, y, z, a, b, c, d, e, f) =>
                $"(({d} - {e})({x} + {y} + {z}) / ({a} - {b} + {c}) + {f} = {(d - e) * (x + y + z) / (a - b + c) + f}";
            var arg1 = 4;
            var arg2 = 5;
            var arg3 = 6;
            var arg4 = 3;
            var arg5 = 2;
            var arg6 = 2;
            var arg7 = 17;
            var arg8 = 18;
            var arg9 = 2;
            var expected = $"(({arg7} - {arg8})({arg1} + {arg2} + {arg3}) / ({arg4} - {arg5} + {arg6}) + {arg9} = {(arg7 - arg8) * (arg1 + arg2 + arg3) / (arg4 - arg5 + arg6) + arg9}";
            var uncurriedResult = uncurried(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            Check.That(expected).IsEqualTo(uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3)(arg4)(arg5)(arg6)(arg7)(arg8)(arg9);
            Check.That(curriedResult).IsEqualTo(uncurriedResult);
        }

        [Fact]
        public void ApplyingArity2ReferentiallyTransparentFunctionReturnsCorrectResult()
        {
            Func<int, int, int> f = (a, b) => a * b;

            Check.That(f(5, 2)).IsEqualTo(f.Apply(5)(2));
        }

        [Fact]
        public void ApplyingArity3ReferentiallyTransparentFunctionReturnsCorrectResult()
        {
            Func<int, int, int, int> f = (a, b, c) => a * b - c;

            Check.That(f(5, 2, 7)).IsEqualTo(f.Apply(5)(2, 7));
        }

        [Fact]
        public void ApplyingArity4ReferentiallyTransparentFunctionReturnsCorrectResult()
        {
            Func<int, int, int, int, int> f = (a, b, c, d) => a * b - c + d;

            Check.That(f(5, 2, 7, 13)).IsEqualTo(f.Apply(5)(2, 7, 13));
        }

        [Fact]
        public void ApplyingArity5ReferentiallyTransparentFunctionReturnsCorrectResult()
        {
            Func<int, int, int, int, int, int> f = (a, b, c, d, e) => a * b - c + d / e;

            Check.That(f(5, 2, 7, 13, 5)).IsEqualTo(f.Apply(5)(2, 7, 13, 5));
        }

        [Fact]
        public void ApplyingArity6ReferentiallyTransparentFunctionReturnsCorrectResult()
        {
            Func<int, int, int, int, int, int, int> g = (a, b, c, d, e, f) => a * b - c + d / e % f;

            Check.That(g(5, 2, 7, 13, 5, 16)).IsEqualTo(g.Apply(5)(2, 7, 13, 5, 16));
        }

        [Fact]
        public void IdentityTest()
        {
            IdentityTestHelper<object>();
            IdentityTestHelper<int>();
            IdentityTestHelper<int?>();
        }
        /// <summary>
        ///A test for Identity
        /// </summary>
        static void IdentityTestHelper<T>() where T : new()
        {
            var target = new T();
            var expected = target;
            var actual = Identity(target);
            Check.That(expected).IsEqualTo(actual);
        }

        [Fact]
        public void WithTimerOfArity0FunctionStopsAndStartsTimerAppropriately()
        {
            var synthesizedWaitInMs = 0;
            var result = default(Complex);
            Func<Complex> compute = () =>
            {
                Sleep(synthesizedWaitInMs);
                return result;
            };
            var computeWithTimer = compute.WithTimer(out var sw);
            Check.That(sw.IsRunning).IsFalse();
            computeWithTimer();
            Check.That(sw.IsRunning).IsFalse();
        }

        [Fact]
        public void WithTimerOfArity1FunctionStopsAndStartsTimerAppropriately()
        {
            var synthesizedWaitInMs = 0;
            Func<Complex, Complex> compute = data =>
            {
                Sleep(synthesizedWaitInMs);
                return new Complex(data.Imaginary, data.Real);
            };
            var computeWithTimer = compute.WithTimer(out var sw);
            Check.That(sw.IsRunning).IsFalse();
            computeWithTimer(new Complex(2, 2));
            Check.That(sw.IsRunning).IsFalse();
        }

        [Fact]
        public void WithTimerOfArity0ActionStopsAndStartsTimerAppropriately()
        {
            var synthesizedWaitInMs = 0;
            var result = default(Complex);
            Action compute = () =>
            {
                Sleep(synthesizedWaitInMs);
                result = new Complex(1, -1);
            };
            var computeWithTimer = compute.WithTimer(out var sw);
            Check.That(sw.IsRunning).IsFalse();
            computeWithTimer();
            Check.That(sw.IsRunning).IsFalse();
        }
    }
}