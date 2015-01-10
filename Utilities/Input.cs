using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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
        public static void WaitForKey(ConsoleKey key = ConsoleKey.Escape) {
            var message = string.Format("Press {0} to continue", key.ToString());
            Console.WriteLine(message);
            for (var k = Console.ReadKey(true); k.Key != key; k = Console.ReadKey(true)) {
                Console.WriteLine(message);
            }
        }
        /// <summary>
        /// Waits for the user to enter a specific key before continuing.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="key">The key the user must enter to continue.</param>
        public static void WaitForKey(string message, ConsoleKey key = ConsoleKey.Escape) {
            Console.WriteLine(message, key.ToString());
            for (var k = Console.ReadKey(true); k.Key != key; k = Console.ReadKey(true)) {
                Console.WriteLine(message, key.ToString());
            }
        }
        /// <summary>
        /// Waits for the user to enter a specific string before continuing.
        /// </summary>
        /// <param name="waitFor">The specific string to wait for before continuing.</param>
        /// <param name="ignoreCase">Indicates whether the string entered must the case of the stringToWaitFor.</param>
        public static void WaitForString(string waitFor, bool ignoreCase = true) {
            Console.WriteLine("Enter {0} to continue", waitFor);
            var input = Console.ReadLine();
            bool valid = string.Compare(input, waitFor, ignoreCase) == 0;
            if (!valid) {
                WaitForString(waitFor, ignoreCase);
            }
        }
    }
}
