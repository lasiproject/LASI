using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.InteropLayer
{
    public static class PerformanceManager
    {
        public static PerformanceMode PerformanceMode {
            set {
                switch (value) {
                    case PerformanceMode.Forground:
                        Algorithm.Concurrency.CurrentMax = 3;
                        break;
                    case PerformanceMode.Minimized:
                        Algorithm.Concurrency.CurrentMax = 2;
                        break;
                    case PerformanceMode.HiddenLongRunning:
                        Algorithm.Concurrency.CurrentMax = 1;
                        break;
                }
            }
        }
    }

    public enum PerformanceMode
    {
        Forground,
        Minimized,
        HiddenLongRunning
    }
}
