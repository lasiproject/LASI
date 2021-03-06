﻿using System.Collections.Generic;
using LASI.Testing.Assertions;
using NFluent;
using Xunit;

namespace LASI.Core.Tests
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
            var target = CreatePrepositionPhrase();
            ILexical prepositionalObject = new VerbPhrase(new BaseVerb("have"));
            target.BindObject(prepositionalObject);
            Assert.True(target.BoundObject == prepositionalObject);
        }

        /// <summary>
        /// A test for BindObject
        /// </summary>
        [Fact]
        public void BindObjectTest()
        {
            var target = new PrepositionalPhrase(new Preposition("on"));
            ILexical prepositionalObject = new NounPhrase(new Determiner("the"), new CommonSingularNoun("table"));
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
            var target = new PrepositionalPhrase(new Preposition("for"));
            ILexical expected = new NounPhrase(new PersonalPronoun("it"));
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        /// A test for OnRightSide
        /// </summary>
        [Fact]
        public void OnRightSideTest()
        {
            var target = new PrepositionalPhrase(new Preposition("for"));
            ILexical expected = new VerbPhrase(new PresentParticiple("slamming"));
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        /// A test for PrepositionalPhrase Constructor
        /// </summary>
        [Fact]
        public void PrepositionalPhraseConstructorTest()
        {
            var composedWords = new[] { new Preposition("on") };
            var target = new PrepositionalPhrase(composedWords);

            Check.That(target.Words).HasSize(composedWords.Length);
            Check.That(target).Satisfies(() =>
                target.Text is "on" && target.ToTheLeftOf is null && target.ToTheRightOf is null && target.BoundObject is null
            );
        }

        /// <summary>
        /// A test for PrepositionalRole
        /// </summary>
        [Fact]
        public void PrepositionalRoleTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            var target = new PrepositionalPhrase(composedWords);
            var expected = PrepositionRole.Undetermined;
            PrepositionRole actual = target.PrepositionRole;
            Assert.Equal(expected, actual);
        }
        /// <summary>
        /// A test for Role
        /// </summary>
        [Fact]
        public void RoleTest()
        {
            var target = new PrepositionalPhrase(new Preposition("on"));

            var expected = PrepositionRole.LocationOrScopeSpecifier;
            PrepositionRole actual;
            target.PrepositionRole = expected;
            actual = target.PrepositionRole;
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            Phrase.VerboseOutput = true;

            var target = new PrepositionalPhrase(new Preposition("for"));
            var left = new NounPhrase(new PersonalPronoun("it"));
            var right = new VerbPhrase(new PresentParticiple("slamming"));
            target.ToTheLeftOf = left;
            target.ToTheRightOf = right;
            var expected = $"PrepositionalPhrase \"for\"\n\tleft linked: {left.Text}\n\tright linked: {right.Text}";

            var actual = target.ToString();

            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        /// A test for ToTheLeftOf
        /// </summary>
        [Fact]
        public void ToTheLeftOfTest()
        {
            var target = new PrepositionalPhrase(new Preposition("on"));
            ILexical expected = new NounPhrase(new Determiner("the"), new CommonSingularNoun("table"));
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
            var target = new PrepositionalPhrase(new Preposition("on"));
            ILexical expected = new ThirdPersonSingularPresentVerb("is");
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Assert.Equal(expected, actual);
        }

        static PrepositionalPhrase CreatePrepositionPhrase() => new PrepositionalPhrase(new Preposition("on"));
    }
}
