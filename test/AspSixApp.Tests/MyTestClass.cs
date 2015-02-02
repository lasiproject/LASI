using NUnit.Framework;

namespace AspSixApp.Tests
{

    [TestFixture(Category = "AspNetVNext", Description = "A test defined within an asp.net vnext project")]
    public class MyTestClass
    {

        public TestCaseData TestCaseOneData() => new TestCaseData(1);
        [TestCaseSource("TestCaseOneData")]
        [Test(Description = "A test test")]
        public void MyTestMethod(TestCaseData data) {
            Assert.Fail();
        }
    }
}

