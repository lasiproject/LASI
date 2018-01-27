using System;

namespace LASI.Testing.Assertions
{
    internal static class FuncExtensions
    {
        public static string GetNominalInfo<T>(this Func<T, bool> requirement)
        {
            var (type, method) = requirement;
            return $"{type}.{method}";
        }
    }

    internal static class DeconstructExtensions
    {
        public static void Deconstruct<T>(this Func<T, bool> f, out string type, out string method) =>
            (type, method) = (f.Method.DeclaringType.Name, f.Method.Name);
    }
}