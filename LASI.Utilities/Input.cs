using System;
using static System.Console;

namespace LASI.Utilities
{
    /// <summary>
    /// Defines static methods which facilitate common Console IO tasks. 
    /// </summary>
    public static class Input
    {
        /// <summary>
        /// Waits for the user to enter a specific key before continuing.
        /// </summary>
        /// <param name="key">The key the user must enter to continue.</param>
        public static void WaitForKey(ConsoleKey key = ConsoleKey.Escape)
        {
            WriteLine($"Press {key.ToString()} to continue");
            for (var k = ReadKey(true); k.Key != key; k = ReadKey(true))
            {
                WriteLine($"Press {key.ToString()} to continue");
            }
        }
        /// <summary>
        /// Waits for the user to enter a specific key before continuing.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="key">The key the user must enter to continue.</param>
        public static void WaitForKey(string message, ConsoleKey key = ConsoleKey.Escape)
        {
            WriteLine(message, key);
            for (var k = ReadKey(true); k.Key != key; k = ReadKey(true))
            {
                WriteLine(message, key);
            }
        }
        /// <summary>
        /// Waits for the user to enter a specific string before continuing.
        /// </summary>
        /// <param name="waitFor">The specific string to wait for before continuing.</param>
        /// <param name="ignoreCase">Indicates whether the string entered must the case of the stringToWaitFor.</param>
        public static void WaitForString(string waitFor, bool ignoreCase = true)
        {
            WriteLine($"Enter {waitFor} to continue");
            var input = ReadLine();
            bool valid = string.Compare(input, waitFor, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;
            if (!valid)
            {
                WaitForString(waitFor, ignoreCase);
            }
        }
    }
}
