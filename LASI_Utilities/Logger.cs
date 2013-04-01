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
        public static void SetDefaultStream(StreamWriter stream) {
            DefaultStream = stream;
        }

        public static StreamWriter DefaultStream {
            get;
            private set;
        }

        public static void Log(bool value) {
            DefaultStream.Write(value);
        }
        public static void Log(int value) {
            DefaultStream.Write(value);
        }
        public static void Log(double value) {
            DefaultStream.Write(value);
        }
        public static void Log(decimal value) {
            DefaultStream.Write(value);
        }
        public static void Log(float value) {
            DefaultStream.Write(value);
        }
        public static void Log(char value) {
            DefaultStream.Write(value);
        }
        public static void Log(char[] buffer) {
            DefaultStream.Write(buffer);
        }
        public static void Log(char[] buffer, int index, int count) {
            DefaultStream.Write(buffer, index, count);
        }
        public static void Log(uint value) {
            DefaultStream.Write(value);
        }
        public static void Log(long value) {
            DefaultStream.Write(value);
        }
        public static void Log(ulong value) {
            DefaultStream.Write(value);
        }
        public static void Log(string value) {
            DefaultStream.Write(value);
        }
        public static void Log(string format, object arg0) {
            DefaultStream.Write(format, arg0);
        }
        public static void Log(string format, object arg0, object arg1) {
            DefaultStream.Write(format, arg0, arg1);
        }
        public static void Log(string format, object arg0, object arg1, object arg2) {
            DefaultStream.Write(format, arg0, arg1, arg2);
        }
        public static void Log(string format, params object[] args) {
            DefaultStream.Write(format, args);
        }
        public static void Log(object value) {
            DefaultStream.Write(value);
        }


        public abstract class LoggerStream
        {
            protected LoggerStream(TextWriter stream) {
            }


        }
        public sealed class ConsoleLogger : LoggerStream
        {
            public ConsoleLogger()
                : base(Console.Out) {
            }
        }
    }
}
