namespace LASI.Utilities.Configuration
{
    /// <summary>
    /// Provides a base class for implementations of the <see cref="IConfig"/> Interface.
    /// </summary>
    public abstract class ConfigBase : LoadableConfigBase, IConfig
    { 
        /// <summary>
        /// Gets the value with the specified key.
        /// </summary>
        /// <param name="name">The name of the value to retrieve.</param>
        /// <returns>The value with the specified key under the specified string comparison.</returns>
        public abstract string this[string name] { get; }

    }
}