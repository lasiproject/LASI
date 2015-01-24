using System;

namespace LASI.UnitTests
{
    public sealed class ExpectedFileTypeWrapperMismatchExceptionAttribute : CustomizedExpectedExceptionBaseAttribute
    {
        public ExpectedFileTypeWrapperMismatchExceptionAttribute() : base(typeof(Content.FileTypeWrapperMismatchException)) { }
    }
}
