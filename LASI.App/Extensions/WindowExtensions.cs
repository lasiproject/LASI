using System;
using System.Windows;

namespace LASI.App.Extensions
{
    /// <summary>
    /// Defines various extensions methods for working with <see cref="Window"/>s.
    /// </summary>
    public static class WindowExtensions
    {
        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified content in front the <paramref name="window"/>.
        /// </summary>
        /// <param name="window">The <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="messageBoxText">Specifies the text to display.</param>
        public static void ShowMessage(this Window window, string messageBoxText) => window.ShowMessage(messageBoxText);

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified content in front the <paramref name="window"/> if the specified condition evaluates to <c>true</c>.
        /// </summary>
        /// <param name="window">The <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="messageBoxText">Specifies the text to display.</param>
        /// <param name="condition">The condition.</param>
        public static void ShowMessageIf(this Window window, string messageBoxText, bool condition) => ShowMessageIf(window, messageBoxText, () => condition);


        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified content in front the <paramref name="window"/> if the specified predicate evaluates to <c>true</c>.
        /// </summary>
        /// <param name="window">The <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="messageBoxText">Specifies the text to display.</param>
        /// <param name="condition">The predicate.</param>
        public static void ShowMessageIf(this Window window, string messageBoxText, Func<bool> condition) => ShowMessageIf(window, messageBoxText, condition);
    }
}
