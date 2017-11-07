using NFluent;
using Xunit;

namespace LASI.Core.Tests
{

    /// <summary>
    ///This is A test class for ParticleTest and is intended
    ///to contain all ParticleTest Unit Tests
    /// </summary>
    public class ParticleTest
    {
        /// <summary>
        ///A test for Particle Constructor
        /// </summary>
        [Fact]
        public void ParticleConstructorTest()
        {
            var text = "about";
            var target = new Particle(text);
            Assert.True(target.Text == "about" && target.ToTheLeftOf == null && target.ToTheRightOf == null && target.BoundObject == null);
        }


        /// <summary>
        ///A test for BindObjectOfPreposition
        /// </summary>
        [Fact]
        public void BindObjectOfPrepositionTest()
        {
            var text = "about";
            var target = new Particle(text);
            ILexical prepositionalObject = new NounPhrase(new[] { new ProperSingularNoun("Ayn"), new ProperSingularNoun("Rand") });
            target.BindObject(prepositionalObject);
            Check.That(target.BoundObject).IsEqualTo(prepositionalObject);
        }

        /// <summary>
        ///A test for OnLeftSide
        /// </summary>
        [Fact]
        public void OnLeftSideTest()
        {
            var text = "about";
            var target = new Particle(text);
            ILexical expected = new VerbPhrase(new[] { new PastTenseVerb("walked") });
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
            var text = "about";
            var target = new Particle(text);
            ILexical expected = new NounPhrase(new Word[] { new Determiner("the"), new CommonPluralNoun("grounds") });
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for PrepositionalRole
        /// </summary>
        [Fact]
        public void PrepositionalRoleTest()
        {
            var text = "about";
            var target = new Particle(text);
            var expected = PrepositionRole.Undetermined;
            PrepositionRole actual;
            actual = target.Role;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for ToTheRightOf
        /// </summary>
        [Fact]
        public void ToTheRightOfTest()
        {
            var text = "about";
            var target = new Particle(text);
            ILexical expected = new NounPhrase(new Word[] { new Determiner("the"), new CommonPluralNoun("grounds") });
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
            var text = "about";
            var target = new Particle(text);
            ILexical expected = new PastTenseVerb("walked");
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
            var text = "about";
            var target = new Particle(text);
            var expected = PrepositionRole.Undetermined;
            PrepositionRole actual;
            target.Role = expected;
            actual = target.Role;
            Check.That(expected).IsEqualTo(actual);
        }

        /// <summary>
        ///A test for BindObject
        /// </summary>
        [Fact]
        public void BindObjectTest()
        {
            var text = "about";
            var target = new Particle(text);
            ILexical prepositionalObject = new NounPhrase(new Word[] { new Determiner("the"), new CommonPluralNoun("grounds") });
            target.BindObject(prepositionalObject);
            Check.That(prepositionalObject).IsEqualTo(target.BoundObject);
            IVerbal verbal = new PastTenseVerb("walked");
            verbal.AttachObjectViaPreposition(target);
            Check.That(prepositionalObject).IsEqualTo(verbal.ObjectOfThePreposition);
        }

    }
}
