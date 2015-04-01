using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using LASI.Utilities.Validation;

namespace LASI.Utilities
{
    /// <summary>
    /// Maps standard output operations, providing a common interface for writing to the Console, Debug, and File output streams.
    /// </summary>
    public static class Logger
    {
        #region Static Constructor

        /// <summary>
        /// Initializes the Output class, setting its destination to the System.Console.Out TextWriter.
        /// </summary>
        static Logger() { SetToConsole(); }

        #endregion Static Constructor

        #region Methods

        /// <summary>
        /// Sets the current output stream to Console.Out, the default which is the default which on run.
        /// </summary>
        public static void SetToConsole()
        {
            OutputMode = Mode.Console;
            writer = Console.Out;
        }

        /// <summary>
        /// Sets the current output to the file specified by the given path.
        /// Defaults to the current working directory of the application.
        /// </summary>
        public static void SetToFile(string path)
        {
            var logPath = Path.Combine(Environment.SpecialFolder.ApplicationData.ToString(), path ?? $"./LasiLog{DateTime.Now}.txt");
            OutputMode = Mode.File;

            var fileStream = new FileStream(path: logPath, mode: FileMode.OpenOrCreate);

            // TODO: Fix race condition which sometimes occurs here. 
            // Possible fix 1: using async calls. to WriteLine -> WriteLineAsync in all methods overloads of Log.
            // This will likely cause some mangling of output, but how much remains to be seen. Performance should be
            // strong.
            // Possible fix 2: wrap StreamWriter in a Synchronized wrapper by calling TextWriter.Synchronized(writer).
            // Possible fix 3: use a logging pacakge, such as log4net. While this would provide proven and tested 
            // abstraction from thread safety issues, it is unclear of the performance implications of using such a library.
            // something critical to LASI is the abbility to parallelize CPU bound operations, many if not most apps 
            // using these libraries are NOT cpu bound. The parallelized operations need to be able to log messages freely.
            // Also, it is another dependency that, at least in the case of log4net, provides countless features that will
            // not be used. Also I particularly dislike having to create a static logger for every type. It's ugly, and cluttered,
            // and also the logic behind such granularity is questionable from a conceptual standpoint.

            writer = TextWriter.Synchronized(new StreamWriter(fileStream));
            writer.WriteLine($"LASI Message Log: {DateTime.Now}");

            AppDomain.CurrentDomain.ProcessExit += delegate { writer.Dispose(); };
            AppDomain.CurrentDomain.DomainUnload += delegate { writer.Dispose(); };
            AppDomain.CurrentDomain.UnhandledException += delegate { writer.Dispose(); };
        }

        /// <summary>
        /// Sets the current output stream to Debug.Out
        /// </summary>
        public static void SetToDebug()
        {
            OutputMode = Mode.Debug;
            writer = DebugOutputProxy.Instance;
        }

        /// <summary>
        /// Sets the current output stream to the specified textWriter.
        /// </summary>
        /// <param name="outputWriter">The text writer to which subsequent messages will be written.</param>
        public static void SetTo(TextWriter outputWriter)
        {
            Validate.NotNull(outputWriter, "outputWriter", "The output writer cannot be null. To disable output, call Output.SetToSilent");
            writer = outputWriter;
            OutputMode = Mode.Custom;
        }

        /// <summary>
        /// Blocks all further output until a call is made to one of the following: 
        /// SetToConsole, SetToFile, SetToDebug, or SetToStringBuilder.
        /// </summary>
        public static void SetToSilent()
        {
            OutputMode = Mode.Silent;
            if (OutputMode == Mode.File) { writer.Close(); }
            writer = TextWriter.Null;
        }

        /// <summary>
        /// Writes a string to the text output stream.
        /// </summary>
        /// <param name="value">The string to write to the text output stream.</param>
        public static void Log(string value) { if (OutputMode != Mode.Silent) writer.WriteLine(value); }

        /// <summary>
        /// Writes a formatted string to the text output stream followed by a line terminator, using the same semantics
        /// as the System.String.Format(System.String,System.Object) method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and write.</param>
        /// <param name="arg1">The second object to format and write.</param>
        /// <exception cref="System.FormatException">
        /// format is not a valid composite format string.-or- The index of a format
        /// item is less than 0 (zero), or greater than or equal to the number of objects
        /// to be formatted (which, for this method overload, is one).
        /// </exception>
        public static void Log(string format, object arg0, object arg1) { if (OutputMode != Mode.Silent) writer.WriteLine(format, arg0, arg1); }
        /// <summary>
        /// Writes a formatted string to the text output stream followed by a line terminator, using the same semantics
        /// as the System.String.Format(System.String,System.Object) method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The first object to format and write.</param>
        /// <param name="arg1">The second object to format and write.</param>
        /// <param name="arg2">The third object to format and write.</param>
        /// <exception cref="System.FormatException">
        /// format is not a valid composite format string.-or- The index of a format
        /// item is less than 0 (zero), or greater than or equal to the number of objects
        /// to be formatted (which, for this method overload, is one).
        /// </exception>
        public static void Log(string format, object arg0, object arg1, object arg2) { if (OutputMode != Mode.Silent) writer.WriteLine(format, arg0, arg1, arg2); }

        /// <summary>
        /// Writes an object to the text output stream.
        /// </summary>
        /// <param name="value">The object to write to the text output stream.</param>
        public static void Log(object value) { if (OutputMode != Mode.Silent) writer.WriteLine(value); }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the System.IO.TextWriter object to which all output is currently written.
        /// </summary>
        private static TextWriter writer;
        /// <summary>
        /// Gets the OutputMode indicating where the output is being directed.
        /// </summary>
        public static Mode OutputMode { get; private set; }

        #endregion

        #region Classes 
        private class DebugOutputProxy : TextWriter
        {
            private DebugOutputProxy() { }
            internal static readonly DebugOutputProxy Instance = new DebugOutputProxy();
            public override void Write(bool value) { Debug.Write(value); }
            public override void Write(char value) { Debug.Write(value); }
            public override void Write(char[] buffer) { Debug.Write(new string(buffer)); }
            public override void Write(char[] buffer, int index, int count) { Debug.Write(string.Join(string.Empty, buffer.Skip(index).Take(count))); }
            public override void Write(decimal value) { Debug.Write(value); }
            public override void Write(double value) { Debug.Write(value); }
            public override void Write(float value) { Debug.Write(value); }
            public override void Write(int value) { Debug.Write(value); }
            public override void Write(long value) { Debug.Write(value); }
            public override void Write(object value) { Debug.Write(value); }
            public override void Write(string format, object arg0) { Debug.Write(string.Format(format, arg0)); }
            public override void Write(string format, object arg0, object arg1) { Debug.Write(string.Format(format, arg0, arg1)); }
            public override void Write(string format, object arg0, object arg1, object arg2) { Debug.Write(string.Format(format, arg0, arg1, arg2)); }
            public override void Write(string format, params object[] arg) { Debug.Write(string.Format(format, arg)); }
            public override void Write(string value) { Debug.Write(value); }
            public override void Write(uint value) { Debug.Write(value); }
            public override void Write(ulong value) { Debug.Write(value); }
            public override void WriteLine() { Debug.WriteLine(string.Empty); }
            public override void WriteLine(bool value) { Debug.WriteLine(value); }
            public override void WriteLine(char value) { Debug.WriteLine(value); }
            public override void WriteLine(char[] buffer) { Debug.WriteLine(new string(buffer)); }
            public override void WriteLine(char[] buffer, int index, int count) { Debug.WriteLine(string.Join(string.Empty, buffer.Skip(index).Take(count))); }
            public override void WriteLine(decimal value) { Debug.WriteLine(value); }
            public override void WriteLine(double value) { Debug.WriteLine(value); }
            public override void WriteLine(float value) { Debug.WriteLine(value); }
            public override void WriteLine(int value) { Debug.WriteLine(value); }
            public override void WriteLine(long value) { Debug.WriteLine(value); }
            public override void WriteLine(object value) { Debug.WriteLine(value); }
            public override void WriteLine(string format, object arg0) { Debug.WriteLine(format, arg0); }
            public override void WriteLine(string format, object arg0, object arg1) { Debug.WriteLine(format, arg0, arg1); }
            public override void WriteLine(string format, object arg0, object arg1, object arg2) { Debug.WriteLine(format, arg0, arg1, arg2); }
            public override void WriteLine(string format, params object[] arg) { Debug.WriteLine(format, arg); }
            public override void WriteLine(string value) { Debug.WriteLine(value); }
            public override void WriteLine(uint value) { Debug.WriteLine(value); }
            public override void WriteLine(ulong value) { Debug.WriteLine(value); }
            public override Encoding Encoding { get { return Encoding.ASCII; } }
            public override void Close() { }
        }
        #endregion

        /// <summary>
        /// Defines the output modes of the Output class.
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// The default. All output will be directed to the current console window.
            /// </summary>
            /// <seealso cref="SetToConsole"/>
            Console,
            /// <summary>
            /// All output will be directed to the IDE's debug window.
            /// </summary>
            /// <seealso cref="SetToDebug"/>
            Debug,
            /// <summary>
            /// All output will be directed to a file.
            /// </summary>
            /// <seealso cref="SetToFile(string)" />
            File,
            /// <summary>
            /// All output will be directed to an externally supplied destination.
            /// </summary>
            /// <seealso cref="SetTo(TextWriter)"/>
            Custom,
            /// <summary>
            /// No output will occur.
            /// </summary>
            /// <seealso cref="SetToSilent"/>
            Silent
        }
    }
}