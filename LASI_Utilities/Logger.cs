using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace LASI.Utilities
{
    public static class Logger
    {
        #region Constructor

        /// <summary>
        /// Provides output stream generalization.
        /// </summary>
        static Logger() {
            SetToConsole();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the current output stream to Console.Out, the default.
        /// </summary>
        public static void SetToConsole() {
            CurrentOutputStream = Console.Out;
            OutputDestination = LoggerDestination.Console;

        }

        /// <summary>
        /// Sets the current output to the file specified by the given path.
        /// Defaults to the current working directory of the application.
        /// </summary>
        public static void SetToFile(string path = @".\LasiLog.txt") {
            OutputDestination = LoggerDestination.File;
            CurrentOutputStream = new FileInfo(path).AppendText();

        }

        /// <summary>
        /// Sets the current output stream to Debug.Out, the default.
        /// </summary>
        public static void SetToDebug() {
            OutputDestination = LoggerDestination.Debug;
        }

        #region Write Methods

        public static void Write(bool value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(value);
            else
                Debug.Write(value.ToString());
        }
        public static void Write(int value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(value);
            else
                Debug.Write(value.ToString());
        }
        public static void Write(double value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(value);
            else
                Debug.Write(value.ToString());
        }
        public static void Write(decimal value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(value);
            else
                Debug.Write(value.ToString());
        }
        public static void Write(float value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(value);
            else
                Debug.Write(value.ToString());
        }
        public static void Write(char value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(value);
            else
                Debug.Write(value.ToString());
        }
        public static void Write(char[] buffer) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(buffer);
            else
                Debug.Write(buffer.Aggregate((sum, c) => sum += c));
        }
        public static void Write(char[] buffer, int index, int count) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(buffer, index, count);
            else
                Debug.Write(buffer.Skip(index).Take(count).Aggregate((sum, c) => sum += c));
        }
        public static void Write(uint value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(value);
            else
                Debug.Write(value.ToString());
        }
        public static void Write(long value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(value);
            else
                Debug.Write(value.ToString());
        }
        public static void Write(ulong value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(value);
            else
                Debug.Write(value.ToString());
        }
        public static void Write(string value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(value);
            else
                Debug.Write(value.ToString());
        }
        public static void Write(string format, object arg0) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(format, arg0);
            else
                Debug.Write(String.Format(format, arg0));
        }
        public static void Write(string format, object arg0, object arg1) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(format, arg0, arg1);
            else
                Debug.Write(String.Format(format, arg0, arg1));
        }
        public static void Write(string format, object arg0, object arg1, object arg2) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(format, arg0, arg1, arg2);
            else
                Debug.Write(String.Format(format, arg0, arg1, arg2));
        }
        public static void Write(string format, params object[] args) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(format, args);
            else
                Debug.Write(String.Format(format, args));
        }
        public static void Write(object value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.Write(value);
            else
                Debug.Write(value);
        }

        #endregion

        #region WriteLine Methods


        public static void WriteLine(bool value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(value);
            else
                Debug.WriteLine(value.ToString());
        }
        public static void WriteLine(int value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(value);
            else
                Debug.WriteLine(value.ToString());
        }
        public static void WriteLine(double value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(value);
            else
                Debug.WriteLine(value.ToString());
        }
        public static void WriteLine(decimal value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(value);
            else
                Debug.WriteLine(value.ToString());
        }
        public static void WriteLine(float value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(value);
            else
                Debug.WriteLine(value.ToString());
        }
        public static void WriteLine(char value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(value);
            else
                Debug.WriteLine(value.ToString());
        }
        public static void WriteLine(char[] buffer) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(buffer);
            else
                Debug.WriteLine(buffer.Aggregate((sum, c) => sum += c));
        }
        public static void WriteLine(char[] buffer, int index, int count) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(buffer, index, count);
            else
                Debug.WriteLine(buffer.Skip(index).Take(count).Aggregate((sum, c) => sum += c));
        }
        public static void WriteLine(uint value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(value);
            else
                Debug.WriteLine(value.ToString());
        }
        public static void WriteLine(long value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(value);
            else
                Debug.WriteLine(value.ToString());
        }
        public static void WriteLine(ulong value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(value);
            else
                Debug.WriteLine(value.ToString());
        }
        public static void WriteLine(string value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(value);
            else
                Debug.WriteLine(value.ToString());
        }
        public static void WriteLine(string format, object arg0) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(format, arg0);
            else
                Debug.WriteLine(String.Format(format, arg0));
        }
        public static void WriteLine(string format, object arg0, object arg1) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(format, arg0, arg1);
            else
                Debug.WriteLine(String.Format(format, arg0, arg1));
        }
        public static void WriteLine(string format, object arg0, object arg1, object arg2) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(format, arg0, arg1, arg2);
            else
                Debug.WriteLine(String.Format(format, arg0, arg1, arg2));
        }
        public static void WriteLine(string format, params object[] args) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(format, args);
            else
                Debug.WriteLine(String.Format(format, args));
        }
        public static void WriteLine(object value) {
            if (OutputDestination != LoggerDestination.Debug)
                CurrentOutputStream.WriteLine(value);
            else
                Debug.WriteLine(value);
        }

        #endregion

        #endregion
        
        #region Properties

        /// <summary>
        /// Gets the System.IO.TextWriter object to which all Logger output is currently written.
        /// </summary>
        private static TextWriter CurrentOutputStream {
            get;
            set;
        }
        /// <summary>
        /// Gets the LoggerDestination indicating where the output from the logger will be directed.
        /// </summary>
        public static LoggerDestination OutputDestination {
            get;
            private set;
        }

        #endregion

    }
    public enum LoggerDestination
    {
        Console,
        Debug,
        File
    }
}
