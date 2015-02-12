namespace LASI.TestUtilities
{
    public sealed class ExpectedFileNotFoundExceptionAttribute : CustomizedExpectedExceptionBaseAttribute
    {
        public ExpectedFileNotFoundExceptionAttribute() : base(typeof(System.IO.FileNotFoundException)) { }
    }
}
