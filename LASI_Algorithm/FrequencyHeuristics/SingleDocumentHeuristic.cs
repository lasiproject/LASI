using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.IEnumerableExtensions;

namespace LASI.Algorithm.FrequencyHeuristics
{
    public abstract class SingleDocumentHeuristic : Heuristic
    {

        public SingleDocumentHeuristic(Document toAnalyse, int maxResultsPerCategory) {
            _sourceDocument = toAnalyse;
            _maxResultsPerCategory = maxResultsPerCategory;
        }

        public virtual Document SourceMaterial {
            get {
                return _sourceDocument;
            }
        }
        public override int MaxResultsPerCategory {
            get {
                return _maxResultsPerCategory;
            }
        }
        private int _maxResultsPerCategory;
        private Document _sourceDocument;
    }

}
