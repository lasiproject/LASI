using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace LASI.Utilities
{

    /// <summary>
    /// Defines static methods which facilitate common Console IO procedures
    /// </summary>
    public static class Input
    {

        /// <summary>
        /// Waits for the user to enter a key before continuing. Provides analogous functionality to the infamous system("pause") command in C++, but is safe and platform independent.
        /// </summary>
        public static void Wait()
        {


            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
        /// <summary>
        /// Waits for the user to enter a specific key before continuing.
        /// </summary>
        /// <param name="key">The key the user must enter to continue</param>
        public static void WaitForKey(ConsoleKey key = ConsoleKey.Escape)
        {
            Console.WriteLine("Press {0} to continue", key.ToString());
            for (var k = Console.ReadKey(true); k.Key != key; k = Console.ReadKey(true)) {
                Console.WriteLine("Press {0} to continue", key.ToString());
            }
        }
        /// <summary>
        /// Waits for the user to enter a specific string before continuing.
        /// </summary>
        /// <param name="waitFor">The specfic string to wait for before continuing.</param>
        /// <param name="ignoreCase">Indicates whether the entered to the string must the case of the waitFor string</param>
        public static void WaitForInput(string waitFor, bool ignoreCase = false)
        {
            Console.WriteLine("Enter {0} to continue", waitFor);
            var input = Console.ReadLine();
            bool valid = string.Compare(input, waitFor, ignoreCase) == 0;
            if (valid)
                return;
            else
                WaitForInput(waitFor, ignoreCase);
        }

        public static void WaitForAnyKey()
        {
            Console.ReadKey();
        }
    }
}
