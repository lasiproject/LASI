namespace LASI.WebService.Config
{
    public static class JsonConfiguration
    {
        public static void AddDefaultSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings)
        {
            settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        }
    }
}