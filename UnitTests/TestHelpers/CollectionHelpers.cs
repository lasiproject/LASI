using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI;
using LASI.Core;
using LASI.Core.DocumentStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.UnitTests.TestHelpers
{
    static class AssertHelper
    {
        public static void AreSequenceEqual<T>(IEnumerable<T> first, IEnumerable<T> second) {
            Assert.IsTrue(first.SequenceEqual(second));
        }
        //public static void AreSequenceEqual<T>(IEnumerable<T> first, IEnumerable<T> second, string message) {
        //    try {
        //        Assert.IsTrue(first.SequenceEqual(second));
        //    }
        //    catch (AssertFailedException x) {
        //        throw new AssertFailedException(message, x);
        //    }
        //}
    }
}
