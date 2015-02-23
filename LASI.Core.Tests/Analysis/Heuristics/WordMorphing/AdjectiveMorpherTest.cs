using System;
using LASI.Core.Analysis.Heuristics.WordMorphing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Test.Assertions;
using System.Linq;

namespace LASI.Core.Analysis.Heuristics.WordMorphing.Tests
{
    [TestClass]
    public class AdjectiveMorpherTest
    {
        [TestMethod]
        public void GetLexicalFormsTest1()
        {
            AdjectiveMorpher adjectiveMorpher = new AdjectiveMorpher();
            string[] adjectiveForms = { "slow", "slower", "slowest", };
            foreach (var synthesizedForms in from form in adjectiveForms
                                             select adjectiveMorpher.GetLexicalForms(form))
            {
                EnumerableAssert.AreSetEqual(adjectiveForms, synthesizedForms, StringComparer.OrdinalIgnoreCase);
            }
        }
        [TestMethod]
        public void GetLexicalFormsTest2()
        {
            AdjectiveMorpher adjectiveMorpher = new AdjectiveMorpher();
            string[] adjectiveForms = { "deep-lamentationist", "deeper-lamentationist", "deepest-lamentationist", };
            foreach (var synthesizedForms in from form in adjectiveForms
                                             select adjectiveMorpher.GetLexicalForms(form))
            {
                EnumerableAssert.AreSetEqual(adjectiveForms, synthesizedForms, StringComparer.OrdinalIgnoreCase);
            }
        }
        [TestMethod]
        public void GetLexicalFormsTest3()
        {
            AdjectiveMorpher adjectiveMorpher = new AdjectiveMorpher();
            string[] adjectiveForms = { "bloodthirsty", "bloodthirstier", "bloodthirstiest", };
            foreach (var synthesizedForms in from form in adjectiveForms
                                             select adjectiveMorpher.GetLexicalForms(form))
            {
                EnumerableAssert.AreSetEqual(adjectiveForms, synthesizedForms, StringComparer.OrdinalIgnoreCase);
            }
        }
        [TestMethod]
        public void GetLexicalFormsTest4()
        {
            AdjectiveMorpher adjectiveMorpher = new AdjectiveMorpher();
            string[] adjectiveForms = { "bloodthirsty-lamentationist", "bloodthirstier-lamentationist", "bloodthirstiest-lamentationist", };
            foreach (var synthesizedForms in from form in adjectiveForms
                                             select adjectiveMorpher.GetLexicalForms(form))
            {
                EnumerableAssert.AreSetEqual(adjectiveForms, synthesizedForms, StringComparer.OrdinalIgnoreCase);
            }
        }

    }
}
