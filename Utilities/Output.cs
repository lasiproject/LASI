using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.Contracts.Validators;

namespace LASI
{
    /// <summary>
    /// Maps standard output operations, providing a common interface for writing to the Console, Debug, and File output streams.
    /// </summary>
    public static class Output
    {
        #region Methods

        /// <summary>
        /// Sets the current output stream to Console.Out, the default which is the default which on run.
        /// </summary>
        public static void SetToConsole() {
            OutputMode = Mode.Console;
            writer = Console.Out;
        }
        /// <summary>
        /// Sets the current output to the file specified by the given path.
        /// Defaults to the current working directory of the application.
        /// </summary>
        public static void SetToFile(string path = @".\LasiLog.txt") {
            OutputMode = Mode.File;
            var newFile = !File.Exists(path);
            var fileStream = new FileStream(path, newFile ? FileMode.Create : FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, false);
            writer.WriteLine((newFile ? string.Empty : "\n\n") + "LASI Message Log: {0}", System.DateTime.Now);
            //Ensure fileStream is properly freed by subscribing to the DomainUnload event of the current domain. 
            //This is necessary because static classes cannot have destructors or finalizers.
            AppDomain.CurrentDomain.ProcessExit += (s, e) => writer.Close();

        }
        /// <summary>
        /// Sets the current output stream to Debug.Out
        /// </summary>
        public static void SetToDebug() {
            OutputMode = Mode.Debug;
            writer = DebugOutputProxy.Instance;
        }
        /// <summary>
        /// Sets the current output stream to the specified textWriter.
        /// </summary>
        /// <param name="outputWriter">The text writer to which subsequent messages will be written.</param>
        public static void SetTo(TextWriter outputWriter) {
            ArgumentValidator.ThrowIfNull(outputWriter, "outputWriter", "The output writer cannot be null. To disable output, call Output.SetToSilent");
            writer = outputWriter;
            OutputMode = Mode.Custom;
        }

        /// <summary>
        /// Blocks all further output until a call is made to one of the following: 
        /// SetToConsole, SetToFile, SetToDebug, or SetToStringBuilder.
        /// </summary>
        public static void SetToSilent() {
            OutputMode = Mode.Silent;
            writer.Close();
            writer = TextWriter.Null;
        }

        #region Write Methods

        /// <summary>
        /// Writes a bool to the text output stream.
        /// </summary>
        /// <param name="value">The bool to write to the text output stream.</param>
        public static void Write(bool value) { writer.Write(value); }
        /// <summary>
        /// Writes an int to the text output stream.
        /// </summary>
        /// <param name="value">The int to write to the text output stream.</param>
        public static void Write(int value) {
            writer.Write(value);
        }
        /// <summary>
        /// Writes a double to the text output stream.
        /// </summary>
        /// <param name="value">The double to write to the text output stream.</param>
        public static void Write(double value) {
            writer.Write(value);
        }
        /// <summary>
        /// Writes a decimal to the text output stream.
        /// </summary>
        /// <param name="value">The decimal to write to the text output stream.</param>
        public static void Write(decimal value) {
            writer.Write(value);
        }
        /// <summary>
        /// Writes a float to the text output stream.
        /// </summary>
        /// <param name="value">The float to write to the text output stream.</param>
        public static void Write(float value) {
            writer.Write(value);
        }
        /// <summary>
        /// Writes a character to the text output stream.
        /// </summary>
        /// <param name="value">The character to write to the text output stream.</param>
        public static void Write(char value) {
            writer.Write(value);
        }
        /// <summary>
        /// Writes a character array to the text output stream.
        /// </summary>
        /// <param name="buffer">The character array to write to the text output stream.</param>
        public static void Write(char[] buffer) { writer.Write(buffer); }
        /// <summary>
        /// Writes a subarray of characters to the text string or stream.
        /// </summary>
        /// <param name="buffer">The character array to write data from.</param>
        /// <param name="index">The character position in the buffer at which to start retrieving data.</param>
        /// <param name="count">The number of characters to write.</param>
        /// <exception cref="System.ArgumentException">The buffer length minus index is less than count.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index or count is negative.</exception>  
        public static void Write(char[] buffer, int index, int count) { writer.Write(buffer, index, count); }
        /// <summary>
        /// Writes a uint to the text output stream.
        /// </summary>
        /// <param name="value">The uint to write to the text output stream.</param>
        public static void Write(uint value) { writer.Write(value); }
        /// <summary>
        /// Writes a long to the text output stream.
        /// </summary>
        /// <param name="value">The long to write to the text output stream.</param>
        public static void Write(long value) { writer.Write(value); }
        /// <summary>
        /// Writes a ulong to the text output stream.
        /// </summary>
        /// <param name="value">The ulong to write to the text output stream.</param>
        public static void Write(ulong value) { writer.Write(value); }
        /// <summary>
        /// Writes a string to the text output stream.
        /// </summary>
        /// <param name="value">The string to write to the text output stream.</param>
        public static void Write(string value) { writer.Write(value); }
        /// <summary>
        /// Writes a formatted string to the text ouput stream, using the same semantics
        /// as the System.String.Format(System.String,System.Object) method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The object to format and write.</param>
        /// <exception cref="System.FormatException">
        /// format is not a valid composite format string.-or- The index of a format
        /// item is less than 0 (zero), or greater than or equal to the number of objects
        /// to be formatted (which, for this method overload, is one).
        /// </exception>
        public static void Write(string format, object arg0) { writer.Write(format, arg0); }
        /// <summary>
        /// Writes a formatted string to the text ouput stream, using the same semantics
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
        public static void Write(string format, object arg0, object arg1) { writer.Write(format, arg0, arg1); }
        /// <summary>
        /// Writes a formatted string to the text ouput stream, using the same semantics
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
        public static void Write(string format, object arg0, object arg1, object arg2) { writer.Write(format, arg0, arg1, arg2); }
        /// <summary>
        /// Writes a formatted string to the text ouput stream, using the same semantics
        /// as the System.String.Format(System.String,System.Object) method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format and write.</param> 
        /// <exception cref="System.FormatException">
        /// format is not a valid composite format string.-or- The index of a format
        /// item is less than 0 (zero), or greater than or equal to the number of objects
        /// to be formatted (which, for this method overload, is one).
        /// </exception>
        public static void Write(string format, params object[] args) { writer.Write(format, args); }
        /// <summary>
        /// Writes an object to the text output stream.
        /// </summary>
        /// <param name="value">The object to write to the text output stream.</param>
        public static void Write(object value) { writer.Write(value); }

        #endregion

        #region WriteLine Methods

        /// <summary>
        /// Writes a line terminator to the ouput stream.
        /// </summary>
        public static void WriteLine() {
            writer.WriteLine();
        }
        /// <summary>
        /// Writes a bool to the text output stream.
        /// </summary>
        /// <param name="value">The bool to write to the text output stream.</param>
        public static void WriteLine(bool value) { writer.WriteLine(value); }
        /// <summary>
        /// Writes a int to the text output stream.
        /// </summary>
        /// <param name="value">The int to write to the text output stream.</param>
        public static void WriteLine(int value) { writer.WriteLine(value); }
        /// <summary>
        /// Writes a double to the text output stream.
        /// </summary>
        /// <param name="value">The double to write to the text output stream.</param>
        public static void WriteLine(double value) { writer.WriteLine(value); }
        /// <summary>
        /// Writes a decimal to the text output stream.
        /// </summary>
        /// <param name="value">The decimal to write to the text output stream.</param>
        public static void WriteLine(decimal value) { writer.WriteLine(value); }
        /// <summary>
        /// Writes a float to the text output stream.
        /// </summary>
        /// <param name="value">The float to write to the text output stream.</param>
        public static void WriteLine(float value) { writer.WriteLine(value); }
        /// <summary>
        /// Writes a char to the text output stream.
        /// </summary>
        /// <param name="value">The char to write to the text output stream.</param>
        public static void WriteLine(char value) { writer.WriteLine(value); }
        /// <summary>
        /// Writes a character array followed by a line terminator to the text output stream.
        /// </summary>
        /// <param name="buffer">The character array to write to the text output stream.</param>
        public static void WriteLine(char[] buffer) { writer.WriteLine(buffer); }
        /// <summary>
        /// Writes a subarray of characters followed by a line terminator to the text output stream.
        /// </summary>
        /// <param name="buffer">The character array to write data from.</param>
        /// <param name="index">The character position in the buffer at which to start retrieving data.</param>
        /// <param name="count">The number of characters to write.</param>
        /// <exception cref="System.ArgumentException">The buffer length minus index is less than count.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index or count is negative.</exception>  
        public static void WriteLine(char[] buffer, int index, int count) {
            writer.WriteLine(buffer, index, count);
        }
        /// <summary>
        /// Writes a char to the text output stream.
        /// </summary>
        /// <param name="value">The char to write to the text output stream.</param>
        public static void WriteLine(uint value) { writer.WriteLine(value); }
        /// <summary>
        /// Writes a long to the text output stream.
        /// </summary>
        /// <param name="value">The long to write to the text output stream.</param>
        public static void WriteLine(long value) { writer.WriteLine(value); }
        /// <summary>
        /// Writes a ulong to the text output stream.
        /// </summary>
        /// <param name="value">The ulong to write to the text output stream.</param>
        public static void WriteLine(ulong value) { writer.WriteLine(value); }
        /// <summary>
        /// Writes a string to the text output stream.
        /// </summary>
        /// <param name="value">The string to write to the text output stream.</param>
        public static void WriteLine(string value) { writer.WriteLine(value); }
        /// <summary>
        /// Writes a formatted string to the text ouput stream followed by a line terminator, using the same semantics
        /// as the System.String.Format(System.String,System.Object) method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg0">The object to format and write.</param>
        /// <exception cref="System.FormatException">
        /// format is not a valid composite format string.-or- The index of a format
        /// item is less than 0 (zero), or greater than or equal to the number of objects
        /// to be formatted (which, for this method overload, is one).
        /// </exception>
        public static void WriteLine(string format, object arg0) { writer.WriteLine(format, arg0); }
        /// <summary>
        /// Writes a formatted string to the text ouput stream followed by a line terminator, using the same semantics
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
        public static void WriteLine(string format, object arg0, object arg1) { writer.WriteLine(format, arg0, arg1); }
        /// <summary>
        /// Writes a formatted string to the text ouput stream followed by a line terminator, using the same semantics
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
        public static void WriteLine(string format, object arg0, object arg1, object arg2) { writer.WriteLine(format, arg0, arg1, arg2); }
        /// <summary>
        /// Writes a formatted string to the text ouput stream followed by a line terminator, using the same semantics
        /// as the System.String.Format(System.String,System.Object) method.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format and write.</param> 
        /// <exception cref="System.FormatException">
        /// format is not a valid composite format string.-or- The index of a format
        /// item is less than 0 (zero), or greater than or equal to the number of objects
        /// to be formatted (which, for this method overload, is one).
        /// </exception>
        public static void WriteLine(string format, params object[] args) {
            writer.WriteLine(format, args);
        }
        /// <summary>
        /// Writes an object to the text output stream.
        /// </summary>
        /// <param name="value">The object to write to the text output stream.</param>
        public static void WriteLine(object value) { writer.WriteLine(value); }

        #endregion

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
            /// The default. All output will be directed to the currrent console window.
            /// </summary>
            /// <see cref="Output.SetToConsole"/>
            Console,
            /// <summary>
            /// All output will be directed to the IDE's debug window.
            /// </summary>
            /// <see cref="Output.SetToDebug"/>
            Debug,
            /// <summary>
            /// All output will be directed to a file.
            /// </summary>
            /// <see cref="Output.SetToFile"></see>
            /// <see cref="Output.SetToFile(string)"></see>
            File,
            /// <summary>
            /// All output will be directed to an externally supplied destination.
            /// </summary>
            /// <see cref="Output.SetTo(TextWriter)"/>
            Custom,
            /// <summary>
            /// No output will occur.
            /// </summary>
            /// <see cref="Output.SetToSilent"/>
            Silent
        }

        /// <summary>
        /// Initializes the Output class, setting its destination to the System.Console.Out TextWriter.
        /// </summary>
        static Output() {
            SetToConsole();
        }

    }
}