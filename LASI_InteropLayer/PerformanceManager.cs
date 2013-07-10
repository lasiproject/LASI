using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
namespace LASI.InteropLayer
{
    public static class PerformanceManager
    {
        public static void SetPerformanceMode(PerforamanceLevel mode) {
            switch (mode) {
                case PerforamanceLevel.High:
                    Algorithm.Concurrency.SetResourceUsageMode(ResourceUsageMode.High);
                    break;
                case PerforamanceLevel.Low:
                    Algorithm.Concurrency.SetResourceUsageMode(ResourceUsageMode.Low);
                    break;
                case PerforamanceLevel.Normal:
                    Algorithm.Concurrency.SetResourceUsageMode(ResourceUsageMode.Normal);
                    break;
            }

        }
    }

    public enum PerforamanceLevel
    {

        High,
        Low,
        Normal
    }
}
