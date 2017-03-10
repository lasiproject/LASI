using System;
using System.Reactive;
using System.Windows;
using System.Windows.Input;

namespace LASI.App.Extensions
{
    public static class WindowExtensions
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
            if (condition)
            { MessageBox.Show(window, messageBoxText); }
        }
        public static void Deconstruct(EventPattern<KeyEventArgs> e, out Key key, out string value) => (key, value) = (e.EventArgs.Key, e.EventArgs.InputSource.ToString());
    }

}
