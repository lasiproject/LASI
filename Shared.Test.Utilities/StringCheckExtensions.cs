using NFluent;
using NFluent.Extensibility;
using System.Collections.Generic;
using System.Linq;
using static System.String;
using static NFluent.Extensibility.FluentMessage;
using static NFluent.Extensibility.ExtensibilityHelper;
using System.IO;

namespace LASI.Testing.Assertions
{
    public static class StringCheckExtensions
    {
        public static ICheckLink<ICheck<FileSystemInfo>> DoesNotExist(this ICheck<string> check) => check.Not.Exists();
        public static ICheckLink<ICheck<FileSystemInfo>> DoesNotExist(this ICheck<FileSystemInfo> check) => check.Not.Exists();

        public static ICheckLink<ICheck<FileSystemInfo>> Exists(this ICheck<string> check)
        {
            var c = Check.That(Directory.GetParent(ExtractChecker(check).Value)); ;
            return c.Exists();
        }
        public static ICheckLink<ICheck<FileSystemInfo>> Exists(this ICheck<FileSystemInfo> check)
        {
            var checker = ExtractChecker(check);
            var value = checker.Value;
            return checker.ExecuteCheck(() =>
            {
                if (!value.Exists)
                {
                    throw new FluentCheckException(BuildMessage($@"{(value is DirectoryInfo
                        ? "Directory"
                        : "File")}""{value.Name}"" does not exist in\n""{(value is FileInfo fy
                            ? fy.DirectoryName
                            : value is DirectoryInfo dx
                                ? dx.Parent.FullName
                                : default)}""and it must.").On(value).ToString());
                }
            },
            BuildMessage($@"{(value is DirectoryInfo
                        ? "Directory"
                        : "File")}""{value.Name}"" exists in\n""{(value is FileInfo f
                            ? f.DirectoryName
                            : value is DirectoryInfo d
                                ? d.Parent.FullName
                                : default)}""and it must not exist.").On(value).ToString()
            );
        }
    }
}
