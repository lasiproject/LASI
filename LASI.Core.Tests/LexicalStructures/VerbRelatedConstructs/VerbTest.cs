using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using NFluent;
using Xunit;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is A test class for VerbTest and is intended
    ///to contain all VerbTest Unit Tests
    /// </summary>
    public class VerbTest
    {
        /// <summary>
        ///A test for Verb Constructor
        /// </summary>
        [Fact]
        public void VerbConstructorTest()
        {
            var text = "insulate";
            Verb target = new BaseVerb(text);

            Check.That(target.Text).Equals(text);
            Check.That(target.Subjects).IsEmpty();
            Check.That(target.DirectObjects).IsEmpty();
            Check.That(target.IndirectObjects).IsEmpty();
            Check.That(target.Modality).IsNull();
            Check.That(target.IsPossessive).IsFalse();
        }

        /// <summary>
        ///A test for AttachObjectViaPreposition
        /// </summary>
        [Fact]
        public void AttachObjectViaPrepositionTest()
        {
            var text = "insulate";
            Verb target = new BaseVerb(text);
            var prepositionObject = new NounPhrase(new PersonalPronoun("them"));
            IPrepositional prep = new Preposition("for");
            prep.BindObject(prepositionObject);
            target.AttachObjectViaPreposition(prep);
            Check.That(target.ObjectOfThePreposition).Equals(prepositionObject);
        }

        /// <summary>
        ///A test for BindDirectObject
        /// </summary>
        [Fact]
        public void BindDirectObjectTest()
        {
            var text = "gave";
            Verb target = new PastTenseVerb(text);
            IEntity directObject = new NounPhrase(new Determiner("a"), new CommonSingularNoun("cake"));
            target.BindDirectObject(directObject);

            Check.That(target.DirectObjects).Contains(directObject).And.HasSize(1);
        }

        /// <summary>
        ///A test for BindIndirectObject
        /// </summary>
        [Fact]
        public void BindIndirectObjectTest()
        {
            var text = "gave";
            Verb target = new PastTenseVerb(text);
            IEntity indirectObject = new PersonalPronoun("him");
            target.BindIndirectObject(indirectObject);

            Check.That(target.IndirectObjects).Contains(indirectObject).And.HasSize(1);
        }
        /// <summary>
        ///A test for BindSubject
        /// </summary>
        [Fact]
        public void BindSubjectTest()
        {
            var text = "gave";
            Verb target = new BaseVerb(text);
            IEntity subject = new PersonalPronoun("he");
            target.BindSubject(subject);

            Check.That(target.Subjects).Contains(subject).And.HasSize(1);
        }



        /// <summary>
        ///A test for ModifyWith
        /// </summary>
        [Fact]
        public void ModifyWithTest()
        {
            var text = "insulate";
            Verb target = new BaseVerb(text);
            IAdverbial adv = new Adverb("sufficiently");
            target.ModifyWith(adv);

            Check.That(target.AdverbialModifiers).Contains(adv).And.HasSize(1);
        }



        /// <summary>
        ///A test for Modality
        /// </summary>
        [Fact]
        public void ModalityTest()
        {
            var text = "insulate";
            Verb target = new BaseVerb(text);
            var expected = new ModalAuxilary("can");
            ModalAuxilary actual;
            target.Modality = expected;
            actual = target.Modality;
            Check.That(actual).Equals(expected);
        }



        /// <summary>
        ///A test for Subjects
        /// </summary>
        [Fact]
        public void SubjectsTest()
        {
            var text = "attack";
            Verb target = new BaseVerb(text);
            IEnumerable<IEntity> actual;
            actual = target.Subjects;
            Check.That(actual).IsEmpty();
            IEntity subject = new CommonPluralNoun("chimpanzees");
            target.BindSubject(subject);
            actual = target.Subjects;
            Check.That(actual).Contains(subject);
            Check.That(target.AggregateSubject).Contains(subject);
        }

        /// <summary>
        ///A test for Modifiers
        /// </summary>
        [Fact]
        public void ModifiersTest()
        {
            var text = "attacked";
            Verb target = new PastTenseVerb(text);
            IEnumerable<IAdverbial> actual;
            actual = target.AdverbialModifiers;
            Check.That(actual).IsEmpty();
            IAdverbial modifier = new Adverb("swiftly");
            target.ModifyWith(modifier);
            actual = target.AdverbialModifiers;
            Check.That(actual).Contains(modifier);
            Check.That(modifier.Modifies).Equals(target);
        }



        /// <summary>
        ///A test for IsPossessive
        /// </summary>
        [Fact]
        public void IsPossessiveTest()
        {
            var text = "has";
            Verb target = new BaseVerb(text);
            bool isClassifier;
            isClassifier = target.IsPossessive;
            Check.That(isClassifier).IsTrue();
        }

        /// <summary>
        ///A test for IsClassifier
        /// </summary>
        [Fact]
        public void IsClassifierTest()
        {
            var text = "is";
            Verb target = new BaseVerb(text);
            bool isClassifier;
            isClassifier = target.IsClassifier;
            Check.That(isClassifier).IsTrue();
        }

        /// <summary>
        ///A test for IndirectObjects
        /// </summary>
        [Fact]
        public void IndirectObjectsTest()
        {
            var text = "attack";
            Verb target = new BaseVerb(text);
            IEnumerable<IEntity> actual;
            actual = target.IndirectObjects;
            Check.That(actual).IsEmpty();
            IEntity indirectObject = new CommonPluralNoun("allies");
            target.BindIndirectObject(indirectObject);
            actual = target.IndirectObjects;
            Check.That(actual).Contains(indirectObject);
            Check.That(target.AggregateIndirectObject).Contains(indirectObject);
        }

        /// <summary>
        ///A test for DirectObjects
        /// </summary>
        [Fact]
        public void DirectObjectsTest()
        {
            var text = "attack";
            Verb target = new BaseVerb(text);
            IEnumerable<IEntity> actual;
            actual = target.IndirectObjects;
            Check.That(actual).IsEmpty();
            IEntity directObject = new CommonPluralNoun("monkeys");
            target.BindDirectObject(directObject);
            actual = target.DirectObjects;
            Check.That(actual).Contains(directObject);
            Check.That(target.AggregateDirectObject).Contains(directObject);
        }

        /// <summary>
        ///A test for AggregateSubject
        /// </summary>
        [Fact]
        public void AggregateSubjectTest()
        {
            var text = "attack";
            Verb target = new BaseVerb(text);
            IAggregateEntity actual;
            actual = target.AggregateSubject;
            Check.That(actual).IsEmpty();
            IEntity subject = new CommonPluralNoun("monkeys");
            target.BindSubject(subject);
            actual = target.AggregateSubject;
            Check.That(new[] { subject }.Except(actual)).IsEmpty();
        }

        /// <summary>
        ///A test for AggregateIndirectObject
        /// </summary>
        [Fact]
        public void AggregateIndirectObjectTest()
        {
            var text = "attack";
            Verb target = new BaseVerb(text);
            IAggregateEntity actual;
            actual = target.AggregateIndirectObject;
            Check.That(actual).IsEmpty();
            IEntity indirectObject = new CommonPluralNoun("monkeys");
            target.BindIndirectObject(indirectObject);
            actual = target.AggregateIndirectObject;
            Check.That(new[] { indirectObject }.Except(actual)).IsEmpty();
        }

        /// <summary>
        ///A test for AggregateDirectObject
        /// </summary>
        [Fact]
        public void AggregateDirectObjectTest()
        {
            var text = "attack";
            Verb target = new BaseVerb(text);
            IAggregateEntity actual;
            actual = target.AggregateDirectObject;
            Check.That(actual).IsEmpty();
            IEntity directObject = new CommonPluralNoun("monkeys");
            target.BindDirectObject(directObject);
            actual = target.AggregateDirectObject;
            Check.That(new[] { directObject }.Except(actual)).IsEmpty();
        }




        /// <summary>
        ///A test for HasSubjectOrObject
        /// </summary>
        [Fact]
        public void HasSubjectOrObjectTest()
        {
            var text = "attack";
            Verb target = new BaseVerb(text);
            IEntity entity = new CommonPluralNoun("monkeys");
            var rand = new Random().Next(-1, 2);
            switch (rand)
            {
                case -1:
                    target.BindSubject(entity);
                    break;
                case 0:
                    target.BindDirectObject(entity);
                    break;
                case 1:
                    target.BindDirectObject(entity);
                    break;
                default:
                    Check.That(false).IsTrue();
                    break;
            }
            var expected = true;
            bool actual;
            actual = target.HasSubjectOrObject(predicate);
            Check.That(actual).Equals(expected);

            bool predicate(IEntity e) => e.Text == "monkeys";
        }



        /// <summary>
        ///A test for HasSubject
        /// </summary>
        [Fact]
        public void HasSubjectTest()
        {
            var text = "hired";
            Verb target = new PastTenseVerb(text);
            Check.That(target.HasSubject()).IsFalse();
            target.BindSubject(new PersonalPronoun("him"));
            Func<IEntity, bool> predicate = s => s.Text == "her";
            var expected = false;
            bool actual;
            actual = target.HasSubject(predicate);
            Check.That(actual).Equals(expected);
            target.BindSubject(new PersonalPronoun("her"));
            expected = true;
            actual = target.HasSubject(predicate);
            Check.That(actual).Equals(expected);
        }

    }
}
