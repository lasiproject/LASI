using System;
using Microsoft.Extensions.Logging;

namespace LASI.WebApp.Logging
{
    public static class OutputLoggerExtensions
    {
        public static ILoggerFactory AddLASIOutput(this ILoggerFactory factory) => factory.Add(new OutputLoggerProvider((category, logLevel) => logLevel >= LogLevel.Information));
        public static ILoggerFactory AddLASIOutput(this ILoggerFactory factory, Func<string, LogLevel, bool> filter) => factory.Add(new OutputLoggerProvider(filter));

        public static ILoggerFactory AddLASIOutput(this ILoggerFactory factory, LogLevel minLevel) => factory.Add(new OutputLoggerProvider((category, logLevel) => logLevel >= minLevel));

        private static ILoggerFactory Add(this ILoggerFactory factory, ILoggerProvider provider)
        {
            factory.AddProvider(provider);
            return factory;
        }
    }
}