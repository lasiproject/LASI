namespace LASI.Content.Tests.Helpers
{
    public sealed class ExpectedFileTypeWrapperMismatchExceptionAttribute : Shared.Test.Attributes.CustomizedExpectedExceptionBaseAttribute
    {
        public ExpectedFileTypeWrapperMismatchExceptionAttribute() : base(typeof(FileTypeWrapperMismatchException)) { }
    }
}
