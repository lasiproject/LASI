using System;
using LASI.Core.Heuristics.Heuristics.WordMorphing;
using System.Linq;
using NFluent;
using Xunit;

namespace LASI.Core.Heuristics.Heuristics.WordMorphing.Tests
{
    public class AdjectiveMorpherTest
    {
        [Fact]
        public void GetLexicalFormsTest1()
        {
            var adjectiveMorpher = new AdjectiveMorpher();
            var adjectiveForms = new[] { "slow", "slower", "slowest" };
            foreach (var synthesizedForms in adjectiveForms.Select(adjectiveMorpher.GetLexicalForms))
            {
                Check.That(adjectiveForms.Except(synthesizedForms, StringComparer.OrdinalIgnoreCase)).IsEmpty();
            }
        }
        [Fact]
        public void GetLexicalFormsTest2()
        {
            var adjectiveMorpher = new AdjectiveMorpher();
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
            var adjectiveMorpher = new AdjectiveMorpher();
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
            var adjectiveMorpher = new AdjectiveMorpher();
            string[] adjectiveForms = { "bloodthirsty-lamentationist", "bloodthirstier-lamentationist", "bloodthirstiest-lamentationist", };
            foreach (var synthesizedForms in from form in adjectiveForms
                                             select adjectiveMorpher.GetLexicalForms(form))
            {
                Check.That(adjectiveForms.Except(synthesizedForms, StringComparer.OrdinalIgnoreCase)).IsEmpty();
            }
        }
    }
}
