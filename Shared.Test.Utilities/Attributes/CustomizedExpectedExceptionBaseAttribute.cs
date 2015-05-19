
namespace Shared.Test.Attributes
{
    using System;

    /// <summary>
    /// Provides a base class for ExpectedExceptionAttributes which throw some specific exception type.
    /// </summary>
    public abstract class CustomizedExpectedExceptionBaseAttribute : Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedExceptionBaseAttribute
    {
        private readonly Type expectedType;
        protected CustomizedExpectedExceptionBaseAttribute(Type expectedType)
        {
            this.expectedType = expectedType;
        }

        protected sealed override void Verify(Exception exception)
        {
            RethrowIfAssertException(exception);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(exception, expectedType);
        }
    }
}