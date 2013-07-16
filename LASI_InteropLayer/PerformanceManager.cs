using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
namespace LASI.InteropLayer
{
    /// <summary>
    /// Controls global performance and resource usage settings.
    /// </summary>
    public static class PerformanceManager
    {
        /// <summary>
        /// Sets the overall performance level to the provided value.
        /// </summary>
        /// <param name="mode">The PerformanceLevel value indicating the new performance and resource usage settings to adobt.</param>
        public static void SetPerformanceMode(PerforamanceLevel mode) {
            switch (mode) {
                case PerforamanceLevel.High:
                    Algorithm.Concurrency.SetConcurrencyLevelByResourceUsageMode(ResourceUsageMode.High);
                    break;
                case PerforamanceLevel.Low:
                    Algorithm.Concurrency.SetConcurrencyLevelByResourceUsageMode(ResourceUsageMode.Low);
                    break;
                case PerforamanceLevel.Normal:
                    Algorithm.Concurrency.SetConcurrencyLevelByResourceUsageMode(ResourceUsageMode.Normal);
                    break;
            }

        }
    }
    /// <summary>
    /// The Performance Levels the application may operate under.
    /// </summary>
    public enum PerforamanceLevel
    {

        High,
        Low,
        Normal
    }
}
