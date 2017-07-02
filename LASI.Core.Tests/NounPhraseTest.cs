using LASI;
using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using NFluent;
using Xunit;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is A test class for NounPhraseTest and is intended
    ///to contain all NounPhraseTest Unit Tests
    /// </summary>
    public class NounPhraseTest
    {
        /// <summary>
        ///A test for NounPhrase Constructorf
        /// </summary>
        [Fact]
        public void NounPhraseConstructorTest()
        {
            IEnumerable<Word> composedWords = new Word[] {
                new ProperPluralNoun("Americans"),
                new Conjunction("and"),
                new ProperPluralNoun("Canadians")
            };
            var target = new NounPhrase(composedWords);
            Check.That(target.Words).IsEqualTo(composedWords);
        }


        /// <summary>
        ///A test for BindDescriptor
        /// </summary>
        [Fact]
        public void BindDescriberTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            var target = new NounPhrase(composedWords);
            IDescriptor adj = new AdjectivePhrase(new Word[] { new CommonSingularNoun("peace"), new PresentParticiple("loving") });
            target.BindDescriptor(adj);
            Check.That(target.Descriptors).Contains(adj);
        }

        /// <summary>
        ///A test for BindPronoun
        /// </summary>
        [Fact]
        public void BindPronounTest()
        {
            var target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            Pronoun pro = new PersonalPronoun("they");
            target.BindReferencer(pro);
            Check.That(target.Referencers).Contains(pro);
            Check.That(pro.RefersTo).Contains(target);
        }


        /// <summary>
        ///A test for Equals
        /// </summary>
        [Fact]
        public void EqualsTest()
        {
            var target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            Check.That(target).IsEqualTo(target as object);
        }

        /// <summary>
        ///A test for Descriptors
        /// </summary>
        [Fact]
        public void DescribedByTest()
        {
            var target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            Check.That(target.Descriptors).IsEmpty();
            IDescriptor adj = new AdjectivePhrase(new CommonSingularNoun("peace"), new PresentParticiple("loving"));
            target.BindDescriptor(adj);
            Check.That(target.Descriptors).Contains(adj);
            IDescriptor adj2 = new Adjective("proud");
            target.BindDescriptor(adj2);
            Check.That(target.Descriptors).Contains(adj, adj2);
        }

        /// <summary>
        ///A test for DirectObjectOf
        /// </summary>
        [Fact]
        public void DirectObjectOfTest()
        {
            var target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IVerbal expected = new BaseVerb("insult");
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
            var target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IVerbal expected = new VerbPhrase(new BaseVerb("gave"), new Adverb("willingly"));
            IVerbal actual;
            target.BindAsIndirectObjectOf(expected);
            actual = target.IndirectObjectOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for IndirectReferences
        /// </summary>
        [Fact]
        public void IndirectReferencesTest()
        {
            var target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            Check.That(target.Referencers).IsEmpty();
            Pronoun pro = new PersonalPronoun("they");
            target.BindReferencer(pro);
            Check.That(target.Referencers.Contains(pro) && pro.RefersTo.Any(e => e == target)).IsTrue();
        }

        /// <summary>
        ///A test for PossessesFor
        /// </summary>
        [Fact]
        public void PossesserTest()
        {
            var target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IEntity expected = new NounPhrase(new ProperSingularNoun("North"), new ProperSingularNoun("America"));
            target.Possesser = expected.ToOption<IPossesser>();
            var actual = target.Possesser;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for SubjectOf
        /// </summary>
        [Fact]
        public void SubjectOfTest()
        {
            var target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IVerbal expected = new BaseVerb("are");
            IVerbal actual;
            target.BindAsSubjectOf(expected);
            actual = target.SubjectOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for AddPossession
        /// </summary>
        [Fact]
        public void AddPossessionTest()
        {
            var target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IEntity possession = new NounPhrase(new Adverb("relatively"), new Adjective("affluent"), new CommonPluralNoun("lifestyles"));
            target.AddPossession(possession);
            Check.That(target.Possessions).Contains(possession);
            Check.That(possession.Possesser).IsEqualTo(target);
        }

        /// <summary>
        ///A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            var target = new NounPhrase(new ProperSingularNoun("LASI"), new Conjunction("and"), new ProperSingularNoun("Timmy"));
            var expected = "NounPhrase \"LASI and Timmy\"";
            Check.That(target.ToString()).IsEqualTo(expected);
        }


        /// <summary>
        ///A test for Referees
        /// </summary>
        [Fact]
        public void RefereesTest()
        {
            var target = new NounPhrase(new Determiner("the"), new Adjective("large"), new CommonSingularNoun("elephants"));
            IEnumerable<IReferencer> expected = new IReferencer[] { new RelativePronoun("that"), new PersonalPronoun("it") };
            IEnumerable<IReferencer> actual;
            foreach (var r in expected)
            {
                target.BindReferencer(r);
            }
            actual = target.Referencers;
            Check.That(actual).Contains(expected);
        }


        /// <summary>
        ///A test for Possessed
        /// </summary>
        [Fact]
        public void PossessedTest()
        {
            var target = new NounPhrase(new Adjective("large"), new CommonSingularNoun("elephants"));

            IEnumerable<IPossessable> expected = new[] { new CommonSingularNoun("trunks") };
            foreach (var ip in expected) { target.AddPossession(ip); }
            Check.That(target.Possessions).Contains(expected);
        }

        /// <summary>
        ///A test for OuterAttributive
        /// </summary>
        [Fact]
        public void OuterAttributiveTest()
        {
            var target = new NounPhrase(new ProperSingularNoun("Catus"));
            var expected = new NounPhrase(new ProperSingularNoun("Felis"));
            NounPhrase actual;
            target.OuterAttributive = expected;
            actual = target.OuterAttributive;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for InnerAttributive
        /// </summary>
        [Fact]
        public void InnerAttributiveTest()
        {
            var target = new NounPhrase(new ProperSingularNoun("Felis"));
            var expected = new NounPhrase(new ProperSingularNoun("Catus"));
            NounPhrase actual;
            target.InnerAttributive = expected;
            actual = target.InnerAttributive;
            Check.That(actual).IsEqualTo(expected);
        }




        /// <summary>
        ///A test for Descriptors
        /// </summary>
        [Fact]
        public void DescriptorsTest()
        {
            var target = new NounPhrase(new Determiner("the"), new CommonSingularNoun("elephants"));
            IEnumerable<IDescriptor> actual;
            actual = target.Descriptors;
            Check.That(target.Descriptors).IsEmpty();
            IDescriptor descriptor = new Adjective("large");
            target.BindDescriptor(descriptor);
            Check.That(target.Descriptors).Contains(descriptor);
        }



        /// <summary>
        ///A test for BindReferencer
        /// </summary>
        [Fact]
        public void BindReferencerTest()
        {
            var target = new NounPhrase(new Determiner("the"), new Adjective("large"), new CommonSingularNoun("elephant"));
            IReferencer pro = new RelativePronoun("which");
            target.BindReferencer(pro);
            Check.That(target.Referencers.All(r => r.RefersTo.Contains(target))).IsTrue();
        }

        /// <summary>
        ///A test for BindDescriptor
        /// </summary>
        [Fact]
        public void BindDescriptorTest()
        {
            var target = new NounPhrase(new Determiner("the"), new Adjective("large"), new CommonSingularNoun("elephants"));
            IDescriptor descriptor = new Adjective("hungry");
            target.BindDescriptor(descriptor);
            Check.That(target.Descriptors).Contains(descriptor);
            Check.That(descriptor.Describes).IsEqualTo(target);
        }
    }
}
