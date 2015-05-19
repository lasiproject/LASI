using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

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
            options.InputFormatters
                .Select(formatter => formatter.Instance)
                .OfType<JsonInputFormatter>()
                .First().SerializerSettings = jsonSerializerSettings;

            options.OutputFormatters
                .Select(formatter => formatter.Instance)
                .OfType<JsonOutputFormatter>()
                .First().SerializerSettings = jsonSerializerSettings;
        }
    }
}
