namespace LASI.UnitTests
{
    /// <summary>
    /// Provides a base class for ExpectedExceptionAttributes which throw some specific exception type.
    /// </summary>
    public abstract class CustomizedExpectedExceptionBaseAttribute : Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedExceptionBaseAttribute
    {
        private readonly System.Type expectedType;
        protected CustomizedExpectedExceptionBaseAttribute(System.Type exceptionType) {
            expectedType = exceptionType;
        }

        protected sealed override void Verify(System.Exception exception) {
            RethrowIfAssertException(exception);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(exception, expectedType);
        }
    }
}