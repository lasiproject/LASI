using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LASI.Utilities
{
    public static partial class Logger
    {
        /// <summary>
        /// A <see cref="TextWriter"/> which delegates to the debug output of a supporting IDE.
        /// </summary>
        sealed class DebugOutputProxy : TextWriter
        {
            DebugOutputProxy() { }
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
            public override Encoding Encoding => Encoding.ASCII;
            public override void Close() { }
        }
    }
}