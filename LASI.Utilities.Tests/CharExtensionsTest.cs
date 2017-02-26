using System;
using System.Linq;
namespace LASI.Utilities.Tests
{
    using Xunit;
    using static System.Char;
    public class CharExtensionsTest
    {
        private const string Consonants = "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ";
        private const string Vowels = "aeiouyAEIOUY";
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        [Fact]
        public void IsConsonantTest()
        {
            foreach (var c in Consonants)
            {
                Assert.True(c.IsConsonant());
            }
        }

        [Fact]
        public void IsVowelTest()
        {
            foreach (var c in Vowels)
            {
                Assert.True(c.IsVowel());
            }
        }

        [Fact]
        public void IsEnglishLetterTest()
        {
            for (var c = MinValue; c < MaxValue; ++c)
            {
                if (Alphabet.Contains(c))
                {
                    Assert.True(c.IsEnglishLetter());
                }
                else
                {
                    Assert.False(c.IsEnglishLetter());
                }
            }
        }
        [Fact]
        public void IsUpperTest()
        {
            for (var c = MinValue; c < MaxValue; ++c)
            {
                Assert.Equal(c.IsUpper(), IsUpper(c));
            }
        }
        [Fact]
        public void IsLowerTest()
        {
            for (var c = MinValue; c < MaxValue; ++c)
            {
                Assert.Equal(c.IsLower(), IsLower(c));
            }
        }

        [Fact]
        public void EqualsIgnoreCaseTest()
        {
            var lowerCaseLetters = Alphabet.Take(26);
            foreach (var lower in lowerCaseLetters)
            {
                var upper = ToUpper(lower);
                Assert.True(upper.EqualsIgnoreCase(lower));
                Assert.True(lower.EqualsIgnoreCase(upper));
                Assert.True(upper.EqualsIgnoreCase(upper));
                Assert.True(lower.EqualsIgnoreCase(lower));
                Assert.Equal(50, Alphabet.Count(c => !c.EqualsIgnoreCase(lower)));
                Assert.Equal(2, Alphabet.Count(c => c.EqualsIgnoreCase(lower)));
                Assert.Equal(50, Alphabet.Count(c => !c.EqualsIgnoreCase(upper)));
                Assert.Equal(2, Alphabet.Count(c => c.EqualsIgnoreCase(upper)));
            }
            var random = new Random();
            Func<int> rand = () => random.Next(MinValue, MaxValue + 1);
            var randomCharacters = Enumerable.Range(0, int.MaxValue)
                .Select(n => (char)rand())
                .Where(c => !(c.IsEnglishLetter() || ToUpper(c).Equals(c)))
                .Take(100);
            foreach (var character in randomCharacters)
            {
                Assert.False(character.EqualsIgnoreCase(character.ToUpper()), $"{character}({character}) IS equal to {character.ToUpper()}({character.ToUpper()})");
            }
        }
    }
}
