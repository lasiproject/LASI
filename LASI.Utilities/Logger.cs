using System;
using System.IO;
using System.Reactive.Linq;
using LASI.Utilities.Validation;

namespace LASI.Utilities
{
    /// <summary>
    /// Maps standard output operations, providing a common interface for writing to the Console, Debug, and File output streams.
    /// </summary>
    public static partial class Logger
    {
        /// <summary>
        /// Initializes the Output class, setting its destination to the System.Console.Out TextWriter.
        /// </summary>
        static Logger() => SetToConsole();

        /// <summary>
        /// Sets the current output to the file specified by the given path. Defaults to the current working directory of the application.
        /// </summary>
        public static void SetToFile(string path)
        {
            var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LASI");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var logPath = Path.Combine(directoryPath, path ?? $"LasiLog.txt");
            OutputMode = Mode.File;

            var fileStream = new FileStream(path: logPath, mode: FileMode.Append, access: FileAccess.Write, share: FileShare.Write);

            // TODO: Fix race condition which sometimes occurs here. Possible fix 1: using async calls. to WriteLine -> WriteLineAsync in all methods overloads of Log. This will likely cause some
            // mangling of output, but how much remains to be seen. Performance should be strong. Possible fix 2: wrap StreamWriter in a Synchronized wrapper by calling TextWriter.Synchronized(writer).
            // Possible fix 3: use a logging package, such as log4net. While this would provide proven and tested abstraction from thread safety issues, it is unclear of the performance implications of
            // using such a library. something critical to LASI is the ability to parallelize CPU bound operations, many if not most apps using these libraries are NOT CPU bound. The parallelized
            // operations need to be able to log messages freely. Also, it is another dependency that, at least in the case of log4net, provides countless features that will not be used. Also I
            // particularly dislike having to create a static logger for every type. It's ugly, and cluttered, and also the logic behind such granularity is questionable from a conceptual standpoint.

            writer = TextWriter.Synchronized(new StreamWriter(fileStream));
            writer.WriteLine($"LASI Log: {DateTimeOffset.Now}");

            var currentDomain = AppDomain.CurrentDomain;

            currentDomain.ProcessExit += delegate { writer.Dispose(); };
            currentDomain.DomainUnload += delegate { writer.Dispose(); };
            currentDomain.UnhandledException += delegate { writer.Dispose(); };
        }

        /// <summary>
        /// Sets the current output stream to Console.Out, the default which is the default which on run.
        /// </summary>
        public static void SetToConsole()
        {
            OutputMode = Mode.Console;
            writer = Console.Out;
        }

        /// <summary>
        /// Sets the current output stream to Debug.Out
        /// </summary>
        public static void SetToDebug()
        {
            OutputMode = Mode.Debug;
#if DEBUG
            writer = DebugOutputProxy.Instance;
#else
            writer = TextWriter.Null;
#endif
        }

        /// <summary>
        /// Sets the current output stream to the specified textWriter.
        /// </summary>
        /// <param name="textWriter">The text writer to which subsequent messages will be written.</param>
        public static void SetTo(TextWriter textWriter)
        {
            Validate.NotNull(textWriter);
            writer = textWriter;
            OutputMode = Mode.Custom;
        }

        /// <summary>
        /// Blocks all further output until a call is made to one of the following: SetToConsole, SetToFile, SetToDebug, or SetToStringBuilder.
        /// </summary>
        public static void SetToSilent()
        {
            OutputMode = Mode.Silent;
            writer = TextWriter.Null;
        }

        /// <summary>
        /// Writes a string to the text output stream.
        /// </summary>
        /// <param name="value">The string to write to the text output stream.</param>
        public static void Log(string value) => writer.WriteLine(value);

        /// <summary>
        /// Writes a formatted string to the text output stream followed by a line terminator, using the same semantics as the System.String.Format(System.String,System.Object) method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">  The first object to format and write.</param>
        /// <param name="arg1">  The second object to format and write.</param>
        /// <exception cref="System.FormatException">
        /// format is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this
        /// method overload, is one).
        /// </exception>
        public static void Log(string format, object arg0, object arg1) => writer.WriteLine(format, arg0, arg1);

        /// <summary>
        /// Writes a formatted string to the text output stream followed by a line terminator, using the same semantics as the <see cref="string.Format(string, object)"/> method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">  The first object to format and write.</param>
        /// <param name="arg1">  The second object to format and write.</param>
        /// <param name="arg2">  The third object to format and write.</param>
        /// <exception cref="System.FormatException">
        /// format is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this
        /// method overload, is one).
        /// </exception>
        public static void Log(string format, object arg0, object arg1, object arg2) => writer.WriteLine(format, arg0, arg1, arg2);

        /// <summary>
        /// Writes an object to the text output stream.
        /// </summary>
        /// <param name="value">The object to write to the text output stream.</param>
        public static void Log(object value) => writer.WriteLine(value);

        /// <summary>
        /// Writes the full details of a <see cref="Exception"/> to the text output stream.
        /// </summary>
        /// <param name="exception">             The <see cref="Exception"/> to write to the text output stream.</param>
        /// <param name="additionalInfoSelector">A function to extract additional details from the exception.</param>
        public static void Log<TException>(TException exception, Func<TException, string> additionalInfoSelector) where TException : Exception
        {
            Log(exception);
            writer.WriteLine($"AdditionalInfo: {additionalInfoSelector(exception)}");
        }

        /// <summary>
        /// Writes the full details of a <see cref="Exception"/> to the text output stream.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> to write to the text output stream.</param>
        public static void Log(Exception exception)
        {
            writer.WriteLine(exception.GetType());
            writer.WriteLine($"{nameof(exception.Message)}: {exception.Message}");
            writer.WriteLine($"{nameof(exception.StackTrace)}: {exception.StackTrace}");
            writer.WriteLine($"{nameof(exception.Data)}: {exception.Data}");
        }

        /// <summary>
        /// Gets the System.IO.TextWriter object to which all output is currently written.
        /// </summary>
        static TextWriter writer;

        /// <summary>
        /// Gets the OutputMode indicating where the output is being directed.
        /// </summary>
        public static Mode OutputMode { get; private set; }
    }
}