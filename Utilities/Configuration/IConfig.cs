
namespace LASI.Utilities.Configuration
{
    /// <summary>
    /// Provides structures to support the exchange of simple, minimal configuration information between assemblies.
    /// </summary>
    /// TODO: Mark with [AssemblyNeutral] as defined by the next version (5.0) of AspNet for better interop.
    /// <summary>
    /// Describes a minimal configuration object for the exchange of simple configuration
    /// information between assemblies.
    /// </summary>
    public interface IConfig
    {
        /// <summary>Gets the value with the specified name.</summary>
        /// <param name="name">The name of the value to retrieve.</param>
        /// <returns>The value with the specified name.</returns>
        string this[string name] { get; }

        /// <summary>
        /// Gets the value with the specified name, matching based on the specified <see cref="System.StringComparison" />.
        /// </summary>
        /// <param name="name">The name of the value to retrieve.</param>
        /// <param name="stringComparison">The string comparison to use for matching the name.</param>
        /// <returns>The value with the specified name, in the context of the <see cref="System.StringComparison" />.</returns>
        string this[string name, System.StringComparison stringComparison] { get; }
    }
}