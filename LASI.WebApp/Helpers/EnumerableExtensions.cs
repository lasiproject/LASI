using System.Collections.Generic;
using Microsoft.AspNet.Mvc.OptionDescriptors;

namespace LASI.WebApp.Helpers
{
    using Utilities;
    using System;
    using System.Linq;
    using OptionDescriptorEnumerable = IEnumerable<IOptionDescriptor<object>>;

    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns all options corresponding to a constituent <see cref="IOptionDescriptor{TOption}"/> describing an option of type <typeparamref name="TOption"/>.
        /// </summary>
        /// <typeparam name="TOption">The type described.</typeparam>
        /// <param name="descriptors">The <see cref="IEnumerable{IOptionDescriptor}"/> to filter.</param>
        /// <returns>
        /// All options which for which a constituent <see cref="IOptionDescriptor{TOption}"/> describes an option of type <typeparamref name="TOption"/>.
        /// </returns>
        public static IEnumerable<TOption> DescribingType<TOption>(this OptionDescriptorEnumerable descriptors) => descriptors.DescribedOfType<TOption>();

        /// <summary>
        /// Returns the first <typeparamref name="TOption"/> corresponding to a constituent <see cref="IOptionDescriptor{TOption}"/>.
        /// </summary>
        /// <typeparam name="TOption">The type described.</typeparam>
        /// <param name="descriptors">The <see cref="IEnumerable{IOptionDescriptor}"/> to search.</param>
        /// <returns>The first <typeparamref name="TOption"/> corresponding to a constituent <see cref="IOptionDescriptor{TOption}"/>.</returns>
        public static TOption FirstDescribing<TOption>(this OptionDescriptorEnumerable descriptors) => descriptors.DescribedOfType<TOption>().First();

        /// <summary>
        /// Returns the first <typeparamref name="TOption"/> corresponding to a constituent <see cref="IOptionDescriptor{TOption}"/> and matching the specified predicate.
        ///</summary>
        /// <typeparam name="TOption">The type described.</typeparam>
        /// <param name="descriptors">The <see cref="IEnumerable{IOptionDescriptor}"/> to search.</param>
        /// <param name="predicate">The predicate to by which to filter.</param>
        /// <returns>
        /// The first <typeparamref name="TOption"/> corresponding to a constituent <see cref="IOptionDescriptor{TOption}"/> and matching the specified predicate.
        /// </returns>
        public static TOption FirstDescribing<TOption>(this OptionDescriptorEnumerable descriptors, Func<TOption, bool> predicate) => descriptors.DescribedOfType<TOption>().First(predicate);

        /// <summary>
        /// Returns the first <typeparamref name="TOption"/> corresponding to a constituent <see cref="IOptionDescriptor{TOption}"/> or the
        /// default value of type <typeparamref name="TOption"/> if none can be found.
        /// </summary>
        /// <typeparam name="TOption">The type described.</typeparam>
        /// <param name="descriptors">The <see cref="IEnumerable{IOptionDescriptor}"/> to search.</param>
        /// <returns>
        /// The first <typeparamref name="TOption"/> corresponding to a constituent <see cref="IOptionDescriptor{TOption}"/> or the default
        /// value of type <typeparamref name="TOption"/> if none can be found.
        /// </returns>
        public static TOption FirstDescribingOrDefault<TOption>(this OptionDescriptorEnumerable descriptors) => descriptors.DescribedOfType<TOption>().First();

        /// <summary>
        /// Returns the first <typeparamref name="TOption"/> corresponding to a constituent <see cref="IOptionDescriptor{TOption}"/> and matching the specified predicate or the default value of type <typeparamref name="TOption"/> if none can be found.
        ///</summary>
        /// <typeparam name="TOption">The type described.</typeparam>
        /// <param name="descriptors">The <see cref="IEnumerable{IOptionDescriptor}"/> to search.</param>
        /// <param name="predicate">The predicate to by which to filter.</param>
        /// <returns>
        /// The first <typeparamref name="TOption"/> corresponding to a constituent <see cref="IOptionDescriptor{TOption}"/> and matching the specified predicate or the default value of type <typeparamref name="TOption"/> if none can be found.
        /// </returns>
        public static TOption FirstDescribingOrDefault<TOption>(this OptionDescriptorEnumerable descriptors, Func<TOption, bool> predicate) => descriptors.DescribedOfType<TOption>().First(predicate);

        /// <summary>
        /// Invokes the specified action for all options corresponding to a constituent <see cref="IOptionDescriptor{TOption}"/> describing
        /// an option of type <typeparamref name="TOption"/>.
        /// </summary>
        /// <typeparam name="TOption">The type described.</typeparam>
        /// <param name="descriptors">
        /// The <see cref="IEnumerable{IOptionDescriptor}"/> s whose matching options the specified action will be invoked on.
        /// </param>
        /// <param name="action">The action to invoke for each <typeparamref name="TOption"/> described by a constituent descriptor.</param>
        public static void ForEachDescribing<TOption>(this OptionDescriptorEnumerable descriptors, Action<TOption> action) => descriptors.DescribedOfType<TOption>().ForEach(action);

        #region Helpers

        /// <summary>
        /// Returns all options corresponding to a constituent <see cref="IOptionDescriptor{TOption}"/> describing an option of type <typeparamref name="TOption"/>.
        /// </summary>
        /// <typeparam name="TOption">The type described.</typeparam>
        /// <param name="descriptors">The <see cref="IEnumerable{IOptionDescriptor}"/> to filter.</param>
        /// <returns>
        /// All options which for which a constituent <see cref="IOptionDescriptor{TOption}"/> describes an option of type <typeparamref name="TOption"/>.
        /// </returns>
        private static IEnumerable<TOption> DescribedOfType<TOption>(this OptionDescriptorEnumerable descriptors) =>
            from descriptor in descriptors
            let instance = descriptor.Instance
            where instance is TOption
            select (TOption)instance;

        #endregion Helpers
    }
}