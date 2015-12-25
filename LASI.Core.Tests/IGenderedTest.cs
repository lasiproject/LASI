using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for IGenderedTest and is intended
    ///to contain all IGenderedTest Unit Tests
    /// </summary>
     public class IGenderedTest
    {
        internal virtual ISimpleGendered CreateGendered()
        {
            ISimpleGendered target = new PersonalPronoun("he");
            return target;
        }

        /// <summary>
        ///A test for Gender
        /// </summary>
        [Fact]
        public void GenderTest()
        {
            ISimpleGendered target = CreateGendered();
            Gender actual;
            actual = target.Gender;
            Assert.Equal(actual, target.Gender);
        }
    }
}
