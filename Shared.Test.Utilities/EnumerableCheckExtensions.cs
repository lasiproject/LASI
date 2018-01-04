using NFluent;
using NFluent.Extensibility;
using System.Collections.Generic;
using System.Linq;
using static System.String;
using static NFluent.Extensibility.FluentMessage;
using static NFluent.Extensibility.ExtensibilityHelper;

namespace LASI.Testing.Assertions
{
    public static class EnumerableCheckExtensions
    {
        public static ICheckLink<ICheck<IEnumerable<T>>> StartsWith<T>(this ICheck<IEnumerable<T>> check, params T[] expectedValues) => Check(check, expectedValues, "start");
        public static ICheckLink<ICheck<IEnumerable<T>>> EndsWith<T>(this ICheck<IEnumerable<T>> check, params T[] expectedValues) => Check(check, expectedValues, "end");

        private static ICheckLink<ICheck<IEnumerable<T>>> Check<T>(ICheck<IEnumerable<T>> check, T[] expectedValues, string thrust)
        {
            RequireNotNull(check, expectedValues);

            var checker = ExtractChecker(check);

            var value = thrust is "end"
                ? checker.Value.Reverse().ToArray()
                : checker.Value;

            var actual = value.Take(expectedValues.Length).ToList();
            return checker.ExecuteCheck(() =>
            {
                if (actual.Count < expectedValues.Length || !actual.Zip(value, EqualityComparer<T>.Default.Equals).All(x => x))
                {
                    var message = BuildMessage($"The {{0}} does not {thrust} with:\n[ {Join(", ", expectedValues)} ]\nit {thrust}s with:\n[ {Join(", ", actual)} ]")
                        .On(value)
                        .ToString();
                    throw new FluentCheckException(message);
                }
            },
            BuildMessage($"The {{0}} {thrust}s with:\n[ {Join(", ", expectedValues)} ]\nit whereas it must not.")
                .For(typeof(IEnumerable<T>).Name)
                .On(value)
                .ToString()
            );
        }

        private static void RequireNotNull<T>(ICheck<IEnumerable<T>> check, IEnumerable<T> expectedValues)
        {
            if (check == null) throw new System.ArgumentNullException(nameof(check));
            if (expectedValues == null) throw new FluentCheckException($"Cannot check against a null sequence of {nameof(expectedValues)}.");
        }
    }
}