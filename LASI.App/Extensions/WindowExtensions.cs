using System;
using System.Windows;

namespace LASI.App.Extensions
{
    static class WindowExtensions
    {
        public static void Message(this Window window, string message)
        {
            window.Message(message);
        }
        public static void MessageIf(this Window window, Func<bool> condition, string message)
        {
            MessageIf(window, condition(), message);
        }
        public static void MessageIf(this Window window, bool condition, string message)
        {
            if (condition) { Message(window, message); }
        }
    }
}
