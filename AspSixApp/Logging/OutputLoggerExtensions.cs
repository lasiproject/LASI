using System;
using Microsoft.Framework.Logging;

namespace AspSixApp.Logging
{
    using ILF = ILoggerFactory;
    public static class OutputLoggerExtensions
    {
        public static ILF AddLASIOutput(this ILF factory) => factory.Add(new OutputLoggerProvider((category, logLevel) => logLevel >= LogLevel.Information));
        public static ILF AddLASIOutput(this ILF factory, Func<string, LogLevel, bool> filter) => factory.Add(new OutputLoggerProvider(filter));

        public static ILF AddLASIOutput(this ILF factory, LogLevel minLevel) => factory.Add(new OutputLoggerProvider((category, logLevel) => logLevel >= minLevel));

        private static ILF Add(this ILF factory, ILoggerProvider provider)
        {
            factory.AddProvider(provider);
            return factory;
        }
    }
}