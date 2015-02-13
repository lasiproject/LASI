// <copyright file="FunctionExtensionsTest.cs">Copyright ©  2013</copyright>

using System;
using LASI.Utilities;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Utilities
{
    [TestClass]
    [PexClass(typeof(FunctionExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class FunctionExtensionsTest
    {
        [PexGenericArguments(typeof(int), typeof(int), typeof(int))]
        [PexMethod]
        public Func<T1, TResult> Apply01<T1, T2, TResult>(Func<T1, T2, TResult> fn, T2 value)
        {
            Func<T1, TResult> result = FunctionExtensions.Apply<T1, T2, TResult>(fn, value);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Apply01(Func`3<!!0,!!1,!!2>, !!1)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        public Func<T2, T3, TResult> Apply02<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> fn, T1 value)
        {
            Func<T2, T3, TResult> result = FunctionExtensions.Apply<T1, T2, T3, TResult>(fn, value);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Apply02(Func`4<!!0,!!1,!!2,!!3>, !!0)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        public Func<T1, T3, TResult> Apply03<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> fn, T2 value)
        {
            Func<T1, T3, TResult> result = FunctionExtensions.Apply<T1, T2, T3, TResult>(fn, value);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Apply03(Func`4<!!0,!!1,!!2,!!3>, !!1)
        }
        [PexGenericArguments(typeof(int), typeof(int), typeof(int))]
        [PexMethod]
        public Func<T2, TResult> Apply<T1, T2, TResult>(Func<T1, T2, TResult> fn, T1 value)
        {
            Func<T2, TResult> result = FunctionExtensions.Apply<T1, T2, TResult>(fn, value);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Apply(Func`3<!!0,!!1,!!2>, !!0)
        }
        [PexGenericArguments(typeof(int), typeof(int), typeof(int))]
        [PexMethod]
        public Func<T1, Func<T2, Action<T3>>> Curry08<T1, T2, T3>(Action<T1, T2, T3> fn)
        {
            Func<T1, Func<T2, Action<T3>>> result = FunctionExtensions.Curry<T1, T2, T3>(fn);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Curry08(Action`3<!!0,!!1,!!2>)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        public Func<T1, Func<T2, Func<T3, Func<T4, TResult>>>> Curry02<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> fn)
        {
            Func<T1, Func<T2, Func<T3, Func<T4, TResult>>>> result
               = FunctionExtensions.Curry<T1, T2, T3, T4, TResult>(fn);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Curry02(Func`5<!!0,!!1,!!2,!!3,!!4>)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        public Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> Curry11<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> fn)
        {
            Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> result
               = FunctionExtensions.Curry<T1, T2, T3, T4, T5, T6>(fn);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Curry11(Action`6<!!0,!!1,!!2,!!3,!!4,!!5>)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        public Func<T1, Func<T2, Func<T3, TResult>>> Curry01<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> fn)
        {
            Func<T1, Func<T2, Func<T3, TResult>>> result = FunctionExtensions.Curry<T1, T2, T3, TResult>(fn);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Curry01(Func`4<!!0,!!1,!!2,!!3>)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        public Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, TResult>>>>>>> Curry05<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> fn)
        {
            Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, TResult>>>>>>> result
               = FunctionExtensions.Curry<T1, T2, T3, T4, T5, T6, T7, TResult>(fn);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Curry05(Func`8<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7>)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        public Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> Curry13<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> fn)
        {
            Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> result
               = FunctionExtensions.Curry<T1, T2, T3, T4, T5, T6, T7, T8>(fn);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Curry13(Action`8<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7>)
        }
        [PexGenericArguments(typeof(int), typeof(int), typeof(int))]
        [PexMethod]
        public Func<T2, T3> Compose<T1, T2, T3>(Func<T1, T3> first, Func<T2, T1> second)
        {
            Func<T2, T3> result = FunctionExtensions.Compose<T1, T2, T3>(first, second);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Compose(Func`2<!!0,!!2>, Func`2<!!1,!!0>)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        public Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> Curry10<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> fn)
        {
            Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> result
               = FunctionExtensions.Curry<T1, T2, T3, T4, T5>(fn);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Curry10(Action`5<!!0,!!1,!!2,!!3,!!4>)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        public Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> Curry12<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> fn)
        {
            Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> result
               = FunctionExtensions.Curry<T1, T2, T3, T4, T5, T6, T7>(fn);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Curry12(Action`7<!!0,!!1,!!2,!!3,!!4,!!5,!!6>)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        public Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, TResult>>>>>> Curry04<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> fn)
        {
            Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, TResult>>>>>> result
               = FunctionExtensions.Curry<T1, T2, T3, T4, T5, T6, TResult>(fn);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Curry04(Func`7<!!0,!!1,!!2,!!3,!!4,!!5,!!6>)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        public Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> Curry03<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> fn)
        {
            Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> result
               = FunctionExtensions.Curry<T1, T2, T3, T4, T5, TResult>(fn);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Curry03(Func`6<!!0,!!1,!!2,!!3,!!4,!!5>)
        }
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        public Func<T1, T2> Compose01<T1, T2>(Func<T1, T2> first, Func<T1, T1> second)
        {
            Func<T1, T2> result = FunctionExtensions.Compose<T1, T2>(first, second);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Compose01(Func`2<!!0,!!1>, Func`2<!!0,!!0>)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        public Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, TResult>>>>>>>> Curry06<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> fn)
        {
            Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, TResult>>>>>>>> result
               = FunctionExtensions.Curry<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(fn);
            return result;
            // TODO: add assertions to method FunctionExtensionsTest.Curry06(Func`9<!!0,!!1,!!2,!!3,!!4,!!5,!!6,!!7,!!8>)
        }
    }
}
