using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace LASI.Utilities
{
    /// <summary>
    /// Maps standard output operations, providing a common interface for writing to the Console, Debug, and text file output streams.
    /// </summary>
    public static class Output
    {
        #region Constructor


        static Output() {
            SetToDebug();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the current output stream to Console.Out, the default.
        /// </summary>
        public static void SetToConsole() {
            OutputMode = OutputMode.Console;
            CurrentStream = Console.Out;
        }

        /// <summary>
        /// Sets the current output to the file specified by the given path.
        /// Defaults to the current working directory of the application.
        /// </summary>
        public static void SetToFile(string path = @".\LasiLog.txt") {
            OutputMode = OutputMode.File;
            CurrentStream = new StreamWriter(path, false);

        }
        public static void SetToStringWriter(StringBuilder sb) {
            OutputMode = OutputMode.StringWriterObject;
            CurrentStream = new StringWriter(sb);
        }

        /// <summary>
        /// Sets the current output stream to Debug.Out
        /// </summary>
        public static void SetToDebug() {
            OutputMode = OutputMode.Debug;
        }

        /// <summary>
        /// Silences all textual output messages.
        /// </summary>
        public static void SetToSilent() {
            OutputMode = OutputMode.Silent;
            CurrentStream = null;
        }

        #region Write Methods

        public static void Write(bool value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        public static void Write(int value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        public static void Write(double value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        public static void Write(decimal value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        public static void Write(float value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        public static void Write(char value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        public static void Write(char[] buffer) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(buffer);
                else
                    Debug.Write(buffer.Aggregate((sum, c) => sum += c));
            }
        }
        public static void Write(char[] buffer, int index, int count) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(buffer, index, count);
                else
                    Debug.Write(buffer.Skip(index).Take(count).Aggregate((sum, c) => sum += c));
            }
        }
        public static void Write(uint value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        public static void Write(long value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        public static void Write(ulong value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        public static void Write(string value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(value);
                else
                    Debug.Write(value.ToString());
            }
        }
        public static void Write(string format, object arg0) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(format, arg0);
                else
                    Debug.Write(String.Format(format, arg0));
            }
        }
        public static void Write(string format, object arg0, object arg1) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(format, arg0, arg1);
                else
                    Debug.Write(String.Format(format, arg0, arg1));
            }
        }
        public static void Write(string format, object arg0, object arg1, object arg2) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(format, arg0, arg1, arg2);
                else
                    Debug.Write(String.Format(format, arg0, arg1, arg2));
            }
        }
        public static void Write(string format, params object[] args) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(format, args);
                else
                    Debug.Write(String.Format(format, args));
            }
        }
        public static void Write(object value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.Write(value);
                else
                    Debug.Write(value);
            }
        }

        #endregion

        #region WriteLine Methods
        public static void WriteLine() {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine();
                else
                    Debug.WriteLine("\n");
            }

        }
        public static void WriteLine(bool value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        public static void WriteLine(int value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        public static void WriteLine(double value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        public static void WriteLine(decimal value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        public static void WriteLine(float value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        public static void WriteLine(char value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        public static void WriteLine(char[] buffer) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(buffer);
                else
                    Debug.WriteLine(buffer.Aggregate((sum, c) => sum += c));
            }
        }
        public static void WriteLine(char[] buffer, int index, int count) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(buffer, index, count);
                else
                    Debug.WriteLine(buffer.Skip(index).Take(count).Aggregate((sum, c) => sum += c));
            }
        }
        public static void WriteLine(uint value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        public static void WriteLine(long value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        public static void WriteLine(ulong value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        public static void WriteLine(string value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(value);
                else
                    Debug.WriteLine(value.ToString());
            }
        }
        public static void WriteLine(string format, object arg0) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(format, arg0);
                else
                    Debug.WriteLine(String.Format(format, arg0));
            }
        }
        public static void WriteLine(string format, object arg0, object arg1) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(format, arg0, arg1);
                else
                    Debug.WriteLine(String.Format(format, arg0, arg1));
            }
        }
        public static void WriteLine(string format, object arg0, object arg1, object arg2) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(format, arg0, arg1, arg2);
                else
                    Debug.WriteLine(String.Format(format, arg0, arg1, arg2));
            }
        }
        public static void WriteLine(string format, params object[] args) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(format, args);
                else
                    Debug.WriteLine(String.Format(format, args));
            }
        }
        public static void WriteLine(object value) {
            if (OutputMode != OutputMode.Silent) {
                if (OutputMode != OutputMode.Debug)
                    CurrentStream.WriteLine(value);
                else
                    Debug.WriteLine(value);
            }
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets the System.IO.TextWriter object to which all Logger output is currently written.
        /// </summary>
        private static TextWriter CurrentStream {
            get;
            set;
        }
        /// <summary>
        /// Gets the LoggerDestination indicating where the output from the logger will be directed.
        /// </summary>
        public static OutputMode OutputMode {
            get;
            private set;
        }

        #endregion

    }

    public enum OutputMode
    {
        Console,
        Debug,
        File,
        StringWriterObject,
        Silent
    }
}
