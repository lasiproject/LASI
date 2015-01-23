namespace LASI.UnitTests
{
    public sealed class ExpectedFileNotFoundExceptionAttribute : Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedExceptionBaseAttribute
    {
        protected override void Verify(System.Exception exception) {
            RethrowIfAssertException(exception);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(exception, typeof(System.IO.FileNotFoundException));
        }
    }
    public sealed class ExpectedInvalidOperationExceptionAttribute : Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedExceptionBaseAttribute
    {
        protected override void Verify(System.Exception exception) {
            RethrowIfAssertException(exception);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(exception, typeof(System.InvalidOperationException));
        }
    }
}
