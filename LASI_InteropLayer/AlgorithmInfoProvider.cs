using LASI.GuiInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.InteropLayer
{
    public static class AlgorithmInfoProvider
    {
        private static Status status = new Status();

        public static Status Status1 {
            get {
                return AlgorithmInfoProvider.status;
            }
            set {
                AlgorithmInfoProvider.status = value;
            }
        }
        public class Status
        {
            ProcessingState[] statuses = new[] { ProcessingState.Pending, ProcessingState.ConvertingFiles, ProcessingState.ParsingWordTags, ProcessingState.AggregatingPhrases, ProcessingState.Done };
            int index = 0;
            public async Task<ProcessingState> GetStatus() {
                await Task.Delay(4000);
          
                        
                    return statuses[++index];
                
            }







        }

    }
}
