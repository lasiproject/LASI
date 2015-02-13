using System.Collections.Generic;
// <copyright file="EnumerableExtensionsTest.cs">Copyright ©  2013</copyright>

using System;
using LASI.Utilities;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Utilities
{
    [TestClass]
    [PexClass(typeof(EnumerableExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class EnumerableExtensionsTest
    {
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public int Product03(IEnumerable<int> source)
        {
            int result = EnumerableExtensions.Product(source);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Product03(IEnumerable`1<Int32>)
        }
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        public TAccumulate Aggregate01<TSource, TAccumulate>(
            IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, int, TAccumulate> func
        )
        {
            TAccumulate result = EnumerableExtensions.Aggregate<TSource, TAccumulate>(source, seed, func);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Aggregate01(IEnumerable`1<!!0>, !!1, Func`4<!!1,!!0,Int32,!!1>)
        }
        [PexGenericArguments(typeof(int), typeof(int), typeof(int))]
        [PexMethod]
        public TResult Aggregate02<TSource, TAccumulate, TResult>(
            IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, int, TAccumulate> func,
            Func<TAccumulate, TResult> resultSelector
        )
        {
            TResult result = EnumerableExtensions.Aggregate<TSource, TAccumulate, TResult>
                                 (source, seed, func, resultSelector);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Aggregate02(IEnumerable`1<!!0>, !!1, Func`4<!!1,!!0,Int32,!!1>, Func`2<!!1,!!2>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public TSource Aggregate<TSource>(IEnumerable<TSource> source, Func<TSource, TSource, int, TSource> func)
        {
            TSource result = EnumerableExtensions.Aggregate<TSource>(source, func);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Aggregate(IEnumerable`1<!!0>, Func`4<!!0,!!0,Int32,!!0>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<TSource> Append<TSource>(IEnumerable<TSource> head, TSource tail)
        {
            IEnumerable<TSource> result = EnumerableExtensions.Append<TSource>(head, tail);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Append(IEnumerable`1<!!0>, !!0)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public Tuple<IEnumerable<T>, IEnumerable<T>> Bisect<T>(IEnumerable<T> source, Func<T, bool> predicate)
        {
            Tuple<IEnumerable<T>, IEnumerable<T>> result = EnumerableExtensions.Bisect<T>(source, predicate);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Bisect(IEnumerable`1<!!0>, Func`2<!!0,Boolean>)
        }
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        public Tuple<TResult, TResult> Bisect01<T, TResult>(
            IEnumerable<T> source,
            Func<T, bool> predicate,
            Func<IEnumerable<T>, TResult> resultSelector
        )
        {
            Tuple<TResult, TResult> result
               = EnumerableExtensions.Bisect<T, TResult>(source, predicate, resultSelector);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Bisect01(IEnumerable`1<!!0>, Func`2<!!0,Boolean>, Func`2<IEnumerable`1<!!0>,!!1>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> EmptyIfNull<T>(IEnumerable<T> source)
        {
            IEnumerable<T> result = EnumerableExtensions.EmptyIfNull<T>(source);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.EmptyIfNull(IEnumerable`1<!!0>)
        }
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public IEnumerable<TSource> ExceptBy<TSource, TKey>(
            IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            Func<TSource, TKey> selector
        )
        {
            IEnumerable<TSource> result = EnumerableExtensions.ExceptBy<TSource, TKey>(first, second, selector);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.ExceptBy(IEnumerable`1<!!0>, IEnumerable`1<!!0>, Func`2<!!0,!!1>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod(MaxRunsWithoutNewTests = 200)]
        [PexAllowedException(typeof(ArgumentNullException))]
        [PexAllowedException(typeof(ArgumentOutOfRangeException))]
        public string Format01<T>(IEnumerable<T> source, long lineLength)
        {
            string result = EnumerableExtensions.Format<T>(source, lineLength);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Format01(IEnumerable`1<!!0>, Int64)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public IEnumerable<Pair<T, int>> WithIndex<T>(IEnumerable<T> source)
        {
            IEnumerable<Pair<T, int>> result = EnumerableExtensions.WithIndex<T>(source);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.WithIndex(IEnumerable`1<!!0>)
        }
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public bool Product07(IEnumerable<bool> source)
        {
            bool result = EnumerableExtensions.Product(source);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Product07(IEnumerable`1<Boolean>)
        }
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public float Product06(IEnumerable<float> source)
        {
            float result = EnumerableExtensions.Product(source);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Product06(IEnumerable`1<Single>)
        }
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        [PexAllowedException(typeof(OverflowException))]
        public decimal Product04(IEnumerable<decimal> source)
        {
            decimal result = EnumerableExtensions.Product(source);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Product04(IEnumerable`1<Decimal>)
        }
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public long Product02(IEnumerable<long> source)
        {
            long result = EnumerableExtensions.Product(source);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Product02(IEnumerable`1<Int64>)
        }
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod(Timeout = 240, MaxConstraintSolverTime = 2)]
        [PexAllowedException(typeof(ArgumentNullException))]
        [PexAllowedException(typeof(ArgumentException))]
        public TSource MinBy01<TSource, TKey>(
            IEnumerable<TSource> source,
            Func<TSource, TKey> selector,
            IComparer<TKey> comparer
        )
        {
            TSource result = EnumerableExtensions.MinBy<TSource, TKey>(source, selector, comparer);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.MinBy01(IEnumerable`1<!!0>, Func`2<!!0,!!1>, IComparer`1<!!1>)
        }
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public bool SetEqualBy<TSource, TKey>(
            IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            Func<TSource, TKey> selector
        )
        {
            bool result = EnumerableExtensions.SetEqualBy<TSource, TKey>(first, second, selector);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.SetEqualBy(IEnumerable`1<!!0>, IEnumerable`1<!!0>, Func`2<!!0,!!1>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public bool SetEqual<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            bool result = EnumerableExtensions.SetEqual<T>(first, second);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.SetEqual(IEnumerable`1<!!0>, IEnumerable`1<!!0>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public bool SetEqual01<T>(
            IEnumerable<T> first,
            IEnumerable<T> second,
            IEqualityComparer<T> comparer
        )
        {
            bool result = EnumerableExtensions.SetEqual<T>(first, second, comparer);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.SetEqual01(IEnumerable`1<!!0>, IEnumerable`1<!!0>, IEqualityComparer`1<!!0>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> Scan<T>(IEnumerable<T> source, Func<T, T, T> func)
        {
            IEnumerable<T> result = EnumerableExtensions.Scan<T>(source, func);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Scan(IEnumerable`1<!!0>, Func`3<!!0,!!0,!!0>)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public IEnumerable<TResult> Zip<TFirst, TSecond, TThird, TResult>(
            IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            IEnumerable<TThird> third,
            Func<TFirst, TSecond, TThird, TResult> selector
        )
        {
            IEnumerable<TResult> result
               = EnumerableExtensions.Zip<TFirst, TSecond, TThird, TResult>(first, second, third, selector);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Zip(IEnumerable`1<!!0>, IEnumerable`1<!!1>, IEnumerable`1<!!2>, Func`4<!!0,!!1,!!2,!!3>)
        }
        [PexGenericArguments(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public IEnumerable<TResult> Zip01<T1, T2, T3, T4, TResult>(
            IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth,
            Func<T1, T2, T3, T4, TResult> selector
        )
        {
            IEnumerable<TResult> result
               = EnumerableExtensions.Zip<T1, T2, T3, T4, TResult>(first, second, third, fourth, selector);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.Zip01(IEnumerable`1<!!0>, IEnumerable`1<!!1>, IEnumerable`1<!!2>, IEnumerable`1<!!3>, Func`5<!!0,!!1,!!2,!!3,!!4>)
        }
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public IDictionary<TKey, Pair<TValue, int>> WithIndex01<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
        {
            IDictionary<TKey, Pair<TValue, int>> result
               = EnumerableExtensions.WithIndex<TKey, TValue>(dictionary);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.WithIndex01(IDictionary`2<!!0,!!1>)
        }
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        [PexAllowedException(typeof(ArgumentException))]
        public TSource MaxBy01<TSource, TKey>(
            IEnumerable<TSource> source,
            Func<TSource, TKey> selector,
            IComparer<TKey> comparer
        )
        {
            TSource result = EnumerableExtensions.MaxBy<TSource, TKey>(source, selector, comparer);
            return result;
            // TODO: add assertions to method EnumerableExtensionsTest.MaxBy01(IEnumerable`1<!!0>, Func`2<!!0,!!1>, IComparer`1<!!1>)
        }
    }
}
