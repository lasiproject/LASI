using System;
using NFluent;
using NFluent.Extensibility;

namespace LASI.Utilities.Tests.Extensions
{
    public static class CheckExtensions
    {
        public static ICheckLink<ICheck<Sut>> Satisfies<Sut>(this ICheck<Sut> check, Func<Sut, bool> requirement)
        {
            // Every check method starts by extracting a checker instance from the check thanks to
            // the ExtensibilityHelper static class.
            var checker = ExtensibilityHelper.ExtractChecker(check);

            // Then, we let the checker's ExecuteCheck() method return the ICheckLink<ICheck<T>> result (with T as string here).
            // This method needs 2 arguments:
            //   1- a lambda that checks what's necessary, and throws a FluentAssertionException in case of failure
            //      The exception message is usually fluently build with the FluentMessage.BuildMessage() static method.
            //
            //   2- a string containing the message for the exception to be thrown by the checker when 
            //      the check fails, in the case we were running the negated version.
            //
            // e.g.:
            var requirementName = GetNominalInfo(requirement);
            return checker.ExecuteCheck(
                () =>
                {
                    if (!requirement(checker.Value))
                    {
                        var errorMessage = FluentMessage.BuildMessage($"The {{0}} does not satisfy the requirement{requirementName}.").For(typeof(Sut).Name).On(checker.Value).ToString();

                        throw new FluentCheckException(errorMessage);
                    }
                },
                FluentMessage.BuildMessage($"The {{0}} satisifies the requirement {requirementName} whereas it must not.").For(typeof(Sut).Name).On(checker.Value).ToString());
        }
        public static ICheckLink<ICheck<Sut>> DoesNotSatisfy<Sut>(this ICheck<Sut> check, Func<Sut, bool> requirement)
        {
            var checker = ExtensibilityHelper.ExtractChecker(check);

            var requirementName = GetNominalInfo(requirement);
            return checker.ExecuteCheck(
                () =>
                {
                    if (requirement(checker.Value))
                    {
                        var errorMessage = FluentMessage.BuildMessage($"The {{0}} satisfies the requirement {requirementName} whereas it must.").For(typeof(Sut).Name).On(checker.Value).ToString();

                        throw new FluentCheckException(errorMessage);
                    }
                },
                FluentMessage.BuildMessage($"The {{0}} does not satisfy the requirement {requirementName}.").For(typeof(Sut).Name).On(checker.Value).ToString());
        }

        private static string GetNominalInfo<SUT>(Func<SUT, bool> requirement) => requirement.Method.DeclaringType.Name + "." + requirement.Method.Name;
    }
}
