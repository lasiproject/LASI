using LASI.Core;
using System.Collections.Generic;
using System.Linq;
using NFluent;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for AdjectiveTest and is intended
    ///to contain all AdjectiveTest Unit Tests
    /// </summary>
    public class AdjectiveTest
    {
        /// <summary>
        ///A test for Adjective Constructor
        /// </summary>
        [Fact]
        public void AdjectiveConstructorTest()
        {
            var text = "orangish";
            var target = new Adjective(text);
            Check.That(target.Text).IsEqualTo(text);
        }

        /// <summary>
        ///A test for ModifyWith
        /// </summary>
        [Fact]
        public void ModifyWithTest()
        {
            var target = new Adjective("orangish");
            var adv = new Adverb("demonstrably");
            var advp = new AdverbPhrase(new[] { adv });
            target.ModifyWith(adv);
            target.ModifyWith(advp);
            Check.That(target.AdverbialModifiers).Contains(adv).And.Contains(advp);
        }

        /// <summary>
        ///A test for Describes
        /// </summary>
        [Fact]
        public void DescribesTest()
        {
            var text = "funny";
            var target = new Adjective(text);
            IEntity expected = new CommonSingularNoun("man");
            IEntity actual;
            target.Describes = expected;
            actual = target.Describes;
            Check.That(expected).IsEqualTo(actual);
            expected = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("woman") });
            target.Describes = expected;
            actual = target.Describes;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for Modifiers
        /// </summary>
        [Fact]
        public void ModifiersTest()
        {
            var text = "funny";
            var target = new Adjective(text);
            IEnumerable<IAdverbial> actual;
            actual = target.AdverbialModifiers;
            Check.That(target.AdverbialModifiers).IsEmpty();
        }


    }
}
