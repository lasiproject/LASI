namespace LASI.Utilities.Extra
{
    using SupportTypes;

    /// <summary>
    /// Provides a set of static methods for manipulating <see cref="Option{T}"/> values.
    /// </summary>
    public static class OptionExtensions
    {
        /// <summary>
        /// Lifts the given <paramref name="value" /> into an <see cref="SupportTypes.Option{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value to lift.</typeparam>
        /// <param name="value">The value to lift into an <see cref="SupportTypes.Option{T}"/>.</param>
        /// <returns>An <see cref="SupportTypes.Option{T}"/> containing the specified value or an empty <see cref="SupportTypes.Option{T}"/> if the element is <c>null</c>.</returns>
        public static Option<T> ToOption<T>(this T value) => Option.ToOption(value);
    }
}