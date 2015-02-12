namespace Shared.Test.Attributes
{
    /// <summary>
    /// Indicates that the test method to which the attribute is applied is expected to always throw
    /// an <see cref="System.InvalidOperationException" />.
    /// </summary>
    public sealed class ExpectedInvalidOperationExceptionAttribute : CustomizedExpectedExceptionBaseAttribute
    {
        /// <summary>
        /// Indicates that an <see cref="System.InvalidOperationException" /> must be thrown for
        /// test to pass.
        /// </summary>
        public ExpectedInvalidOperationExceptionAttribute() : base(typeof(System.InvalidOperationException)) { }
    }
}