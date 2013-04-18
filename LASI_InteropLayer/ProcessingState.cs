using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.GuiInterop
{
    public enum ProcessingState
    {
        Initializing,
        ConvertingFiles,
        ParsingTaggedData,
        AggregatingPhrases,
        ComputingMetrics,
        CrossReferencing,
        Completed,
        Completing,
        LoadingThesauri,
        TaggingData,
        BuildingAssociations
    }
}