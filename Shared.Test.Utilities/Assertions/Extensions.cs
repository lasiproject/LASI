using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Test.Assertions
{
    public static class Extensions
    {
        public static void Assert(this IEnumerable<Action> assertions) => assertions.ToList().ForEach(a => a());
    }
}
