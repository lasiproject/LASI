using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    Concurrency.SetFromResourceUsageMode(ResourceMode.High);
                    Memory.SetFromResourceUsageMode(ResourceMode.High);
                    break;
                case PerforamanceLevel.Normal:
                    Concurrency.SetFromResourceUsageMode(ResourceMode.Normal);
                    Memory.SetFromResourceUsageMode(ResourceMode.Normal);
                    break;
                case PerforamanceLevel.Low:
                    Concurrency.SetFromResourceUsageMode(ResourceMode.Low);
                    Memory.SetFromResourceUsageMode(ResourceMode.Low);
                    break;
            }
        }
    }
    /// <summary>
    /// The Performance Levels the application may operate under.
    /// </summary>
    public enum PerforamanceLevel
    {
        /// <summary>
        /// High Performance.
        /// </summary>
        High,
        /// <summary>
        /// Low Performance.
        /// </summary>
        Low,
        /// <summary>
        /// Normal Performance.
        /// </summary>
        Normal
    }
}
