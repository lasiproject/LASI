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
            NounPhrase target = new NounPhrase(composedWords);
            Check.That(target.Words).IsEqualTo(composedWords);
        }


        /// <summary>
        ///A test for BindDescriptor
        /// </summary>
        [Fact]
        public void BindDescriberTest()
        {
            IEnumerable<Word> composedWords = new Word[] { new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians") };
            NounPhrase target = new NounPhrase(composedWords);
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
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
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
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            Check.That(target).IsEqualTo(target as object);
        }

        /// <summary>
        ///A test for Descriptors
        /// </summary>
        [Fact]
        public void DescribedByTest()
        {
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
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
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IVerbal expected = new BaseVerb("insult");
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
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IVerbal expected = new VerbPhrase(new BaseVerb("gave"), new Adverb("willingly"));
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
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
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
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IEntity expected = new NounPhrase(new ProperSingularNoun("North"), new ProperSingularNoun("America"));
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
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
            IVerbal expected = new BaseVerb("are");
            IVerbal actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for AddPossession
        /// </summary>
        [Fact]
        public void AddPossessionTest()
        {
            NounPhrase target = new NounPhrase(new ProperPluralNoun("Americans"), new Conjunction("and"), new ProperPluralNoun("Canadians"));
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
            NounPhrase target = new NounPhrase(new ProperSingularNoun("LASI"), new Conjunction("and"), new ProperSingularNoun("Timmy"));
            string expected = "NounPhrase \"LASI and Timmy\"";
            string actual;
            actual = target.ToString();
            Check.That(actual).IsEqualTo(expected);
        }


        /// <summary>
        ///A test for Referees
        /// </summary>
        [Fact]
        public void RefereesTest()
        {
            NounPhrase target = new NounPhrase(new Determiner("the"), new Adjective("large"), new CommonSingularNoun("elephants"));
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
            NounPhrase target = new NounPhrase(new Adjective("large"), new CommonSingularNoun("elephants"));
            IEnumerable<IPossessable> actual;
            IEnumerable<IPossessable> expected = new[] { new CommonSingularNoun("trunks") };
            actual = target.Possessions;
            foreach (var ip in expected) { target.AddPossession(ip); }
            Check.That(actual).Contains(expected);
        }

        /// <summary>
        ///A test for OuterAttributive
        /// </summary>
        [Fact]
        public void OuterAttributiveTest()
        {
            NounPhrase target = new NounPhrase(new ProperSingularNoun("Catus"));
            NounPhrase expected = new NounPhrase(new ProperSingularNoun("Felis"));
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
            NounPhrase target = new NounPhrase(new ProperSingularNoun("Felis"));
            NounPhrase expected = new NounPhrase(new ProperSingularNoun("Catus"));
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
            NounPhrase target = new NounPhrase(new Determiner("the"), new CommonSingularNoun("elephants"));
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
            NounPhrase target = new NounPhrase(new Determiner("the"), new Adjective("large"), new CommonSingularNoun("elephant"));
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
            NounPhrase target = new NounPhrase(new Determiner("the"), new Adjective("large"), new CommonSingularNoun("elephants"));
            IDescriptor descriptor = new Adjective("hungry");
            target.BindDescriptor(descriptor);
            Check.That(target.Descriptors).Contains(descriptor);
            Check.That(descriptor.Describes).IsEqualTo(target);

        }
    }
}
