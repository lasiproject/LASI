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
        public static void SetPerformanceMode(PerforamanceMode mode)
        {
            switch (mode) {
                case PerforamanceMode.High:
                    Algorithm.Concurrency.SetResourceUsageMode(ResourceUsageMode.High);
                    break;
                case PerforamanceMode.Low:
                    Algorithm.Concurrency.SetResourceUsageMode(ResourceUsageMode.Low);
                    break;
                case PerforamanceMode.Normal:
                    Algorithm.Concurrency.SetResourceUsageMode(ResourceUsageMode.Normal);
                    break;
            }

        }
    }

    public enum PerforamanceMode
    {

        High,
        Low,
        Normal
    }
}
