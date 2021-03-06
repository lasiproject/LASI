﻿using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using NFluent;
using Fact = Xunit.FactAttribute;
using LASI.Utilities;
using static LASI.Utilities.EnumerableExtensions;

namespace LASI.Core.Tests
{
    //using Tripple = ValueTuple<Word, Word, Word>;
    /// <summary>
    ///This is A test class for VerbPhraseTest and is intended
    ///to contain all VerbPhraseTest Unit Tests
    /// </summary>
    public class VerbPhraseTest
    {
        /// <summary>
        ///A test for AdjectivalModifier
        /// </summary>
        [Fact]
        public void AdjectivalModifierTest()
        {
            var target = new VerbPhrase(new PastTenseVerb("grew"));
            IEntity subject = new PersonalPronoun("he");
            target.BindSubject(subject);
            IDescriptor expected = new Adjective("tall");
            IDescriptor actual;
            target.PostpositiveDescriptor = expected;
            actual = target.PostpositiveDescriptor;
            Check.That(actual).IsEqualTo(expected);
            Check.That(subject.Descriptors).Contains(expected);
        }
        /// <summary>
        ///A test for AggregateDirectObject
        /// </summary>
        [Fact]
        public void AggregateDirectObjectTest()
        {
            var target = CreateVerbPhrase();
            IAggregateEntity directObject = new AggregateEntity(
                new NounPhrase(new ProperSingularNoun("John"), new ProperSingularNoun("Smith")),
                new NounPhrase(new PossessivePronoun("his"), new CommonPluralNoun("cats"))
            );
            target.BindDirectObject(directObject);
            IAggregateEntity actual;
            actual = target.AggregateDirectObject;
            Check.That(directObject.Except(actual)).IsEmpty();
        }
        /// <summary>
        ///A test for AggregateIndirectObject
        /// </summary>
        [Fact]
        public void AggregateIndirectObjectTest()
        {
            var target = CreateVerbPhrase();
            var indirectObject = new AggregateEntity(
                new NounPhrase(
                    new ProperSingularNoun("John"),
                    new ProperSingularNoun("Smith")
                ), new NounPhrase(
                    new PossessivePronoun("his"),
                    new CommonPluralNoun("cats")
                )
            );
            target.BindIndirectObject(indirectObject);
            IAggregateEntity actual;
            actual = target.AggregateIndirectObject;
            Check.That(actual.Except(indirectObject)).IsEmpty();
        }
        /// <summary>
        ///A test for AggregateSubject
        /// </summary>
        [Fact]
        public void AggregateSubjectTest()
        {
            var target = CreateVerbPhrase();
            var subject = new AggregateEntity(
                new NounPhrase(
                    new ProperSingularNoun("John"),
                    new ProperSingularNoun("Smith")
                ), new NounPhrase(
                    new PossessivePronoun("his"),
                    new CommonPluralNoun("cats")
                )
            );
            target.BindSubject(subject);
            IAggregateEntity actual;
            actual = target.AggregateSubject;
            Check.That(actual.Except(subject)).IsEmpty();
        }
        /// <summary>
        ///A test for AttachObjectViaPreposition
        /// </summary>
        [Fact]
        public void AttachObjectViaPrepositionTest()
        {
            var target = new VerbPhrase(new BaseVerb("consume"));
            IPrepositional prepositional = new Preposition("with");
            ILexical prepositionalObject = new NounPhrase(new Adjective("great"), new CommonSingularNoun("haste"));
            prepositional.BindObject(prepositionalObject);
            target.AttachObjectViaPreposition(prepositional);
            Check.That(target.ObjectOfThePreposition).IsEqualTo(prepositionalObject);
        }
        /// <summary>
        ///A test for BindDirectObject
        /// </summary>
        [Fact]
        public void BindDirectObjectTest()
        {
            var target = CreateVerbPhrase();
            IEntity directObject = new NounPhrase(new Determiner("the"), new CommonSingularNoun("forest"));
            target.BindDirectObject(directObject);
            Check.That(target.HasDirectObject(e => e == directObject)).IsTrue();
        }
        /// <summary>
        ///A test for BindIndirectObject
        /// </summary>
        [Fact]
        public void BindIndirectObjectTest()
        {
            var target = CreateVerbPhrase();
            IEntity indirectObject = new ProperSingularNoun("John");
            target.BindIndirectObject(indirectObject);
            Check.That(target.HasIndirectObject(e => e == indirectObject)).IsTrue();
        }
        /// <summary>
        ///A test for BindSubject
        /// </summary>
        [Fact]
        public void BindSubjectTest()
        {
            var target = CreateVerbPhraseHelp();
            IEntity subject = new CommonPluralNoun("cats");
            target.BindSubject(subject);
            Check.That(target.HasSubject(e => e == subject)).IsTrue();
        }
        /// <summary>
        ///A test for BoundSubject
        /// </summary>
        [Fact]
        public void BoundSubjectTest()
        {
            var target = CreateVerbPhrase();
            IEntity expected = new PersonalPronoun("he");
            IEntity actual;
            target.BindSubject(expected);
            actual = target.Subjects.First();
            Check.That(actual).IsEqualTo(expected);
        }
        /// <summary>
        ///A test for DirectObjects
        /// </summary>
        [Fact]
        public void DirectObjectsTest()
        {
            var target = CreateVerbPhrase();
            IEnumerable<IEntity> actual;
            actual = target.DirectObjects;
            Check.That(target.DirectObjects).IsEmpty();
            IEntity directObject = new NounPhrase(
                new Determiner("the"),
                new CommonSingularNoun("book")
            );
            target.BindDirectObject(directObject);
            Check.That(target.DirectObjects).Contains(directObject);
        }
        /// <summary>
        ///A test for HasDirectObject
        /// </summary>
        [Fact]
        public void HasDirectObjectTest()
        {
            var target = CreateVerbPhrase();
            IEntity entity = new CommonPluralNoun("cats");
            target.BindDirectObject(entity);

            var expected = true;
            bool actual;
            actual = target.HasDirectObject(predicate);
            Check.That(actual).IsEqualTo(expected);

            bool predicate(IEntity e) => e.Text == "cats";
        }
        /// <summary>
        ///A test for HasIndirectObject
        /// </summary>
        [Fact]
        public void HasIndirectObjectTest()
        {
            var target = CreateVerbPhraseHelp();
            IEntity entity = new CommonPluralNoun("cats");
            target.BindIndirectObject(entity);

            var expected = true;
            bool actual;
            actual = target.HasIndirectObject(predicate);
            Check.That(actual).IsEqualTo(expected);

            bool predicate(IEntity e) => e.Text == "cats";
        }
        /// <summary>
        ///A test for HasObject
        /// </summary>
        [Fact]
        public void HasObjectTest()
        {
            var target = CreateVerbPhraseHelp();
            IEntity entity = new CommonPluralNoun("cats");

            var expected = true;
            bool actual;
            target.BindDirectObject(entity);
            actual = target.HasObject(predicate);
            Check.That(actual).IsEqualTo(expected);
            target.BindIndirectObject(entity);
            actual = target.HasObject(predicate);
            Check.That(actual).IsEqualTo(expected);

            bool predicate(IEntity e) => e.Text == "cats";
        }
        [Fact]
        public void HasSubjectOrObjectRespectsDirectObjects()
        {
            var target = CreateVerbPhraseHelp();

            target.BindDirectObject(new CommonPluralNoun("cats"));

            Check.That(target.HasSubjectOrObject(e => e.Text == "cats")).IsTrue();
        }
        [Fact]
        public void HasSubjectOrObjectRespectsIndirectObjects()
        {
            var target = CreateVerbPhraseHelp();

            target.BindIndirectObject(new CommonPluralNoun("cats"));

            Check.That(target.HasSubjectOrObject(e => e.Text == "cats")).IsTrue();
        }
        /// <summary>
        ///A test for HasSubjectOrObject
        /// </summary>
        [Fact]
        public void HasSubjectOrObjectRespectsSubjects()
        {
            var target = CreateVerbPhraseHelp();

            target.BindSubject(new CommonPluralNoun("cats"));

            Check.That(target.HasSubjectOrObject(e => e.Text == "cats")).IsTrue();
        }
        /// <summary>
        ///A test for HasSubject
        /// </summary>
        [Fact]
        public void HasSubjectTest()
        {
            var target = CreateVerbPhraseHelp();

            target.BindSubject(new CommonPluralNoun("cats"));

            Check.That(target.HasSubject(e => e.Text == "cats")).IsTrue();
        }
        /// <summary>
        ///A test for IndirectObjects
        /// </summary>
        [Fact]
        public void IndirectObjectsTest()
        {
            var target = CreateVerbPhrase();
            IEnumerable<IEntity> actual;
            actual = target.IndirectObjects;
            Check.That(target.IndirectObjects).IsEmpty();
            IEntity indirectObject = new PersonalPronoun("them");
            target.BindIndirectObject(indirectObject);
            Check.That(target.IndirectObjects).Contains(indirectObject);
        }
        /// <summary>
        ///A test for IsClassifier
        /// </summary>
        [Fact]
        public void IsClassifierTest()
        {
            var target = new VerbPhrase(
                new Adverb("certainly"),
                new PastTenseVerb("is")
            );

            Check.That(target.IsClassifier).IsTrue();
        }
        /// <summary>
        ///A test for IsClassifier
        /// </summary>
        [Fact]
        public void IsClassifierTest1()
        {
            var target = new VerbPhrase(
                new Adverb("certainly"),
                new PastTenseVerb("owned"),
                new Quantifier("many")
            );

            Check.That(target.IsClassifier).IsFalse();
        }
        /// <summary>
        ///A test for IsPossessive
        /// </summary>
        [Fact]
        public void IsPossessiveTest()
        {
            var target = new VerbPhrase(
                new Adverb("certainly"),
                new PastTenseVerb("had"),
                new Quantifier("many")
            );
            Check.That(target.IsPossessive).IsTrue();
        }
        /// <summary>
        ///A test for IsPossessive
        /// </summary>
        [Fact]
        public void IsPossessiveTest1()
        {
            var target = new VerbPhrase(
                new Adverb("certainly"),
                new PastTenseVerb("was")
            );

            Check.That(target.IsPossessive).IsFalse();
        }
        /// <summary>
        ///A test for Modality
        /// </summary>
        [Fact]
        public void ModalityTest()
        {
            var target = CreateVerbPhrase();
            var expected = new ModalAuxilary("cannot");
            ModalAuxilary actual;
            target.Modality = expected;
            actual = target.Modality;
            Check.That(actual).IsEqualTo(expected);
        }
        /// <summary>
        ///A test for ModifyWith
        /// </summary>
        [Fact]
        public void ModifyWithTest()
        {
            var target = CreateVerbPhrase();
            IAdverbial adv = new Adverb("daringly");
            target.ModifyWith(adv);
            Check.That(target.AdverbialModifiers).Contains(adv);
        }
        /// <summary>
        ///A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            Phrase.VerboseOutput = false;
            var target = new VerbPhrase(
                new BaseVerb("run"),
                new Adverb("swiftly"),
                new Preposition("through")
            );
            var expected = "VerbPhrase \"run swiftly through\"";
            string actual;
            actual = target.ToString();
            Check.That(actual).IsEqualTo(expected);
        }
        /// <summary>
        ///A test for VerbPhrase Constructor
        /// </summary>
        [Fact]
        public void VerbPhraseConstructorTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new BaseVerb("run"), new Adverb("swiftly"), new Preposition("through") };
            var target = new VerbPhrase(composedWords);
            Check.That(target.Words).IsEqualTo(composedWords);
        }
        /// <summary>
        ///A test for VerbPhrase Constructor
        /// </summary>
        [Fact]
        public void VerbPhraseConstructorTest1()
        {
            var target = new VerbPhrase(new BaseVerb("run"), new Adverb("swiftly"), new Preposition("through"));
            var words = target.Words.ToList();

            Check.That(words[0].Text).IsEqualTo("run");
            Check.That(words[0].GetType()).IsEqualTo(typeof(BaseVerb));
            Check.That(words[1].Text).IsEqualTo("swiftly");
            Check.That(words[1].GetType()).IsEqualTo(typeof(Adverb));
            Check.That(words[2].Text).IsEqualTo("through");
            Check.That(words[2].GetType()).IsEqualTo(typeof(Preposition));
        }

        private static VerbPhrase CreateVerbPhrase() => new VerbPhrase(new BaseVerb("run"), new Adverb("swiftly"), new Preposition("through"));
        private static VerbPhrase CreateVerbPhraseHelp() => new VerbPhrase(new BaseVerb("help"));
    }
}
