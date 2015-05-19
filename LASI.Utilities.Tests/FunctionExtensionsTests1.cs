using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
namespace LASI.Utilities.Tests
{
    using static System.Threading.Thread;
    using static FunctionExtensions;
    [TestClass]
    public class FunctionExtensionsTests1
    {
        [TestMethod]
        public void ComposeTest()
        {
            Func<double, double> f = x => x * x;
            Func<double, double> g = x => x - 1;
            var target = f.Compose(g);
            var value = 5D;
            var temp = g(value);
            var expected = f(temp);
            var actual = target(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ComposeCallsGBeforeFTest()
        {
            bool fCalled = false;
            bool gCalled = false;
            Func<double, double> f = x =>
            {
                Assert.IsTrue(gCalled);
                fCalled = true;
                return x * x;
            };
            Func<double, double> g = x =>
            {
                Assert.IsFalse(fCalled);
                gCalled = true;
                return x - 1;
            };
            var target = f.Compose(g)(5);
            Assert.IsTrue(fCalled);
            Assert.IsTrue(gCalled);
        }

        [TestMethod]
        public void ComposePropagatesExceptionsThrownByGAndDoesNotCallFTest()
        {
            bool fCalled = false;
            bool gCalled = false;
            Func<double, double> f;
            Func<double, double> g;
            string failure = $"{nameof(g)} threw";
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
            catch (Exception e) when (e.Message == failure)
            {
            }
            Assert.IsFalse(fCalled);
            Assert.IsTrue(gCalled);
        }

        [TestMethod]
        public void ComposeTest4()
        {
            ComposeTest1Helper<GenericParameterHelper, GenericParameterHelper, GenericParameterHelper>();
        }
        [TestMethod]
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
        public void ComposeTest1Helper<R, U, T>()
        {
            Func<R, T> f = r => default(T);
            Func<U, R> g = u => default(R);
            Func<U, T> expected = u => default(T);
            Func<U, T> actual;
            var y = f.Compose(g);
            actual = FunctionExtensions.Compose(f, g);
            Assert.AreEqual(expected(default(U)), default(T));

        }

        [TestMethod]
        public void AndThenCallsA1BeforeA2()
        {
            bool a1Called = false;
            bool a2Called = false;

            Action<string> a1 = s =>
            {
                Assert.IsFalse(a2Called);
                a1Called = true;
                Logger.Log(s);
            };
            Action<string> a2 = s =>
            {
                Assert.IsTrue(a1Called);
                a2Called = true;
                Logger.Log(s.ToUpper());
            };
            var target = a1.AndThen(a2);
            target("hello");
            Assert.IsTrue(a1Called);
            Assert.IsTrue(a2Called);
        }

        [TestMethod]
        public void AndThenTest5()
        {
            bool a1Called = false;
            bool a2Called = false;

            Action<string> a1 = s =>
            {
                Assert.IsFalse(a2Called);
                a1Called = true;
                Logger.Log(s);
            };
            Action a2 = () =>
            {
                Assert.IsTrue(a1Called);
                a2Called = true;
                Logger.Log($"called {nameof(a2)}");
            };
            var target = a1.AndThen(a2);
            target("hello");
            Assert.IsTrue(a1Called);
            Assert.IsTrue(a2Called);
        }

        [TestMethod]
        public void AndThenTest6()
        {
            bool a1Called = false;
            bool a2Called = false;

            Action a1 = () =>
            {
                Assert.IsFalse(a2Called);
                a1Called = true;
                Logger.Log($"called {nameof(a1)}");
            };
            Action a2 = () =>
            {
                Assert.IsTrue(a1Called);
                a2Called = true;
                Logger.Log($"called {nameof(a2)}");
            };
            var target = a1.AndThen(a2);
            target();
            Assert.IsTrue(a1Called);
            Assert.IsTrue(a2Called);
        }

        [TestMethod]
        public void CurryOfTwoArgumentFunctionTest()
        {
            Func<int, int, string> uncurried = (x, y) => $"{x} * {y} = {x * y}";
            var curried = uncurried.Curry();
            var arg1 = 4;
            var arg2 = 5;
            var expected = $"{arg1} * {arg2} = {arg1 * arg2}";
            var uncurriedResult = uncurried(arg1, arg2);
            Assert.AreEqual(expected, uncurriedResult);
            var curriedResult = curried(arg1)(arg2);
            Assert.AreEqual(curriedResult, uncurriedResult);
        }

        [TestMethod]
        public void CurryOfThreeArgumentFunctionTest()
        {
            Func<int, int, int, string> uncurried = (x, y, z) => $"{x} * {y} * {z} = {x * y * z}";
            var arg1 = 4;
            var arg2 = 5;
            var arg3 = 6;
            var expected = $"{arg1} * {arg2} * {arg3} = {arg1 * arg2 * arg3}";
            var uncurriedResult = uncurried(arg1, arg2, arg3);
            Assert.AreEqual(expected, uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3);
            Assert.AreEqual(curriedResult, uncurriedResult);
        }

        [TestMethod]
        public void CurryOfFourArgumentFunctionTest()
        {
            Func<int, int, int, int, string> uncurried = (x, y, z, a) => $"({x} * {y} * {z}) / {a} = {(x * y * z) / a}";
            var arg1 = 4;
            var arg2 = 5;
            var arg3 = 6;
            var arg4 = 3;
            var expected = $"({arg1} * {arg2} * {arg3}) / {arg4} = {(arg1 * arg2 * arg3) / arg4}";
            var uncurriedResult = uncurried(arg1, arg2, arg3, arg4);
            Assert.AreEqual(expected, uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3)(arg4);
            Assert.AreEqual(curriedResult, uncurriedResult);
        }

        [TestMethod]
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
            Assert.AreEqual(expected, uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3)(arg4)(arg5);
            Assert.AreEqual(curriedResult, uncurriedResult);
        }

        [TestMethod]
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
            Assert.AreEqual(expected, uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3)(arg4)(arg5)(arg6);
            Assert.AreEqual(curriedResult, uncurriedResult);
        }

        [TestMethod]
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
            Assert.AreEqual(expected, uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3)(arg4)(arg5)(arg6)(arg7);
            Assert.AreEqual(curriedResult, uncurriedResult);
        }

        [TestMethod]
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
            Assert.AreEqual(expected, uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3)(arg4)(arg5)(arg6)(arg7)(arg8);
            Assert.AreEqual(curriedResult, uncurriedResult);
        }

        [TestMethod]
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
            Assert.AreEqual(expected, uncurriedResult);

            var curried = uncurried.Curry();
            var curriedResult = curried(arg1)(arg2)(arg3)(arg4)(arg5)(arg6)(arg7)(arg8)(arg9);
            Assert.AreEqual(curriedResult, uncurriedResult);
        }

        [TestMethod]
        public void CurryTest8()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void CurryTest9()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void CurryTest10()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void CurryTest11()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void CurryTest12()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void CurryTest13()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest1()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest2()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest3()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest4()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest5()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest6()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest7()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest8()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest9()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest10()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest11()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void ApplyTest12()
        {
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod]
        public void IdentityTest()
        {
            IdentityTestHelper<object>();
            IdentityTestHelper<int>();
            IdentityTestHelper<int?>();
        }
        /// <summary>
        ///A test for Identity
        /// </summary>
        public void IdentityTestHelper<T>() where T : new()
        {
            T target = new T();
            T expected = target;
            T actual = Identity(target);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void WithTimerTest()
        {
            int synthesizedWaitInMs = 0;
            Complex result = default(Complex);
            Func<Complex> compute = () =>
            {
                Sleep(synthesizedWaitInMs);
                return result;
            };
            System.Diagnostics.Stopwatch sw;

            var computeWithTimer = compute.WithTimer(out sw);
            Assert.IsFalse(sw.IsRunning);
            computeWithTimer();
            Assert.IsFalse(sw.IsRunning);
        }

        [TestMethod]
        public void WithTimerTest1()
        {
            int synthesizedWaitInMs = 0;
            Func<Complex, Complex> compute = data =>
            {
                Sleep(synthesizedWaitInMs);
                return new Complex(data.Imaginary, data.Real);
            };
            System.Diagnostics.Stopwatch sw;

            var computeWithTimer = compute.WithTimer(out sw);
            Assert.IsFalse(sw.IsRunning);
            computeWithTimer(new Complex(2, 2));
            Assert.IsFalse(sw.IsRunning);
        }

        [TestMethod]
        public void WithTimerTest2()
        {
            int synthesizedWaitInMs = 0;
            Complex result = default(Complex);
            Action compute = () =>
            {
                Sleep(synthesizedWaitInMs);
                result = new Complex(1, -1);
            };
            System.Diagnostics.Stopwatch sw;

            var computeWithTimer = compute.WithTimer(out sw);
            Assert.IsFalse(sw.IsRunning);
            computeWithTimer();
            Assert.IsFalse(sw.IsRunning);
        }
    }
}