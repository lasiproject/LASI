using LASI;
using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using NFluent;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is A test class for PronounTest and is intended
    ///to contain all PronounTest Unit Tests
    /// </summary>
    public class PersonalPronounTest
    {
        /// <summary>
        ///A test for PersonalPronoun Constructor
        /// </summary>
        [Fact]
        public void PronounConstructorTest()
        {
            var text = "him";
            var target = new PersonalPronoun(text);
            Assert.True(target.Text == text, "Text property value correctly initialized via parameter");
            //Assert.IsTrue(from.BoundEntity == null,"Bound Entity property was initialized to NULL");
        }

        /// <summary>
        ///A test for BindPronoun
        /// </summary>
        [Fact]
        public void BindPronounTest()
        {
            var text = "him";
            var target = new PersonalPronoun(text);
            Pronoun pro = new PersonalPronoun("them");
            target.BindReferencer(pro);
            Check.That(target.Referencers).Contains(pro);
        }

        /// <summary>
        ///A test for Equals
        /// </summary>
        [Fact]
        public void EqualsTest()
        {
            var text = "her";
            var target = new PersonalPronoun(text);
            var obj = target as object;
            var expected = true;
            bool actual;
            actual = target.Equals(obj);
            Check.That(actual).IsEqualTo(expected);
        }





        /// <summary>
        ///A test for BoundEntity
        /// </summary>
        [Fact]
        public void BoundEntityTest()
        {
            var text = "him";
            var target = new PersonalPronoun(text);
            IEntity expected = new ProperSingularNoun("Aluan");
            IAggregateEntity actual;
            target.BindAsReferringTo(expected);
            actual = target.RefersTo;
            Check.That(actual).Contains(expected);
        }

        /// <summary>
        ///A test for DirectObjectOf
        /// </summary>
        [Fact]
        public void DirectObjectOfTest()
        {
            var text = "him";
            var target = new PersonalPronoun(text);
            IVerbal expected = new PastTenseVerb("frightened");
            target.BindAsDirectObjectOf(expected);
            var actual = target.DirectObjectOf;
            Check.That(actual.Match((IVerbal x) => x.Text)).IsEqualTo(expected.Text);

        }

        /// <summary>
        ///A test for IndirectObjectOf
        /// </summary>
        [Fact]
        public void IndirectObjectOfTest()
        {
            var text = "him";
            var target = new PersonalPronoun(text);
            var verbal = new PastTenseVerb("frightened");

            target.BindAsIndirectObjectOf(verbal);
            var actual = target.IndirectObjectOf;
            Check.That(actual).IsEqualTo(verbal);
        }

        /// <summary>
        ///A test for IndirectReferences
        /// </summary>
        [Fact]
        public void IndirectReferencesTest()
        {
            var text = "he";
            var target = new PersonalPronoun(text);
            Pronoun referencer = new PersonalPronoun("himslef");
            target.BindReferencer(referencer);
            Check.That(target.Referencers).Contains(referencer);
        }

        /// <summary>
        ///A test for SubjectOf
        /// </summary>
        [Fact]
        public void SubjectOfTest()
        {
            var text = "him";
            var target = new PersonalPronoun(text);
            IVerbal expected = new PastTenseVerb("frightened");
            target.BindAsSubjectOf(expected);
            var actual = target.SubjectOf;
            Check.That(actual).IsEqualTo(expected);

        }

        /// <summary>
        ///A test for EntityKind
        /// </summary>
        [Fact]
        public void EntityKindTest()
        {
            var text = "they";
            var target = new PersonalPronoun(text);
            EntityKind actual;
            actual = target.EntityKind;
            Check.That(EntityKind.Undefined).IsEqualTo(actual);
            target.BindAsReferringTo(new NounPhrase(new[] { new CommonPluralNoun("apples") }));
            Check.That(EntityKind.ThingMultiple).IsEqualTo(target.EntityKind);
        }

        /// <summary>
        ///A test for PersonalPronoun Constructor
        /// </summary>
        [Fact]
        public void PersonalPronounConstructorTest()
        {
            var text = "her";
            var target = new PersonalPronoun(text);
            Check.That(target.Text).IsEqualTo(text);
            Check.That(target.IsFemale()).IsTrue();
            Check.That(target.IsThirdPerson()).IsTrue();
            Check.That(target.EntityKind).IsEqualTo(EntityKind.PersonFemale);
        }
    }
}
