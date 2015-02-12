using Shared.Test.Attributes;

namespace LASI.Content.Tests.Helpers
{
    public sealed class ExpectedFileTypeWrapperMismatchExceptionAttribute : CustomizedExpectedExceptionBaseAttribute
    {
        public ExpectedFileTypeWrapperMismatchExceptionAttribute() : base(typeof(FileTypeWrapperMismatchException)) { }
    }
}
