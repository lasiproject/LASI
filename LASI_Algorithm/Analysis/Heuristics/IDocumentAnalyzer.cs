using System;
using System.Threading.Tasks;
namespace LASI.Algorithm.Heuristics
{
    interface IDocumentAnalyzer
    {
        ResultSet Analyse();
        Task<ResultSet> AnalyseAsync();

        int MaxResults {
            get;
        }
        System.Collections.Generic.IEnumerable<ILexical> SourceDocument {
            get;
        }
    }
}
