using System;
using System.Linq;
namespace LASI.Utilities.Tests
{
    using NFluent;
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

        [Theory]
        [
            InlineData(0), InlineData(1), InlineData(2), InlineData(3), InlineData(4), InlineData(5),
            InlineData(6), InlineData(7), InlineData(8), InlineData(9), InlineData(10), InlineData(11),
            InlineData(12), InlineData(13), InlineData(14), InlineData(15), InlineData(16), InlineData(17),
            InlineData(18), InlineData(19), InlineData(20), InlineData(21), InlineData(22), InlineData(23),
            InlineData(24), InlineData(25)
        ]
        public void EqualsIgnoreCaseTest(char lowerIndex)
        {
            var lower = Alphabet[lowerIndex];
            var upper = ToUpper(lower);
            Assert.True(upper.EqualsIgnoreCase(lower));
            Assert.True(lower.EqualsIgnoreCase(upper));
            Assert.True(upper.EqualsIgnoreCase(upper));
            Assert.True(lower.EqualsIgnoreCase(lower));
            Check.That(50).IsEqualTo(Alphabet.Count(c => !c.EqualsIgnoreCase(lower)));
            Check.That(2).IsEqualTo(Alphabet.Count(c => c.EqualsIgnoreCase(lower)));
            Check.That(50).IsEqualTo(Alphabet.Count(c => !c.EqualsIgnoreCase(upper)));
            Check.That(2).IsEqualTo(Alphabet.Count(c => c.EqualsIgnoreCase(upper)));
        }

        [Fact]
        public void EqualsIgnoreCaseTest2(char lowerIndex)
        {
            var random = new Random();
            int rand() => random.Next(MinValue, MaxValue + 1);
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
