﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LASI.Utilities.Tests
{
    [TestClass]
    public class CharExtensionsTests
    {
        private const string CONSONANTS = "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ";
        private const string VOWELS = "aeiouyAEIOUY";
        private const string ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        [TestMethod]
        public void IsConsonantTest() {
            foreach (var c in CONSONANTS) {
                Assert.IsTrue(c.IsConsonant());
            }
        }

        [TestMethod]
        public void IsVowelTest() {
            foreach (var c in VOWELS) {
                Assert.IsTrue(c.IsVowel());
            }
        }

        [TestMethod]
        public void IsEnglishLetterTest() {
            for (var c = char.MinValue; c < char.MaxValue; ++c) {
                if (ALPHABET.Contains(c)) {
                    Assert.IsTrue(c.IsAlphabetic());
                } else {
                    Assert.IsFalse(c.IsAlphabetic());
                }
            }
        }
        [TestMethod]
        public void IsUpperTest() {
            for (char c = char.MinValue; c < char.MaxValue; ++c) { Assert.AreEqual(c.IsUpper(), char.IsUpper(c)); }
        }
        [TestMethod]
        public void IsLowerTest() {
            for (char c = char.MinValue; c < char.MaxValue; ++c) { Assert.AreEqual(c.IsLower(), char.IsLower(c)); }
        }
    }
}
