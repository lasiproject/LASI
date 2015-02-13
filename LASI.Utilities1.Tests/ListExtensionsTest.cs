using System.Collections.Generic;
// <copyright file="ListExtensionsTest.cs">Copyright ©  2013</copyright>

using System;
using LASI.Utilities.Specialized.Enhanced.Linq.List;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Utilities.Specialized.Enhanced.Linq.List
{
    [TestClass]
    [PexClass(typeof(ListExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ListExtensionsTest
    {
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod(MaxConditions = 1000, MaxRunsWithoutNewTests = 200)]
        [PexAllowedException(typeof(ArgumentNullException))]
        public List<R> Select<T, R>(List<T> list, Func<T, R> selector)
        {
            List<R> result = ListExtensions.Select<T, R>(list, selector);
            return result;
            // TODO: add assertions to method ListExtensionsTest.Select(List`1<!!0>, Func`2<!!0,!!1>)
        }
        [PexGenericArguments(typeof(int), typeof(int), typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public List<R> SelectMany01<T, TCollection, R>(
            List<T> list,
            Func<T, IEnumerable<TCollection>> collectionSelector,
            Func<T, TCollection, R> resultSelector
        )
        {
            List<R> result
               = ListExtensions.SelectMany<T, TCollection, R>(list, collectionSelector, resultSelector);
            return result;
            // TODO: add assertions to method ListExtensionsTest.SelectMany01(List`1<!!0>, Func`2<!!0,IEnumerable`1<!!1>>, Func`3<!!0,!!1,!!2>)
        }
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public List<R> SelectMany<T, R>(List<T> list, Func<T, IEnumerable<R>> selector)
        {
            List<R> result = ListExtensions.SelectMany<T, R>(list, selector);
            return result;
            // TODO: add assertions to method ListExtensionsTest.SelectMany(List`1<!!0>, Func`2<!!0,IEnumerable`1<!!1>>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public List<T> Skip<T>(List<T> source, int count)
        {
            List<T> result = ListExtensions.Skip<T>(source, count);
            return result;
            // TODO: add assertions to method ListExtensionsTest.Skip(List`1<!!0>, Int32)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public List<T> Take<T>(List<T> source, int count)
        {
            List<T> result = ListExtensions.Take<T>(source, count);
            return result;
            // TODO: add assertions to method ListExtensionsTest.Take(List`1<!!0>, Int32)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public List<T> Where<T>(List<T> list, Func<T, bool> predicate)
        {
            List<T> result = ListExtensions.Where<T>(list, predicate);
            return result;
            // TODO: add assertions to method ListExtensionsTest.Where(List`1<!!0>, Func`2<!!0,Boolean>)
        }
    }
}
