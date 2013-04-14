using LASI.GuiInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.InteropLayer
{


    public class StatusProvider
    {
        ProcessingState[] statuses = new[] { ProcessingState.ConvertingFiles, ProcessingState.ParsingWordTags, ProcessingState.AggregatingPhrases, ProcessingState.ComputingMetrics, ProcessingState.CrossReferencing, ProcessingState.Completed };
        int index = 0;
        public async Task<ProcessingState> GetStatus() {
            await Task.Delay(1);


            return statuses[++index];

        }


    }
}
