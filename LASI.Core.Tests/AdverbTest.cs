using LASI;
using LASI.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using NFluent;
using Fact = Xunit.FactAttribute;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is A test class for AdverbTest and is intended
    ///to contain all AdverbTest Unit Tests
    /// </summary>
    public class AdverbTest
    {
        /// <summary>
        ///A test for Modifies
        /// </summary>
        [Fact]
        public void ModifiedTest()
        {
            Adverb quickly = new Adverb("quickly");
            IAdverbialModifiable run = new BaseVerb("run");
            quickly.Modifies = run;
            Check.That(quickly.Modifies).IsEqualTo(run);
        }

        /// <summary>
        ///A test for Adverb Constructor
        /// </summary>
        [Fact]
        public void AdverbConstructorTest()
        {
            Adverb quickly = new Adverb("quickly");
            Check.That(quickly.Text).IsEqualTo("quickly");
            Check.That(quickly.Modifies).IsNull();
        }

        /// <summary>
        ///A test for Modifies
        /// </summary>
        [Fact]
        public void ModifiesTest()
        {
            Adverb quickly = new Adverb("quickly");
            IAdverbialModifiable ran = new BaseVerb("ran");
            quickly.Modifies = ran;
            Check.That(quickly.Modifies).IsEqualTo(ran);
        }

        /// <summary>
        ///A test for Modifiers
        /// </summary>
        [Fact]
        public void ModifiersTest()
        {
            Adverb unfothomably = new Adverb("unfothomably");
            IEnumerable<IAdverbial> modifiers = new[] { new Adverb("uncertainly"), new Adverb("possibly") };
            foreach (var modifier in modifiers)
            {
                unfothomably.ModifyWith(modifier);
            }
            foreach (var modifier in modifiers)
            {
                Check.That(unfothomably.AdverbialModifiers).Contains(modifier);
                Check.That(modifier.Modifies).IsEqualTo(unfothomably);
            }
        }

        /// <summary>
        ///A test for ModifyWith
        /// </summary>
        [Fact]
        public void ModifyWithTest()
        {
            Adverb unfothomably = new Adverb("unfothomably");
            IAdverbial uncertainly = new Adverb("uncertainly");
            unfothomably.ModifyWith(uncertainly);
            Check.That(unfothomably.AdverbialModifiers).Contains(uncertainly);
            unfothomably.ModifyWith(uncertainly);
            Check.That(uncertainly.Modifies).IsEqualTo(unfothomably);
        }
    }
}
