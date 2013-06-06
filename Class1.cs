ing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public static class Class1
    {

        static void test() {
            var nums = from i in Enumerable.Range(0, 100)
                       where i % 2 == 0
                       select i;
        }


    }
}
