using LASI.Content.FileConverters;
using LASI.Utilities.Configuration;

namespace LASI.Content
{
    public static class Configuration
    {
        public static void Initialize(IConfig settings)
        {
            TaggerInterop.SharpNLPTagger.Settings = settings;
            DocToDocXConverter.Config = settings;
        }
    }
}
