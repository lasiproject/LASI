using System;
using LASI.Testing.Assertions;
using NFluent;
using NFluent.Extensibility;

namespace LASI.Testing.Assertions
{
    public static class CheckLinkExtensions
    {
        public static ICheckLink<ICheck<T>> Satisfies<T>(this ICheck<T> check, Func<T, bool> requirement)
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
            var requirementName = requirement.GetNominalInfo();
            return checker.ExecuteCheck(
                () =>
                {
                    if (!requirement(checker.Value))
                    {
                        var errorMessage = FluentMessage.BuildMessage($"The {{0}} does not satisfy the requirement {requirementName}.")
                            .On(checker.Value)
                            .ToString();

                        throw new FluentCheckException(errorMessage);
                    }
                },
                FluentMessage.BuildMessage($"The {{0}} satisifies the requirement {requirementName} whereas it must not.")
                    .On(checker.Value)
                    .ToString()
            );
        }

        public static ICheckLink<ICheck<T>> Satisfies<T>(this ICheck<T> check, Func<bool> requirement)
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
            // e.g.:;
            return checker.ExecuteCheck(
                () =>
                {
                    var req = requirement();
                    if (!req)
                    {
                        var errorMessage = FluentMessage.BuildMessage($"The {{0}} does not satisfy the requirement{requirement}.")
                            .On(checker.Value)
                            .ToString();

                        throw new FluentCheckException(errorMessage);
                    }
                },
                FluentMessage.BuildMessage($"The {{0}} satisifies the requirement {requirement} whereas it must not.")
                    .On(checker.Value)
                    .ToString()
                );
        }

        public static ICheckLink<ICheck<T>> Satisfies<T>(this ICheck<T> check, bool requirement)
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
            // e.g.:;
            return checker.ExecuteCheck(
                () =>
                {
                    if (!requirement)
                    {
                        var errorMessage = FluentMessage.BuildMessage($"The {{0}} does not satisfy the requirement{requirement}.")
                            .On(checker.Value)
                            .ToString();

                        throw new FluentCheckException(errorMessage);
                    }
                },
                FluentMessage.BuildMessage($"The {{0}} satisifies the requirement {requirement} whereas it must not.")
                    .On(checker.Value)
                    .ToString()
                );
        }

        public static ICheckLink<ICheck<T>> DoesNotSatisfy<T>(this ICheck<T> check, Func<T, bool> requirement)
        {
            var checker = ExtensibilityHelper.ExtractChecker(check);

            var requirementName = requirement.GetNominalInfo();
            return checker.ExecuteCheck(
                () =>
                {
                    if (requirement(checker.Value))
                    {
                        var errorMessage = FluentMessage.BuildMessage($"The {{0}} satisfies the requirement {requirementName} whereas it must not.")
                            .On(checker.Value)
                            .ToString();

                        throw new FluentCheckException(errorMessage);
                    }
                },
                FluentMessage.BuildMessage($"The {{0}} does not satisfy the requirement {requirementName}.")
                    .On(checker.Value)
                    .ToString()
            );
        }

        public static ICheckLink<ICheck<T>> DoesNotSatisfy<T>(this ICheck<T> check, Func<bool> requirement)
        {
            var checker = ExtensibilityHelper.ExtractChecker(check);

            return checker.ExecuteCheck(
                () =>
                {
                    if (requirement())
                    {
                        var errorMessage = FluentMessage.BuildMessage($"The {{0}} satisfies the requirement {requirement} whereas it must not.")
                            .On(checker.Value)
                            .ToString();

                        throw new FluentCheckException(errorMessage);
                    }
                },
                FluentMessage.BuildMessage($"The {{0}} does not satisfy the requirement {requirement}.")
                    .On(checker.Value)
                    .ToString()
            );
        }

        public static ICheckLink<ICheck<T>> DoesNotSatisfy<T>(this ICheck<T> check, bool requirement)
        {
            var checker = ExtensibilityHelper.ExtractChecker(check);

            return checker.ExecuteCheck(
                () =>
                {
                    if (requirement)
                    {
                        var errorMessage = FluentMessage.BuildMessage($"The {{0}} satisfies the requirement {requirement} whereas it must not.")
                            .On(checker.Value)
                            .ToString();

                        throw new FluentCheckException(errorMessage);
                    }
                },
                FluentMessage.BuildMessage($"The {{0}} does not satisfy the requirement {requirement}.")
                    .For(typeof(T).Name)
                    .On(checker.Value)
                    .ToString()
            );
        }
    }
}