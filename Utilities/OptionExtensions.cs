namespace LASI.Utilities.Extra
{
    using static FunctionExtensions;
    using System.Collections.Generic;
    using System.Linq;

    public static class OptionExtensions
    {
        /// <summary>
        /// Lifts the given <paramref name="value" /> into an <see cref="SupportTypes.Option{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value to lift.</typeparam>
        /// <param name="value">The value to lift into an <see cref="SupportTypes.Option{T}"/>.</param>
        /// <returns>An <see cref="SupportTypes.Option{T}"/> containing the specified value or an empty <see cref="SupportTypes.Option{T}"/> if the element is <c>null</c>.</returns>
        public static SupportTypes.Option<T> ToOption<T>(this T value) => SupportTypes.Option.ToOption(value);
    }
}