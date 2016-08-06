using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for SymbolTest and is intended
    ///to contain all SymbolTest Unit Tests
    /// </summary>
    public class SymbolTest
    {
        /// <summary>
        ///A test for Symbol Constructor
        /// </summary>
        [Fact]
        public void SymbolConstructorTest()
        {
            var character = ',';
            var target = new Symbol(character.ToString());
            Assert.Equal(character, target.LiteralCharacter);
            Assert.Equal(character.ToString(), target.Text);
        }

    }
}
