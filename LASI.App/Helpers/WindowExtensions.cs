using System;
using System.Windows;

namespace LASI.App.Helpers
{
    /// <summary>
    /// Provides convenient extensions for the <see cref="Window"/> type. 
    /// </summary>
    public static class WindowExtensions
    {
        /// <summary>
        /// Immediately closes the window if the specified predicate returns true. 
        /// </summary>
        /// <param name="window"> The window. </param>
        /// <param name="predicate"> The predicate which determines if the window will be closed. </param>
        /// <returns> The window. </returns>
        public static Window CloseIf(this Window window, Func<bool> predicate)
        {
            if (predicate())
            {
                window.Close();
            }
            return window;
        }

        /// <summary>
        /// Immediately closes the window if the specified condition is true. 
        /// </summary>
        /// <param name="window"> The window. </param>
        /// <param name="condition"> The condition to test. </param>
        /// <returns> The window. </returns>
        public static Window CloseIf(this Window window, bool condition) => window.CloseIf(() => condition);

        /// <summary>
        /// Attempts to reposition the <see cref="Window"/> in relation to the desktop, using the location of the specified <see cref="Window" /> as a reference. 
        /// </summary>
        /// <param name="window"> The window to reposition.</param>
        /// <param name="other"> The window to use as a reference when repositioning. </param>
        /// <returns>The window.</returns>
        public static Window Reposition(this Window window, Window other)
        {
            window.Reposition(other.Top, other.Left);
            return window;
        }
        /// <summary>
        /// Attempts to reposition the <see cref="Window"/> using the specified point's top and left coordinates.
        /// </summary>
        /// <param name="window"> The window to reposition.</param>
        /// <param name="position">A point specifying the desired top and left coordinates.</param>
        /// <returns>The window.</returns>
        public static Window Reposition(this Window window, Point position)
        {
            window.Reposition(position.Y, position.X);
            return window;
        }

        /// <summary>
        /// Attempts to reposition the <see cref="Window"/>, in relation to the desktop, based on the specified values for top and left. 
        /// </summary>
        /// <param name="window"> The window to reposition. </param>
        /// <param name="top"> The window's top edge, in relation to the desktop. </param>
        /// <param name="left"> The window's left edge, in relation to the desktop. </param>
        /// <returns> The window. </returns>
        public static Window Reposition(this Window window, double top, double left)
        {
            window.Top = top;
            window.Left = left;
            return window;
        }

        /// <summary>
        /// Sets the title of the given <see cref="Window"/> to the specified string. 
        /// </summary>
        /// <param name="window"> The window. </param>
        /// <param name="title"> The new title of the window. </param>
        /// <returns> The window. </returns>
        public static Window SetTitle(this Window window, string title)
        {
            window.Title = title;
            return window;
        }

        /// <summary>
        /// Swaps this window with the given other window. Hiding it and showing the other in the same general location. 
        /// </summary>
        /// <param name="window"> The window to swap out. </param>
        /// <param name="other"> The window to swap in. </param>
        public static void SwapWith(this Window window, Window other)
        {
            other.Reposition(window);
            other.Show();
            window.Hide();
        }
    }
}