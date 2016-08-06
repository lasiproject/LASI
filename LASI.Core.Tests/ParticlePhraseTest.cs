using System.Collections.Generic;
using System.Linq;
using NFluent;
using Xunit;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is A test class for ParticlePhraseTest and is intended
    ///to contain all ParticlePhraseTest Unit Tests
    /// </summary>
    public class ParticlePhraseTest
    {
        /// <summary>
        ///A test for ParticlePhrase Constructor
        /// </summary>
        [Fact]
        public void ParticlePhraseConstructorTest()
        {
            var composedWords = new[] { new Particle("away") };
            var target = new ParticlePhrase(composedWords);
            Check.That(from w1 in composedWords
                       join w2 in composedWords on w1 equals w2
                       select new
                       {
                           w1,
                           w2
                       }).HasSize(composedWords.Length);
        }

        /// <summary>
        ///A test for BindObjectOfPreposition
        /// </summary>
        [Fact]
        public void BindObjectOfPrepositionTest()
        {
            IEnumerable<Word> composedWords = new[] { new Particle("about") };
            var target = new ParticlePhrase(composedWords);
            ILexical prepositionalObject = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("house") });
            target.BindObject(prepositionalObject);
            Check.That(target.BoundObject).IsEqualTo(prepositionalObject);
        }

        /// <summary>
        ///A test for OnLeftSide
        /// </summary>
        [Fact]
        public void OnLeftSideTest()
        {
            IEnumerable<Word> composedWords = new[] { new Particle("away") };
            var target = new ParticlePhrase(composedWords);
            ILexical expected = new PastTenseVerb("gave");
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for OnRightSide
        /// </summary>
        [Fact]
        public void OnRightSideTest()
        {
            IEnumerable<Word> composedWords = new[] { new Particle("away") };
            var target = new ParticlePhrase(composedWords);
            ILexical expected = new Preposition("for");
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Check.That(expected).IsEqualTo(actual);
        }



        /// <summary>
        ///A test for ToTheRightOf
        /// </summary>
        [Fact]
        public void ToTheRightOfTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new Particle("off"), new Preposition("of") };
            var target = new ParticlePhrase(composedWords);
            ILexical expected = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("world") });
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for ToTheLeftOf
        /// </summary>
        [Fact]
        public void ToTheLeftOfTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new Particle("off"), new Preposition("of") };
            var target = new ParticlePhrase(composedWords);
            ILexical expected = new PronounPhrase(new[] { new PersonalPronoun("they") });
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for Role
        /// </summary>
        [Fact]
        public void RoleTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new Particle("off"), new Preposition("of") };
            var target = new ParticlePhrase(composedWords);
            var expected = PrepositionRole.Undetermined;
            PrepositionRole actual;
            target.Role = expected;
            actual = target.Role;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for BindObject
        /// </summary>
        [Fact]
        public void BindObjectTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new Particle("off"), new Preposition("of") };
            var target = new ParticlePhrase(composedWords);
            ILexical prepositionalObject = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("world") });
            target.BindObject(prepositionalObject);
            Check.That(target.BoundObject).IsEqualTo(prepositionalObject);
            IVerbal linkedVerbal = new VerbPhrase(new PastTenseVerb("jumped"));
            linkedVerbal.AttachObjectViaPreposition(target);
            Check.That(linkedVerbal.ObjectOfThePreposition).IsEqualTo(prepositionalObject);
        }


    }
}
