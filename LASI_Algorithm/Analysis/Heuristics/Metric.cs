using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Algorithm.Analysis;

namespace LASI.Algorithm.Heuristics
{
    public class ResultSet
    {
        public ResultSet(DocumentConstructs.Document document) {
            source = document;
        }
        private List<SubjectVerbObjectResult> svoTripples = new List<SubjectVerbObjectResult>();

        public List<SubjectVerbObjectResult> SvoTripples {
            get {
                return svoTripples;
            }
            set {
                svoTripples = value;
            }
        }

        private DocumentConstructs.Document source;
    }
}
