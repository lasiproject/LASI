using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI
{
    /// <summary>
    /// Maps standard output operations, providing a common interface for writing to the Console, Debug, and File output streams.
    /// </summary>
    public static class Output
    {
        #region Static Constructor

        static Output()
        {
            SetToConsole();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the current output stream to Console.Out, the default.
        /// </summary>
        public static void SetToConsole()
        {
            OutputMode = OutputMode.Console;
            currentStream = Console.Out as StreamWriter;
        }

        /// <summary>
        /// Sets the current output to the file specified by the given path.
        /// Defaults to the current working directory of the application.
        /// </summary>
        public static void SetToFile(string path = @".\LasiLog.txt")
        {
            OutputMode = OutputMode.File;
            var newFile = !File.Exists(path);
            fileStream = new FileStream(path, newFile ? FileMode.Create : FileMode.Append, FileAccess.Write);
            using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.WriteLine((newFile ? "" : "\n\n") + "LASI Message Log: {0}", System.DateTime.Now); }
            //Ensure fileStream is properly freed by subscribing to the DomainUnload event of the current domain. 
            //This is necessary because static classes cannot have destructors or finalizers.
            AppDomain.CurrentDomain.DomainUnload += (s, e) => { fileStream.Flush(); fileStream.Dispose(); };
        }

        private static FileStream fileStream;

        /// <summary>
        /// Sets the current output stream to Debug.Out
        /// </summary>
        public static void SetToDebug()
        {
            OutputMode = OutputMode.Debug;
        }

        /// <summary>
        /// Blocks all further output until a call is made to one of the following: 
        /// SetToConsole, SetToFile, SetToDebug, or SetToStringBuilder.
        /// </summary>
        public static void SilenceAll()
        {
            OutputMode = OutputMode.Silent;
            currentStream = null;
        }

        #region Write Methods
        /// <summary>
        /// Writes a bool to the text output stream.
        /// </summary>
        /// <param name="value">The bool to write to the text output stream.</param>
        public static void Write(bool value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }

        /// <summary>
        /// Writes an int to the text output stream.
        /// </summary>
        /// <param name="value">The int to write to the text output stream.</param>
        public static void Write(int value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }

        /// <summary>
        /// Writes a double to the text output stream.
        /// </summary>
        /// <param name="value">The double to write to the text output stream.</param>
        public static void Write(double value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        /// <summary>
        /// Writes a decimal to the text output stream.
        /// </summary>
        /// <param name="value">The decimal to write to the text output stream.</param>
        public static void Write(decimal value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        /// <summary>
        /// Writes a float to the text output stream.
        /// </summary>
        /// <param name="value">The float to write to the text output stream.</param>
        public static void Write(float value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        /// <summary>
        /// Writes a character to the text output stream.
        /// </summary>
        /// <param name="value">The character to write to the text output stream.</param>
        public static void Write(char value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        /// <summary>
        /// Writes a character array to the text output stream.
        /// </summary>
        /// <param name="buffer">The character array to write to the text output stream.</param>
        public static void Write(char[] buffer)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(buffer); } else if (OutputMode == OutputMode.Console)
                    Console.Write(buffer);
                else
                    Debug.Write(buffer.Aggregate((sum, c) => sum += c));
            }
        }
        /// <summary>
        /// Writes a subarray of characters to the text string or stream.
        /// </summary>
        /// <param name="buffer">The character array to write data from.</param>
        /// <param name="index">The character position in the buffer at which to start retrieving data.</param>
        /// <param name="count">The number of characters to write.</param>
        /// <exception cref="System.ArgumentException">The buffer length minus index is less than count.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index or count is negative.</exception>  
        public static void Write(char[] buffer, int index, int count)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(buffer, index, count); } else if (OutputMode == OutputMode.Console)
                    Console.Write(buffer, index, count);
                else
                    Debug.Write(buffer.Skip(index).Take(count).Aggregate((sum, c) => sum += c));
            }
        }
        /// <summary>
        /// Writes a uint to the text output stream.
        /// </summary>
        /// <param name="value">The uint to write to the text output stream.</param>
        public static void Write(uint value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        /// <summary>
        /// Writes a long to the text output stream.
        /// </summary>
        /// <param name="value">The long to write to the text output stream.</param>
        public static void Write(long value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        /// <summary>
        /// Writes a ulong to the text output stream.
        /// </summary>
        /// <param name="value">The ulong to write to the text output stream.</param>
        public static void Write(ulong value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        /// <summary>
        /// Writes a string to the text output stream.
        /// </summary>
        /// <param name="value">The string to write to the text output stream.</param>
        public static void Write(string value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
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
        public static void Write(string format, object arg0)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(format, arg0); } else if (OutputMode == OutputMode.Console)
                    Console.Write(String.Format(format, arg0));
                else
                    Debug.Write(String.Format(format, arg0));
            }
        }
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
        public static void Write(string format, object arg0, object arg1)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(format, arg0, arg1); } else if (OutputMode == OutputMode.Console)
                    Console.Write(String.Format(format, arg0, arg1));
                else
                    Debug.Write(String.Format(format, arg0, arg1));
            }
        }
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
        public static void Write(string format, object arg0, object arg1, object arg2)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(format, arg0, arg1, arg2); } else if (OutputMode == OutputMode.Console)
                    Console.Write(String.Format(format, arg0, arg1, arg2));
                else
                    Debug.Write(String.Format(format, arg0, arg1, arg2));
            }
        }
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
        public static void Write(string format, params object[] args)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(format, args); } else if (OutputMode == OutputMode.Console)
                    Console.Write(format, args);
                else
                    Debug.Write(String.Format(format, args));
            }
        }
        /// <summary>
        /// Writes an object to the text output stream.
        /// </summary>
        /// <param name="value">The object to write to the text output stream.</param>
        public static void Write(object value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.Write(value);
                else
                    Debug.Write(value);
            }
        }

        #endregion

        #region WriteLine Methods
        /// <summary>
        /// Writes a line terminator to the ouput stream.
        /// </summary>
        public static void WriteLine()
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.WriteLine(); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine();
                else
                    Debug.WriteLine("");
            }

        }
        /// <summary>
        /// Writes a bool to the text output stream.
        /// </summary>
        /// <param name="value">The bool to write to the text output stream.</param>
        public static void WriteLine(bool value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        /// <summary>
        /// Writes a int to the text output stream.
        /// </summary>
        /// <param name="value">The int to write to the text output stream.</param>
        public static void WriteLine(int value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        /// <summary>
        /// Writes a double to the text output stream.
        /// </summary>
        /// <param name="value">The double to write to the text output stream.</param>
        public static void WriteLine(double value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        /// <summary>
        /// Writes a decimal to the text output stream.
        /// </summary>
        /// <param name="value">The decimal to write to the text output stream.</param>
        public static void WriteLine(decimal value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        /// <summary>
        /// Writes a float to the text output stream.
        /// </summary>
        /// <param name="value">The float to write to the text output stream.</param>
        public static void WriteLine(float value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        /// <summary>
        /// Writes a char to the text output stream.
        /// </summary>
        /// <param name="value">The char to write to the text output stream.</param>
        public static void WriteLine(char value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        /// <summary>
        /// Writes a character array followed by a line terminator to the text output stream.
        /// </summary>
        /// <param name="buffer">The character array to write to the text output stream.</param>
        public static void WriteLine(char[] buffer)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.WriteLine(buffer); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(buffer);
                else
                    Debug.WriteLine(buffer.Aggregate((sum, c) => sum += c));
            }
        }
        /// <summary>
        /// Writes a subarray of characters followed by a line terminator to the text output stream.
        /// </summary>
        /// <param name="buffer">The character array to write data from.</param>
        /// <param name="index">The character position in the buffer at which to start retrieving data.</param>
        /// <param name="count">The number of characters to write.</param>
        /// <exception cref="System.ArgumentException">The buffer length minus index is less than count.</exception>
        /// <exception cref="System.ArgumentNullException">The buffer parameter is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index or count is negative.</exception>  
        public static void WriteLine(char[] buffer, int index, int count)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.WriteLine(buffer, index, count); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(buffer, index, count);
                else
                    Debug.WriteLine(buffer.Skip(index).Take(count).Aggregate((sum, c) => sum += c));
            }
        }
        /// <summary>
        /// Writes a char to the text output stream.
        /// </summary>
        /// <param name="value">The char to write to the text output stream.</param>
        public static void WriteLine(uint value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        /// <summary>
        /// Writes a long to the text output stream.
        /// </summary>
        /// <param name="value">The long to write to the text output stream.</param>
        public static void WriteLine(long value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        /// <summary>
        /// Writes a ulong to the text output stream.
        /// </summary>
        /// <param name="value">The ulong to write to the text output stream.</param>
        public static void WriteLine(ulong value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        /// <summary>
        /// Writes a string to the text output stream.
        /// </summary>
        /// <param name="value">The string to write to the text output stream.</param>
        public static void WriteLine(string value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
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
        public static void WriteLine(string format, object arg0)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.WriteLine(format, arg0); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(format, arg0);
                else
                    Debug.WriteLine(String.Format(format, arg0));
            }
        }
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
        public static void WriteLine(string format, object arg0, object arg1)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.WriteLine(format, arg0, arg1); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(format, arg0, arg1);
                else
                    Debug.WriteLine(String.Format(format, arg0, arg1));
            }
        }
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
        public static void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.WriteLine(format, arg0, arg1, arg2); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(format, arg0, arg1, arg2);
                else
                    Debug.WriteLine(String.Format(format, arg0, arg1, arg2));
            }
        }
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
        public static void WriteLine(string format, params object[] args)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.WriteLine(format, args); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(format, args);
                else
                    Debug.WriteLine(String.Format(format, args));
            }
        }
        /// <summary>
        /// Writes an object to the text output stream.
        /// </summary>
        /// <param name="value">The object to write to the text output stream.</param>
        public static void WriteLine(object value)
        {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode == OutputMode.File)
                    using (var writer = new StreamWriter(fileStream, Encoding.ASCII, 1024, true)) { writer.Write(value); } else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(value);
                else if (OutputMode == OutputMode.Console)
                    Console.WriteLine(value);
                else
                    Debug.WriteLine(value);
            }
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets the System.IO.TextWriter object to which all output is currently written.
        /// </summary>
        private static StreamWriter currentStream;

        /// <summary>
        /// Gets the OutputMode indicating where the output is being directed.
        /// </summary>
        public static OutputMode OutputMode
        {
            get;
            private set;
        }

        #endregion

    }
    /// <summary>
    /// Defines the OutputModes of the Output class.
    /// </summary>
    public enum OutputMode
    {
        /// <summary>
        /// All output will be redirected to the currrent console window.
        /// </summary>
        Console,
        /// <summary>
        /// All output will be redirected to the currrent debug window.
        /// </summary>
        Debug,
        /// <summary>
        /// All output will be redirected to a log file.
        /// </summary>
        File,
        /// <summary>
        /// All output will be hidden.
        /// </summary>
        Silent
    }
}
