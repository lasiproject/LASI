using System;
using LASI.Core.Analysis.Heuristics.WordMorphing;
using Shared.Test.Assertions;
using System.Linq;
using NFluent;
using Xunit;

namespace LASI.Core.Analysis.Heuristics.WordMorphing.Tests
{
    public class AdjectiveMorpherTest
    {
        [Fact]
        public void GetLexicalFormsTest1()
        {
            AdjectiveMorpher adjectiveMorpher = new AdjectiveMorpher();
            string[] adjectiveForms = { "slow", "slower", "slowest", };
            foreach (var synthesizedForms in from form in adjectiveForms
                                             select adjectiveMorpher.GetLexicalForms(form))
            {
                Check.That(adjectiveForms.Except(synthesizedForms, StringComparer.OrdinalIgnoreCase)).IsEmpty();
            }
        }
        [Fact]
        public void GetLexicalFormsTest2()
        {
            AdjectiveMorpher adjectiveMorpher = new AdjectiveMorpher();
            string[] adjectiveForms = { "deep-lamentationist", "deeper-lamentationist", "deepest-lamentationist", };
            foreach (var synthesizedForms in from form in adjectiveForms
                                             select adjectiveMorpher.GetLexicalForms(form))
            {
                Check.That(adjectiveForms.Except(synthesizedForms, StringComparer.OrdinalIgnoreCase)).IsEmpty();
            }
        }
        [Fact]
        public void GetLexicalFormsTest3()
        {
            AdjectiveMorpher adjectiveMorpher = new AdjectiveMorpher();
            string[] adjectiveForms = { "bloodthirsty", "bloodthirstier", "bloodthirstiest", };
            foreach (var synthesizedForms in from form in adjectiveForms
                                             select adjectiveMorpher.GetLexicalForms(form))
            {
                Check.That(adjectiveForms.Except(synthesizedForms, StringComparer.OrdinalIgnoreCase)).IsEmpty();
            }
        }
        [Fact]
        public void GetLexicalFormsTest4()
        {
            AdjectiveMorpher adjectiveMorpher = new AdjectiveMorpher();
            string[] adjectiveForms = { "bloodthirsty-lamentationist", "bloodthirstier-lamentationist", "bloodthirstiest-lamentationist", };
            foreach (var synthesizedForms in from form in adjectiveForms
                                             select adjectiveMorpher.GetLexicalForms(form))
            {
                Check.That(adjectiveForms.Except(synthesizedForms, StringComparer.OrdinalIgnoreCase)).IsEmpty();
            }
        }

    }
}
