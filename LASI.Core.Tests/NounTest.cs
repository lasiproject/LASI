using LASI.Core;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using NFluent;
using Xunit;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is A test class for NounTest and is intended
    ///to contain all NounTest Unit Tests
    /// </summary>
    public class NounTest
    {
        /// <summary>
        ///A test for AddPossession
        /// </summary>
        [Fact]
        public void AddPossessionTest()
        {
            var target = new CommonSingularNoun("dog");
            IEntity possession = new NounPhrase(new CommonSingularNoun("chew"), new CommonSingularNoun("toy"));
            target.AddPossession(possession);
            Check.That(target.Possessions).Contains(possession);
            Check.That(possession.Possessor).IsEqualTo(target);
        }

        /// <summary>
        ///A test for BindDescriptor
        /// </summary>
        [Fact]
        public void BindDescriberTest()
        {
            var target = new CommonSingularNoun("dog");
            IDescriptor adjective = new Adjective("rambunctious");
            target.BindDescriptor(adjective);
            Check.That(target.Descriptors).Contains(adjective).Only();
            Check.That(adjective.Describes).IsEqualTo(target);
        }

        /// <summary>
        ///A test for BindPronoun
        /// </summary>
        [Fact]
        public void BindPronounTest()
        {
            var target = new CommonSingularNoun("dog");
            Pronoun pronoun = new PersonalPronoun("it");
            target.BindReferencer(pronoun);
            Check.That(target.Referencers).Contains(pronoun).Only();
            Check.That(pronoun.RefersTo).Contains(target);
        }

        /// <summary>
        ///A test for Descriptors
        /// </summary>
        [Fact]
        public void DescribedByTest()
        {
            var target = new CommonSingularNoun("dog");

            Check.That(target.Descriptors).IsNotNull().And.IsEmpty();
        }

        /// <summary>
        ///A test for DirectObjectOf
        /// </summary>
        [Fact]
        public void DirectObjectOfTest()
        {
            var target = new CommonSingularNoun("dog");
            IVerbal expected = new PastTenseVerb("walked");
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
            var target = new CommonSingularNoun("dog");
            IVerbal expected = new PastTenseVerb("gave");
            target.BindAsIndirectObjectOf(expected);
            var actual = target.IndirectObjectOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for IndirectReferences
        /// </summary>
        [Fact]
        public void IndirectReferencesTest()
        {
            var target = new CommonSingularNoun("dog");
            IEnumerable<IReferencer> actual = target.Referencers;
            Check.That(actual).IsNotNull().And.IsEmpty();
        }

        /// <summary>
        ///A test for Possessed
        /// </summary>
        [Fact]
        public void PossessedTest()
        {
            var target = new CommonSingularNoun("dog");
            IEnumerable<IPossessable> actual;
            actual = target.Possessions;
            Check.That(actual).IsNotNull().And.IsEmpty();
        }

        /// <summary>
        ///A test for Possessor
        /// </summary>
        [Fact]
        public void PossessorTest()
        {
            var target = new CommonSingularNoun("dog");
            var possessor = new NounPhrase(new Adjective("Red"), new CommonSingularNoun("Team"));

            target.Possessor = possessor;
            var actual = target.Possessor;
            Check.That(actual).IsEqualTo(possessor);
        }

        /// <summary>
        ///A test for SubjectOf
        /// </summary>
        [Fact]
        public void SubjectOfTest()
        {
            var target = new CommonSingularNoun("dog");
            IVerbal expected = new SingularPresentVerb("runs");
            IVerbal actual;
            target.BindAsSubjectOf(expected);
            actual = target.SubjectOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for SuperTaxonomicNoun
        /// </summary>
        [Fact]
        public void SuperTaxonomicNounTest()
        {
            Noun target = new CommonSingularNoun("dog");
            Noun expected = new ProperSingularNoun("Highland");
            target.PrecedingAdjunctNoun = expected;
            Noun actual = target.PrecedingAdjunctNoun;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for SubTaxonomicNoun
        /// </summary>
        [Fact]
        public void SubTaxonomicNounTest()
        {
            var target = new CommonSingularNoun("dog");
            Noun expected = new CommonSingularNoun("food");
            Noun actual;
            target.FollowingAdjunctNoun = expected;
            actual = target.FollowingAdjunctNoun;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for Referees
        /// </summary>
        [Fact]
        public void RefereesTest()
        {
            var target = new CommonSingularNoun("dog");
            IEnumerable<IReferencer> actual;
            actual = target.Referencers;
            Check.That(actual).IsEmpty();
            Pronoun pro = new PersonalPronoun("it");
            target.BindReferencer(pro);
            Check.That(target.Referencers).Contains(pro).Only();
            Check.That(target.Referencers.All(r => r.RefersTo == target || r.RefersTo.Contains(target))).IsTrue();
        }

        /// <summary>
        ///A test for QuantifiedBy
        /// </summary>
        [Fact]
        public void QuantifiedByTest()
        {
            var target = new CommonSingularNoun("dog");
            var quantifying = new Quantifier("3");
            target.QuantifiedBy = quantifying;
            var actual = target.QuantifiedBy;
            Check.That(actual).IsEqualTo(quantifying);
            Check.That(target.QuantifiedBy.Quantifies).IsEqualTo(target);
        }

        /// <summary>A test for Descriptors</summary>
        [Fact]
        public void DescriptorsTest()
        {
            var target = new CommonSingularNoun("dog");

            Check.That(target.Descriptors).IsEmpty();
            var descriptors = new[] { new Adjective("red"), new Adjective("slow") };

            foreach (var descriptor in descriptors)
            {
                target.BindDescriptor(descriptor);
            }
            var boundDescriptors = target.Descriptors;
            Check.That(boundDescriptors).Contains(descriptors).Only();
        }

        /// <summary>
        ///A test for BindReferencer
        /// </summary>
        [Fact]
        public void BindReferencerTest()
        {
            var target = new CommonSingularNoun("dog");
            var pronoun = new PersonalPronoun("it");
            target.BindReferencer(pronoun);
            Check.That(target.Referencers).Contains(pronoun).Only();
            Check.That(target.Referencers.All(r => r.RefersTo == target || r.RefersTo.Contains(target))).IsTrue();
        }

        /// <summary>
        ///A test for BindDeterminer
        /// </summary>
        [Fact]
        public void BindDeterminerTest()
        {
            var target = new CommonSingularNoun("dog");
            var determiner = new Determiner("the");
            target.BindDeterminer(determiner);
            Check.That(target.Determiner).IsEqualTo(determiner);
            Check.That(determiner.Determines).IsEqualTo(target);
        }

        /// <summary>
        ///A test for BindDescriptor
        /// </summary>
        [Fact]
        public void BindDescriptorTest()
        {
            var target = new CommonSingularNoun("dog");
            IDescriptor descriptor = new Adjective("red");
            target.BindDescriptor(descriptor);
            Check.That(target.Descriptors).Contains(descriptor).Only();
            Check.That(descriptor.Describes).IsEqualTo(target);
        }
    }
}