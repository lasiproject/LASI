using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using NFluent.Extensibility;
using static NFluent.Extensibility.FluentMessage;
using static NFluent.Extensibility.ExtensibilityHelper;

namespace LASI.Testing.Assertions
{
    public static class StopwatchCheckExtensions
    {
        public static ICheckLink<ICheck<Stopwatch>> IsNotRunning(this ICheck<Stopwatch> check)
        {
            var checker = ExtensibilityHelper.ExtractChecker(check);

            return checker.ExecuteCheck(() =>
             {
                 if (checker.Value.IsRunning)
                 {
                     throw new FluentCheckException(checker.BuildMessage("The timer is running").On(checker.Value).ToString());
                 }
             }, "The timer is not running");

        }
        public static ICheckLinkWhich<ICheck<Stopwatch>, ICheck<TimeSpan>> Elapsed(this ICheck<Stopwatch> check)
        {
            var checker = ExtensibilityHelper.ExtractChecker(check);
            var extendableCheckLink = checker.BuildLinkWhich(Check.That(checker.Value.Elapsed));
            return extendableCheckLink;
        }
        public static ICheckLink<ICheck<Stopwatch>> IsRunning(this ICheck<Stopwatch> check)
        {
            var checker = ExtensibilityHelper.ExtractChecker(check);

            return checker.ExecuteCheck(() =>
            {
                if (!checker.Value.IsRunning)
                {
                    throw new FluentCheckException(checker.BuildMessage("The timer is not running").On(checker.Value).ToString());
                }
            }, "The timer is running.");
        }
    }
}
