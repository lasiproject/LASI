using LASI.Core;
using System;
using System.Linq;
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
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            };
            AggregateEntity target = new AggregateEntity(members);
            Check.That(target).Contains(members);
        }

        /// <summary>
        ///A test for AggregateEntity Constructor
        /// </summary>
        [Fact]
        public void AggregateEntityConstructorTest1()
        {
            IEntity e1 = new NounPhrase(new ProperPluralNoun("Americans"));
            IEntity e2 = new NounPhrase(new ProperPluralNoun("Canadians"));
            AggregateEntity target = new AggregateEntity(e1, e2);
            Check.That(target).Contains(e1);
            Check.That(target).Contains(e2);

        }
        /// <summary>
        ///A test for AddPossession
        /// </summary>
        [Fact]
        public void AddPossessionTest()
        {
            AggregateEntity target = new AggregateEntity(
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
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            };
            AggregateEntity target = new AggregateEntity(members
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
            IEnumerable<IEntity> members = new[] {
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            };
            AggregateEntity target = new AggregateEntity(members);
            IReferencer pro = new PersonalPronoun("them");
            target.BindReferencer(pro);
            Check.That(target.Referencers).Contains(pro);
            Assert.True(pro.RefersTo.Contains(target) || pro.RefersTo == target || pro.RefersTo.SetEqual(target));
            foreach (IEntity e in members)
            {
                Assert.True(pro.RefersTo.Contains(e) || pro.RefersTo == e);

            }
        }

        /// <summary>
        ///A test for GetEnumerator
        /// </summary>
        [Fact]
        public void GetEnumeratorTest()
        {
            IEnumerable<IEntity> members = new IEntity[] { };
            AggregateEntity target = new AggregateEntity(members
            );
            IEnumerator<IEntity> expected = members.GetEnumerator();
            IEnumerator<IEntity> actual;
            actual = target.GetEnumerator();
            while (expected.MoveNext() | actual.MoveNext())
            {
                Check.That(actual.Current).IsEqualTo(expected.Current);
            }
        }

        /// <summary>
        ///A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            );
            string expected = "[ 2 ] NounPhrase \"Americans\" NounPhrase \"Canadians\"";
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
            AggregateEntity target = new AggregateEntity(
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
            AggregateEntity target = new AggregateEntity(
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
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            IVerbal expected = new VerbPhrase(new BaseVerb("eat"));
            target.BindAsDirectObjectOf(expected);

            IVerbal actual = target.DirectObjectOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for IndirectObjectOf
        /// </summary>
        [Fact]
        public void IndirectObjectOfTest()
        {
            AggregateEntity target = new AggregateEntity(
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
            AggregateEntity target = new AggregateEntity(members);
            double expected = new Random().NextDouble();
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
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            var possessions = new[]  {
                new NounPhrase(new CommonPluralNoun("claws")),
                new NounPhrase(new CommonPluralNoun("teeth"))
            };
            foreach (var possession in possessions) { target.AddPossession(possession); }
            IEnumerable<IPossessable> actual;
            actual = target.Possessions;
            Check.That(actual).Contains(possessions);
        }

        /// <summary>
        ///A test for Possesser
        /// </summary>
        [Fact]
        public void PossesserTest()
        {
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new CommonPluralNoun("cats")),
                new NounPhrase(new CommonPluralNoun("dogs"))
            );
            IPossesser expected = new NounPhrase(new ProperPluralNoun("Americans"));
            IPossesser actual;
            target.Possesser = expected;
            actual = target.Possesser;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for SubjectOf
        /// </summary>
        [Fact]
        public void SubjectOfTest()
        {
            AggregateEntity target = new AggregateEntity(
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
            AggregateEntity target = new AggregateEntity(
                new NounPhrase(new ProperPluralNoun("Americans")),
                new NounPhrase(new ProperPluralNoun("Canadians"))
            );
            string expected = "Americans ," + " Canadians";
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
            IEnumerable<IEntity> members = new IEntity[] { };
            AggregateEntity target = new AggregateEntity(members);
            double expected = new Random().NextDouble();
            double actual;
            target.Weight = expected;
            actual = target.Weight;
            Check.That(actual).IsEqualTo(expected);
        }
    }
}
