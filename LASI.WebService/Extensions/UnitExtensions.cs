using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LASI.WebService.Extensions
{
    public static class UnitExtensions
    {
        public static TimeSpan Milliseconds(in this int milliseconds) => TimeSpan.FromMilliseconds(milliseconds);
        public static TimeSpan Seconds(in this int seconds) => TimeSpan.FromSeconds(seconds);
        public static TimeSpan Minutes(in this int minutes) => TimeSpan.FromMinutes(minutes);
    }
}
