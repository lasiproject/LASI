using System;
using Microsoft.Framework.Logging;

namespace AspSixApp.Logging
{
    public class OutputLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> filter;

        public OutputLoggerProvider(Func<string, LogLevel, bool> filter)
        {
            this.filter = filter;
        }
        public ILogger Create(string name)
        {
            LASI.Utilities.Logger.SetToFile(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create),
                $"WebApp_log{DateTime.Now.ToFileTimeUtc()}"));
            return new Logger(name, filter);
        }

        private class Logger : ILogger
        {
            private readonly Func<string, LogLevel, bool> filter;
            private readonly string name;

            public Logger(string name, Func<string, LogLevel, bool> filter)
            {
                this.name = name ?? typeof(LASI.Utilities.Logger).FullName;
                this.filter = filter ?? delegate { return true; };
            }
            public IDisposable BeginScope(object state) => null;

            public bool IsEnabled(LogLevel logLevel) => filter(name, logLevel);


            public void Write(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
            {
                if (IsEnabled(logLevel))
                {
                    var message = formatter != null ? formatter(state, exception) : $"{exception.Message}\n{exception.StackTrace}";

                    var severity = logLevel.ToString().ToUpperInvariant();
                    LASI.Utilities.Logger.Log($"[{severity}:{name}] {message}");
                }
            }
        }
    }
}