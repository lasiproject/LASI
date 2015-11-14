using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LASI.Core.Testss
{
    /// <summary>
    ///This is A test class for PrepositionalPhraseTest and is intended
    ///to contain all PrepositionalPhraseTest Unit Tests
    /// </summary>
    public class PrepositionalPhraseTest
    {
        /// <summary>
        /// A test for BindObjectOfPreposition
        /// </summary>
        [Fact]
        public void BindObjectOfPrepositionTest()
        {
            PrepositionalPhrase target = CreatePrepositionPhrase();
            ILexical prepositionalObject = new VerbPhrase(new Word[] { new BaseVerb("have") });
            target.BindObject(prepositionalObject);
            Assert.True(target.BoundObject == prepositionalObject);
        }

        /// <summary>
        /// A test for BindObject
        /// </summary>
        [Fact]
        public void BindObjectTest()
        {
            var composedWords = new[] { new Preposition("on") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            ILexical prepositionalObject = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("table") });
            target.BindObject(prepositionalObject);
            Assert.Equal(prepositionalObject, target.BoundObject);
            IVerbal verbal = new ThirdPersonSingularPresentVerb("is");
            verbal.AttachObjectViaPreposition(target);
            Assert.Equal(prepositionalObject, verbal.ObjectOfThePreposition);
        }

        /// <summary>
        /// A test for OnLeftSide
        /// </summary>
        [Fact]
        public void OnLeftSideTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            ILexical expected = new NounPhrase(new[] { new PersonalPronoun("it") });
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// A test for OnRightSide
        /// </summary>
        [Fact]
        public void OnRightSideTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            ILexical expected = new VerbPhrase(new[] { new PresentParticiple("slamming") });
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// A test for PrepositionalPhrase Constructor
        /// </summary>
        [Fact]
        public void PrepositionalPhraseConstructorTest()
        {
            var composedWords = new[] { new Preposition("on") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);

            Assert.True(target.Words.Count() == composedWords.Count());
            Assert.True(target.Text == "on" && target.ToTheLeftOf == null && target.ToTheRightOf == null && target.BoundObject == null);
        }

        /// <summary>
        /// A test for PrepositionalRole
        /// </summary>
        [Fact]
        public void PrepositionalRoleTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            PrepositionRole expected = PrepositionRole.Undetermined;
            PrepositionRole actual;
            actual = target.Role;
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// A test for Role
        /// </summary>
        [Fact]
        public void RoleTest()
        {
            var composedWords = new[] { new Preposition("on") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);

            PrepositionRole expected = PrepositionRole.SpatialSpecifier;
            PrepositionRole actual;
            target.Role = expected;
            actual = target.Role;
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            Phrase.VerboseOutput = true;
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            target.ToTheLeftOf = new NounPhrase(new[] { new PersonalPronoun("it") });
            target.ToTheRightOf = new VerbPhrase(new[] { new PresentParticiple("slamming") });
            string expected = string.Format("PrepositionalPhrase \"for\"\n\tleft linked: {0}\n\tright linked: {1}", target.ToTheLeftOf, target.ToTheRightOf);
            string actual;
            actual = target.ToString();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// A test for ToTheLeftOf
        /// </summary>
        [Fact]
        public void ToTheLeftOfTest()
        {
            var composedWords = new[] { new Preposition("on") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            ILexical expected = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("table") });
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// A test for ToTheRightOf
        /// </summary>
        [Fact]
        public void ToTheRightOfTest()
        {
            var composedWords = new[] { new Preposition("on") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            ILexical expected = new ThirdPersonSingularPresentVerb("is");
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Assert.Equal(expected, actual);
        }

        private static PrepositionalPhrase CreatePrepositionPhrase()
        {
            var composedWords = new[] { new Preposition("on") };
            return new PrepositionalPhrase(composedWords);
        }
    }
}