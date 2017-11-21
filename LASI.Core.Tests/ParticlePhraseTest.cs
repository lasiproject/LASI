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
            var target = new ParticlePhrase(new Particle("about"));
            ILexical prepositionalObject = new NounPhrase(new Determiner("the"), new CommonSingularNoun("house"));
            target.BindObject(prepositionalObject);
            Check.That(target.BoundObject).IsEqualTo(prepositionalObject);
        }

        /// <summary>
        ///A test for OnLeftSide
        /// </summary>
        [Fact]
        public void OnLeftSideTest()
        {
            var target = new ParticlePhrase(new Particle("away"));
            ILexical expected = new PastTenseVerb("gave");
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for OnRightSide
        /// </summary>
        [Fact]
        public void OnRightSideTest()
        {
            var target = new ParticlePhrase(new Particle("away"));
            ILexical expected = new Preposition("for");
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Check.That(actual).IsEqualTo(expected);
        }



        /// <summary>
        ///A test for ToTheRightOf
        /// </summary>
        [Fact]
        public void ToTheRightOfTest()
        {
            var target = new ParticlePhrase(new Particle("off"), new Preposition("of"));
            ILexical expected = new NounPhrase(new Determiner("the"), new CommonSingularNoun("world"));
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for ToTheLeftOf
        /// </summary>
        [Fact]
        public void ToTheLeftOfTest()
        {
            var target = new ParticlePhrase(new Particle("off"), new Preposition("of"));
            ILexical expected = new PronounPhrase(new PersonalPronoun("they"));
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Role
        /// </summary>
        [Fact]
        public void RoleTest()
        {
            var target = new ParticlePhrase(new Particle("off"), new Preposition("of"));
            var expected = PrepositionRole.Undetermined;
            PrepositionRole actual;
            target.Role = expected;
            actual = target.Role;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for BindObject
        /// </summary>
        [Fact]
        public void BindObjectTest()
        {
            var target = new ParticlePhrase(new Particle("off"), new Preposition("of"));
            ILexical prepositionalObject = new NounPhrase(new Determiner("the"), new CommonSingularNoun("world"));
            target.BindObject(prepositionalObject);
            Check.That(target.BoundObject).IsEqualTo(prepositionalObject);
            IVerbal linkedVerbal = new VerbPhrase(new PastTenseVerb("jumped"));
            linkedVerbal.AttachObjectViaPreposition(target);
            Check.That(linkedVerbal.ObjectOfThePreposition).IsEqualTo(prepositionalObject);
        }

    }
}
