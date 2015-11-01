using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using NFluent.Extensibility;

namespace LASI.Content.Tests.Extensions
{
    public static class NFluent
    {
        public static ICheckLink<ICheck<SUT>> Satisfies<SUT>(this ICheck<SUT> check, Func<SUT, bool> requirement)
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
            string requirementName = GetNominalInfo(requirement);
            return checker.ExecuteCheck(
                () =>
                {
                    if (!requirement(checker.Value))
                    {
                        var errorMessage = FluentMessage.BuildMessage($"The {{0}} does not satisfy the requirement{requirementName}.").For(typeof(SUT).GetType().Name).On(checker.Value).ToString();

                        throw new FluentCheckException(errorMessage);
                    }
                },
                FluentMessage.BuildMessage($"The {{0}} satisifies the requirement {requirementName} whereas it must not.").For(typeof(SUT).GetType().Name).On(checker.Value).ToString());
        }
        public static ICheckLink<ICheck<SUT>> DoesNotSatisfy<SUT>(this ICheck<SUT> check, Func<SUT, bool> requirement)
        {
            var checker = ExtensibilityHelper.ExtractChecker(check);

            string requirementName = GetNominalInfo(requirement);
            return checker.ExecuteCheck(
                () =>
                {
                    if (requirement(checker.Value))
                    {
                        var errorMessage = FluentMessage.BuildMessage($"The {{0}} satisfies the requirement {requirementName} whereas it must.").For(typeof(SUT).Name).On(checker.Value).ToString();

                        throw new FluentCheckException(errorMessage);
                    }
                },
                FluentMessage.BuildMessage($"The {{0}} does not satisfy the requirement {requirementName}.").For(typeof(SUT).Name).On(checker.Value).ToString());
        }

        private static string GetNominalInfo<SUT>(Func<SUT, bool> requirement)
        {
            return requirement.Method.DeclaringType.Name + "." + requirement.Method.Name;
        }
    }
}
