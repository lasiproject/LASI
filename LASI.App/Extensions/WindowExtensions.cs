using System;
using System.Windows;

namespace LASI.App.Extensions
{
    static class WindowExtensions
    {
        public static void ShowMessage(this Window window, string messageBoxText)
        {
            window.ShowMessage(messageBoxText);
        }
        public static void MessageIf(this Window window, Func<bool> condition, string messageBoxText)
        {
            MessageIf(window, condition(), messageBoxText);
        }
        public static void MessageIf(this Window window, bool condition, string messageBoxText)
        {
            if (condition) { MessageBox.Show(window, messageBoxText); }
        }
    }
}
