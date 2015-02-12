using System;

namespace LASI.TestUtilities
{
    public sealed class ExpectedFileTypeWrapperMismatchExceptionAttribute : CustomizedExpectedExceptionBaseAttribute
    {
        public ExpectedFileTypeWrapperMismatchExceptionAttribute() : base(typeof(Content.FileTypeWrapperMismatchException)) { }
    }
}
