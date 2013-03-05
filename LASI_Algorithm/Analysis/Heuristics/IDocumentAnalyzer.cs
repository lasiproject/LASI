using System;
using System.Threading.Tasks;
namespace LASI.Algorithm.Heuristics
{
    interface IDocumentAnalyzer
    {
        Metric Analyse();
        Task<Metric> AnalyseAsync();

        int MaxResults {
            get;
            set;
        }
        PronounMode PronounMode {
            get;
            set;
        }
        Document SourceDocument {
            get;
            set;
        }
    }
}
