using NFluent;
using Xunit;

namespace LASI.Core.Tests
{

    /// <summary>
    ///This is A test class for PrepositionTest and is intended
    ///to contain all PrepositionTest Unit Tests
    /// </summary>
    public class PrepositionTest
    {
        /// <summary>
        ///A test for Preposition Constructor
        /// </summary>
        [Fact]
        public void PrepositionConstructorTest()
        {
            var text = "into";
            var target = new Preposition(text);
            Check.That(target.Text).IsEqualTo(text);
            Check.That(target.BoundObject).IsNull();
            Check.That(target.ToTheLeftOf).IsNull();
            Check.That(target.ToTheRightOf).IsNull();
        }

        /// <summary>
        ///A test for BindObjectOfPreposition
        /// </summary>
        [Fact]
        public void BindObjectOfPrepositionTest()
        {
            var text = "into";
            var target = new Preposition(text);
            ILexical prepositionalObject = new NounPhrase(new Determiner("the"), new CommonSingularNoun("drawer"));
            target.BindObject(prepositionalObject);
            Check.That(target.BoundObject).IsEqualTo(prepositionalObject);
        }


        /// <summary>
        ///A test for OnLeftSide
        /// </summary>
        [Fact]
        public void OnLeftSideTest()
        {
            var text = "into";
            var target = new Preposition(text);
            ILexical expected = new PastTenseVerb("gazed");
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for OnRightSide
        /// </summary>
        [Fact]
        public void OnRightSideTest()
        {
            var text = "into";
            var target = new Preposition(text);
            ILexical expected = new NounPhrase(new PossessivePronoun("your"), new CommonSingularNoun("soul"));
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Check.That(actual).IsEqualTo(expected);
        }


        /// <summary>
        ///A test for ToTheRightOf
        /// </summary>
        [Fact]
        public void ToTheRightOfTest()
        {
            var text = "into";
            var target = new Preposition(text);
            ILexical expected = new PastTenseVerb("gazed");
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for ToTheLeftOf
        /// </summary>
        [Fact]
        public void ToTheLeftOfTest()
        {
            var text = "inside";
            var target = new Preposition(text);
            ILexical expected = new NounPhrase(new PossessivePronoun("your"), new CommonSingularNoun("soul"));
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Check.That(actual).IsEqualTo(expected);
        }



        /// <summary>
        ///A test for Role
        /// </summary>
        [Fact]
        public void RoleTest()
        {
            var text = "over";
            var target = new Preposition(text);
            var expected = PrepositionRole.LocationOrScopeSpecifier;
            PrepositionRole actual;
            target.PrepositionRole = expected;
            actual = target.PrepositionRole;
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            var text = "with";
            var target = new Preposition(text);
            var expected = "Preposition \"with\"";
            string actual;
            actual = target.ToString();
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for BindObject
        /// </summary>
        [Fact]
        public void BindObjectTest()
        {
            var text = "with";
            var target = new Preposition(text);
            ILexical prepositionalObject = new PersonalPronoun("them");
            target.BindObject(prepositionalObject);
            Check.That(prepositionalObject).IsEqualTo(target.BoundObject);
        }
    }
}
