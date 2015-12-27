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
            string text = "into";
            Preposition target = new Preposition(text);
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
            string text = "into";
            Preposition target = new Preposition(text);
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
            string text = "into";
            Preposition target = new Preposition(text);
            ILexical expected = new PastTenseVerb("gazed");
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Check.That(expected).IsEqualTo(actual);

        }

        /// <summary>
        ///A test for OnRightSide
        /// </summary>
        [Fact]
        public void OnRightSideTest()
        {
            string text = "into";
            Preposition target = new Preposition(text);
            ILexical expected = new NounPhrase(new PossessivePronoun("your"), new CommonSingularNoun("soul"));
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Check.That(expected).IsEqualTo(actual);

        }


        /// <summary>
        ///A test for ToTheRightOf
        /// </summary>
        [Fact]
        public void ToTheRightOfTest()
        {
            string text = "into";
            Preposition target = new Preposition(text);
            ILexical expected = new PastTenseVerb("gazed");
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for ToTheLeftOf
        /// </summary>
        [Fact]
        public void ToTheLeftOfTest()
        {
            string text = "inside";
            Preposition target = new Preposition(text);
            ILexical expected = new NounPhrase(new PossessivePronoun("your"), new CommonSingularNoun("soul"));
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Check.That(expected).IsEqualTo(actual);
        }



        /// <summary>
        ///A test for Role
        /// </summary>
        [Fact]
        public void RoleTest()
        {
            string text = "over";
            Preposition target = new Preposition(text);
            PrepositionRole expected = PrepositionRole.SpatialSpecifier;
            PrepositionRole actual;
            target.Role = expected;
            actual = target.Role;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for ToString
        /// </summary>
        [Fact]
        public void ToStringTest()
        {
            string text = "with";
            Preposition target = new Preposition(text);
            string expected = "Preposition \"with\"";
            string actual;
            actual = target.ToString();
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for BindObject
        /// </summary>
        [Fact]
        public void BindObjectTest()
        {
            string text = "with";
            Preposition target = new Preposition(text);
            ILexical prepositionalObject = new PersonalPronoun("them");
            target.BindObject(prepositionalObject);
            Check.That(prepositionalObject).IsEqualTo(target.BoundObject);
        }
    }
}
