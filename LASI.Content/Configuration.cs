using LASI.Content.FileConverters;
using LASI.Utilities.Configuration;

namespace LASI.Content
{
    /// <summary>
    /// Enables programmatically deferred provisioning or overriding of resource locations.
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Programatically configure <see cref="LASI.Content"/>. Enables deferred provision or overriding values for "ResourcesDirectory", "WordnetSearchDirectory", "MaximumEntropyModelDirectory", and so on.
        /// </summary>
        /// <param name="settings">Configuration to use in lieu of defaults where specified.</param>
        public static void Initialize(IConfig settings)
        {
            TaggerInterop.SharpNLPTagger.Settings = settings;
            DocToDocXConverter.Config = settings;
        }
    }
}