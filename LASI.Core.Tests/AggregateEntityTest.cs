using LASI.Core;
using System;
using System.Linq;
using static System.Linq.Enumerable;
using System.Collections.Generic;
using LASI.Utilities;
using NFluent;
using Xunit;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is a test class for AggregateEntityTest and is intended
    ///to contain all AggregateEntityTest Unit Tests
    /// </summary>
    public class AggregateEntityTest
    {
        /// <summary>
        ///A test for AggregateEntity Constructor
        /// </summary>
        [Fact]
        public void AggregateEntityConstructorTest()
        {
            IEntity[] members = {
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            };
            var target = new AggregateEntity(members);

            Check.That(target).Contains(members);
        }

        /// <summary>
        ///A test for AggregateEntity Constructor
        /// </summary>
        [Fact]
        public void AggregateEntityConstructorTest1()
        {
            IEntity americans = new NounPhrase(new ProperPluralNoun("Americans"));
            IEntity canadians = new NounPhrase(new ProperPluralNoun("Canadians"));

            var target = new AggregateEntity(americans, canadians);

            Check.That(target).Contains(americans);
            Check.That(target).Contains(canadians);
        }
        /// <summary>
        ///A test for AddPossession
        /// </summary>
        [Fact]
        public void AddPossessionTest()
        {
            var target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            IPossessable possession = new NounPhrase(new CommonSingularNoun("fur"));

            target.AddPossession(possession);

            Check.That(target.Possessions).Contains(possession);
        }

        /// <summary>
        ///A test for BindDescriptor
        /// </summary>
        [Fact]
        public void BindDescriptorTest()
        {
            var target = new AggregateEntity(
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            );

            IDescriptor descriptor = new Adjective("rambunctious");

            target.BindDescriptor(descriptor);

            Check.That(target.Descriptors).Contains(descriptor);
        }

        /// <summary>
        ///A test for BindPronoun
        /// </summary>
        [Fact]
        public void BindPronounTest()
        {
            IEntity[] members = {
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            };
            var target = new AggregateEntity(members);
            var them = new PersonalPronoun("them");
            target.BindReferencer(them);

            Check.That(target.Referencers).Contains(them);
            Check.That(them.RefersTo.Contains(target) || them.RefersTo == target || them.RefersTo.SetEqual(target)).IsTrue();

            foreach (var member in members)
            {
                Assert.True(them.RefersTo.Contains(member) || them.RefersTo == member);
            }
        }

        /// <summary>
        ///A test for GetEnumerator
        /// </summary>
        [Fact]
        public void GetEnumeratorTest()
        {
            var members = Empty<IEntity>();
            var target = new AggregateEntity(members);

            using (var expected = members.GetEnumerator())
            using (var actual = target.GetEnumerator())
            {
                while (expected.MoveNext() | actual.MoveNext())
                {
                    Check.That(actual.Current).IsEqualTo(expected.Current);
                }
            }
        }

        /// <summary>
        ///A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            var target = new AggregateEntity(
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            );
            var expected = "[ 2 ] NounPhrase \"Americans\" NounPhrase \"Canadians\"";
            string actual;
            actual = target.ToString();
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for BoundPronouns
        /// </summary>
        [Fact]
        public void BoundPronounsTest()
        {
            var target = new AggregateEntity(
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            );
            IEnumerable<IReferencer> actual;
            IEnumerable<IReferencer> expected = new[] { new PersonalPronoun("them") };

            foreach (var r in expected) { target.BindReferencer(r); }
            actual = target.Referencers;
            Check.That(actual).Contains(expected);
        }

        /// <summary>
        ///A test for Descriptors
        /// </summary>
        [Fact]
        public void DescriptorsTest()
        {
            var target = new AggregateEntity(
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            );
            IEnumerable<IDescriptor> actual;
            IDescriptor adj1 = new Adjective("western");
            IDescriptor adj2 = new Adjective("proud");
            target.BindDescriptor(adj1);
            target.BindDescriptor(adj2);
            actual = target.Descriptors;
            Check.That(target.Descriptors).Contains(adj1);
            Check.That(adj1.Describes).IsEqualTo(target);
            Check.That(target.Descriptors).Contains(adj2);
            Check.That(adj2.Describes).IsEqualTo(target);
        }

        /// <summary>
        ///A test for DirectObjectOf
        /// </summary>
        [Fact]
        public void DirectObjectOfTest()
        {
            var target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            var expected = new VerbPhrase(new BaseVerb("eat"));
            target.BindAsDirectObjectOf(expected);

            var actual = target.DirectObjectOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for IndirectObjectOf
        /// </summary>
        [Fact]
        public void IndirectObjectOfTest()
        {
            var target = new AggregateEntity(
                new CommonSingularNoun("spoon"),
                new CommonSingularNoun("fork")
            );
            IVerbal expected = new VerbPhrase(new PastTenseVerb("were"), new PastParticiple("eaten"));
            IVerbal actual;
            target.BindAsIndirectObjectOf(expected);
            actual = target.IndirectObjectOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for MetaWeight
        /// </summary>
        [Fact]
        public void MetaWeightTest()
        {
            IEnumerable<IEntity> members = new IEntity[] { };
            var target = new AggregateEntity(members);
            var expected = new Random().NextDouble();
            double actual;
            target.MetaWeight = expected;
            actual = target.MetaWeight;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Possessed
        /// </summary>
        [Fact]
        public void PossessedTest()
        {
            var target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            var possessions = new[] {
                new NounPhrase(new CommonPluralNoun("claws")),
                new NounPhrase(new CommonPluralNoun("teeth"))
            };
            foreach (var possession in possessions) { target.AddPossession(possession); }
            IEnumerable<IPossessable> actual;
            actual = target.Possessions;
            Check.That(actual).Contains(possessions);
        }

        /// <summary>
        ///A test for <see cref="AggregateEntity.Possessor"/>
        /// </summary>
        [Fact]
        public void PossessorTest()
        {
            var target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            IPossessor expected = new NounPhrase(new ProperPluralNoun("Americans"));
            target.Possessor = expected;
            var actual = target.Possessor;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for SubjectOf
        /// </summary>
        [Fact]
        public void SubjectOfTest()
        {
            var target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            IVerbal expected = new BaseVerb("eat");
            IVerbal actual;
            target.BindAsSubjectOf(expected);
            actual = target.SubjectOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Text
        /// </summary>
        [Fact]
        public void TextTest()
        {
            var target = new AggregateEntity(
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            );
            var expected = "Americans ," + " Canadians";
            string actual;
            actual = target.Text;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Weight
        /// </summary>
        [Fact]
        public void WeightTest()
        {
            var members = Empty<IEntity>();
            var target = new AggregateEntity(members);
            var expected = new Random().NextDouble();
            double actual;
            target.Weight = expected;
            actual = target.Weight;
            Check.That(actual).IsEqualTo(expected);
        }
    }
}
