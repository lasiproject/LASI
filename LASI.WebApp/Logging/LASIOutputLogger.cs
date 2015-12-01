using System;
using Microsoft.Extensions.Logging;

namespace LASI.WebApp.Logging
{
    public class OutputLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> filter;

        public OutputLoggerProvider(Func<string, LogLevel, bool> filter)
        {
            this.filter = filter;
        }
        public ILogger CreateLogger(string name)
        {
            var path = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create),
                $"WebApp_log{DateTimeOffset.Now.ToFileTime()}.txt");
            Utilities.Logger.SetToFile(null);
            return new Logger(name, filter);
        }

        private class Logger : ILogger
        {
            private readonly Func<string, LogLevel, bool> filter;
            private readonly string name;

            public Logger(string name, Func<string, LogLevel, bool> filter)
            {
                this.name = name ?? typeof(Utilities.Logger).FullName;
                this.filter = filter ?? delegate { return true; };
            }
            public IDisposable BeginScope(object state) => BeginScopeImpl(state);

            public IDisposable BeginScopeImpl(object state) => new LoggerScope(state);

            public bool IsEnabled(LogLevel logLevel) => filter(name, logLevel);

            public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
            {
                if (IsEnabled(logLevel))
                {
                    var message = formatter != null ? formatter(state, exception) : $"{exception.Message}\n{exception.StackTrace}";
                    var severity = logLevel.ToString().ToUpperInvariant();
                    Utilities.Logger.Log($"[{severity}:{name}] {message}");
                }
            }

            private sealed class LoggerScope : IDisposable
            {
                public LoggerScope(object state)
                {
                    this.state = state;
                }
                private object state;

                #region IDisposable Support
                private bool disposedValue = false; // To detect redundant calls

                void Dispose(bool disposing)
                {
                    if (!disposedValue)
                    {
                        if (disposing)
                        {
                            state = null;
                        }
                        disposedValue = true;
                    }
                }
                public void Dispose()
                {
                    Dispose(true);
                }
                #endregion


            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }
        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}