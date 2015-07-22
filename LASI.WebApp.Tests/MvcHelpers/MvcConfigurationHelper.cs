using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace LASI.WebApp.Tests.MvcHelpers
{
    public static class MvcConfigurationHelper
    {
        public static void ConfigureMvcJsonFormatters(MvcOptions options)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                Error = (s, e) => { throw e.ErrorContext.Error; },
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            options.InputFormatters.OfType<JsonInputFormatter>().First().SerializerSettings = jsonSerializerSettings;

            options.OutputFormatters.OfType<JsonOutputFormatter>().First().SerializerSettings = jsonSerializerSettings;
        }
    }
}
