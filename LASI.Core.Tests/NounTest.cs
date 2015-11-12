using LASI.Core;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using Shared.Test.Assertions;
using NFluent;

namespace LASI.Core.Tests
{
    using Fact = Xunit.FactAttribute;
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
            Noun target = CreateNoun();
            IEntity possession = new NounPhrase(new[] { new CommonSingularNoun("chew"), new CommonSingularNoun("toy") });
            target.AddPossession(possession);
            Check.That(target.Possessions).Contains(possession);
            Check.That(possession.Possesser).IsEqualTo(target);
        }

        /// <summary>
        ///A test for BindDescriptor
        /// </summary>
        [Fact]
        public void BindDescriberTest()
        {
            Noun target = CreateNoun();
            IDescriptor adjective = new Adjective("rambunctious");
            target.BindDescriptor(adjective);
            Check.That(target.Descriptors).Contains(adjective);
            Check.That(adjective.Describes).IsEqualTo(target);
        }

        /// <summary>
        ///A test for BindPronoun
        /// </summary>
        [Fact]
        public void BindPronounTest()
        {
            Noun target = CreateNoun();
            Pronoun pro = new PersonalPronoun("it");
            target.BindReferencer(pro);
            Check.That(target.Referencers).Contains(pro);
            Check.That(pro.RefersTo).Contains(target);
        }

        /// <summary>
        ///A test for Descriptors
        /// </summary>
        [Fact]
        public void DescribedByTest()
        {
            Noun target = CreateNoun();

            Check.That(target.Descriptors).IsNotNull().And.IsEmpty();
        }

        /// <summary>
        ///A test for DirectObjectOf
        /// </summary>
        [Fact]
        public void DirectObjectOfTest()
        {
            Noun target = CreateNoun();
            IVerbal expected = new PastTenseVerb("walked");
            IVerbal actual;
            target.DirectObjectOf = expected;
            actual = target.DirectObjectOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for IndirectObjectOf
        /// </summary>
        [Fact]
        public void IndirectObjectOfTest()
        {
            Noun target = CreateNoun();
            IVerbal expected = new PastTenseVerb("gave");
            IVerbal actual;
            target.IndirectObjectOf = expected;
            actual = target.IndirectObjectOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for IndirectReferences
        /// </summary>
        [Fact]
        public void IndirectReferencesTest()
        {
            Noun target = CreateNoun();
            IEnumerable<IReferencer> actual;
            actual = target.Referencers;
            Check.That(actual).IsNotNull().And.IsEmpty();
        }

        /// <summary>
        ///A test for Possessed
        /// </summary>
        [Fact]
        public void PossessedTest()
        {
            Noun target = CreateNoun();
            IEnumerable<IPossessable> actual;
            actual = target.Possessions;
            Check.That(actual).IsNotNull().And.IsEmpty();
        }

        /// <summary>
        ///A test for PossessesFor
        /// </summary>
        [Fact]
        public void PossesserTest()
        {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IEntity expected = new NounPhrase(new Word[] { new Adjective("Red"), new CommonSingularNoun("Team") });
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
            Noun target = CreateNoun();
            IVerbal expected = new SingularPresentVerb("runs");
            IVerbal actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for SuperTaxonomicNoun
        /// </summary>
        [Fact]
        public void SuperTaxonomicNounTest()
        {
            Noun target = CreateNoun();
            Noun expected = new ProperSingularNoun("Highland");
            Noun actual;
            target.PrecedingAdjunctNoun = expected;
            actual = target.PrecedingAdjunctNoun;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for SubTaxonomicNoun
        /// </summary>
        [Fact]
        public void SubTaxonomicNounTest()
        {
            Noun target = CreateNoun();
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
            Noun target = CreateNoun();
            IEnumerable<IReferencer> actual;
            actual = target.Referencers;
            Check.That(actual).IsEmpty();
            Pronoun pro = new PersonalPronoun("it");
            target.BindReferencer(pro);
            Check.That(target.Referencers).Contains(pro);
            Check.That(target.Referencers.All(r => r.RefersTo == target || r.RefersTo.Contains(target))).IsTrue();
        }

        /// <summary>
        ///A test for QuantifiedBy
        /// </summary>
        [Fact]
        public void QuantifiedByTest()
        {
            Noun target = CreateNoun();
            IQuantifier expected = new Quantifier("3");
            IQuantifier actual;
            target.QuantifiedBy = expected;
            actual = target.QuantifiedBy;
            Check.That(actual).IsEqualTo(expected);
            Check.That(target.QuantifiedBy.Quantifies).IsEqualTo(target);
        }

        /// <summary>A test for Descriptors</summary>
        [Fact]
        public void DescriptorsTest()
        {
            Noun target = CreateNoun();

            IEnumerable<IDescriptor> actual;
            actual = target.Descriptors;
            Check.That(actual).IsEmpty();
            IEnumerable<IDescriptor> descriptors = new[] { new Adjective("red"), new Adjective("slow") };

            foreach (var d in descriptors) { target.BindDescriptor(d); }
            actual = target.Descriptors;

            EnumerableAssert.AreSetEqual(actual, descriptors);
        }

        /// <summary>
        ///A test for BindReferencer
        /// </summary>
        [Fact]
        public void BindReferencerTest()
        {
            Noun target = CreateNoun(); // TODO: Initialize to an appropriate value
            IReferencer pro = new PersonalPronoun("it");
            target.BindReferencer(pro);
            Check.That(target.Referencers).Contains(pro);
            Check.That(target.Referencers.All(r => r.RefersTo == target || r.RefersTo.Contains(target))).IsTrue();
        }

        /// <summary>
        ///A test for BindDeterminer
        /// </summary>
        [Fact]
        public void BindDeterminerTest()
        {
            Noun target = CreateNoun();
            Determiner determiner = new Determiner("the");
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
            Noun target = CreateNoun();
            IDescriptor descriptor = new Adjective("red");
            target.BindDescriptor(descriptor);
            Check.That(target.Descriptors).Contains(descriptor);
            Check.That(descriptor.Describes).IsEqualTo(target);
        }

        private Noun CreateNoun()
        {
            Noun target = new CommonSingularNoun("dog");
            return target;
        }
    }
}