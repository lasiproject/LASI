using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.IEnumerableExtensions;

namespace LASI.Algorithm.Heuristics
{
    public abstract class SingleDocumentHeuristic : Heuristic
    {

        protected SingleDocumentHeuristic(Document toAnalyse, int maxResultsPerCategory) {
            _sourceDocument = toAnalyse;
            _maxResultsPerCategory = maxResultsPerCategory;
        }
        /// <summary>
        /// Gets the document which the Heurisitc is to assess.
        /// </summary>
        public virtual Document SourceMaterial {
            get {
                return _sourceDocument;
            }
        }
        /// <summary>
        /// Gets the maximum number of results, in each category, that the Heuristic will return.
        /// </summary>
        public override int MaxResultsPerCategory {
            get {
                return _maxResultsPerCategory;
            }
        }
        private int _maxResultsPerCategory;
        private Document _sourceDocument;
    }

}
