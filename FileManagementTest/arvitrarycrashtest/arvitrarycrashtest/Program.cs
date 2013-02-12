using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arvitrarycrashtest
{
    class Program
    {
        static void Main(string[] args) {
            var q = from i in Enumerable.Range(0, new Random().Next(0, 20))
                    select Math.Sqrt(i * i * i);
            foreach (var i in q)
                Console.WriteLine(i);
            for (var k = Console.ReadKey().Key; k != ConsoleKey.Escape; k = Console.ReadKey().Key) {
            }
        }
    }
}
