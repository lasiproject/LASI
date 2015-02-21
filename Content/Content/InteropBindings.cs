namespace LASI.Content.InteropBindings
{
    public class Configuration
    {
        public static void Initialize(Utilities.Configuration.IConfig config)
        {
            TaggerInterop.SharpNLPTagger.Settings = config;
            DocToDocXConverter.Config = config;
        }
    }
}