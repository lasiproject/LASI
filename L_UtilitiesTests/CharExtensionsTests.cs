using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LASI.Utilities.Tests
{
    [TestClass()]
    public class CharExtensionsTests
    {
        [TestMethod()]
        public void IsConsonantTest() {
            foreach (var c in "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ") {
                Assert.IsTrue(c.IsConsonant());
            }

        }

        [TestMethod()]
        public void IsVowelTest() {
            foreach (var c in "aeiouyAEIOUY") {
                Assert.IsTrue(c.IsVowel());
            }
        }

        [TestMethod()]
        public void IsEnglishLetterTest() {
            for (var c = char.MinValue; c < char.MaxValue; ++c) {
                if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(c)) {
                    Assert.IsTrue(c.IsEnglishLetter());
                }
                else {
                    Assert.IsFalse(c.IsEnglishLetter());
                }
            }
        }
    }
}
